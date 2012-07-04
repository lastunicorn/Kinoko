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
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.AssemblySubjectsProviderTests
{
    [TestFixture()]
    public class GetKinokoSubjectsTests
    {
        private AssemblySubjectsProvider kinokoSubjectsProvider;
        private Assembly assembly;
     
        [SetUp]
        public void SetUp()
        {
            kinokoSubjectsProvider = new AssemblySubjectsProvider();
            assembly = Assembly.LoadFile("AssemblyWithMethodsForTesting.dll");
            kinokoSubjectsProvider.Load(assembly);
        }
             
        [Test]
        public void returns_kinoko_subject_for_public_method_with_KinokoTest_attribute()
        {
            IEnumerable<KinokoSubject> subject = kinokoSubjectsProvider.GetKinokoSubjects();

            AssertContainsTaskForMethod(subject, "PublicMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_private_method_with_KinokoTest_attribute()
        {
            IEnumerable<KinokoSubject> subject = kinokoSubjectsProvider.GetKinokoSubjects();

            AssertDoesNotContainTaskForMethod(subject, "PrivateMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_public_static_method()
        {
            IEnumerable<KinokoSubject> subject = kinokoSubjectsProvider.GetKinokoSubjects();

            AssertDoesNotContainTaskForMethod(subject, "StaticPublicMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_private_static_method()
        {
            IEnumerable<KinokoSubject> subject = kinokoSubjectsProvider.GetKinokoSubjects();

            AssertDoesNotContainTaskForMethod(subject, "StaticPrivateMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_normal_public_method()
        {
            IEnumerable<KinokoSubject> subject = kinokoSubjectsProvider.GetKinokoSubjects();

            AssertDoesNotContainTaskForMethod(subject, "PublicMethod");
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

