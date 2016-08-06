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

namespace DustInTheWind.Kinoko.Tests.Console.KinokoRunnerTests
{
    [TestFixture]
    public class ConstructorTests
    {
        private Mock<IConsole> console;
        private Mock<IKinokoContext> kinoko;
        private Mock<IUI> ui;
        private ProgressBarFactory progressBarFactory;

        [SetUp]
        public void SetUp()
        {
            console = new Mock<IConsole>();
            kinoko = new Mock<IKinokoContext>();
            ui = new Mock<IUI>();
            progressBarFactory = new ProgressBarFactory(console.Object);
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
                Assert.That(ex.ParamName, Is.EqualTo("progressBarFactory"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_kinoko_is_null()
        {
            try
            {
                new KinokoRunner(progressBarFactory, null, ui.Object);
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
                new KinokoRunner(progressBarFactory, kinoko.Object, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("ui"));
                throw;
            }
        }
    }
}
