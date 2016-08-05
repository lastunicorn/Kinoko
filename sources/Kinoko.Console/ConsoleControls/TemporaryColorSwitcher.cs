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

namespace DustInTheWind.Kinoko.KinokoConsole.ConsoleControls
{
    /// <summary>
    /// Temporarly changes the color used to write text in the console.
    /// The previous color is restored when the current instance is disposed.
    /// </summary>
    public class TemporaryColorSwitcher : IDisposable
    {
        /// <summary>
        /// The old color of the text.
        /// </summary>
        private readonly ConsoleColor oldColor;

        /// <summary>
        /// The console for which to change the text color.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryColorSwitcher"/> class.
        /// </summary>
        public TemporaryColorSwitcher(IConsole console, ConsoleColor temporarColor)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;
            oldColor = console.ForegroundColor;
            console.ForegroundColor = temporarColor;
        }

        #region IDisposable Members

        /// <summary>
        /// Specifies if the current instance has already been disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the current instance and optionally releases the managed resources.
        /// </summary>
        /// <remarks>
        /// <para>Dispose(bool disposing) executes in two distinct scenarios.</para>
        /// <para>If the method has been called directly or indirectly by a user's code managed and unmanaged resources can be disposed.</para>
        /// <para>If the method has been called by the runtime from inside the finalizer you should not reference other objects. Only unmanaged resources can be disposed.</para>
        /// </remarks>
        /// <param name="disposing">Specifies if the method has been called by a user's code (true) or by the runtime from inside the finalizer (false).</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    console.ForegroundColor = oldColor;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // ...

                disposed = true;
            }
        }

        ~TemporaryColorSwitcher()
        {
            Dispose(false);
        }

        #endregion
    }
}

