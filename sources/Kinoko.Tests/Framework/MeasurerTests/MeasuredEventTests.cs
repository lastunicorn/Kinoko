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

namespace DustInTheWind.Kinoko.Tests.Framework.MeasurerTests
{
    /// <summary>
    /// Unit tests for the <see cref="Measurer.Measured"/> event.
    /// </summary>
    [TestFixture]
    public class MeasuredEventTests
    {
        private Measurer measurer;

        [SetUp]
        public void SetUp()
        {
            KinokoSubject subject = () => Thread.Sleep(10);
            measurer = new Measurer(subject, 1);
        }

        [Test]
        public void Measured_is_called_after_the_subject_is_measured()
        {
            bool eventCalled = false;
            measurer.Measured += (sender, e) => {
                eventCalled = true;
            };

            measurer.Run();

            Assert.That(eventCalled, Is.True);
        }

        [Test]
        public void Measured_is_called_with_correct_sender()
        {
            object senderObject = null;
            measurer.Measured += (sender, e) => {
                senderObject = sender;
            };

            measurer.Run();

            Assert.That(senderObject, Is.SameAs(measurer));
        }

        [Test]
        public void Measured_is_called_with_not_null_event_args()
        {
            MeasuredEventArgs eventArgs = null;
            measurer.Measured += (sender, e) => {
                eventArgs = e;
            };

            measurer.Run();

            Assert.That(eventArgs, Is.Not.Null);
        }
    }
}
