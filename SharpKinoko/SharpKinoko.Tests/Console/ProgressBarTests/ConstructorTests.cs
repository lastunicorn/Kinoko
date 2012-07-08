using System;
using NUnit.Framework;
using DustInTheWind.SharpKinokoConsole;
using Moq;

namespace DustInTheWind.SharpKinoko.Tests.Console.ProgressBarTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_console_is_null()
        {
            try
            {
                new ProgressBar(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("console"));
                throw;
            }
        }

        [Test]
        public void Width_has_initial_value_50()
        {
            ProgressBar progressBar = CreateProgressBar();

            Assert.That(progressBar.Width, Is.EqualTo(50));
        }

        [Test]
        public void ProgressChar_has_initial_value_star()
        {
            ProgressBar progressBar = CreateProgressBar();

            Assert.That(progressBar.ProgressChar, Is.EqualTo('*'));
        }

        private ProgressBar CreateProgressBar()
        {
            Mock<IConsole> console = new Mock<IConsole>();
            return new ProgressBar(console.Object);
        }
    }
}

