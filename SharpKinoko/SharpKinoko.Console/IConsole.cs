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

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Represents a console.
    /// </summary>
    public interface IConsole
    {
        int CursorTop { get; set; }

        int CursorLeft { get; set; }

        int WindowWidth { get; set; }

        ConsoleColor ForegroundColor { get; set; }

        void Write(string value);

        void WriteLine();

        void WriteLine(string format, params object[] args);

        ConsoleKeyInfo ReadKey(bool intercept);
    }
}

