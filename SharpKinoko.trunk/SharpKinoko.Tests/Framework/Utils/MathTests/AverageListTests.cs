using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Framework.Utils.MathTests
{
    [TestFixture]
    public class AverageListTests
    {
        [Test]
        public void returns_the_number_for_list_with_one_int_value()
        {
            double actual = DustInTheWind.SharpKinoko.Utils.Math.Average(new List<double> { 13 });

            Assert.That(actual, Is.EqualTo(13));
        }

        [Test]
        public void returns_the_number_for_list_with_one_double_value()
        {
            double actual = DustInTheWind.SharpKinoko.Utils.Math.Average(new List<double> { 13.5 });

            Assert.That(actual, Is.EqualTo(13.5));
        }

        [Test]
        public void returns_0_for_list_with_no_value()
        {
            double actual = DustInTheWind.SharpKinoko.Utils.Math.Average(new List<double>());

            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void returns_12_5_for_list_with_two_values_10_and_15()
        {
            double actual = DustInTheWind.SharpKinoko.Utils.Math.Average(new List<double> { 10, 15 });

            Assert.That(actual, Is.EqualTo(12.5));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_when_list_is_null()
        {
            try
            {
                DustInTheWind.SharpKinoko.Utils.Math.Average(null as List<double>);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("values"));
                throw;
            }
        }
    }
}
