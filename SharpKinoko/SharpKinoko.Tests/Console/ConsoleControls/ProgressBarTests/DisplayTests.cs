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

using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.ConsoleControls.ProgressBarTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ProgressBar.Display()"/> method.
    /// </summary>
    [TestFixture]
    public class DisplayTests
    {
        private Mock<IConsole> console;
        private ProgressBar progressBar;

        [SetUp]
        public void SetUp()
        {
            console = new Mock<IConsole>();
            progressBar = new ProgressBar(console.Object);
        }
        
        [Test]
        public void reads_current_position_of_the_cursor()
        {
            progressBar.Display();

            console.VerifyGet(x => x.CursorLeft, Times.Once());
            console.VerifyGet(x => x.CursorTop, Times.Once());
        }

        [Test]
        public void displays_the_empty_progress_bar_with_default_50_width()
        {
            progressBar.Display();

            string emptyProgressBar = "[" + new string(' ', 48) + "]";
            console.Verify(x => x.Write(emptyProgressBar), Times.Once());
        }

        [Test]
        public void displays_the_empty_progress_bar_on_70_chars_when_width_is_set_to_70()
        {
            progressBar.Width = 70;

            progressBar.Display();

            string emptyProgressBar = "[" + new string(' ', 68) + "]";
            console.Verify(x => x.Write(emptyProgressBar), Times.Once());
        }

        [Test]
        public void leaves_the_cursor_at_the_0percent_position()
        {
            console.SetupGet(x => x.CursorLeft).Returns(10);
            console.SetupGet(x => x.CursorTop).Returns(15);

            progressBar.Display();

            console.VerifySet(x => x.CursorLeft = 11, Times.Once());
            console.VerifySet(x => x.CursorTop = 15, Times.Once());
        }
    }
}

