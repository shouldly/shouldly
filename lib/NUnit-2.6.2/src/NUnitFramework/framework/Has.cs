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
    /// a number of constraints used in Asserts.
    /// </summary>
    public class Has
    {
        #region No

        /// <summary>
        /// Returns a ConstraintExpression that negates any
        /// following constraint.
        /// </summary>
        public static ConstraintExpression No
        {
            get { return new ConstraintExpression().Not; }
        }

        #endregion

        #region All

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding if all of them succeed.
        /// </summary>
        public static ConstraintExpression All
        {
            get { return new ConstraintExpression().All; }
        }

        #endregion

        #region Some

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding if at least one of them succeeds.
        /// </summary>
        public static ConstraintExpression Some
        {
            get { return new ConstraintExpression().Some; }
        }

        #endregion

        #region None

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding if all of them fail.
        /// </summary>
        public static ConstraintExpression None
        {
            get { return new ConstraintExpression().None; }
        }

        #endregion

        #region Exactly(n)

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding only if a specified number of them succeed.
        /// </summary>
        public static ConstraintExpression Exactly(int expectedCount)
        {
            return new ConstraintExpression().Exactly(expectedCount);
        }

        #endregion

        #region Property

        /// <summary>
        /// Returns a new PropertyConstraintExpression, which will either
        /// test for the existence of the named property on the object
        /// being tested or apply any following constraint to that property.
        /// </summary>
        public static ResolvableConstraintExpression Property(string name)
        {
            return new ConstraintExpression().Property(name);
        }

        #endregion

        #region Length

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the Length property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression Length
        {
            get { return Property("Length"); }
        }

        #endregion

        #region Count

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the Count property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression Count
        {
            get { return Property("Count"); }
        }

        #endregion

        #region Message

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the Message property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression Message
        {
            get { return Property("Message"); }
        }

        #endregion

        #region InnerException

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the InnerException property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression InnerException
        {
            get { return Property("InnerException"); }
        }

        #endregion

        #region Attribute

        /// <summary>
        /// Returns a new AttributeConstraint checking for the
        /// presence of a particular attribute on an object.
        /// </summary>
        public static ResolvableConstraintExpression Attribute(Type expectedType)
        {
            return new ConstraintExpression().Attribute(expectedType);
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// Returns a new AttributeConstraint checking for the
        /// presence of a particular attribute on an object.
        /// </summary>
        public static ResolvableConstraintExpression Attribute<T>()
        {
            return Attribute(typeof(T));
        }
#endif

        #endregion

        #region Member

        /// <summary>
        /// Returns a new CollectionContainsConstraint checking for the
        /// presence of a particular object in the collection.
        /// </summary>
        public static CollectionContainsConstraint Member(object expected)
        {
            return new CollectionContainsConstraint(expected);
        }

        #endregion

    }
}
