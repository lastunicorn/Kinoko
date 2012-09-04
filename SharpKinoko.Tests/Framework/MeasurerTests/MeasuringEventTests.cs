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

namespace DustInTheWind.SharpKinoko.Tests.Framework.MeasurerTests
{
    /// <summary>
    /// Unit tests for the <see cref="Measurer.Measuring"/> event.
    /// </summary>
    [TestFixture]
    public class MeasuringEventTests
    {
        private Measurer measurer;

        [SetUp]
        public void SetUp()
        {
            KinokoSubject subject = () => Thread.Sleep(10);
            measurer = new Measurer(subject, 1);
        }

        [Test]
        public void Measuring_is_called_before_measuring_the_subject()
        {
            bool eventCalled = false;
            measurer.Measuring += (sender, e) => {
                eventCalled = true;
            };

            measurer.Run();

            Assert.That(eventCalled, Is.True);
        }

        [Test]
        public void Measuring_is_called_with_correct_sender()
        {
            object senderObject = null;
            measurer.Measuring += (sender, e) => {
                senderObject = sender;
            };

            measurer.Run();

            Assert.That(senderObject, Is.SameAs(measurer));
        }

        [Test]
        public void Measuring_is_called_with_not_null_event_args()
        {
            MeasuringEventArgs eventArgs = null;
            measurer.Measuring += (sender, e) => {
                eventArgs = e;
            };

            measurer.Run();

            Assert.That(eventArgs, Is.Not.Null);
        }
    }
}
