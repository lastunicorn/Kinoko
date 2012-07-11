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
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.Utils.CommandArguments
{
    /// <summary>
    /// A list of <see cref="Argument"/> objects.
    /// </summary>
    public class ArgumentList : CollectionBase
    {
        /// <summary>
        /// The arguments indexed by name.
        /// </summary>
        private Dictionary<string, Argument> argumentsByName = new Dictionary<string, Argument>();

        #region Add

        /// <summary>
        /// Adds a new argument to the list.
        /// </summary>
        /// <param name="name">The name of the new argument.</param>
        /// <param name="value">The value of the new argument.</param>
        /// <exception cref="ArgumentNullException">The name is null.</exception>
        public void Add(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (argumentsByName.ContainsKey(name))
            {
                argumentsByName[name].Value = value;
            }
            else
            {
                Argument argument = new Argument(name, value);
                List.Add(argument);
                argumentsByName.Add(name, argument);
            }
        }

        /// <summary>
        /// Adds a new argument to the list.
        /// </summary>
        /// <param name="argument">The <see cref="Argument"/> object to be added to the list.</param>
        /// <exception cref="ArgumentNullException">The <see cref="Argument"/> object is null.</exception>
        public void Add(Argument argument)
        {
            if (argument == null)
                throw new ArgumentNullException("argument");

            if (argumentsByName.ContainsKey(argument.Name))
            {
                argumentsByName[argument.Name].Value = argument.Value;
            }
            else
            {
                List.Add(argument);
                argumentsByName.Add(argument.Name, argument);
            }
        }

        #endregion

        #region Contains

        /// <summary>
        /// Checks if the list contains the specified argument.
        /// </summary>
        /// <param name="argument">The <see cref="Argument"/> object to be searched for in the list.</param>
        /// <returns>true if the argument exists in the list; false otherwise.</returns>
        public bool Contains(Argument argument)
        {
            return List.Contains(argument);
        }

        /// <summary>
        /// Checks if the list contains an argument with the specified name.
        /// </summary>
        /// <param name="name">The name of the argument to searched for.</param>
        /// <returns>rue if an argument with the specified name exists in the list; false otherwise.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Contains(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return argumentsByName.ContainsKey(name);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets the <see cref="Argument"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="Argument"/> to get.</param>
        /// <returns>The <see cref="Argument"/> at the specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the list.</exception>
        public Argument this[int index]
        {
            get { return (Argument)List[index]; }
        }

        /// <summary>
        /// Gets the <see cref="Argument"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Argument"/> to get.</param>
        /// <returns>The <see cref="Argument"/> with the specified name.</returns>
        /// <exception cref="ArgumentNullException">The name is null.</exception>
        public Argument this[string name]
        {
            get
            {
                if (name == null)
                    throw new ArgumentNullException("name");

                return argumentsByName[name];
            }
        }

        #endregion
    }
}
