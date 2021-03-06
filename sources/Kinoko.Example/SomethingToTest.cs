﻿// SharpKinoko
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

using System.Threading;

namespace DustInTheWind.Kinoko.Example
{
    public class SomethingToTest
    {
        public object O;

        [KinokoTask]
        public void Sleep30()
        {
            Thread.Sleep(30);
        }

        [KinokoTask]
        public void EmptyForLoop1000000()
        {
            for (int i = 0; i < 1000000; i++)
            {
            }
        }

        [KinokoTask]
        public void CallEmptyMethodInForLoop1000000()
        {
            for (int i = 0; i < 1000000; i++)
            {
                SomeMethod();
            }
        }

        private void SomeMethod()
        {
        }

        [KinokoTask]
        public void InstanciateObjects1000000()
        {
            for (int i = 0; i < 1000000; i++)
            {
                O = new object();
            }
        }
    }

}
