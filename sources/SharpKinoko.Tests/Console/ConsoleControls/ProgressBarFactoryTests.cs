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
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Moq;

namespace DustInTheWind.SharpKinoko.Tests.Console.ConsoleControls
{
    /// <summary>
    /// Unit tests for <see cref="ProgressBarFactory"/> class.
    /// </summary>
    [TestFixture]
    public class ProgressBarFactoryTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void constructor_throws_if_console_is_null()
        {
            try
            {
                new ProgressBarFactory(null);    
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("console"));
                throw;
            }
        }

        [Test]
        public void CreateProgressBar_returns_new_instance_of_ProgressBar()
        {
            Mock<IConsole> console = new Mock<IConsole>();
            ProgressBarFactory progressBarFactory = new ProgressBarFactory(console.Object);

            ProgressBar progressBar = progressBarFactory.CreateProgressBar();

            Assert.That(progressBar, Is.Not.Null);
        }

        [Test]
        public void CreateProgressBar_returns_diffrent_instances_on_every_call()
        {
            Mock<IConsole> console = new Mock<IConsole>();
            ProgressBarFactory progressBarFactory = new ProgressBarFactory(console.Object);

            ProgressBar progressBar1 = progressBarFactory.CreateProgressBar();
            ProgressBar progressBar2 = progressBarFactory.CreateProgressBar();

            Assert.That(progressBar1, Is.Not.SameAs(progressBar2));
        }
    }
}

