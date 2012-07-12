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

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls
{
    public class ProgressBar
    {
        private int progressPercentage;

        public int ProgressPercentage
        {
            get { return progressPercentage; }
        }

        private int progressCharCount;
        private readonly IConsole console;

        public int Width { get; set; }

        public char ProgressChar { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ProgressBar(IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;

            Width = 50;
            ProgressChar = '*';
        }

        public void SetProgress(int percentage)
        {
            if (percentage < 0)
                percentage = 0;

            if (percentage > 100)
                percentage = 100;

            if (percentage == progressPercentage)
                return;

            progressPercentage = percentage;

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

        public void Display()
        {
            WriteEmptyProgressBarColored();
        }

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

