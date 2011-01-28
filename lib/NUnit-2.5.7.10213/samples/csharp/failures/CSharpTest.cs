// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

namespace NUnit.Samples 
{
	using System;
	using NUnit.Framework;

	/// <summary>Some simple Tests.</summary>
	/// 
	[TestFixture] 
	public class SimpleCSharpTest
	{
		/// <summary>
		/// 
		/// </summary>
		protected int fValue1;
		/// <summary>
		/// 
		/// </summary>
		protected int fValue2;
		
		/// <summary>
		/// 
		/// </summary>
		[SetUp] public void Init() 
		{
			fValue1= 2;
			fValue2= 3;
		}

		/// <summary>
		/// 
		/// </summary>
		///
		[Test] public void Add() 
		{
			double result= fValue1 + fValue2;
			// forced failure result == 5
			Assert.AreEqual(6, result, "Expected Failure.");
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test] public void DivideByZero() 
		{
			int zero= 0;
			int result= 8/zero;
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test] public void Equals() 
		{
			Assert.AreEqual(12, 12, "Integer");
			Assert.AreEqual(12L, 12L, "Long");
			Assert.AreEqual('a', 'a', "Char");
			Assert.AreEqual((object)12, (object)12, "Integer Object Cast");
            
			Assert.AreEqual(12, 13, "Expected Failure (Integer)");
			Assert.AreEqual(12.0, 11.99, 0.0, "Expected Failure (Double).");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ExpectAnException()
		{
			throw new InvalidCastException();
		}

		[Test]
		[Ignore("ignored test")]
		public void IgnoredTest()
		{
			throw new Exception();
		}
	}
}