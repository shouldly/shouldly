// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

namespace NUnit.Samples.Money 
{

	/// <summary>The common interface for simple Monies and MoneyBags.</summary>
	interface IMoney 
	{

		/// <summary>Adds a money to this money.</summary>
		IMoney Add(IMoney m);

		/// <summary>Adds a simple Money to this money. This is a helper method for
		/// implementing double dispatch.</summary>
		IMoney AddMoney(Money m);

		/// <summary>Adds a MoneyBag to this money. This is a helper method for
		/// implementing double dispatch.</summary>
		IMoney AddMoneyBag(MoneyBag s);

		/// <value>True if this money is zero.</value>
		bool IsZero { get; }

		/// <summary>Multiplies a money by the given factor.</summary>
		IMoney Multiply(int factor);

		/// <summary>Negates this money.</summary>
		IMoney Negate();

		/// <summary>Subtracts a money from this money.</summary>
		IMoney Subtract(IMoney m);
	}
}
