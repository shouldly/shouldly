// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Drawing;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class EqualTest : IExpectException
    {

        [Test, ExpectedException(typeof(AssertionException))]
        public void FailedStringMatchShowsFailurePosition()
        {
            Assert.That( "abcdgfe", new EqualConstraint( "abcdefg" ) );
        }

        static readonly string testString = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [Test, ExpectedException(typeof(AssertionException))]
        public void LongStringsAreTruncated()
        {
            string expected = testString;
            string actual = testString.Replace('k', 'X');

            Assert.That(actual, new EqualConstraint(expected));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void LongStringsAreTruncatedAtBothEndsIfNecessary()
        {
            string expected = testString;
            string actual = testString.Replace('Z', '?');

            Assert.That(actual, new EqualConstraint(expected));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void LongStringsAreTruncatedAtFrontEndIfNecessary()
        {
            string expected = testString;
            string actual = testString  + "+++++";

            Assert.That(actual, new EqualConstraint(expected));
        }

//        [Test]
//        public void NamedAndUnnamedColorsCompareAsEqual()
//        {
//            EqualConstraint.SetConstraintForType(typeof(Color), typeof(SameColorAs));
//            Assert.That(System.Drawing.Color.Red,
//                Is.EqualTo(System.Drawing.Color.FromArgb(255, 0, 0)));
//        }

        public void HandleException(Exception ex)
        {
            StringReader rdr = new StringReader(ex.Message);
            /* skip */ rdr.ReadLine();
            string expected = rdr.ReadLine();
            if (expected != null && expected.Length > 11)
                expected = expected.Substring(11);
            string actual = rdr.ReadLine();
            if (actual != null && actual.Length > 11)
                actual = actual.Substring(11);
            string line = rdr.ReadLine();
            Assert.That(line, new NotConstraint(new EqualConstraint(null)), "No caret line displayed");
            int caret = line.Substring(11).IndexOf('^');

            int minLength = Math.Min(expected.Length, actual.Length);
            int minMatch = Math.Min(caret, minLength);

            if (caret != minLength)
            {
                if (caret > minLength ||
                    expected.Substring(0, minMatch) != actual.Substring(0, minMatch) ||
                    expected[caret] == actual[caret])
                    Assert.Fail("Message Error: Caret does not point at first mismatch..." + Environment.NewLine + ex.Message);
            }

            if (expected.Length > 68 || actual.Length > 68 || caret > 68)
                Assert.Fail("Message Error: Strings are not truncated..." + Environment.NewLine + ex.Message);
        }

        public class SameColorAs : Constraint
        {
            private Color expectedColor;

            public SameColorAs(Color expectedColor)
            {
                this.expectedColor = expectedColor;
            }

            public override bool Matches(object actual)
            {
                this.actual = actual;
                return actual is Color && ((Color)actual).ToArgb() == expectedColor.ToArgb();
            }

            public override void WriteDescriptionTo(MessageWriter writer)
            {
                writer.WriteExpectedValue( "same color as " + expectedColor );
            }
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void TestPropertyWithPrivateSetter()
        {
            SomeClass obj = new SomeClass();
            Assert.That(obj.BrokenProp, Is.EqualTo(string.Empty));
        }

        private class SomeClass
        {
            public string BrokenProp
            {
                get { return string.Empty; }
                private set { }
            }
        }
#endif
    }
}