// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	/// <summary>
	/// AssertionHelper is an optional base class for user tests,
	/// allowing the use of shorter names for constraints and
	/// asserts and avoiding conflict with the definition of 
	/// <see cref="Is"/>, from which it inherits much of its
	/// behavior, in certain mock object frameworks.
	/// </summary>
	public class AssertionHelper : ConstraintFactory
    {
        #region Assert
        //private Assertions assert = new Assertions();
        //public virtual Assertions Assert
        //{
        //    get { return assert; }
        //}
        #endregion

        #region Expect

        #region Object
        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure. Works
        /// identically to Assert.That
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        public void Expect(object actual, IResolveConstraint constraint)
        {
            Assert.That(actual, constraint, null, null);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure. Works
        /// identically to Assert.That.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public void Expect(object actual, IResolveConstraint constraint, string message)
        {
            Assert.That(actual, constraint, message, null);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure. Works
        /// identically to Assert.That
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public void Expect(object actual, IResolveConstraint constraint, string message, params object[] args)
        {
            Assert.That(actual, constraint, message, args);
        }
        #endregion

        #region ActualValueDelegate
        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        public void Expect(ActualValueDelegate del, IResolveConstraint expr)
        {
            Assert.That(del, expr.Resolve(), null, null);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public void Expect(ActualValueDelegate del, IResolveConstraint expr, string message)
        {
            Assert.That(del, expr.Resolve(), message, null);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public void Expect(ActualValueDelegate del, IResolveConstraint expr, string message, params object[] args)
        {
            Assert.That(del, expr, message, args);
        }
        #endregion

        #region ref Object
#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// Apply a constraint to a referenced value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        public void Expect<T>(ref T actual, IResolveConstraint constraint)
        {
            Assert.That(ref actual, constraint.Resolve(), null, null);
        }

        /// <summary>
        /// Apply a constraint to a referenced value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public void Expect<T>(ref T actual, IResolveConstraint constraint, string message)
        {
            Assert.That(ref actual, constraint.Resolve(), message, null);
        }

        /// <summary>
        /// Apply a constraint to a referenced value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="expression">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public void Expect<T>(ref T actual, IResolveConstraint expression, string message, params object[] args)
        {
            Assert.That(ref actual, expression, message, args);
        }
#else
        /// <summary>
        /// Apply a constraint to a referenced boolean, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        public void Expect(ref bool actual, IResolveConstraint constraint)
        {
            Assert.That(ref actual, constraint.Resolve(), null, null);
        }

        /// <summary>
        /// Apply a constraint to a referenced value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public void Expect(ref bool actual, IResolveConstraint constraint, string message)
        {
            Assert.That(ref actual, constraint.Resolve(), message, null);
        }

        /// <summary>
        /// Apply a constraint to a referenced value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The actual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public void Expect(ref bool actual, IResolveConstraint expression, string message, params object[] args)
        {
            Assert.That( ref actual, expression, message, args );
        }
#endif
        #endregion

        #region Boolean
        /// <summary>
		/// Asserts that a condition is true. If the condition is false the method throws
		/// an <see cref="AssertionException"/>. Works Identically to Assert.That.
		/// </summary> 
		/// <param name="condition">The evaluated condition</param>
		/// <param name="message">The message to display if the condition is false</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		public void Expect(bool condition, string message, params object[] args)
		{
			Assert.That(condition, Is.True, message, args);
		}

		/// <summary>
		/// Asserts that a condition is true. If the condition is false the method throws
		/// an <see cref="AssertionException"/>. Works Identically to Assert.That.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		/// <param name="message">The message to display if the condition is false</param>
		public void Expect(bool condition, string message)
		{
			Assert.That(condition, Is.True, message, null);
		}

		/// <summary>
		/// Asserts that a condition is true. If the condition is false the method throws
		/// an <see cref="AssertionException"/>. Works Identically Assert.That.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		public void Expect(bool condition)
		{
			Assert.That(condition, Is.True, null, null);
        }
        #endregion

        /// <summary>
        /// Asserts that the code represented by a delegate throws an exception
        /// that satisfies the constraint provided.
        /// </summary>
        /// <param name="code">A TestDelegate to be executed</param>
        /// <param name="constraint">A ThrowsConstraint used in the test</param>
        public void Expect(TestDelegate code, IResolveConstraint constraint)
        {
            Assert.That((object)code, constraint);
        }

        #endregion

        #region Map
        /// <summary>
		/// Returns a ListMapper based on a collection.
		/// </summary>
		/// <param name="original">The original collection</param>
		/// <returns></returns>
		public ListMapper Map( ICollection original )
		{
			return new ListMapper( original );
		}
		#endregion
	}
}
