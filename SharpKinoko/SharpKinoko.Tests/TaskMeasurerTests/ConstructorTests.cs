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
using Moq;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.TaskMeasurerTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void constructor_throws_if_task_is_null()
        {
            try
            {
                new TaskMeasurer(null, 1);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("task"));
                throw;
            }
        }

        [Test]
        public void constructor_saves_task_internally()
        {
            KinokoTask task = () => { };

            TaskMeasurer measurer = new TaskMeasurer(task, 1);

            Assert.That(measurer.Task, Is.SameAs(task));
        }

        [Test]
        public void constructor_saves_repeatMeasurementCount_internally()
        {
            KinokoTask task = () => { };
            int repeatMeasurementCount = 10;

            TaskMeasurer taskMeasurer = new TaskMeasurer(task, repeatMeasurementCount);

            Assert.That(taskMeasurer.RepeatMeasurementCount, Is.EqualTo(repeatMeasurementCount));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void constructor_throws_if_repeatMeasurementCount_is_zero()
        {
            try
            {
                KinokoTask task = () => { };
                int repeatMeasurementCount = 0;

                new TaskMeasurer(task, repeatMeasurementCount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("taskRunCount"));
                throw;
            }
        }
    }
}

