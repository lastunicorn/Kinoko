using DustInTheWind.SharpKinoko.SharpKinokoConsole.CommandArguments;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Console.CommandArguments.ArgumentsTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void initializes_RawArgs_with_the_received_args_array()
        {
            string[] args = new string[0];
            Arguments arguments = new Arguments(args);

            Assert.That(arguments.RawArgs, Is.SameAs(args));
        }
    }
}

