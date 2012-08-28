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
using System.Collections.Generic;
using CommandLine;
using DustInTheWind.SharpKinoko.SharpKinokoConsole;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.KinokoApplicationTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="KinokoApplication.Start"/> method.
    /// </summary>
    [TestFixture]
    public class StartTests
    {
        private CommandLineOptions commandLineOptions;
        private Mock<IUI> ui;
        private Mock<IKinokoRunner> kinokoRunner;
        private Mock<IConsole> console;
        private KinokoApplication kinokoApplication;

        [SetUp]
        public void SetUp()
        {
            commandLineOptions = new CommandLineOptions();
            ui = new Mock<IUI>();
            kinokoRunner = new Mock<IKinokoRunner>();
            console = new Mock<IConsole>();
            ui.Setup(x => x.Console).Returns(console.Object);
            kinokoApplication = new KinokoApplication(commandLineOptions, ui.Object, kinokoRunner.Object);
        }

        [Test]
        public void sets_ForegroundColor_to_green()
        {
            kinokoApplication.Start();

            console.VerifySet(x => x.ForegroundColor = ConsoleColor.DarkGreen, Times.Once());
        }

        [Test]
        public void ui_WriteKinokoHeader_is_called()
        {
            kinokoApplication.Start();

            ui.Verify(x => x.WriteKinokoHeader(), Times.Once());
        }

        [Test]
        public void error_message_is_displayed_if_WriteKinokoHeader_trows()
        {
            Exception ex = new Exception("Test");
            ui.Setup(x => x.WriteKinokoHeader()).Throws(ex);

            kinokoApplication.Start();

            ui.Verify(x => x.DisplayError(ex), Times.Once());
        }

        [Test]
        public void no_error_is_displayed_if_no_exception_is_thrown()
        {
            kinokoApplication.Start();

            ui.Verify(x => x.DisplayError(It.IsAny<Exception>()), Times.Never());
        }

        [Test]
        public void Pause_is_called_at_the_end_if_no_exception_is_thrown()
        {
            kinokoApplication.Start();

            ui.Verify(x => x.Pause(), Times.Once());
        }

        [Test]
        public void Pause_is_called_at_the_end_if_exception_is_thrown()
        {
            Exception ex = new Exception("Test");
            ui.Setup(x => x.WriteKinokoHeader()).Throws(ex);

            kinokoApplication.Start();

            ui.Verify(x => x.Pause(), Times.Once());
        }

        [Test]
        public void if_no_parsing_errors_KinokoRunner_is_started_with_the_list_of_assemblies()
        {
            string[] args = new[] { "-a", "aaa.dll" };
            CommandLineParser parser = new CommandLineParser();
            parser.ParseArguments(args, commandLineOptions);

            kinokoApplication.Start();

            kinokoRunner.Verify(x => x.StartMeasuring(new[] { "aaa.dll" }, It.IsAny<int>()));
        }

        [Test]
        public void if_no_parsing_errors_KinokoRunner_is_started_with_RepeatMeasurementCount_10()
        {
            string[] args = new[] { "-a", "aaa.dll" };
            CommandLineParser parser = new CommandLineParser();
            parser.ParseArguments(args, commandLineOptions);

            kinokoApplication.Start();

            kinokoRunner.Verify(x => x.StartMeasuring(It.IsAny<IEnumerable<string>>(), 10));
        }
    }
}

