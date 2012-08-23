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

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Represents a task to be run by kinoko.
    /// It includes the method to be tested and some other meta information.
    /// </summary>
    public class KinokoTask
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the subject to be tested.
        /// </summary>
        public KinokoSubject Subject { get; set; }
    }
}