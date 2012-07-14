using DustInTheWind.SharpKinoko.SharpKinokoConsole.CommandArguments;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.CommandArguments.ArgumentsTests
{
    [TestFixture]
    public class CountTests
    {
        [Test]
        public void Count_is_0_if_args_array_is_empty()
        {
            Arguments arguments = new Arguments(new string[0]);

            Assert.That(arguments.Count, Is.EqualTo(0));
        }

        [Test]
        public void Count_is_0_if_args_is_null()
        {
            Arguments arguments = new Arguments(null);

            Assert.That(arguments.Count, Is.EqualTo(0));
        }

        [Test]
        public void count_is_1_for_string_with_one_argument()
        {
            string[] args = new string[] { "-a" };
            Arguments arguments = new Arguments(args);

            Assert.That(arguments.Count, Is.EqualTo(1));
        }

        [Test]
        public void count_is_2_for_string_with_two_arguments()
        {
            string[] args = new string[] { "-a", "-b" };
            Arguments arguments = new Arguments(args);

            Assert.That(arguments.Count, Is.EqualTo(2));
        }
    }
}

