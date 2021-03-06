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
using DustInTheWind.Kinoko.KinokoConsole;
using DustInTheWind.Kinoko.KinokoConsole.ConsoleControls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.Kinoko.Tests.Console.KinokoApplicationTests
{
    /// <summary>
    /// Unit tests for the constructor function of the <see cref="KinokoApplication"/> class.
    /// </summary>
    [TestFixture]
    public class ConstructorTests
    {
        private KinokoRunner kinokoWrapper;
        private Mock<IConsole> console;
        private UI ui;
        private KinokoContext kinokoContext;
        private CommandLineOptions options;
        private ProgressBarFactory progressBarFactory;

        [SetUp]
        public void SetUp()
        {
            options = new CommandLineOptions();
            console = new Mock<IConsole>();
            ui = new UI(console.Object);
            kinokoContext = new KinokoContext();
            progressBarFactory = new ProgressBarFactory(console.Object);
            kinokoWrapper = new KinokoRunner(progressBarFactory, kinokoContext, ui);
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
        public void throws_if_kinokoRunner_is_null()
        {
            try
            {
                new KinokoApplication(options, ui, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("kinokoRunner"));
                throw;
            }
        }
    }
}

