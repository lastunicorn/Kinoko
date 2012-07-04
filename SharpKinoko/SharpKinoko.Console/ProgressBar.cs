using System;

namespace DustInTheWind.SharpKinokoConsole
{
    public class ProgressBar
    {
        private int progressPercentage;
        private int progressCharCount;

        public int Width { get; set; }

        public char ProgressChar { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ProgressBar()
        {
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
            Console.CursorTop = top;
            Console.CursorLeft = left;

            WriteEmptyProgressBarColored();
        }

        private void WriteEmptyProgressBarColored()
        {
            if (ForegroundColor.HasValue)
            {
                using (new TemporaryColorSwitcher(ForegroundColor.Value))
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
            int top = Console.CursorTop;
            int left = Console.CursorLeft;

            int clientWidth = Width - 2;

            Console.Write("[");
            Console.Write(new String(' ', clientWidth));
            Console.Write("]");

            Console.CursorTop = top;
            Console.CursorLeft = left + 1;
        }

        private void AddProgressCharsColored(int charCount)
        {
            if (ForegroundColor.HasValue)
            {
                using (new TemporaryColorSwitcher(ForegroundColor.Value))
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
            Console.Write(new string(ProgressChar, charCount));
        }
    }

}

