using System;
using System.Reflection;
using DustInTheWind.SharpKinoko;

namespace DustInTheWind.SharpKinokoConsole
{
    public class HelpWritter
    {
        private IConsole console;
        private GuiHelpers guiHelpers;

        public HelpWritter(IConsole console, GuiHelpers guiHelpers)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            if (guiHelpers == null)
                throw new ArgumentNullException("guiHelpers");

            this.console = console;
            this.guiHelpers = guiHelpers;
        }

        public void WriteKinokoHeader()
        {
            using (new TemporaryColorSwitcher(console, ConsoleColor.Green))
            {
                Assembly assembly = Assembly.GetAssembly(typeof(Kinoko));
                console.WriteLine("Kinoko Console ver. {0}", assembly.GetName().Version.ToString(3));
                guiHelpers.WriteFullLine('=');
                console.WriteLine();
            }
        }

        public void WriteTaskTitle(DustInTheWind.SharpKinoko.KinokoSubject subject)
        {
            console.WriteLine();
            console.Write("Measuring subject: ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine(subject.Method.Name);
            }
        }

        public void WriteTaskResult(DustInTheWind.SharpKinoko.KinokoResult result)
        {
            console.WriteLine();
            console.Write("Average time: ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine("{0:#,##0.00} milisec", result.Average);
            }
        }

        public void WriteLoadingAssembly(string assemblyFileName)
        {
            console.Write("Start measuring subjects from assembly ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine(assemblyFileName);
            }
        }

        public void WriteShortHelp()
        {
            console.WriteLine("Expected: KinokoConsole <assemblyFileName>");
        }
    }
}

