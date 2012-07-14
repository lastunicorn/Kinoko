using DustInTheWind.SharpKinoko.SharpKinokoConsole.CommandArguments;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.CommandArguments.ArgumentsTests
{
    [TestFixture]
    public class ContainsTests
    {
        [Test]
        public void returns_false_if_argumentName_is_null()
        {
            Arguments arguments = new Arguments(new string[] {"-a", "-b"});

            Assert.That(arguments.Contains(null), Is.False);
        }

        [Test]
        public void returns_false_if_args_is_null()
        {
            Arguments arguments = new Arguments(null);

            Assert.That(arguments.Contains("a"), Is.False);
        }

        [Test]
        public void returns_false_if_args_is_empty()
        {
            Arguments arguments = new Arguments(new string[0]);

            Assert.That(arguments.Contains("a"), Is.False);
        }

        [Test]
        public void returns_false_if_argumentName_is_not_contained_in_args_array()
        {
            Arguments arguments = new Arguments(new string[] { "-a", "-b" });

            Assert.That(arguments.Contains("z"), Is.False);
        }

        [Test]
        public void returns_true_if_argumentName_is_contained_in_the_args_array()
        {
            Arguments arguments = new Arguments(new string[] { "-a", "-b" });

            Assert.That(arguments.Contains("a"), Is.True);
        }
    }
}

