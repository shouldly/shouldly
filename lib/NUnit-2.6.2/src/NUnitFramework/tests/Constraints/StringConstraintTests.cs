// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class SubstringTest : ConstraintTestBase, IExpectException
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new SubstringConstraint("hello");
            expectedDescription = "String containing \"hello\"";
            stringRepresentation = "<substring \"hello\">";
        }

        internal object[] SuccessData = new object[] { "hello", "hello there", "I said hello", "say hello to fred" };
        
        internal object[] FailureData = new object[] { "goodbye", "HELLO", "What the hell?", string.Empty, null };

        internal string[] ActualValues = new string[] { "\"goodbye\"", "\"HELLO\"", "\"What the hell?\"", "<string.Empty>", "null" }; 

        public void HandleException(Exception ex)
        {
            Assert.That(ex.Message, new EqualConstraint(
                TextMessageWriter.Pfx_Expected + "String containing \"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa...\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual   + "\"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx...\"" + Environment.NewLine));
        }
    }

    [TestFixture]
    public class SubstringTestIgnoringCase : ConstraintTestBase
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new SubstringConstraint("hello").IgnoreCase;
            expectedDescription = "String containing \"hello\", ignoring case";
            stringRepresentation = "<substring \"hello\">";
        }

        internal object[] SuccessData = new object[] { "Hello", "HellO there", "I said HELLO", "say hello to fred" };
        
        internal object[] FailureData = new object[] { "goodbye", "What the hell?", string.Empty, null };

        internal string[] ActualValues = new string[] { "\"goodbye\"", "\"What the hell?\"", "<string.Empty>", "null" };
    }

    [TestFixture]
    public class StartsWithTest : ConstraintTestBase
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new StartsWithConstraint("hello");
            expectedDescription = "String starting with \"hello\"";
            stringRepresentation = "<startswith \"hello\">";
        }

        internal object[] SuccessData = new object[] { "hello", "hello there" };

        internal object[] FailureData = new object[] { "goodbye", "HELLO THERE", "I said hello", "say hello to fred", string.Empty, null };

        internal string[] ActualValues = new string[] { "\"goodbye\"", "\"HELLO THERE\"", "\"I said hello\"", "\"say hello to fred\"", "<string.Empty>", "null" };
    }

    [TestFixture]
    public class StartsWithTestIgnoringCase : ConstraintTestBase
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new StartsWithConstraint("hello").IgnoreCase;
            expectedDescription = "String starting with \"hello\", ignoring case";
            stringRepresentation = "<startswith \"hello\">";
        }

        internal object[] SuccessData = new object[] { "Hello", "HELLO there" };
            
        internal object[] FailureData = new object[] { "goodbye", "What the hell?", "I said hello", "say Hello to fred", string.Empty, null };

        internal string[] ActualValues = new string[] { "\"goodbye\"", "\"What the hell?\"", "\"I said hello\"", "\"say Hello to fred\"", "<string.Empty>", "null" };
    }

    [TestFixture]
    public class EndsWithTest : ConstraintTestBase
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new EndsWithConstraint("hello");
            expectedDescription = "String ending with \"hello\"";
            stringRepresentation = "<endswith \"hello\">";
        }

        internal object[] SuccessData = new object[] { "hello", "I said hello" };
            
        internal object[] FailureData = new object[] { "goodbye", "What the hell?", "hello there", "say hello to fred", string.Empty, null };

        internal string[] ActualValues = new string[] { "\"goodbye\"", "\"What the hell?\"", "\"hello there\"", "\"say hello to fred\"", "<string.Empty>", "null" };
    }

    [TestFixture]
    public class EndsWithTestIgnoringCase : ConstraintTestBase
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new EndsWithConstraint("hello").IgnoreCase;
            expectedDescription = "String ending with \"hello\", ignoring case";
            stringRepresentation = "<endswith \"hello\">";
        }

        internal object[] SuccessData = new object[] { "HELLO", "I said Hello" };
            
        internal object[] FailureData = new object[] { "goodbye", "What the hell?", "hello there", "say hello to fred", string.Empty, null };

        internal string[] ActualValues = new string[] { "\"goodbye\"", "\"What the hell?\"", "\"hello there\"", "\"say hello to fred\"", "<string.Empty>", "null" };
    }

    //[TestFixture]
    //public class EqualIgnoringCaseTest : ConstraintTest
    //{
    //    [SetUp]
    //    public void SetUp()
    //    {
    //        Matcher = new EqualConstraint("Hello World!").IgnoreCase;
    //        Description = "\"Hello World!\", ignoring case";
    //    }

    //    object[] SuccessData = new object[] { "hello world!", "Hello World!", "HELLO world!" };
            
    //    object[] FailureData = new object[] { "goodbye", "Hello Friends!", string.Empty, null };

    //    string[] ActualValues = new string[] { "\"goodbye\"", "\"Hello Friends!\"", "<string.Empty>", "null" };
    //}
}
