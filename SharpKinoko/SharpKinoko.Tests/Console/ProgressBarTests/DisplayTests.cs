using System;
using NUnit.Framework;
using DustInTheWind.SharpKinokoConsole;
using Moq;

namespace DustInTheWind.SharpKinoko.Tests.Console.ProgressBarTests
{
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
        public void sets_the_cursor_at_the_0_position()
        {
            console.SetupGet(x => x.CursorLeft).Returns(10);
            console.SetupGet(x => x.CursorTop).Returns(15);

            progressBar.Display();

            console.VerifySet(x => x.CursorLeft = 11, Times.Once());
            console.VerifySet(x => x.CursorTop = 15, Times.Once());
        }

        [Test]
        public void displays_the_empty_progress_bar_with_default_width()
        {
            progressBar.Display();

            string emptyProgressBar = "[" + new string(' ', 48) + "]";
            console.Verify(x => x.Write(emptyProgressBar), Times.Once());
        }

        [Test]
        public void displays_the_empty_progress_bar_with_70_width()
        {
            progressBar.Width = 70;

            progressBar.Display();

            string emptyProgressBar = "[" + new string(' ', 68) + "]";
            console.Verify(x => x.Write(emptyProgressBar), Times.Once());
        }
    }
}

