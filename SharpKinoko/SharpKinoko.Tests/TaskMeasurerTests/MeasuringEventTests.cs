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

using System.Threading;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.TaskMeasurerTests
{
    [TestFixture]
    public class MeasuringEventTests
    {
        private TaskMeasurer taskMeasurer;

        [SetUp]
        public void SetUp()
        {
            KinokoTask task = () => Thread.Sleep(10);
            taskMeasurer = new TaskMeasurer(task, 1);
        }

        [Test]
        public void Measuring_is_called_before_running_the_task()
        {
            bool eventCalled = false;
            taskMeasurer.Measuring += (sender, e) => {
                eventCalled = true;
            };

            taskMeasurer.Run();

            Assert.That(eventCalled, Is.True);
        }

        [Test]
        public void Measuring_is_called_with_correct_sender()
        {
            object senderObject = null;
            taskMeasurer.Measuring += (sender, e) => {
                senderObject = sender;
            };

            taskMeasurer.Run();

            Assert.That(senderObject, Is.SameAs(taskMeasurer));
        }

        [Test]
        public void Measuring_is_called_with_not_null_event_args()
        {
            MeasuringEventArgs eventArgs = null;
            taskMeasurer.Measuring += (sender, e) => {
                eventArgs = e;
            };

            taskMeasurer.Run();

            Assert.That(eventArgs, Is.Not.Null);
        }
    }
}
