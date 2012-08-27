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
using CommandLine;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Ninject;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Prepares the environment into which a new <see cref="KinokoApplication"/> instance is started.
    /// </summary>
    internal class Bootstrapper
    {
        /// <summary>
        /// The Ninject kernel. A IOC container.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The command line arguments used by the user to start the application.
        /// </summary>
        private readonly string[] args;

        /// <summary>
        /// The options obtained by parsing the args.
        /// </summary>
        private CommandLineOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DustInTheWind.SharpKinoko.SharpKinokoConsole.Bootstrapper"/> class.
        /// </summary>
        /// <param name='args'>The command line arguments used by the user to start the application.</param>
        public Bootstrapper(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            this.args = args;
        }

        /// <summary>
        /// Creates the ninject kernel instance, initializes it and starts a new instance of <see cref="KinokoApplication"/>.
        /// </summary>
        public void Start()
        {            
            try
            {
                CreateOptions();
                CreateAndConfigureNinjectKernel();
                RunApplication();
            }
            catch (Exception ex)
            {
                IConsole console = new ConsoleWrapper();
                UI guiHelpers = new UI(console);

                guiHelpers.DisplayError(ex);
                guiHelpers.Pause();
            }
        }

        private void CreateOptions()
        {
            CommandLineOptions options = new CommandLineOptions();

            CommandLineParserSettings parserSettings = new CommandLineParserSettings();
            CommandLineParser parser = new CommandLineParser(parserSettings);

            parser.ParseArguments(args, options);

            this.options = options;
        }

        private void CreateAndConfigureNinjectKernel()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<UI>().ToSelf().InSingletonScope();
            kernel.Bind<IConsole>().To<ConsoleWrapper>().InSingletonScope();
            kernel.Bind<CommandLineOptions>().ToConstant(options);

            this.kernel = kernel;
        }

        private void RunApplication()
        {
            KinokoApplication kinokoApplication = kernel.Get<KinokoApplication>();
            kinokoApplication.Start();
        }
    }
}

