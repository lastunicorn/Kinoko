﻿// SharpKinoko
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
using System.Text;
using DustInTheWind.SharpKinoko.SharpKinokoConsole;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Moq;
using NUnit.Framework;
using Ninject;

namespace DustInTheWind.SharpKinoko.Tests.Console.KinokoRunnerTests
{
    [TestFixture]
    public class ConstructorTests
    {
        private Mock<IKernel> kernel;
        private Mock<IKinoko> kinoko;
        private Mock<IUI> ui;

        [SetUp]
        public void SetUp()
        {
            kernel = new Mock<IKernel>();
            kinoko = new Mock<IKinoko>();
            ui = new Mock<IUI>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_kernel_is_null()
        {
            try
            {
                new KinokoRunner(null, kinoko.Object, ui.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("kernel"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_kinoko_is_null()
        {
            try
            {
                new KinokoRunner(kernel.Object, null, ui.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("kinoko"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_ui_is_null()
        {
            try
            {
                new KinokoRunner(kernel.Object, kinoko.Object, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("ui"));
                throw;
            }
        }
    }
}
