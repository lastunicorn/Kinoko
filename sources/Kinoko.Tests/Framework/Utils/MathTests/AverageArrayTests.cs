using System;
using NUnit.Framework;
using Math = DustInTheWind.Kinoko.Utils.Math;

namespace DustInTheWind.Kinoko.Tests.Framework.Utils.MathTests
{
    [TestFixture]
    public class AverageArrayTests
    {
        [Test]
        public void returns_the_number_for_array_with_one_int_value()
        {
            double actual = Math.Average(new double[] { 13 });

            Assert.That(actual, Is.EqualTo(13));
        }

        [Test]
        public void returns_the_number_for_array_with_one_double_value()
        {
            double actual = Math.Average(new double[] { 13.5 });

            Assert.That(actual, Is.EqualTo(13.5));
        }

        [Test]
        public void returns_0_for_array_with_no_value()
        {
            double actual = Math.Average(new double[0]);

            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void returns_12_5_for_array_with_two_values_10_and_15()
        {
            double actual = Math.Average(new double[] { 10, 15 });

            Assert.That(actual, Is.EqualTo(12.5));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_when_array_is_null()
        {
            try
            {
                Math.Average(null as double[]);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("values"));
                throw;
            }
        }
    }
}
