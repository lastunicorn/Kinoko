using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    class CommandLineArguments
    {
        [Option("a", "assemly", Required = false, HelpText = "A list of assembly file names.")]
        public string AssemblyFileNames { get; set; }
    }
}
