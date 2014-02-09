// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework.Constraints
{
	/// <summary>
	/// Summary description for MsgUtilTests.
	/// </summary>
	[TestFixture]
	public class MsgUtilTests
	{
        [TestCase("\n", "\\n")]
        [TestCase("\n\n", "\\n\\n")]
        [TestCase("\n\n\n", "\\n\\n\\n")]
        [TestCase("\r", "\\r")]
        [TestCase("\r\r", "\\r\\r")]
        [TestCase("\r\r\r", "\\r\\r\\r")]
        [TestCase("\r\n", "\\r\\n")]
        [TestCase("\n\r", "\\n\\r")]
        [TestCase("This is a\rtest message", "This is a\\rtest message")]
        [TestCase("", "")]
#if CLR_2_0 || CLR_4_0
        [TestCase(null, null)]
#endif
        [TestCase("\t", "\\t")]
        [TestCase("\t\n", "\\t\\n")]
        [TestCase("\\r\\n", "\\\\r\\\\n")]
        // TODO: Figure out why this fails in Mono
        // TODO: Need Platform property on test case
        //[TestCase("\0", "\\0")]
        [TestCase("\a", "\\a")]
        [TestCase("\b", "\\b")]
        [TestCase("\f", "\\f")]
        [TestCase("\v", "\\v")]
        // New Line
        [TestCase("\x0085", "\\x0085", Description = "Next line character")]
        [TestCase("\x2028", "\\x2028", Description = "Line separator character")]
        [TestCase("\x2029", "\\x2029", Description = "Paragraph separator character")]
        public void EscapeControlCharsTest(string input, string expected)
		{
            Assert.AreEqual( expected, MsgUtils.EscapeControlChars(input) );
		}

#if MONO
        [Test]
        public void EscapeNullCharInString()
        {
            Assert.That(MsgUtils.EscapeControlChars("\0"), Is.EqualTo("\\0"));
        }
#endif

        private const string s52 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [TestCase(s52, 52, 0, s52, TestName="NoClippingNeeded")]
        [TestCase(s52, 29, 0, "abcdefghijklmnopqrstuvwxyz...", TestName="ClipAtEnd")]
        [TestCase(s52, 29, 26, "...ABCDEFGHIJKLMNOPQRSTUVWXYZ", TestName="ClipAtStart")]
        [TestCase(s52, 28, 26, "...ABCDEFGHIJKLMNOPQRSTUV...", TestName="ClipAtStartAndEnd")]
        public void TestClipString(string input, int max, int start, string result)
        {
            Assert.AreEqual(result, MsgUtils.ClipString(input, max, start));
        }

        //[TestCase('\0')]
        //[TestCase('\r')]
        //public void CharacterArgumentTest(char c)
        //{
        //}

        [Test]
        public void ClipExpectedAndActual_StringsFitInLine()
        {
            string eClip = s52;
            string aClip = "abcde";
            MsgUtils.ClipExpectedAndActual(ref eClip, ref aClip, 52, 5);
            Assert.AreEqual(s52, eClip);
            Assert.AreEqual("abcde", aClip);

            eClip = s52;
            aClip = "abcdefghijklmno?qrstuvwxyz";
            MsgUtils.ClipExpectedAndActual(ref eClip, ref aClip, 52, 15);
            Assert.AreEqual(s52, eClip);
            Assert.AreEqual("abcdefghijklmno?qrstuvwxyz", aClip);
        }

        [Test]
        public void ClipExpectedAndActual_StringTailsFitInLine()
        {
            string s1 = s52;
            string s2 = s52.Replace('Z', '?');
            MsgUtils.ClipExpectedAndActual(ref s1, ref s2, 29, 51);
            Assert.AreEqual("...ABCDEFGHIJKLMNOPQRSTUVWXYZ", s1);
        }

        [Test]
        public void ClipExpectedAndActual_StringsDoNotFitInLine()
        {
            string s1 = s52;
            string s2 = "abcdefghij";
            MsgUtils.ClipExpectedAndActual(ref s1, ref s2, 29, 10);
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz...", s1);
            Assert.AreEqual("abcdefghij", s2);

            s1 = s52;
            s2 = "abcdefghijklmno?qrstuvwxyz";
            MsgUtils.ClipExpectedAndActual(ref s1, ref s2, 25, 15);
            Assert.AreEqual("...efghijklmnopqrstuvw...", s1);
            Assert.AreEqual("...efghijklmno?qrstuvwxyz", s2);
        }
	}
}
