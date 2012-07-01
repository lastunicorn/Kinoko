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
using System.Reflection;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.AssemblyTasksProviderTests
{
    [TestFixture()]
    public class GetAllTestMethodsTests
    {
        private AssemblyTasksProvider tasksProvider;
        private Assembly assembly;
     
        [SetUp]
        public void SetUp()
        {
            tasksProvider = new AssemblyTasksProvider();
            assembly = Assembly.LoadFile("AssemblyWithMethodsForTesting.dll");
            tasksProvider.Load(assembly);
        }
             
        [Test]
        public void returns_public_method_with_KinokoTest_attribute()
        {
            IList<MethodInfo> methods = tasksProvider.GetAllTestMethods();
         
            AssertContainsMethod(methods, "PublicMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_private_method_with_KinokoTest_attribute()
        {
            IList<MethodInfo> methods = tasksProvider.GetAllTestMethods();
         
            AssertDoesNotContainMethod(methods, "PrivateMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_public_static_method()
        {
            IList<MethodInfo> methods = tasksProvider.GetAllTestMethods();
         
            AssertDoesNotContainMethod(methods, "StaticPublicMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_private_static_method()
        {
            IList<MethodInfo> methods = tasksProvider.GetAllTestMethods();
         
            AssertDoesNotContainMethod(methods, "StaticPrivateMethodWithTestAttribute");
        }
     
        [Test]
        public void does_not_return_normal_public_method()
        {
            IList<MethodInfo> methods = tasksProvider.GetAllTestMethods();
         
            AssertDoesNotContainMethod(methods, "PublicMethod");
        }

        private MethodInfo GetMethodFromAssembly(string methodName)
        {
            return assembly.GetType("AssemblyWithMethodsForTesting.ClassForTest")
             .GetMethod(methodName);
        }

        private void AssertContainsMethod(IEnumerable<MethodInfo> methods, string methodName)
        {
            MethodInfo expectedMethod = GetMethodFromAssembly(methodName);
            Assert.That(methods, Contains.Item(expectedMethod));
        }

        private void AssertDoesNotContainMethod(IEnumerable<MethodInfo> methods, string methodName)
        {
            MethodInfo expectedMethod = GetMethodFromAssembly(methodName);
            Assert.That(methods, Has.No.Member(expectedMethod));
        }
    }
}

