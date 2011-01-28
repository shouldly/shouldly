// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Collections;
using System.Reflection;

namespace NUnit.Core.Extensibility
{
	/// <summary>
	/// TestDecoratorCollection is an ExtensionPoint for TestDecorators and
	/// implements the ITestDecorator interface itself, passing calls 
	/// on to the individual decorators.
	/// </summary>
	public class TestDecoratorCollection : ExtensionPoint, IExtensionPoint2, ITestDecorator
	{
		#region Constructor
		public TestDecoratorCollection(IExtensionHost host)
			: base( "TestDecorators", host, 10 ) { }
		#endregion

		#region ITestDecorator Members

		public Test Decorate(Test test, MemberInfo member)
		{
			Test decoratedTest = test;

			foreach( ITestDecorator decorator in Extensions )
				decoratedTest = decorator.Decorate( decoratedTest, member );

			return decoratedTest;
		}
		#endregion

		#region ExtensionPoint Overrides
		protected override bool IsValidExtension(object extension)
		{
			return extension is ITestDecorator; 
		}
		#endregion
	}
}
