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
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using NUnit.Framework;
using DustInTheWind.SharpKinoko.SharpKinokoConsole;
using Moq;
using Ninject;

namespace DustInTheWind.SharpKinoko.Tests.Console.KinokoConsoleTests
{
    /// <summary>
    /// Unit tests for the constructor function of the <see cref="KinokoConsole"/> class.
    /// </summary>
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_ui_is_null()
        {
            try
            {
                Mock<IConsole> console = new Mock<IConsole>();
                Mock<IKernel> kernel = new Mock<IKernel>();
                UI ui = new UI(console.Object);
                KinokoWrapper kinokoWrapper = new KinokoWrapper(kernel.Object, ui);

                new KinokoApplication(null, kinokoWrapper, new string[0]);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("ui"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_kinokoWrapper_is_null()
        {
            try
            {
                Mock<IConsole> console = new Mock<IConsole>();
                Mock<IKernel> kernel = new Mock<IKernel>();
                UI ui = new UI(console.Object);
                KinokoWrapper kinokoWrapper = new KinokoWrapper(kernel.Object, ui);

                new KinokoApplication(ui, null, new string[0]);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("kinokoWrapper"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_args_is_null()
        {
            try
            {
                Mock<IConsole> console = new Mock<IConsole>();
                Mock<IKernel> kernel = new Mock<IKernel>();
                UI ui = new UI(console.Object);
                KinokoWrapper kinokoWrapper = new KinokoWrapper(kernel.Object, ui);

                new KinokoApplication(ui, kinokoWrapper, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("args"));
                throw;
            }
        }
    }
}

