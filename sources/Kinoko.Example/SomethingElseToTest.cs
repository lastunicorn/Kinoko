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


namespace DustInTheWind.Kinoko.Example
{
    public class SomethingElseToTest
    {
        public int A;

        [KinokoTask]
        public void IncrementInForLoop1000000()
        {
            A = 0;

            for (int i = 0; i < 1000000; i++)
            {
                A++;
            }
        }

        [KinokoTask]
        public void CallIncrementMethodInForLoop1000000()
        {
            A = 0;

            for (int i = 0; i < 1000000; i++)
            {
                SomeMethod();
            }
        }

        private void SomeMethod()
        {
            // Increment a public field so that the compiler should not optimize removing the method.
            A++;
        }
    }
}

