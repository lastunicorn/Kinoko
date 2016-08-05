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

using NUnit.Framework;

namespace DustInTheWind.Kinoko.Tests.Framework.KinokoResultTests
{
    /// <summary>
    /// Unit tests for the constructor of the <see cref="KinokoResult"/> class.
    /// </summary>
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void the_list_of_measurements_is_empty()
        {
            KinokoResult kinokoResult = new KinokoResult();

            Assert.That(kinokoResult.Measurements.Length, Is.EqualTo(0));
        }

        [Test]
        public void the_average_is_0()
        {
            KinokoResult kinokoResult = new KinokoResult();

            Assert.That(kinokoResult.Average, Is.EqualTo(0));
        }
    }
}
