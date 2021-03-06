// SharpKinoko
// Copyright (C) 2010 Dust in the Wind
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace DustInTheWind.Kinoko.Providers
{
    /// <summary>
    /// Searches through an assembly for methods marked with <see cref="KinokoTaskAttribute"/> attribute.
    /// </summary>
    public class AssemblyTasksProvider : ITasksProvider
    {
        /// <summary>
        /// The assembly into which to search for kinoko subjects.
        /// </summary>
        private Assembly assembly;

        /// <summary>
        /// Load the assembly into which to search for kinoko subjects.
        /// </summary>
        /// <param name='assembly'>The assembly into which to search for kinoko subjects.</param>
        /// <exception cref='ArgumentNullException'>Is thrown when the assembly is <see langword="null" />.</exception>
        public void Load(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            this.assembly = assembly;
        }

        /// <summary>
        /// Returns a list of <see cref="KinokoTask"/>s for all methods that are marked as kinoko subjects.
        /// </summary>
        /// <returns>A list of <see cref="KinokoTask"/> objects.</returns>
        public IEnumerable<KinokoTask> GetKinokoTasks()
        {
            List<KinokoTask> tasks = new List<KinokoTask>();

            IEnumerable<MethodInfo> methods = SearchForAllValidMethods();

            foreach (MethodInfo method in methods)
                tasks.Add(CreateKinokoTask(method));

            return tasks;
        }

        /// <summary>
        /// Searches for kinoko subject methods into the assembly.
        /// </summary>
        /// <returns>A list of <see cref="MethodInfo"/> representing the subject methods.</returns>
        private IEnumerable<MethodInfo> SearchForAllValidMethods()
        {
            List<MethodInfo> allMethods = new List<MethodInfo>();

            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (!IsValidClass(type))
                    continue;

                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                foreach (MethodInfo method in methods)
                {
                    if (method.GetParameters().Length > 0)
                        continue;

                    if (method.IsGenericMethod)
                        continue;

                    Attribute attr = Attribute.GetCustomAttribute(method, typeof(KinokoTaskAttribute), false);

                    if (attr != null)
                        allMethods.Add(method);
                }
            }

            return allMethods;
        }

        private static bool IsValidClass(Type type)
        {
            ConstructorInfo constructor = type.GetConstructor(new Type[0]);
            //return constructor != null && !constructor.IsStatic;
            return constructor != null;
        }

        /// <summary>
        /// Creates a <see cref="KinokoTask"/> object for the specified method.
        /// </summary>
        /// <param name='method'>The method for which to create the task.</param>
        /// <returns>A <see cref="KinokoTask"/> object.</returns>
        private KinokoTask CreateKinokoTask(MethodInfo method)
        {
            return new KinokoTask
            {
                Category = method.ReflectedType.FullName,
                Subject = CreateKinokoSubject(method)
            };
        }

        /// <summary>
        /// Creates a <see cref="KinokoSubject"/> delegete for the specified method.
        /// </summary>
        /// <param name='method'>The method for which to create the delegate.</param>
        /// <returns>A <see cref="KinokoSubject"/> delegate.</returns>
        private KinokoSubject CreateKinokoSubject(MethodInfo method)
        {
            if (method.IsStatic)
                return Delegate.CreateDelegate(typeof(KinokoSubject), method) as KinokoSubject;

            object obj = InstanciateParentClassForMethod(method);
            return Delegate.CreateDelegate(typeof(KinokoSubject), obj, method.Name) as KinokoSubject;
        }

        private static object InstanciateParentClassForMethod(MethodInfo method)
        {
            ConstructorInfo constructor = method.ReflectedType.GetConstructor(new Type[0]);
            return constructor.Invoke(new object[0]);
        }
    }
}