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
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Framework.AssemblySubjectsProviderTests
{
    [TestFixture()]
    public class GetKinokoSubjectsTests
    {
        private AssemblySubjectsProvider kinokoSubjectsProvider;
        private Assembly assembly;
        private IEnumerable<KinokoSubject> subjects;
     
        [SetUp]
        public void SetUp()
        {
            kinokoSubjectsProvider = new AssemblySubjectsProvider();
            assembly = Assembly.LoadFile(Path.GetFullPath("AssemblyWithMethodsForTesting.dll"));
            kinokoSubjectsProvider.Load(assembly);
            subjects = kinokoSubjectsProvider.GetKinokoSubjects();
        }
             
        [Test]
        public void includes_public_method_with_KinokoSubject_attribute()
        {
            AssertContainsTaskForMethod(subjects, "PublicMethodWithAttribute");
        }
     
        [Test]
        public void does_not_include_normal_public_method()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethod");
        }
     
        [Test]
        public void does_not_include_private_method_with_KinokoSubject_attribute()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PrivateMethodWithAttribute");
        }
     
        [Test]
        public void includes_public_static_method_with_attribute()
        {
            AssertContainsTaskForMethod(subjects, "PublicStaticMethodWithAttribute");
        }
     
        [Test]
        public void does_not_include_private_static_method_with_attribute()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PrivateStaticMethodWithAttribute");
        }

        [Test]
        public void does_not_include_normal_public_static_method()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicStaticMethod");
        }

        [Test]
        public void does_not_include_public_methods_with_parameters_and_attribute()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethodWithParametersAndAttribute");
        }

        [Test]
        public void does_not_include_public_methods_with_generic_parameters_and_attribute()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethodWithGenericParameterAndAttribute");
        }

        [Test]
        public void does_not_include_public_method_with_attribute_from_class_with_no_parameterless_constructor()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethodInClassWithNoParameterlessConstructor");
        }

        [Test]
        public void does_not_include_public_method_with_attribute_from_class_with_no_public_constructor()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethodInClassWithNoPublicConstructor");
        }

        [Test]
        public void does_not_include_public_method_with_attributes_from_class_with_generic_parameters()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethodInClassWithGenericPatameter");
        }

        [Test]
        public void does_not_include_public_method_with_attribute_from_class_with_only_static_parameterless_constructor()
        {
            AssertDoesNotContainTaskForMethod(subjects, "PublicMethodInClassWithStaticConstructor");
        }

        private MethodInfo GetMethodFromAssembly(string methodName)
        {
            return assembly.GetType("AssemblyWithMethodsForTesting.ClassForTest")
             .GetMethod(methodName);
        }

        private void AssertContainsTaskForMethod(IEnumerable<KinokoSubject> subjects, string methodName)
        {
            Assert.True(subjects.Any(x => ((Delegate)x).Method.Name == methodName));
        }

        private void AssertDoesNotContainTaskForMethod(IEnumerable<KinokoSubject> subjects, string methodName)
        {
            Assert.False(subjects.Any(x => ((Delegate)x).Method.Name == methodName));
        }
    }
}

