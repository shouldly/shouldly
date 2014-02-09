// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.UiException.StackTraceAnalyzers;
using NUnit.Framework;
using NUnit.UiException.StackTraceAnalysers;

namespace NUnit.UiException.Tests.StackTraceAnalyzers
{
    [TestFixture]
    public class TestIErrorParser
    {
        protected StackTraceParser _stack;
        protected IErrorParser[] _array;

        public TestIErrorParser()
        {
            _stack = new StackTraceParser();

            return;
        }

        [SetUp]
        public void SetUp()
        {
            PathCompositeParser pathParser;

            pathParser = new PathCompositeParser();

            _array = new IErrorParser[]  {
                pathParser.UnixPathParser,
                pathParser.WindowsPathParser,
                new FunctionParser(),
                new PathCompositeParser(),
                new LineNumberParser()
            };

            return;
        }

        [Test]
        public void Test_IErrorParser_Can_Throw_ParserNullException()
        {
            bool hasRaisedException;

            foreach (IErrorParser item in _array)
            {
                hasRaisedException = false;

                try {
                    item.TryParse(null, new RawError("test")); // throws exception
                }
                catch (Exception e)
                {
                    Assert.That(e.Message.Contains("parser"), Is.True);
                    hasRaisedException = true;
                }

                Assert.That(hasRaisedException, Is.True,
                    item.ToString() + " failed to raise exception");
            }

            return;
        }

        [Test]
        public void Test_IErrorParser_Can_Throw_ArgsNullException()
        {
            bool hasRaisedException;

            foreach (IErrorParser item in _array)
            {
                hasRaisedException = false;

                try
                {
                    item.TryParse(_stack, null); // throws exception
                }
                catch (Exception e)
                {
                    Assert.That(e.Message.Contains("args"), Is.True);
                    hasRaisedException = true;
                }

                Assert.That(hasRaisedException, Is.True,
                    item.ToString() + " failed to raise exception");
            }

            return;
        }

        public RawError AcceptValue(IErrorParser parser, string error)
        {
            RawError res;

            res = new RawError(error);
            Assert.That(parser.TryParse(_stack, res), Is.True, "Failed to parse \"{0}\"", error);

            return (res);
        }

        public void RejectValue(IErrorParser parser, string error)
        {
            Assert.That(parser.TryParse(_stack, new RawError(error)), Is.False);
        }
    }
}
