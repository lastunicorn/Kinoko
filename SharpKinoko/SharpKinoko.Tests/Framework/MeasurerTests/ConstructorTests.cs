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

namespace DustInTheWind.SharpKinoko.Tests.Framework.MeasurerTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void constructor_throws_if_subject_is_null()
        {
            try
            {
                new Measurer(null, 1);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("subject"));
                throw;
            }
        }

        [Test]
        public void constructor_saves_subject_internally()
        {
            KinokoSubject subject = () => { };

            Measurer measurer = new Measurer(subject, 1);

            Assert.That(measurer.Subject, Is.SameAs(subject));
        }

        [Test]
        public void constructor_saves_repeatCount_internally()
        {
            KinokoSubject subject = () => { };
            int repeatCount = 10;

            Measurer measurer = new Measurer(subject, repeatCount);

            Assert.That(measurer.RepeatCount, Is.EqualTo(repeatCount));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void constructor_throws_if_repeatCount_is_zero()
        {
            try
            {
                KinokoSubject subject = () => { };
                int repeatCount = 0;

                new Measurer(subject, repeatCount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("repeatCount"));
                throw;
            }
        }
    }
}

