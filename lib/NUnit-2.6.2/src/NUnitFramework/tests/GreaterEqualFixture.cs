// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework.Tests
{
    [TestFixture]
    public class GreaterEqualFixture : MessageChecker
    {
        private readonly int i1 = 5;
        private readonly int i2 = 4;
		private readonly uint u1 = 12345879;
		private readonly uint u2 = 12345678;
		private readonly long l1 = 12345879;
		private readonly long l2 = 12345678;
		private readonly ulong ul1 = 12345879;
		private readonly ulong ul2 = 12345678;
		private readonly float f1 = 3.543F;
        private readonly float f2 = 2.543F;
        private readonly decimal de1 = 53.4M;
        private readonly decimal de2 = 33.4M;
        private readonly double d1 = 4.85948654;
        private readonly double d2 = 1.0;
        private readonly System.Enum e1 = System.Data.CommandType.TableDirect;
        private readonly System.Enum e2 = System.Data.CommandType.StoredProcedure;

		[Test]
		public void GreaterOrEqual_Int32()
		{
			Assert.GreaterOrEqual(i1, i1);           
			Assert.GreaterOrEqual(i1, i2);
		}

		[Test]
		public void GreaterOrEqual_UInt32()
		{
			Assert.GreaterOrEqual(u1, u1);
			Assert.GreaterOrEqual(u1, u2);
		}

		[Test]
		public void GreaterOrEqual_Long()
		{
			Assert.GreaterOrEqual(l1, l1);
			Assert.GreaterOrEqual(l1, l2);
		}

		[Test]
		public void GreaterOrEqual_ULong()
		{
			Assert.GreaterOrEqual(ul1, ul1);
			Assert.GreaterOrEqual(ul1, ul2);
		}

		[Test]
		public void GreaterOrEqual_Double()
		{
			Assert.GreaterOrEqual(d1, d1, "double");
			Assert.GreaterOrEqual(d1, d2, "double");
		}

		[Test]
		public void GreaterOrEqual_Decimal()
		{
			Assert.GreaterOrEqual(de1, de1, "{0}", "decimal");
			Assert.GreaterOrEqual(de1, de2, "{0}", "decimal");
		}

		[Test]
		public void GreaterOrEqual_Float()
		{
			Assert.GreaterOrEqual(f1, f1, "float");
			Assert.GreaterOrEqual(f1, f2, "float");
		}

		[Test]
		public void MixedTypes()
		{	
			Assert.GreaterOrEqual( 5, 3L, "int to long");
			Assert.GreaterOrEqual( 5, 3.5f, "int to float" );
			Assert.GreaterOrEqual( 5, 3.5d, "int to double" );
			Assert.GreaterOrEqual( 5, 3U, "int to uint" );
			Assert.GreaterOrEqual( 5, 3UL, "int to ulong" );
			Assert.GreaterOrEqual( 5, 3M, "int to decimal" );

			Assert.GreaterOrEqual( 5L, 3, "long to int");
			Assert.GreaterOrEqual( 5L, 3.5f, "long to float" );
			Assert.GreaterOrEqual( 5L, 3.5d, "long to double" );
			Assert.GreaterOrEqual( 5L, 3U, "long to uint" );
			Assert.GreaterOrEqual( 5L, 3UL, "long to ulong" );
			Assert.GreaterOrEqual( 5L, 3M, "long to decimal" );

			Assert.GreaterOrEqual( 8.2f, 5, "float to int" );
			Assert.GreaterOrEqual( 8.2f, 8L, "float to long" );
			Assert.GreaterOrEqual( 8.2f, 3.5d, "float to double" );
			Assert.GreaterOrEqual( 8.2f, 8U, "float to uint" );
			Assert.GreaterOrEqual( 8.2f, 8UL, "float to ulong" );
			Assert.GreaterOrEqual( 8.2f, 3.5M, "float to decimal" );

			Assert.GreaterOrEqual( 8.2d, 5, "double to int" );
			Assert.GreaterOrEqual( 8.2d, 5L, "double to long" );
			Assert.GreaterOrEqual( 8.2d, 3.5f, "double to float" );
			Assert.GreaterOrEqual( 8.2d, 8U, "double to uint" );
			Assert.GreaterOrEqual( 8.2d, 8UL, "double to ulong" );
			Assert.GreaterOrEqual( 8.2d, 3.5M, "double to decimal" );
			

			Assert.GreaterOrEqual( 5U, 3, "uint to int" );
			Assert.GreaterOrEqual( 5U, 3L, "uint to long" );
			Assert.GreaterOrEqual( 5U, 3.5f, "uint to float" );
			Assert.GreaterOrEqual( 5U, 3.5d, "uint to double" );
			Assert.GreaterOrEqual( 5U, 3UL, "uint to ulong" );
			Assert.GreaterOrEqual( 5U, 3M, "uint to decimal" );
			
			Assert.GreaterOrEqual( 5ul, 3, "ulong to int" );
			Assert.GreaterOrEqual( 5UL, 3L, "ulong to long" );
			Assert.GreaterOrEqual( 5UL, 3.5f, "ulong to float" );
			Assert.GreaterOrEqual( 5UL, 3.5d, "ulong to double" );
			Assert.GreaterOrEqual( 5UL, 3U, "ulong to uint" );
			Assert.GreaterOrEqual( 5UL, 3M, "ulong to decimal" );
			
			Assert.GreaterOrEqual( 5M, 3, "decimal to int" );
			Assert.GreaterOrEqual( 5M, 3L, "decimal to long" );
			Assert.GreaterOrEqual( 5M, 3.5f, "decimal to float" );
			Assert.GreaterOrEqual( 5M, 3.5d, "decimal to double" );
			Assert.GreaterOrEqual( 5M, 3U, "decimal to uint" );
			Assert.GreaterOrEqual( 5M, 3UL, "decimal to ulong" );
		}

		[Test, ExpectedException(typeof(AssertionException))]
        public void NotGreaterOrEqual()
        {
			expectedMessage =
				"  Expected: greater than or equal to 5" + Environment.NewLine +
				"  But was:  4" + Environment.NewLine;
			Assert.GreaterOrEqual(i2, i1);
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void NotGreaterEqualIComparable()
        {
			expectedMessage =
				"  Expected: greater than or equal to TableDirect" + Environment.NewLine +
				"  But was:  StoredProcedure" + Environment.NewLine;
			Assert.GreaterOrEqual(e2, e1);
        }

        [Test]
        public void FailureMessage()
        {
            string msg = null;

            try
            {
                Assert.GreaterOrEqual(7, 99);
            }
            catch (AssertionException ex)
            {
                msg = ex.Message;
            }

            StringAssert.Contains( TextMessageWriter.Pfx_Expected + "greater than or equal to 99", msg);
            StringAssert.Contains( TextMessageWriter.Pfx_Actual + "7", msg);
		}
	}
}
