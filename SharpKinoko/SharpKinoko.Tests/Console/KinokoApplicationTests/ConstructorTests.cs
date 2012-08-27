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
using DustInTheWind.SharpKinoko.SharpKinokoConsole;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Moq;
using Ninject;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.KinokoApplicationTests
{
    /// <summary>
    /// Unit tests for the constructor function of the <see cref="KinokoApplication"/> class.
    /// </summary>
    [TestFixture]
    public class ConstructorTests
    {
        private KinokoRunner kinokoWrapper;
        private Mock<IConsole> console;
        private Mock<IKernel> kernel;
        private UI ui;
        private Kinoko kinoko;
        private CommandLineOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new CommandLineOptions();
            console = new Mock<IConsole>();
            kernel = new Mock<IKernel>();
            ui = new UI(console.Object);
            kinoko = new Kinoko();
            kinokoWrapper = new KinokoRunner(kernel.Object, kinoko, ui);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_options_is_null()
        {
            try
            {
                new KinokoApplication(null, ui, kinokoWrapper);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("options"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_ui_is_null()
        {
            try
            {
                new KinokoApplication(options, null, kinokoWrapper);
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
                new KinokoApplication(options, ui, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("kinokoWrapper"));
                throw;
            }
        }
    }
}

