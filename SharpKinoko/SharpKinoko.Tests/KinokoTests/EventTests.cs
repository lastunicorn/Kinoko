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
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;

namespace DustInTheWind.SharpKinoko.Tests.KinokoTests
{
    [TestFixture]
    public class EventTests
    {
        private MockRepository mocks;
        private Kinoko kinoko;
        private int taskRunTime = 50;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            kinoko = new Kinoko();
            kinoko.Task = new KinokoTask(delegate { Thread.Sleep(taskRunTime); });
        }

        [Test]
        public void TestBeforeTaskRun([Values(1, 2, 3)]int taskRunCount)
        {
            EventHandler<BeforeTaskRunEventArgs> eva = mocks.StrictMock<EventHandler<BeforeTaskRunEventArgs>>();
            kinoko.BeforeTaskRun += eva;

            using (mocks.Record())
            {
                for (int i = 0; i < taskRunCount; i++)
                {
                    eva(kinoko, new BeforeTaskRunEventArgs(i));
                    LastCall.Repeat.Once().Constraints(
                        Rhino.Mocks.Constraints.Is.Same(kinoko),
                        new Rhino.Mocks.Constraints.PropertyConstraint("StepIndex", Rhino.Mocks.Constraints.Is.Equal(i))
                        );
                }
            }

            using (mocks.Playback())
            {
                kinoko.TaskRunCount = taskRunCount;
                kinoko.Run();
            }
        }

        [Test]
        public void TestAfterTaskRun([Values(1, 2, 3)]int taskRunCount)
        {
            EventHandler<AfterTaskRunEventArgs> eva = mocks.StrictMock<EventHandler<AfterTaskRunEventArgs>>();
            kinoko.AfterTaskRun += eva;

            int tolerance = 2;

            using (mocks.Record())
            {
                for (int i = 0; i < taskRunCount; i++)
                {
                    eva(null, null);
                    LastCall.Repeat.Once().IgnoreArguments().Constraints(
                        Rhino.Mocks.Constraints.Is.Same(kinoko),
                        new Rhino.Mocks.Constraints.And(
                            Rhino.Mocks.Constraints.Is.NotNull(),
                            new Rhino.Mocks.Constraints.And(
                                new Rhino.Mocks.Constraints.PropertyConstraint("StepIndex", Rhino.Mocks.Constraints.Is.Equal(i)),
                                new Rhino.Mocks.Constraints.PropertyConstraint("Time", Rhino.Mocks.Constraints.Is.Matching<double>(new Predicate<double>(delegate(double d) { return d > taskRunTime - tolerance && d < taskRunTime + tolerance; })))
                            )
                        )
                        );
                }
            }

            using (mocks.Playback())
            {
                kinoko.TaskRunCount = taskRunCount;
                kinoko.Run();
            }
        }
    }
}
