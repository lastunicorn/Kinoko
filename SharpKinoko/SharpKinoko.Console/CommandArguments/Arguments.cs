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

using System.Collections.Generic;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole.CommandArguments
{
    /// <summary>
    /// This class parses the list of arguments received from the command line and stores
    /// them as key-value pairs.
    /// </summary>
    public class Arguments
    {
        /// <summary>
        /// The list of arguments contained by the current instance.
        /// </summary>
        private readonly List<Argument> arguments = new List<Argument>();

        /// <summary>
        /// The arguments indexed by name.
        /// </summary>
        private readonly Dictionary<string, Argument> argumentsByName = new Dictionary<string, Argument>();

        #region public string[] RawArgs

        /// <summary>
        /// The string array of arguments received from the command line.
        /// </summary>
        private readonly string[] rawArgs;

        /// <summary>
        /// Gets the string array of arguments received from the command line.
        /// </summary>
        public string[] RawArgs
        {
            get { return rawArgs; }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Arguments"/> with
        /// the string array received from the command line.
        /// </summary>
        /// <param name="args">The string array of arguments received from the command line.</param>
        public Arguments(string[] args)
        {
            rawArgs = args;

            if (args != null)
                Parse(args);
        }

        #endregion

        private void Parse(IList<string> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                string name = args[i].Substring(1);
                string value = i + 1 < args.Count ? args[i + 1] : null;
                Add(name, value);
            }
        }

        private void Add(string name, string value)
        {
            Argument argument = new Argument(name, value);
            arguments.Add(argument);
            argumentsByName.Add(name, argument);
        }

        //
        //        private void Parse(string[] args)
        //        {
        //            // Ensure that the arguments contains no spaces
        //            //args = SplitBySpaces(args);
        //         
        //            Regex remover = new Regex(@"^(['""]?)(.*?)\1$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        //         
        //            string previousArgumentName = null;
        //         
        //            // Check if the first argument respects the rule. It is allowed to not start with the "-", "--" or "/". If so, parse it separately.
        //            int startIndex = 0;
        //            if (args.Length > 0)
        //            {
        //                if (!args[0].StartsWith("-") && !args[0].StartsWith("--") && !args[0].StartsWith("/"))
        //                {
        //                    arguments.Add(args[0], null);
        //                    startIndex = 1;
        //                }
        //            }
        //         
        //            // Valid parameters forms:
        //            // {-,/,--}param{ ,=,:}((",')value(",'))
        //            // Examples:
        //            // -param1 value1 --param2 /param3:"Test-:-work" 
        //            //   /param4=happy -param5 '--=nice=--'
        //            for (int i = startIndex; i < args.Length; i++)
        //            {
        //                string txt = args[i];
        //                string[] parts = new string[2];
        //         
        //                if (args.Length > 0)
        //                {
        //                    int nameStartIndex = 0;
        //         
        //                    if (txt[0] == '-')
        //                    {
        //                        if (txt.Length > 1 && txt[1] == '-')
        //                        {
        //                            nameStartIndex = 2;
        //                        }
        //                        else
        //                        {
        //                            nameStartIndex = 1;
        //                        }
        //                    }
        //                    else if (txt[0] == '/')
        //                    {
        //                        nameStartIndex = 1;
        //                    }
        //         
        //                    if (nameStartIndex > 0)
        //                    {
        //                        // I found an argument name.
        //                        int separatorIndex = txt.IndexOfAny(new char[] { '=', ':', ' ' });
        //         
        //                        if (separatorIndex == nameStartIndex)
        //                        {
        //                            // Error: The name-value pair does not contain a name.
        //                        }
        //                        else if (separatorIndex > nameStartIndex)
        //                        {
        //                            parts = new string[] {
        //                             txt.Substring(nameStartIndex, separatorIndex - nameStartIndex),
        //                             txt.Substring(separatorIndex + 1)
        //                         };
        //                        }
        //                        else
        //                        {
        //                            parts = new string[] { txt.Substring(nameStartIndex), null };
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // I found an argument value.
        //                        parts = new string[] { null, txt };
        //                    }
        //                }
        //         
        //         
        //         
        //                if (parts[0] == null)
        //                {
        //                    // No argument name found.
        //         
        //                    if (parts[1] == null)
        //                    {
        //                        // No argument value found.
        //         
        //                        // Error: No argument name or value found. Ignore the record.
        //                    }
        //                    else
        //                    {
        //                        if (previousArgumentName == null)
        //                        {
        //                            // Previous argument does not exist.
        //         
        //                            // Error: Argument value found but no argument name.
        //                        }
        //                        else
        //                        {
        //                            // Previous argument exists.
        //         
        //                            parts[1] = remover.Replace(parts[1], "$2");
        //                            arguments.Add(previousArgumentName, parts[1]);
        //                            previousArgumentName = null;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    // New argument name found.
        //         
        //                    if (previousArgumentName != null)
        //                    {
        //                        // Previous argument does exist.
        //         
        //                        // Save the previous argument with null value.
        //                        arguments.Add(previousArgumentName, null);
        //                        previousArgumentName = null;
        //                    }
        //         
        //         
        //                    if (parts[1] == null)
        //                    {
        //                        // Argument value not found.
        //         
        //                        // Save the argument name for later processing.
        //                        previousArgumentName = parts[0];
        //                    }
        //                    else
        //                    {
        //                        // Argument value found.
        //         
        //                        // Create a new Argument object.
        //                        parts[1] = remover.Replace(parts[1], "$2");
        //                        arguments.Add(parts[0], parts[1]);
        //                    }
        //                }
        //            }
        //         
        //            // Check if a parameter is still waiting for its value.
        //            if (previousArgumentName != null)
        //            {
        //                arguments.Add(previousArgumentName, null);
        //            }
        //        }
        //

        #region Indexers

        /// <summary>
        /// Returns the argument with the specified name.
        /// </summary>
        /// <param name="argumentName">The name of the argument to return.</param>
        /// <returns>The argument with the specified name or null if no argument has the specified name.</returns>
        public string this[string argumentName]
        {
            get
            {
                if (argumentsByName.ContainsKey(argumentName))
                    return argumentsByName[argumentName].Value;

                return null;
            }
        }

        //        /// <summary>
        //        /// Returns the argument at the specified index.
        //        /// </summary>
        //        /// <param name="index">The index of the argument to return.</param>
        //        /// <returns>The argument at the specified index.</returns>
        //        public Argument this[int index]
        //        {
        //            get { return arguments[index]; }
        //        }

        #endregion

        public bool Contains(string argumentName)
        {
            if (argumentName == null)
                return false;

            return argumentsByName.ContainsKey(argumentName);
        }

        /// <summary>
        /// Returns the number of the arguments in the list.
        /// </summary>
        public int Count
        {
            get { return arguments.Count; }
        }
    }
}