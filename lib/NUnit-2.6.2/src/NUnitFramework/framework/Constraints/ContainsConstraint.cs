// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
	// TODO Needs tests
	/// <summary>
	/// ContainsConstraint tests a whether a string contains a substring
	/// or a collection contains an object. It postpones the decision of
	/// which test to use until the type of the actual argument is known.
	/// This allows testing whether a string is contained in a collection
	/// or as a substring of another string using the same syntax.
	/// </summary>
	public class ContainsConstraint : Constraint
	{
		object expected;
		Constraint realConstraint;
        bool ignoreCase;

        private EqualityAdapter adapter = null;

		private Constraint RealConstraint
		{
			get 
			{
				if ( realConstraint == null )
				{
                    if (actual is string)
                    {
                        StringConstraint constraint = new SubstringConstraint((string)expected);
                        if (this.ignoreCase)
                            constraint = constraint.IgnoreCase;
                        this.realConstraint = constraint;
                    }
                    else
					{
                        CollectionContainsConstraint constraint = new CollectionContainsConstraint(expected);
						
						if (this.adapter != null)
							constraint.comparer.ExternalComparers.Add(adapter);
							
						this.realConstraint = constraint;
					}
				}
				
				return realConstraint;
			}
			set 
			{ 
				realConstraint = value; 
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContainsConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
		public ContainsConstraint( object expected )
			: base(expected)
		{
			this.expected = expected;
		}

        /// <summary>
        /// Flag the constraint to ignore case and return self.
        /// </summary>
        public ContainsConstraint IgnoreCase
        {
            get { this.ignoreCase = true; return this; }
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
		public override bool Matches(object actual)
		{
            this.actual = actual;
			return this.RealConstraint.Matches( actual );
		}

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			this.RealConstraint.WriteDescriptionTo(writer);
		}

        /// <summary>
        /// Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using(IComparer comparer)
        {
            this.adapter = EqualityAdapter.For(comparer);
            return this;
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using<T>(IComparer<T> comparer)
        {
            this.adapter = EqualityAdapter.For(comparer);
            return this;
        }

        /// <summary>
        /// Flag the constraint to use the supplied Comparison object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using<T>(Comparison<T> comparer)
        {
            this.adapter = EqualityAdapter.For(comparer);
            return this;
        }

        /// <summary>
        /// Flag the constraint to use the supplied IEqualityComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using(IEqualityComparer comparer)
        {
            this.adapter = EqualityAdapter.For(comparer);
            return this;
        }

        /// <summary>
        /// Flag the constraint to use the supplied IEqualityComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using<T>(IEqualityComparer<T> comparer)
        {
            this.adapter = EqualityAdapter.For(comparer);
            return this;
        }
#endif
	}
}
