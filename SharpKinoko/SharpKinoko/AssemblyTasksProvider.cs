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
    public class AssemblyTasksProvider : ITasksProvider
    {
        private Assembly assembly;
        private List<MethodInfo> testMethods;

        public void Load(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            this.assembly = assembly;
        }

        public IList<MethodInfo> GetAllTestMethods()
        {
            if (testMethods == null)
                CreateListOfMethods();

            return testMethods;
        }

        private void CreateListOfMethods()
        {
            List<MethodInfo> testMethods = new List<MethodInfo>();
         
            Type[] types = assembly.GetTypes();
         
            foreach (Type type in types)
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
         
                foreach (MethodInfo method in methods)
                {
                    Attribute attr = Attribute.GetCustomAttribute(method, typeof(KinokoTaskAttribute), false);
         
                    if (attr != null)
                        testMethods.Add(method);
                }
            }
         
            this. testMethods = testMethods;
        }

        public IEnumerable<KinokoTask> GetTasks()
        {
            List<KinokoTask> tasks = new List<KinokoTask>();

            IEnumerable<MethodInfo> methods = GetAllTestMethods();

            foreach (MethodInfo method in methods)
            {
                tasks.Add(CreateKinokoTask(method));
            }

            return tasks;
        }

        static KinokoTask CreateKinokoTask(MethodInfo method)
        {
            ConstructorInfo constructor = method.ReflectedType.GetConstructor(new Type[0]);
            object obj = constructor.Invoke(new object[0]);
            KinokoTask task = Delegate.CreateDelegate(typeof(KinokoTask), obj, method.Name) as KinokoTask;
            return task;
        }
    }
}

