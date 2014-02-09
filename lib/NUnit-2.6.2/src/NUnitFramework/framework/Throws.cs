// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
    /// <summary>
    /// Helper class with properties and methods that supply
    /// constraints that operate on exceptions.
    /// </summary>
    public class Throws
    {
        #region Exception

        /// <summary>
        /// Creates a constraint specifying an expected exception
        /// </summary>
        public static ResolvableConstraintExpression Exception
        {
            get { return new ConstraintExpression().Append(new ThrowsOperator()); }
        }

        #endregion

        #region InnerException

        /// <summary>
        /// Creates a constraint specifying an exception with a given InnerException
        /// </summary>
        public static ResolvableConstraintExpression InnerException
        {
            get { return Exception.InnerException; }
        }

        #endregion

        #region TargetInvocationException

        /// <summary>
        /// Creates a constraint specifying an expected TargetInvocationException
        /// </summary>
        public static ExactTypeConstraint TargetInvocationException
        {
            get { return TypeOf(typeof(System.Reflection.TargetInvocationException)); }
        }

        #endregion

        #region ArgumentException

        /// <summary>
        /// Creates a constraint specifying an expected TargetInvocationException
        /// </summary>
        public static ExactTypeConstraint ArgumentException
        {
            get { return TypeOf(typeof(System.ArgumentException)); }
        }

        #endregion

        #region InvalidOperationException

        /// <summary>
        /// Creates a constraint specifying an expected TargetInvocationException
        /// </summary>
        public static ExactTypeConstraint InvalidOperationException
        {
            get { return TypeOf(typeof(System.InvalidOperationException)); }
        }

        #endregion

        #region Nothing

        /// <summary>
        /// Creates a constraint specifying that no exception is thrown
        /// </summary>
        public static ThrowsNothingConstraint Nothing
        {
            get { return new ThrowsNothingConstraint(); }
        }

        #endregion

        #region TypeOf

        /// <summary>
        /// Creates a constraint specifying the exact type of exception expected
        /// </summary>
        public static ExactTypeConstraint TypeOf(Type expectedType)
        {
            return Exception.TypeOf(expectedType);
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// Creates a constraint specifying the exact type of exception expected
        /// </summary>
        public static ExactTypeConstraint TypeOf<T>()
        {
            return TypeOf(typeof(T));
        }
#endif

        #endregion

        #region InstanceOf

        /// <summary>
        /// Creates a constraint specifying the type of exception expected
        /// </summary>
        public static InstanceOfTypeConstraint InstanceOf(Type expectedType)
        {
            return Exception.InstanceOf(expectedType);
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// Creates a constraint specifying the type of exception expected
        /// </summary>
        public static InstanceOfTypeConstraint InstanceOf<T>()
        {
            return InstanceOf(typeof(T));
        }
#endif

        #endregion

    }
}
