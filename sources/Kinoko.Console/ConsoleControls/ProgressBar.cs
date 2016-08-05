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
using System.Text;

namespace DustInTheWind.Kinoko.KinokoConsole.ConsoleControls
{
    /// <summary>
    /// Writes a progress bar into the console.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It presumes that the progress bar fits into the current line.
    /// </para>
    /// <para>
    /// While the progress bar is used, the cursor should not be moved.
    /// The ProgressBar control does not write the whole control to the console every time it updates the progress.
    /// Instead it presums the cursor is in the right location and just adds more stars (if needed).
    /// </para>
    /// </remarks>
    public class ProgressBar
    {
        /// <summary>
        /// The console where the control is displayed.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        /// The number of stars that are currently displayed for the current percentage.
        /// </summary>
        private int progressCharCount;

        /// <summary>
        /// Gets the percentage currently displayed by the control.
        /// </summary>
        /// <remarks>For setting the percentage, please use <see cref="SetProgress"/> method.</remarks>
        public int ProgressPercentage { get; private set; }

        /// <summary>
        /// Gets or sets the width of the progress bar. It includes the first and tha last characters that are two brackets.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the character used to display the progress. By default it is '*'.
        /// </summary>
        public char ProgressChar { get; set; }

        /// <summary>
        /// Gets or sets the color used to write the progress bar control to the console.
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name='console'>The console where to write the progress bar.</param>
        public ProgressBar(IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;

            Width = 50;
            ProgressChar = '*';
        }

        /// <summary>
        /// Displays the progress bar at the current location of the cursor.
        /// </summary>
        public void Display()
        {
            WriteEmptyProgressBarColored();
        }

        /// <summary>
        /// Displays the progress bar at the specified location.
        /// </summary>
        /// <param name="top">The column index from where to start displaying the progress bar.</param>
        /// <param name="left">The row index from where to start displaying the progress bar.</param>
        public void Display(int top, int left)
        {
            console.CursorTop = top;
            console.CursorLeft = left;

            WriteEmptyProgressBarColored();
        }

        private void WriteEmptyProgressBarColored()
        {
            if (ForegroundColor.HasValue)
            {
                using (new TemporaryColorSwitcher(console, ForegroundColor.Value))
                {
                    WriteEmptyProgressBar();
                }
            }
            else
            {
                WriteEmptyProgressBar();
            }
        }

        private void WriteEmptyProgressBar()
        {
            int top = console.CursorTop;
            int left = console.CursorLeft;

            int clientWidth = Width - 2;

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(new String(' ', clientWidth));
            sb.Append("]");

            console.Write(sb.ToString());

            console.CursorTop = top;
            console.CursorLeft = left + 1;
        }

        /// <summary>
        /// Changes the percentage displayed by the progress bar.
        /// </summary>
        /// <param name="percentage">The new percentage to be displayed by the progress bar.</param>
        /// <remarks>If the new value is less then 0, 0 is displayed. If the new value is grater then 100, 100 is displayed.</remarks>
        public void SetProgress(int percentage)
        {
            if (percentage < 0)
                percentage = 0;

            if (percentage > 100)
                percentage = 100;

            if (percentage == ProgressPercentage)
                return;

            ProgressPercentage = percentage;

            int newCharCount = TransformToCharCount(percentage);

            if (newCharCount == progressCharCount)
                return;

            if (newCharCount > progressCharCount)
            {
                AddProgressCharsColored(newCharCount - progressCharCount);
                progressCharCount = newCharCount;
            }
        }

        private int TransformToCharCount(int percentage)
        {
            int clientWidth = Width - 2;

            return percentage * clientWidth / 100;
        }

        private void AddProgressCharsColored(int charCount)
        {
            if (ForegroundColor.HasValue)
            {
                using (new TemporaryColorSwitcher(console, ForegroundColor.Value))
                {
                    AddProgressChars(charCount);
                }
            }
            else
            {
                AddProgressChars(charCount);
            }
        }

        private void AddProgressChars(int charCount)
        {
            console.Write(new string(ProgressChar, charCount));
        }
    }

}

