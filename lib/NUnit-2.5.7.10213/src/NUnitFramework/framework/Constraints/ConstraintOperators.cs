// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
    #region ConstraintOperator Base Class
    /// <summary>
    /// The ConstraintOperator class is used internally by a
    /// ConstraintBuilder to represent an operator that 
    /// modifies or combines constraints. 
    /// 
    /// Constraint operators use left and right precedence
    /// values to determine whether the top operator on the
    /// stack should be reduced before pushing a new operator.
    /// </summary>
    public abstract class ConstraintOperator
    {
        private object leftContext;
        private object rightContext;

		/// <summary>
		/// The precedence value used when the operator
		/// is about to be pushed to the stack.
		/// </summary>
		protected int left_precedence;
        
		/// <summary>
		/// The precedence value used when the operator
		/// is on the top of the stack.
		/// </summary>
		protected int right_precedence;

		/// <summary>
		/// The syntax element preceding this operator
		/// </summary>
        public object LeftContext
        {
            get { return leftContext; }
            set { leftContext = value; }
        }

		/// <summary>
		/// The syntax element folowing this operator
		/// </summary>
        public object RightContext
        {
            get { return rightContext; }
            set { rightContext = value; }
        }

		/// <summary>
		/// The precedence value used when the operator
		/// is about to be pushed to the stack.
		/// </summary>
		public virtual int LeftPrecedence
        {
            get { return left_precedence; }
        }

		/// <summary>
		/// The precedence value used when the operator
		/// is on the top of the stack.
		/// </summary>
		public virtual int RightPrecedence
        {
            get { return right_precedence; }
        }

		/// <summary>
		/// Reduce produces a constraint from the operator and 
		/// any arguments. It takes the arguments from the constraint 
		/// stack and pushes the resulting constraint on it.
		/// </summary>
		/// <param name="stack"></param>
		public abstract void Reduce(ConstraintBuilder.ConstraintStack stack);
    }
    #endregion

    #region Prefix Operators

    #region PrefixOperator
    /// <summary>
	/// PrefixOperator takes a single constraint and modifies
	/// it's action in some way.
	/// </summary>
    public abstract class PrefixOperator : ConstraintOperator
    {
		/// <summary>
		/// Reduce produces a constraint from the operator and 
		/// any arguments. It takes the arguments from the constraint 
		/// stack and pushes the resulting constraint on it.
		/// </summary>
		/// <param name="stack"></param>
		public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            stack.Push(ApplyPrefix(stack.Pop()));
        }

        /// <summary>
        /// Returns the constraint created by applying this
        /// prefix to another constraint.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public abstract Constraint ApplyPrefix(Constraint constraint);
    }
    #endregion

    #region NotOperator
    /// <summary>
    /// Negates the test of the constraint it wraps.
    /// </summary>
    public class NotOperator : PrefixOperator
    {
        /// <summary>
        /// Constructs a new NotOperator
        /// </summary>
        public NotOperator()
        {
            // Not stacks on anything and only allows other
            // prefix ops to stack on top of it.
            this.left_precedence = this.right_precedence = 1;
        }

        /// <summary>
        /// Returns a NotConstraint applied to its argument.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new NotConstraint(constraint);
        }
    }
    #endregion

    #region Collection Operators
    /// <summary>
    /// Abstract base for operators that indicate how to
    /// apply a constraint to items in a collection.
    /// </summary>
    public abstract class CollectionOperator : PrefixOperator
    {
        /// <summary>
        /// Constructs a CollectionOperator
        /// </summary>
        public CollectionOperator()
        {
            // Collection Operators stack on everything
            // and allow all other ops to stack on them
            this.left_precedence = 1;
            this.right_precedence = 10;
        }
    }

    /// <summary>
    /// Represents a constraint that succeeds if all the 
    /// members of a collection match a base constraint.
    /// </summary>
    public class AllOperator : CollectionOperator
    {
        /// <summary>
        /// Returns a constraint that will apply the argument
        /// to the members of a collection, succeeding if
        /// they all succeed.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new AllItemsConstraint(constraint);
        }
    }

    /// <summary>
    /// Represents a constraint that succeeds if any of the 
    /// members of a collection match a base constraint.
    /// </summary>
    public class SomeOperator : CollectionOperator
    {
        /// <summary>
        /// Returns a constraint that will apply the argument
        /// to the members of a collection, succeeding if
        /// any of them succeed.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new SomeItemsConstraint(constraint);
        }
    }

    /// <summary>
    /// Represents a constraint that succeeds if none of the 
    /// members of a collection match a base constraint.
    /// </summary>
    public class NoneOperator : CollectionOperator
    {
        /// <summary>
        /// Returns a constraint that will apply the argument
        /// to the members of a collection, succeeding if
        /// none of them succeed.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new NoItemConstraint(constraint);
        }
    }
    #endregion

    #region WithOperator
    /// <summary>
    /// Represents a constraint that simply wraps the
    /// constraint provided as an argument, without any
    /// further functionality, but which modifes the
    /// order of evaluation because of its precedence.
    /// </summary>
    public class WithOperator : PrefixOperator
    {
        /// <summary>
        /// Constructor for the WithOperator
        /// </summary>
        public WithOperator()
        {
            this.left_precedence = 1;
            this.right_precedence = 4;
        }

        /// <summary>
        /// Returns a constraint that wraps its argument
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return constraint;
        }
    }
    #endregion

    #region SelfResolving Operators

    #region SelfResolvingOperator
    /// <summary>
    /// Abstract base class for operators that are able to reduce to a 
    /// constraint whether or not another syntactic element follows.
    /// </summary>
    public abstract class SelfResolvingOperator : ConstraintOperator
    {
    }
    #endregion

    #region PropOperator
    /// <summary>
    /// Operator used to test for the presence of a named Property
    /// on an object and optionally apply further tests to the
    /// value of that property.
    /// </summary>
    public class PropOperator : SelfResolvingOperator
    {
        private string name;

        /// <summary>
        /// Gets the name of the property to which the operator applies
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Constructs a PropOperator for a particular named property
        /// </summary>
        public PropOperator(string name)
        {
            this.name = name;

            // Prop stacks on anything and allows only 
            // prefix operators to stack on it.
            this.left_precedence = this.right_precedence = 1;
        }

        /// <summary>
        /// Reduce produces a constraint from the operator and 
        /// any arguments. It takes the arguments from the constraint 
        /// stack and pushes the resulting constraint on it.
        /// </summary>
        /// <param name="stack"></param>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (RightContext == null || RightContext is BinaryOperator)
                stack.Push(new PropertyExistsConstraint(name));
            else
                stack.Push(new PropertyConstraint(name, stack.Pop()));
        }
    }
    #endregion

    #region AttributeOperator
    /// <summary>
    /// Operator that tests for the presence of a particular attribute
    /// on a type and optionally applies further tests to the attribute.
    /// </summary>
    public class AttributeOperator : SelfResolvingOperator
    {
        private Type type;

        /// <summary>
        /// Construct an AttributeOperator for a particular Type
        /// </summary>
        /// <param name="type">The Type of attribute tested</param>
        public AttributeOperator(Type type)
        {
            this.type = type;

            // Attribute stacks on anything and allows only 
            // prefix operators to stack on it.
            this.left_precedence = this.right_precedence = 1;
        }

        /// <summary>
        /// Reduce produces a constraint from the operator and 
        /// any arguments. It takes the arguments from the constraint 
        /// stack and pushes the resulting constraint on it.
        /// </summary>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (RightContext == null || RightContext is BinaryOperator)
                stack.Push(new AttributeExistsConstraint(type));
            else
                stack.Push(new AttributeConstraint(type, stack.Pop()));
        }
    }
    #endregion

    #region ThrowsOperator
    /// <summary>
    /// Operator that tests that an exception is thrown and
    /// optionally applies further tests to the exception.
    /// </summary>
    public class ThrowsOperator : SelfResolvingOperator
    {
        /// <summary>
        /// Construct a ThrowsOperator
        /// </summary>
        public ThrowsOperator()
        {
            // ThrowsOperator stacks on everything but
            // it's always the first item on the stack
            // anyway. It is evaluated last of all ops.
            this.left_precedence = 1;
            this.right_precedence = 100;
        }

        /// <summary>
        /// Reduce produces a constraint from the operator and 
        /// any arguments. It takes the arguments from the constraint 
        /// stack and pushes the resulting constraint on it.
        /// </summary>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (RightContext == null || RightContext is BinaryOperator)
                stack.Push(new ThrowsConstraint(null));
            else
                stack.Push(new ThrowsConstraint(stack.Pop()));
        }
    }
    #endregion

    #endregion

    #endregion

    #region Binary Operators

    #region BinaryOperator
    /// <summary>
    /// Abstract base class for all binary operators
    /// </summary>
    public abstract class BinaryOperator : ConstraintOperator
    {
        /// <summary>
        /// Reduce produces a constraint from the operator and 
        /// any arguments. It takes the arguments from the constraint 
        /// stack and pushes the resulting constraint on it.
        /// </summary>
        /// <param name="stack"></param>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            Constraint right = stack.Pop();
            Constraint left = stack.Pop();
            stack.Push(ApplyOperator(left, right));
        }

        /// <summary>
        /// Gets the left precedence of the operator
        /// </summary>
        public override int LeftPrecedence
        {
            get
            {
                return RightContext is CollectionOperator
                    ? base.LeftPrecedence + 10
                    : base.LeftPrecedence;
            }
        }

        /// <summary>
        /// Gets the right precedence of the operator
        /// </summary>
        public override int RightPrecedence
        {
            get
            {
                return RightContext is CollectionOperator
                    ? base.RightPrecedence + 10
                    : base.RightPrecedence;
            }
        }

        /// <summary>
        /// Abstract method that produces a constraint by applying
        /// the operator to its left and right constraint arguments.
        /// </summary>
        public abstract Constraint ApplyOperator(Constraint left, Constraint right);
    }
    #endregion

    #region AndOperator
    /// <summary>
    /// Operator that requires both it's arguments to succeed
    /// </summary>
    public class AndOperator : BinaryOperator
    {
        /// <summary>
        /// Construct an AndOperator
        /// </summary>
        public AndOperator()
        {
            this.left_precedence = this.right_precedence = 2;
        }

        /// <summary>
        /// Apply the operator to produce an AndConstraint
        /// </summary>
        public override Constraint ApplyOperator(Constraint left, Constraint right)
        {
            return new AndConstraint(left, right);
        }
    }
    #endregion

    #region OrOperator
    /// <summary>
    /// Operator that requires at least one of it's arguments to succeed
    /// </summary>
    public class OrOperator : BinaryOperator
    {
        /// <summary>
        /// Construct an OrOperator
        /// </summary>
        public OrOperator()
        {
            this.left_precedence = this.right_precedence = 3;
        }

        /// <summary>
        /// Apply the operator to produce an OrConstraint
        /// </summary>
        public override Constraint ApplyOperator(Constraint left, Constraint right)
        {
            return new OrConstraint(left, right);
        }
    }
    #endregion

    #endregion
}
