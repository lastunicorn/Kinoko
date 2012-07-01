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
using Moq;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.KinokoTests
{
    [TestFixture]
    public class ConstructorTests
    {
        #region Kinoko()

        [Test]
        public void Test()
        {

        }

        #endregion

        #region Kinoko(TaskProvider taskProvider, taskRunCount)

        [Test]
        public void constructor_retrieves_tasks_from_provider()
        {
            Mock<ITasksProvider> tasksProvider = new Mock<ITasksProvider>();

            new Kinoko(tasksProvider.Object, 10);

            tasksProvider.Verify(x=>x.GetTasks(),Times.Once());
        }

        #endregion
    }
}

