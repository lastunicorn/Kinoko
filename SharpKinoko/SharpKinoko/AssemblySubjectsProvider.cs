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

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Searches through an assembly for methods marked with <see cref="KinokoTargetAttribute"/> attribute.
    /// </summary>
    public class AssemblySubjectsProvider : ISubjectsProvider
    {
        /// <summary>
        /// The assembly into which the search is performed.
        /// </summary>
        private Assembly assembly;

        public void Load(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            this.assembly = assembly;
        }

        public IEnumerable<KinokoSubject> GetKinokoSubjects()
        {
            List<KinokoSubject> subjects = new List<KinokoSubject>();

            IEnumerable<MethodInfo> methods = SearchForAllMethods();

            foreach (MethodInfo method in methods)
            {
                subjects.Add(CreateKinokoTask(method));
            }

            return subjects;
        }

        private IEnumerable<MethodInfo> SearchForAllMethods()
        {
            List<MethodInfo> allMethods = new List<MethodInfo>();

            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (MethodInfo method in methods)
                {
                    Attribute attr = Attribute.GetCustomAttribute(method, typeof(KinokoTargetAttribute), false);

                    if (attr != null)
                        allMethods.Add(method);
                }
            }

            return allMethods;
        }

        private KinokoSubject CreateKinokoTask(MethodInfo method)
        {
            ConstructorInfo constructor = method.ReflectedType.GetConstructor(new Type[0]);
            object obj = constructor.Invoke(new object[0]);
            return Delegate.CreateDelegate(typeof(KinokoSubject), obj, method.Name) as KinokoSubject;
        }
    }
}

