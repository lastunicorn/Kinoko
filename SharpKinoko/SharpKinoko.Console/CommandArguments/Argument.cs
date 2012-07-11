// Copyright (C) 2009-2011 Dust in the Wind
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

namespace DustInTheWind.Utils.CommandArguments
{
    /// <summary>
    /// Represents an argument received in the command line of the application.
    /// </summary>
    public class Argument
    {
        #region public string Name

        /// <summary>
        /// The name of the argument.
        /// <c>null</c> value is not accepted.
        /// </summary>
        private string name;

        /// <summary>
        /// Gets the name of the argument.
        /// <c>null</c> value is not accepted.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        #endregion

        #region public string Value

        /// <summary>
        /// The value of the argument.
        /// </summary>
        private string value;

        /// <summary>
        /// Gets or sets the value of the argument.
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Argument"/> class with
        /// the name of the argument and its value.
        /// </summary>
        /// <param name="name">The name of the argument. It can not be null.</param>
        /// <param name="value">The value of the argument. It is allowed to be null.</param>
        /// <exception cref="ArgumentNullException">The name is null.</exception>
        public Argument(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.value = value;
        }

        #endregion


        #region Equals

        /// <summary>
        /// Determines whether this instance of <see cref="Argument"/> and a specified object,
        /// which must also be a <see cref="Argument"/> object, have the same name and value.
        /// </summary>
        /// <param name="obj">An <see cref="System.Object"/> to compare this instance with.</param>
        /// <returns>true if obj is an <see cref="Argument"/> and its name and value are the same as this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Argument))
                return false;

            Argument arg = obj as Argument;

            return name == arg.name && value == arg.value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} = {1}", name, value);
        }

        #endregion
    }
}
