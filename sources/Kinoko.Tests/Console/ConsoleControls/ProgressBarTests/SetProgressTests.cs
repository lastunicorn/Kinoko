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

using DustInTheWind.Kinoko.KinokoConsole.ConsoleControls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.Kinoko.Tests.Console.ConsoleControls.ProgressBarTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ProgressBar.SetProgress"/> method.
    /// </summary>
    [TestFixture]
    public class SetProgressTests
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
        public void ProgressPercentage_is_set_with_the_new_value_if_it_is_a_valid_one()
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
        public void writes_no_star_if_progress_changed_to_1_and_width_is_50()
        {
            progressBar.SetProgress(1);

            // 1% -> (48*1)/100 = 0.48 stars
            console.Verify(x => x.Write(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void writes_no_star_if_progress_changed_to_2_and_width_is_50()
        {
            progressBar.SetProgress(2);

            // 2% -> (48*2)/100 = 0.96 stars
            console.Verify(x => x.Write(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void writes_one_star_if_progress_changed_to_3_and_width_is_50()
        {
            progressBar.SetProgress(3);

            // 3% -> (48*3)/100 = 1.44 stars
            console.Verify(x => x.Write("*"), Times.Once());
        }

        [Test]
        public void writes_14_stars_if_progress_changed_to_30_and_width_is_50()
        {
            progressBar.SetProgress(30);

            // 30% -> (48*30)/100 = 14.4 stars
            console.Verify(x => x.Write(new string('*', 14)), Times.Once());
        }

        [Test]
        public void writes_one_star_if_progress_changed_from_30_to_32_and_width_is_50()
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

