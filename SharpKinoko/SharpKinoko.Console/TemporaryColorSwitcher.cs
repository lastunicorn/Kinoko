using System;

namespace DustInTheWind.SharpKinokoConsole
{
    public class TemporaryColorSwitcher : IDisposable
    {
        private ConsoleColor oldColor;
        private IConsole console;

        /// <summary>
        /// Initializes a new instance of the <see cref="DustInTheWind.SharpKinokoConsole.ColorSwitcher"/> class.
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
        private bool disposed = false;

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

