// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class NotEqualFixture : MessageChecker
	{
		[Test]
		public void NotEqual()
		{
			Assert.AreNotEqual( 5, 3 );
		}

		[Test, ExpectedException( typeof( AssertionException ) )]
		public void NotEqualFails()
		{
			expectedMessage =
				"  Expected: not 5" + Environment.NewLine +
				"  But was:  5" + Environment.NewLine;
			Assert.AreNotEqual( 5, 5 );
		}

		[Test]
		public void NullNotEqualToNonNull()
		{
			Assert.AreNotEqual( null, 3 );
		}

		[Test, ExpectedException( typeof( AssertionException ) )]
		public void NullEqualsNull()
		{
			expectedMessage =
				"  Expected: not null" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
			Assert.AreNotEqual( null, null );
		}

		[Test]
		public void ArraysNotEqual()
		{
			Assert.AreNotEqual( new object[] { 1, 2, 3 }, new object[] { 1, 3, 2 } );
		}

		[Test, ExpectedException( typeof( AssertionException ) )]
		public void ArraysNotEqualFails()
		{
			expectedMessage =
				"  Expected: not < 1, 2, 3 >" + Environment.NewLine +
				"  But was:  < 1, 2, 3 >" + Environment.NewLine;
			Assert.AreNotEqual( new object[] { 1, 2, 3 }, new object[] { 1, 2, 3 } );
		}

		[Test]
		public void UInt()
		{
			uint u1 = 5;
			uint u2 = 8;
			Assert.AreNotEqual( u1, u2 );
		}

        [Test]
        public void NotEqualSameTypes()
        {
            byte b1 = 35;
            sbyte sb2 = 35;
            decimal d4 = 35;
            double d5 = 35;
            float f6 = 35;
            int i7 = 35;
            uint u8 = 35;
            long l9 = 35;
            short s10 = 35;
            ushort us11 = 35;

            System.Byte b12 = 35;
            System.SByte sb13 = 35;
            System.Decimal d14 = 35;
            System.Double d15 = 35;
            System.Single s16 = 35;
            System.Int32 i17 = 35;
            System.UInt32 ui18 = 35;
            System.Int64 i19 = 35;
            System.UInt64 ui20 = 35;
            System.Int16 i21 = 35;
            System.UInt16 i22 = 35;

            Assert.AreNotEqual(23, b1);
            Assert.AreNotEqual(23, sb2);
            Assert.AreNotEqual(23, d4);
            Assert.AreNotEqual(23, d5);
            Assert.AreNotEqual(23, f6);
            Assert.AreNotEqual(23, i7);
            Assert.AreNotEqual(23, u8);
            Assert.AreNotEqual(23, l9);
            Assert.AreNotEqual(23, s10);
            Assert.AreNotEqual(23, us11);

            Assert.AreNotEqual(23, b12);
            Assert.AreNotEqual(23, sb13);
            Assert.AreNotEqual(23, d14);
            Assert.AreNotEqual(23, d15);
            Assert.AreNotEqual(23, s16);
            Assert.AreNotEqual(23, i17);
            Assert.AreNotEqual(23, ui18);
            Assert.AreNotEqual(23, i19);
            Assert.AreNotEqual(23, ui20);
            Assert.AreNotEqual(23, i21);
            Assert.AreNotEqual(23, i22);
        }
   }
}