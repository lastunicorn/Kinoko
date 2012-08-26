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
using NUnit.Framework;
using DustInTheWind.SharpKinoko.SharpKinokoConsole;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Moq;

namespace DustInTheWind.SharpKinoko.Tests.Console.ConsoleControls.GuiHelpersTests
{
    [TestFixture]
    public class DisplayErrorTests
    {
        [Test]
        public void text_color_is_changed_to_red()
        {
            Mock<IConsole> console = new Mock<IConsole>();
            console.SetupGet(x => x.WindowWidth).Returns(100);
            UI guiHelpers = new UI(console.Object);

            guiHelpers.DisplayError("error message");

            console.VerifySet(x => x.ForegroundColor = ConsoleColor.Red, Times.Once());
        }

//      
//        public void writes_empty_line_at_the_begining()
//        {
//
//        }
    }
}

