// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

namespace NUnit.Samples.Money 
{

	using System;
	using System.Text;

	/// <summary>A simple Money.</summary>
	class Money: IMoney 
	{

		private int fAmount;
		private String fCurrency;
        
		/// <summary>Constructs a money from the given amount and
		/// currency.</summary>
		public Money(int amount, String currency) 
		{
			fAmount= amount;
			fCurrency= currency;
		}

		/// <summary>Adds a money to this money. Forwards the request to
		/// the AddMoney helper.</summary>
		public IMoney Add(IMoney m) 
		{
			return m.AddMoney(this);
		}

		public IMoney AddMoney(Money m) 
		{
			if (m.Currency.Equals(Currency) )
				return new Money(Amount+m.Amount, Currency);
			return new MoneyBag(this, m);
		}

		public IMoney AddMoneyBag(MoneyBag s) 
		{
			return s.AddMoney(this);
		}

		public int Amount 
		{
			get { return fAmount; }
		}

		public String Currency 
		{
			get { return fCurrency; }
		}

		public override bool Equals(Object anObject) 
		{
			if (IsZero)
				if (anObject is IMoney)
					return ((IMoney)anObject).IsZero;
			if (anObject is Money) 
			{
				Money aMoney= (Money)anObject;
				return aMoney.Currency.Equals(Currency)
					&& Amount == aMoney.Amount;
			}
			return false;
		}

		public override int GetHashCode() 
		{
			return fCurrency.GetHashCode()+fAmount;
		}

		public bool IsZero 
		{
			get { return Amount == 0; }
		}

		public IMoney Multiply(int factor) 
		{
			return new Money(Amount*factor, Currency);
		}

		public IMoney Negate() 
		{
			return new Money(-Amount, Currency);
		}

		public IMoney Subtract(IMoney m) 
		{
			return Add(m.Negate());
		}

		public override String ToString() 
		{
			StringBuilder buffer = new StringBuilder();
			buffer.Append("["+Amount+" "+Currency+"]");
			return buffer.ToString();
		}
	}
}
