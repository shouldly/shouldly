// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class StringAssertTests : MessageChecker
	{
		[Test]
		public void Contains()
		{
			StringAssert.Contains( "abc", "abc" );
			StringAssert.Contains( "abc", "***abc" );
			StringAssert.Contains( "abc", "**abc**" );
		}

        [Test, ExpectedException(typeof(AssertionException))]
		public void ContainsFails()
		{
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String containing \"abc\"" + System.Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"abxcdxbc\"" + System.Environment.NewLine;
            StringAssert.Contains("abc", "abxcdxbc");
		}

        [Test]
        public void DoesNotContain()
        {
            StringAssert.DoesNotContain("x", "abc");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void DoesNotContainFails()
        {
            StringAssert.DoesNotContain("abc", "**abc**");
        }

        [Test]
		public void StartsWith()
		{
			StringAssert.StartsWith( "abc", "abcdef" );
			StringAssert.StartsWith( "abc", "abc" );
		}

        [Test, ExpectedException(typeof(AssertionException))]
		public void StartsWithFails()
		{
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String starting with \"xyz\"" + System.Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"abcxyz\"" + System.Environment.NewLine;
            StringAssert.StartsWith("xyz", "abcxyz");
		}

        [Test]
        public void DoesNotStartWith()
        {
            StringAssert.DoesNotStartWith("x", "abc");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void DoesNotStartWithFails()
        {
            StringAssert.DoesNotStartWith("abc", "abc**");
        }

        [Test]
		public void EndsWith()
		{
			StringAssert.EndsWith( "abc", "abc" );
			StringAssert.EndsWith( "abc", "123abc" );
		}

        [Test, ExpectedException(typeof(AssertionException))]
		public void EndsWithFails()
		{
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String ending with \"xyz\"" + System.Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"abcdef\"" + System.Environment.NewLine;
            StringAssert.EndsWith( "xyz", "abcdef" );
		}

        [Test]
        public void DoesNotEndWith()
        {
            StringAssert.DoesNotEndWith("x", "abc");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void DoesNotEndWithFails()
        {
            StringAssert.DoesNotEndWith("abc", "***abc");
        }

        [Test]
		public void CaseInsensitiveCompare()
		{
			StringAssert.AreEqualIgnoringCase( "name", "NAME" );
		}

        [Test, ExpectedException(typeof(AssertionException))]
		public void CaseInsensitiveCompareFails()
		{
            expectedMessage =
                "  Expected string length 4 but was 5. Strings differ at index 4." + System.Environment.NewLine
                + TextMessageWriter.Pfx_Expected + "\"Name\", ignoring case" + System.Environment.NewLine
                + TextMessageWriter.Pfx_Actual   + "\"NAMES\"" + System.Environment.NewLine
                + "  ---------------^" + System.Environment.NewLine;
            StringAssert.AreEqualIgnoringCase("Name", "NAMES");
		}

		[Test]		
		public void IsMatch()
		{
			StringAssert.IsMatch( "a?bc", "12a3bc45" );
		}

        [Test, ExpectedException(typeof(AssertionException))]
		public void IsMatchFails()
		{
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String matching \"a?b*c\"" + System.Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"12ab456\"" + System.Environment.NewLine;
            StringAssert.IsMatch("a?b*c", "12ab456");
		}

		[Test]
		public void DifferentEncodingsOfSameStringAreNotEqual()
		{
			string input = "Hello World";
			byte[] data = System.Text.Encoding.Unicode.GetBytes( input );
			string garbage = System.Text.Encoding.UTF8.GetString( data );

			Assert.AreNotEqual( input, garbage );
		}
	}
}
