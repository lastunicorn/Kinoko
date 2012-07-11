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

namespace DustInTheWind.SharpKinoko.Tests.Framework.KinokoTests
{
    [TestFixture]
    public class MeasuredEventTests
    {
        private Kinoko kinoko;
        private KinokoSubject subject;
        private int repeatCount = 1;

        [SetUp]
        public void SetUp()
        {
            kinoko = new Kinoko();
            subject = new KinokoSubject(delegate {
                Thread.Sleep(10); });
        }

        [Test]
        public void Measured_is_called_after_the_subject_is_measured()
        {
            bool eventCalled = false;
            kinoko.Measured += (sender, e) => {
                eventCalled = true;
            };

            kinoko.Run(subject, repeatCount);

            Assert.That(eventCalled, Is.True);
        }

        [Test]
        public void Measured_is_called_with_correct_sender()
        {
            object senderObject = null;
            kinoko.Measured += (sender, e) => {
                senderObject = sender;
            };

            kinoko.Run(subject, repeatCount);

            Assert.That(senderObject, Is.SameAs(kinoko));
        }

        [Test]
        public void Measured_is_called_with_not_null_event_args()
        {
            MeasuredEventArgs eventArgs = null;
            kinoko.Measured += (sender, e) => {
                eventArgs = e;
            };

            kinoko.Run(subject, repeatCount);

            Assert.That(eventArgs, Is.Not.Null);
        }
    }
}
