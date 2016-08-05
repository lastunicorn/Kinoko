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
using DustInTheWind.Kinoko.KinokoConsole.ConsoleControls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.Kinoko.Tests.Console.ConsoleControls.TemporaryColorSwitcherTests
{
    /// <summary>
    /// Contains unit tests for the constructor of <see cref="TemporaryColorSwitcher"/> class.
    /// </summary>
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_console_is_null()
        {
            try
            {
                new TemporaryColorSwitcher(null, ConsoleColor.Red);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("console"));
                throw;
            }
        }

        [Test]
        public void retrieves_the_current_ForegroundColor_of_the_console()
        {
            Mock<IConsole> console = new Mock<IConsole>();

            new TemporaryColorSwitcher(console.Object, ConsoleColor.Red);

            console.VerifyGet(x => x.ForegroundColor, Times.Once());
        }

        [Test]
        public void changes_the_ForegroundColor_of_the_console()
        {
            Mock<IConsole> console = new Mock<IConsole>();

            new TemporaryColorSwitcher(console.Object, ConsoleColor.Red);

            console.VerifySet(x => x.ForegroundColor = ConsoleColor.Red, Times.Once());
        }

        [Test]
        public void changes_back_the_ForegroundColor_of_the_console_to_the_old_value_when_instance_is_disposed()
        {
            Mock<IConsole> console = new Mock<IConsole>();
            console.SetupGet(x => x.ForegroundColor).Returns(ConsoleColor.Yellow);

            TemporaryColorSwitcher temporaryColorSwitcher = new TemporaryColorSwitcher(console.Object, ConsoleColor.Red);
            temporaryColorSwitcher.Dispose();

            console.VerifySet(x => x.ForegroundColor = ConsoleColor.Yellow, Times.Once());
        }
    }
}
