// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Reflection;

namespace NUnit.Core.Extensibility
{
	/// <summary>
	/// DecoratorPriority wraps constants that may be used
    /// to represent the relative priority of TestDecorators.
    /// Decorators with a lower priority are applied first
    /// so that higher priority decorators wrap them.
    /// 
    /// NOTE: This feature is subject to change.
	/// </summary>
    public class DecoratorPriority
	{
	    /// <summary>
	    /// The default priority, equivalent to Normal
	    /// </summary>
        public static readonly int Default = 0;
        /// <summary>
        /// Priority for Decorators that must apply first 
        /// </summary>
		public static readonly int First = 1;
        /// <summary>
        /// Normal Decorator priority
        /// </summary>
		public static readonly int Normal = 5;
        /// <summary>
        /// Priority for Decorators that must apply last
        /// </summary>
	    public static readonly int Last = 9;
	}

	/// <summary>
	/// The ITestDecorator interface is exposed by a class that knows how to
	/// enhance the functionality of a test case or suite by decorating it.
	/// </summary>
	public interface ITestDecorator
	{
		/// <summary>
		/// Examine the a Test and either return it as is, modify it
		/// or return a different TestCase.
		/// </summary>
		/// <param name="test">The Test to be decorated</param>
		/// <param name="member">The MethodInfo used to construct the test</param>
		/// <returns>The resulting Test</returns>
		Test Decorate( Test test, MemberInfo member );
	}
}
