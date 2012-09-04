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
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.ConsoleControls.UITests
{
    /// <summary>
    /// Unit tests for the constructor function of the <see cref="UI"/> class.
    /// </summary>
    [TestFixture]
    public class ContructorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_console_is_null()
        {
            try
            {
                new UI(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("console"));
                throw;
            }
        }
    }
}

