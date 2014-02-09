// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework.Tests
{
    [TestFixture]
    public class LessEqualFixture : MessageChecker
    {
        private readonly int i1 = 5;
        private readonly int i2 = 8;
		private readonly uint u1 = 12345678;
		private readonly uint u2 = 12345879;
		private readonly long l1 = 12345678;
		private readonly long l2 = 12345879;
		private readonly ulong ul1 = 12345678;
		private readonly ulong ul2 = 12345879;
		private readonly float f1 = 3.543F;
        private readonly float f2 = 8.543F;
        private readonly decimal de1 = 53.4M;
        private readonly decimal de2 = 83.4M;
        private readonly double d1 = 4.85948654;
        private readonly double d2 = 8.0;
        private readonly System.Enum e1 = System.Data.CommandType.StoredProcedure;
        private readonly System.Enum e2 = System.Data.CommandType.TableDirect;

        [Test]
        public void LessOrEqual()
        {
            // Test equality check for all forms
            Assert.LessOrEqual(i1, i1);
            Assert.LessOrEqual(i1, i1, "int");
            Assert.LessOrEqual(i1, i1, "{0}", "int");
			Assert.LessOrEqual(u1, u1);
			Assert.LessOrEqual(u1, u1, "uint");
			Assert.LessOrEqual(u1, u1, "{0}", "uint");
			Assert.LessOrEqual(l1, l1);
			Assert.LessOrEqual(l1, l1, "long");
			Assert.LessOrEqual(l1, l1, "{0}", "long");
			Assert.LessOrEqual(ul1, ul1);
			Assert.LessOrEqual(ul1, ul1, "ulong");
			Assert.LessOrEqual(ul1, ul1, "{0}", "ulong");
			Assert.LessOrEqual(d1, d1);
            Assert.LessOrEqual(d1, d1, "double");
            Assert.LessOrEqual(d1, d1, "{0}", "double");
            Assert.LessOrEqual(de1, de1);
            Assert.LessOrEqual(de1, de1, "decimal");
            Assert.LessOrEqual(de1, de1, "{0}", "decimal");
            Assert.LessOrEqual(f1, f1);
            Assert.LessOrEqual(f1, f1, "float");
            Assert.LessOrEqual(f1, f1, "{0}", "float");

            // Testing all forms after seeing some bugs. CFP
            Assert.LessOrEqual(i1, i2);
            Assert.LessOrEqual(i1, i2, "int");
            Assert.LessOrEqual(i1, i2, "{0}", "int");
            Assert.LessOrEqual(u1, u2);
            Assert.LessOrEqual(u1, u2, "uint");
            Assert.LessOrEqual(u1, u2, "{0}", "uint");
			Assert.LessOrEqual(l1, l2);
			Assert.LessOrEqual(l1, l2, "long");
			Assert.LessOrEqual(l1, l2, "{0}", "long");
			Assert.LessOrEqual(ul1, ul2);
			Assert.LessOrEqual(ul1, ul2, "ulong");
			Assert.LessOrEqual(ul1, ul2, "{0}", "ulong");
			Assert.LessOrEqual(d1, d2);
            Assert.LessOrEqual(d1, d2, "double");
            Assert.LessOrEqual(d1, d2, "{0}", "double");
            Assert.LessOrEqual(de1, de2);
            Assert.LessOrEqual(de1, de2, "decimal");
            Assert.LessOrEqual(de1, de2, "{0}", "decimal");
            Assert.LessOrEqual(f1, f2);
            Assert.LessOrEqual(f1, f2, "float");
            Assert.LessOrEqual(f1, f2, "{0}", "float");
        }

		[Test]
		public void MixedTypes()
		{	
			Assert.LessOrEqual( 5, 8L, "int to long");
			Assert.LessOrEqual( 5, 8.2f, "int to float" );
			Assert.LessOrEqual( 5, 8.2d, "int to double" );
			Assert.LessOrEqual( 5, 8U, "int to uint" );
			Assert.LessOrEqual( 5, 8UL, "int to ulong" );
			Assert.LessOrEqual( 5, 8M, "int to decimal" );

			Assert.LessOrEqual( 5L, 8, "long to int");
			Assert.LessOrEqual( 5L, 8.2f, "long to float" );
			Assert.LessOrEqual( 5L, 8.2d, "long to double" );
			Assert.LessOrEqual( 5L, 8U, "long to uint" );
			Assert.LessOrEqual( 5L, 8UL, "long to ulong" );
			Assert.LessOrEqual( 5L, 8M, "long to decimal" );

			Assert.LessOrEqual( 3.5f, 5, "float to int" );
			Assert.LessOrEqual( 3.5f, 8L, "float to long" );
			Assert.LessOrEqual( 3.5f, 8.2d, "float to double" );
			Assert.LessOrEqual( 3.5f, 8U, "float to uint" );
			Assert.LessOrEqual( 3.5f, 8UL, "float to ulong" );
			Assert.LessOrEqual( 3.5f, 8.2M, "float to decimal" );

			Assert.LessOrEqual( 3.5d, 5, "double to int" );
			Assert.LessOrEqual( 3.5d, 5L, "double to long" );
			Assert.LessOrEqual( 3.5d, 8.2f, "double to float" );
			Assert.LessOrEqual( 3.5d, 8U, "double to uint" );
			Assert.LessOrEqual( 3.5d, 8UL, "double to ulong" );
			Assert.LessOrEqual( 3.5d, 8.2M, "double to decimal" );
			

			Assert.LessOrEqual( 5U, 8, "uint to int" );
			Assert.LessOrEqual( 5U, 8L, "uint to long" );
			Assert.LessOrEqual( 5U, 8.2f, "uint to float" );
			Assert.LessOrEqual( 5U, 8.2d, "uint to double" );
			Assert.LessOrEqual( 5U, 8UL, "uint to ulong" );
			Assert.LessOrEqual( 5U, 8M, "uint to decimal" );
			
			Assert.LessOrEqual( 5ul, 8, "ulong to int" );
			Assert.LessOrEqual( 5UL, 8L, "ulong to long" );
			Assert.LessOrEqual( 5UL, 8.2f, "ulong to float" );
			Assert.LessOrEqual( 5UL, 8.2d, "ulong to double" );
			Assert.LessOrEqual( 5UL, 8U, "ulong to uint" );
			Assert.LessOrEqual( 5UL, 8M, "ulong to decimal" );
			
			Assert.LessOrEqual( 5M, 8, "decimal to int" );
			Assert.LessOrEqual( 5M, 8L, "decimal to long" );
			Assert.LessOrEqual( 5M, 8.2f, "decimal to float" );
			Assert.LessOrEqual( 5M, 8.2d, "decimal to double" );
			Assert.LessOrEqual( 5M, 8U, "decimal to uint" );
			Assert.LessOrEqual( 5M, 8UL, "decimal to ulong" );
		}

		[Test, ExpectedException(typeof(AssertionException))]
        public void NotLessOrEqual()
        {
			expectedMessage =
				"  Expected: less than or equal to 5" + Environment.NewLine +
				"  But was:  8" + Environment.NewLine;
			Assert.LessOrEqual(i2, i1);
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void NotLessEqualIComparable()
        {
			expectedMessage =
				"  Expected: less than or equal to StoredProcedure" + Environment.NewLine +
				"  But was:  TableDirect" + Environment.NewLine;
			Assert.LessOrEqual(e2, e1);
        }

        [Test,ExpectedException(typeof(AssertionException))]
        public void FailureMessage()
        {
			expectedMessage =
				"  Expected: less than or equal to 4" + Environment.NewLine +
				"  But was:  9" + Environment.NewLine;
            Assert.LessOrEqual(9, 4);
        }
    }
}


