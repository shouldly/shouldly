// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

namespace NUnit.Samples.Money 
{
	using System;
	using NUnit.Framework;
	/// <summary>
	/// 
	/// </summary>
	/// 
	[TestFixture]
	public class MoneyTest 
	{
		private Money f12CHF;
		private Money f14CHF;
		private Money f7USD;
		private Money f21USD;
        
		private MoneyBag fMB1;
		private MoneyBag fMB2;

		/// <summary>
		/// 
		/// </summary>
		/// 
		[SetUp]
		protected void SetUp() 
		{
			f12CHF= new Money(12, "CHF");
			f14CHF= new Money(14, "CHF");
			f7USD= new Money( 7, "USD");
			f21USD= new Money(21, "USD");

			fMB1= new MoneyBag(f12CHF, f7USD);
			fMB2= new MoneyBag(f14CHF, f21USD);
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void BagMultiply() 
		{
			// {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
			Money[] bag = { new Money(24, "CHF"), new Money(14, "USD") };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, fMB1.Multiply(2));
			Assert.AreEqual(fMB1, fMB1.Multiply(1));
			Assert.IsTrue(fMB1.Multiply(0).IsZero);
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void BagNegate() 
		{
			// {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
			Money[] bag= { new Money(-12, "CHF"), new Money(-7, "USD") };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, fMB1.Negate());
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void BagSimpleAdd() 
		{
			// {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
			Money[] bag= { new Money(26, "CHF"), new Money(7, "USD") };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, fMB1.Add(f14CHF));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void BagSubtract() 
		{
			// {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
			Money[] bag= { new Money(-2, "CHF"), new Money(-14, "USD") };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, fMB1.Subtract(fMB2));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void BagSumAdd() 
		{
			// {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
			Money[] bag= { new Money(26, "CHF"), new Money(28, "USD") };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, fMB1.Add(fMB2));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void IsZero() 
		{
			Assert.IsTrue(fMB1.Subtract(fMB1).IsZero);

			Money[] bag = { new Money(0, "CHF"), new Money(0, "USD") };
			Assert.IsTrue(new MoneyBag(bag).IsZero);
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void MixedSimpleAdd() 
		{
			// [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
			Money[] bag= { f12CHF, f7USD };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, f12CHF.Add(f7USD));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void MoneyBagEquals() 
		{
			//NOTE: Normally we use Assert.AreEqual to test whether two
			// objects are equal. But here we are testing the MoneyBag.Equals()
			// method itself, so using AreEqual would not serve the purpose.
			Assert.IsFalse(fMB1.Equals(null)); 

			Assert.IsTrue(fMB1.Equals( fMB1 ));
			MoneyBag equal= new MoneyBag(new Money(12, "CHF"), new Money(7, "USD"));
			Assert.IsTrue(fMB1.Equals(equal));
			Assert.IsTrue(!fMB1.Equals(f12CHF));
			Assert.IsTrue(!f12CHF.Equals(fMB1));
			Assert.IsTrue(!fMB1.Equals(fMB2));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void MoneyBagHash() 
		{
			MoneyBag equal= new MoneyBag(new Money(12, "CHF"), new Money(7, "USD"));
			Assert.AreEqual(fMB1.GetHashCode(), equal.GetHashCode());
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void MoneyEquals() 
		{
			//NOTE: Normally we use Assert.AreEqual to test whether two
			// objects are equal. But here we are testing the MoneyBag.Equals()
			// method itself, so using AreEqual would not serve the purpose.
			Assert.IsFalse(f12CHF.Equals(null)); 
			Money equalMoney= new Money(12, "CHF");
			Assert.IsTrue(f12CHF.Equals( f12CHF ));
			Assert.IsTrue(f12CHF.Equals( equalMoney ));
			Assert.IsFalse(f12CHF.Equals(f14CHF));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void MoneyHash() 
		{
			Assert.IsFalse(f12CHF.Equals(null)); 
			Money equal= new Money(12, "CHF");
			Assert.AreEqual(f12CHF.GetHashCode(), equal.GetHashCode());
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void Normalize() 
		{
			Money[] bag= { new Money(26, "CHF"), new Money(28, "CHF"), new Money(6, "CHF") };
			MoneyBag moneyBag= new MoneyBag(bag);
			Money[] expected = { new Money(60, "CHF") };
			// note: expected is still a MoneyBag
			MoneyBag expectedBag= new MoneyBag(expected);
			Assert.AreEqual(expectedBag, moneyBag);
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void Normalize2() 
		{
			// {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
			Money expected= new Money(7, "USD");
			Assert.AreEqual(expected, fMB1.Subtract(f12CHF));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void Normalize3() 
		{
			// {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
			Money[] s1 = { new Money(12, "CHF"), new Money(3, "USD") };
			MoneyBag ms1= new MoneyBag(s1);
			Money expected= new Money(4, "USD");
			Assert.AreEqual(expected, fMB1.Subtract(ms1));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void Normalize4() 
		{
			// [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
			Money[] s1 = { new Money(12, "CHF"), new Money(3, "USD") };
			MoneyBag ms1= new MoneyBag(s1);
			Money expected= new Money(-3, "USD");
			Assert.AreEqual(expected, f12CHF.Subtract(ms1));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void Print() 
		{
			Assert.AreEqual("[12 CHF]", f12CHF.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void SimpleAdd() 
		{
			// [12 CHF] + [14 CHF] == [26 CHF]
			Money expected= new Money(26, "CHF");
			Assert.AreEqual(expected, f12CHF.Add(f14CHF));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void SimpleBagAdd() 
		{
			// [14 CHF] + {[12 CHF][7 USD]} == {[26 CHF][7 USD]}
			Money[] bag= { new Money(26, "CHF"), new Money(7, "USD") };
			MoneyBag expected= new MoneyBag(bag);
			Assert.AreEqual(expected, f14CHF.Add(fMB1));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void SimpleMultiply() 
		{
			// [14 CHF] *2 == [28 CHF]
			Money expected= new Money(28, "CHF");
			Assert.AreEqual(expected, f14CHF.Multiply(2));
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void SimpleNegate() 
		{
			// [14 CHF] negate == [-14 CHF]
			Money expected= new Money(-14, "CHF");
			Assert.AreEqual(expected, f14CHF.Negate());
		}

		/// <summary>
		/// 
		/// </summary>
		/// 
		[Test]
		public void SimpleSubtract() 
		{
			// [14 CHF] - [12 CHF] == [2 CHF]
			Money expected= new Money(2, "CHF");
			Assert.AreEqual(expected, f14CHF.Subtract(f12CHF));
		}
	}
}