using System;
using System.Text;

namespace DustInTheWind.SharpKinokoConsole
{
    public class ProgressBar
    {
        private int progressPercentage;

        public int ProgressPercentage
        {
            get { return progressPercentage; }
        }

        private int progressCharCount;
        private IConsole console;

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

