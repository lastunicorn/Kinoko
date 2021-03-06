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

using DustInTheWind.Kinoko;

namespace AssemblyWithMethodsForTesting
{
    public class ClassForTest
    {
        [KinokoTask]
        public void PublicMethodWithAttribute()
        {
        }

        public void PublicMethod()
        {
        }

        [KinokoTask]
        public void PublicMethodWithParametersAndAttribute(int a)
        {
        }

        [KinokoTask]
        public void PublicMethodWithGenericParameterAndAttribute<T>()
        {

        }
     
        [KinokoTask]
        private void PrivateMethodWithAttribute()
        {
        }
     
        [KinokoTask]
        public static void PublicStaticMethodWithAttribute()
        {
        }
     
        public static void PublicStaticMethod()
        {
        }
     
        [KinokoTask]
        private static void PrivateStaticMethodWithAttribute()
        {
        }
    }
}

