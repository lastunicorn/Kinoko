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

namespace DustInTheWind.SharpKinoko.Tests.KinokoTests
{
    [TestFixture]
    public class MeasuringEventTests
    {
        private Kinoko kinoko;
        private int repeatMeasurementCount = 50;

        [SetUp]
        public void SetUp()
        {
            kinoko = new Kinoko();
            kinoko.Task = new KinokoTask(delegate { Thread.Sleep(repeatMeasurementCount); });
        }

        [Test]
        public void Measuring_is_called_before_running_the_task()
        {
            bool eventCalled = false;
            kinoko.Measuring += (sender, e) => {
                eventCalled = true;
            };

            kinoko.Run();

            Assert.That(eventCalled, Is.True);
        }

        [Test]
        public void Measuring_is_called_with_correct_sender()
        {
            object senderObject = null;
            kinoko.Measuring += (sender, e) => {
                senderObject = sender;
            };

            kinoko.Run();

            Assert.That(senderObject, Is.SameAs(kinoko));
        }

        [Test]
        public void Measuring_is_called_with_not_null_event_args()
        {
            MeasuringEventArgs eventArgs = null;
            kinoko.Measuring += (sender, e) => {
                eventArgs = e;
            };

            kinoko.Run();

            Assert.That(eventArgs, Is.Not.Null);
        }
    }
}
