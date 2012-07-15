using DustInTheWind.SharpKinoko.SharpKinokoConsole.CommandArguments;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.CommandArguments.ArgumentsTests
{
    [TestFixture]
    public class IndexerTests
    {
        [Test]
        public void argument_has_null_value_if_only_name_provided_in_the_string()
        {
            string[] args = new[] { "-a" };
            Arguments arguments = new Arguments(args);

            Assert.That(arguments["a"], Is.Null);
        }

        [Test]
        public void argument_has_value_if_is_provided_in_the_string()
        {
            string[] args = new[] { "-a", "val1" };
            Arguments arguments = new Arguments(args);

            Assert.That(arguments["a"], Is.EqualTo("val1"));
        }

        [Test]
        public void argument_has_null_value_if_next_item_is_another_argument()
        {
            string[] args = new[] { "-a", "-b" };
            Arguments arguments = new Arguments(args);

            Assert.That(arguments["a"], Is.Null);
        }
    }
}

