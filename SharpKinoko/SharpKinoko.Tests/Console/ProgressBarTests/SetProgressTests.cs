using System;
using NUnit.Framework;
using DustInTheWind.SharpKinokoConsole;
using Moq;

namespace DustInTheWind.SharpKinoko.Tests.Console.ProgressBarTests
{
    [TestFixture]
    public class SetProgressTests
    {
        private Mock<IConsole> console ;
        private ProgressBar progressBar;

        [SetUp]
        public void SetUp()
        {
            console = new Mock<IConsole>();
            progressBar = new ProgressBar(console.Object);
        }

        [Test]
        public void ProgressPercentage_is_set_with_the_new_value()
        {
            progressBar.SetProgress(10);

            Assert.That(progressBar.ProgressPercentage, Is.EqualTo(10));
        }

        [Test]
        public void ProgressPercentage_is_set_to_0_if_new_value_is_less_then_0()
        {
            progressBar.SetProgress(-20);

            Assert.That(progressBar.ProgressPercentage, Is.EqualTo(0));
        }

        [Test]
        public void ProgressPercentage_is_set_to_100_if_new_value_is_greater_then_100()
        {
            progressBar.SetProgress(200);

            Assert.That(progressBar.ProgressPercentage, Is.EqualTo(100));
        }

        [Test]
        public void writes_no_star_if_progress_changed_to_1()
        {
            progressBar.SetProgress(1);

            // 1% -> (48*1)/100 = 0.48 stars
            console.Verify(x => x.Write(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void writes_no_star_if_progress_changed_to_2()
        {
            progressBar.SetProgress(2);

            // 2% -> (48*2)/100 = 0.96 stars
            console.Verify(x => x.Write(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void writes_one_star_if_progress_changed_to_3()
        {
            progressBar.SetProgress(3);

            // 3% -> (48*3)/100 = 1.44 stars
            console.Verify(x => x.Write("*"), Times.Once());
        }

        [Test]
        public void writes_14_stars_if_progress_changed_to_3()
        {
            progressBar.SetProgress(30);

            // 30% -> (48*30)/100 = 14.4 stars
            console.Verify(x => x.Write(new string('*', 14)), Times.Once());
        }

        [Test]
        public void writes_one_star_if_progress_changed_from_30_to_32()
        {
            progressBar.SetProgress(30);
            progressBar.SetProgress(32);

            // 30% -> (48*30)/100 = 14.4 stars
            // 32% -> (48*32)/100 = 15.36 stars
            console.Verify(x => x.Write(new string('*', 14)), Times.Once());
            console.Verify(x => x.Write("*"), Times.Once());
        }
    }
}

