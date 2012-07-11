using System;
using NUnit.Framework;
using DustInTheWind.Utils.CommandArguments;

namespace DustInTheWind.SharpKinoko.Tests.Console.CommandArguments
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

        #region Count

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

        #endregion

        [Test]
        public void Contains_returns_false_if_argumentName_is_null()
        {
            Arguments arguments = new Arguments(new string[] {"-a", "-b"});

            Assert.That(arguments.Contains(null), Is.False);
        }

        [Test]
        public void Contains_returns_false_if_args_is_null()
        {
            Arguments arguments = new Arguments(null);

            Assert.That(arguments.Contains("a"), Is.False);
        }

        [Test]
        public void Contains_returns_false_if_args_is_empty()
        {
            Arguments arguments = new Arguments(new string[0]);

            Assert.That(arguments.Contains("a"), Is.False);
        }

        [Test]
        public void Contains_returns_false_if_argumentName_is_not_contained_in_args_array()
        {
            Arguments arguments = new Arguments(new string[] { "-a", "-b" });

            Assert.That(arguments.Contains("z"), Is.False);
        }

        [Test]
        public void Contains_returns_true_if_argumentName_is_contained_in_the_args_array()
        {
            Arguments arguments = new Arguments(new string[] { "-a", "-b" });

            Assert.That(arguments.Contains("a"), Is.True);
        }

        [Test]
        public void argument_has_null_value_if_only_name_provided_in_the_string()
        {
            string[] args = new string[] { "-a" };
            Arguments arguments = new Arguments(args);

            Assert.That(arguments["a"], Is.Null);
        }
    }
}

