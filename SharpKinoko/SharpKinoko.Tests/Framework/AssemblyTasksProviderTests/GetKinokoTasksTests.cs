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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.SharpKinoko.Providers;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Framework.AssemblyTasksProviderTests
{
    /// <summary>
    /// Unit tests for the <see cref="AssemblyTasksProvider.GetKinokoTasks"/> method.
    /// </summary>
    [TestFixture]
    public class GetKinokoTasksTests
    {
        private AssemblyTasksProvider kinokoTasksProvider;
        private Assembly assembly;
        private IEnumerable<KinokoTask> tasks;
     
        [SetUp]
        public void SetUp()
        {
            kinokoTasksProvider = new AssemblyTasksProvider();
            assembly = Assembly.LoadFile(Path.GetFullPath("AssemblyWithMethodsForTesting.dll"));
            kinokoTasksProvider.Load(assembly);
            tasks = kinokoTasksProvider.GetKinokoTasks();
        }
             
        [Test]
        public void includes_public_method_with_KinokoSubject_attribute()
        {
            AssertContainsTaskForMethod(tasks, "PublicMethodWithAttribute");
        }
     
        [Test]
        public void does_not_include_normal_public_method()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethod");
        }
     
        [Test]
        public void does_not_include_private_method_with_KinokoSubject_attribute()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PrivateMethodWithAttribute");
        }
     
        [Test]
        public void includes_public_static_method_with_attribute()
        {
            AssertContainsTaskForMethod(tasks, "PublicStaticMethodWithAttribute");
        }
     
        [Test]
        public void does_not_include_private_static_method_with_attribute()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PrivateStaticMethodWithAttribute");
        }

        [Test]
        public void does_not_include_normal_public_static_method()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicStaticMethod");
        }

        [Test]
        public void does_not_include_public_methods_with_parameters_and_attribute()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethodWithParametersAndAttribute");
        }

        [Test]
        public void does_not_include_public_methods_with_generic_parameters_and_attribute()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethodWithGenericParameterAndAttribute");
        }

        [Test]
        public void does_not_include_public_method_with_attribute_from_class_with_no_parameterless_constructor()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethodInClassWithNoParameterlessConstructor");
        }

        [Test]
        public void does_not_include_public_method_with_attribute_from_class_with_no_public_constructor()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethodInClassWithNoPublicConstructor");
        }

        [Test]
        public void does_not_include_public_method_with_attributes_from_class_with_generic_parameters()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethodInClassWithGenericPatameter");
        }

        [Test]
        public void does_not_include_public_method_with_attribute_from_class_with_only_static_parameterless_constructor()
        {
            AssertDoesNotContainTaskForMethod(tasks, "PublicMethodInClassWithStaticConstructor");
        }

        private void AssertContainsTaskForMethod(IEnumerable<KinokoTask> tasks, string methodName)
        {
            Assert.True(tasks.Any(x => x.Subject.Method.Name == methodName));
        }

        private void AssertDoesNotContainTaskForMethod(IEnumerable<KinokoTask> tasks, string methodName)
        {
            Assert.False(tasks.Any(x => x.Subject.Method.Name == methodName));
        }
    }
}

