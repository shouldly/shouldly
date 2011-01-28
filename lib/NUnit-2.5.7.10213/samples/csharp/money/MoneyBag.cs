// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

namespace NUnit.Samples.Money 
{

	using System;
	using System.Collections;
	using System.Text;

	/// <summary>A MoneyBag defers exchange rate conversions.</summary>
	/// <remarks>For example adding 
	/// 12 Swiss Francs to 14 US Dollars is represented as a bag 
	/// containing the two Monies 12 CHF and 14 USD. Adding another
	/// 10 Swiss francs gives a bag with 22 CHF and 14 USD. Due to 
	/// the deferred exchange rate conversion we can later value a 
	/// MoneyBag with different exchange rates.
	///
	/// A MoneyBag is represented as a list of Monies and provides 
	/// different constructors to create a MoneyBag.</remarks>
	class MoneyBag: IMoney 
	{
		private ArrayList fMonies= new ArrayList(5);

		private MoneyBag() 
		{
		}
		public MoneyBag(Money[] bag) 
		{
			for (int i= 0; i < bag.Length; i++) 
			{
				if (!bag[i].IsZero)
					AppendMoney(bag[i]);
			}
		}
		public MoneyBag(Money m1, Money m2) 
		{
			AppendMoney(m1);
			AppendMoney(m2);
		}
		public MoneyBag(Money m, MoneyBag bag) 
		{
			AppendMoney(m);
			AppendBag(bag);
		}
		public MoneyBag(MoneyBag m1, MoneyBag m2) 
		{
			AppendBag(m1);
			AppendBag(m2);
		}
		public IMoney Add(IMoney m) 
		{
			return m.AddMoneyBag(this);
		}
		public IMoney AddMoney(Money m) 
		{
			return (new MoneyBag(m, this)).Simplify();
		}
		public IMoney AddMoneyBag(MoneyBag s) 
		{
			return (new MoneyBag(s, this)).Simplify();
		}
		private void AppendBag(MoneyBag aBag) 
		{
			foreach (Money m in aBag.fMonies)
				AppendMoney(m);
		}
		private void AppendMoney(Money aMoney) 
		{
			IMoney old= FindMoney(aMoney.Currency);
			if (old == null) 
			{
				fMonies.Add(aMoney);
				return;
			}
			fMonies.Remove(old);
			IMoney sum= old.Add(aMoney);
			if (sum.IsZero) 
				return;
			fMonies.Add(sum);
		}
		private bool Contains(Money aMoney) 
		{
			Money m= FindMoney(aMoney.Currency);
			return m.Amount == aMoney.Amount;
		}
		public override bool Equals(Object anObject) 
		{
			if (IsZero)
				if (anObject is IMoney)
					return ((IMoney)anObject).IsZero;
            
			if (anObject is MoneyBag) 
			{
				MoneyBag aMoneyBag= (MoneyBag)anObject;
				if (aMoneyBag.fMonies.Count != fMonies.Count)
					return false;
                
				foreach (Money m in fMonies) 
				{
					if (!aMoneyBag.Contains(m))
						return false;
				}
				return true;
			}
			return false;
		}
		private Money FindMoney(String currency) 
		{
			foreach (Money m in fMonies) 
			{
				if (m.Currency.Equals(currency))
					return m;
			}
			return null;
		}
		public override int GetHashCode() 
		{
			int hash= 0;
			foreach (Money m in fMonies) 
			{
				hash^= m.GetHashCode();
			}
			return hash;
		}
		public bool IsZero 
		{
			get { return fMonies.Count == 0; }
		}
		public IMoney Multiply(int factor) 
		{
			MoneyBag result= new MoneyBag();
			if (factor != 0) 
			{
				foreach (Money m in fMonies) 
				{
					result.AppendMoney((Money)m.Multiply(factor));
				}
			}
			return result;
		}
		public IMoney Negate() 
		{
			MoneyBag result= new MoneyBag();
			foreach (Money m in fMonies) 
			{
				result.AppendMoney((Money)m.Negate());
			}
			return result;
		}
		private IMoney Simplify() 
		{
			if (fMonies.Count == 1)
				return (IMoney)fMonies[0];
			return this;
		}
		public IMoney Subtract(IMoney m) 
		{
			return Add(m.Negate());
		}
		public override String ToString() 
		{
			StringBuilder buffer = new StringBuilder();
			buffer.Append("{");
			foreach (Money m in fMonies)
				buffer.Append(m);
			buffer.Append("}");
			return buffer.ToString();
		}
	}
}
