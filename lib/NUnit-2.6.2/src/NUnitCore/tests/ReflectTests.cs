// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class ReflectTests
	{
		private readonly Type myType = typeof( Colors.MyClass );
		private readonly BindingFlags BF = BindingFlags.Public | BindingFlags.Instance;

		[Test]
		public void CanDetectAttributes()
		{
			Assert.IsFalse( Reflect.HasAttribute( myType, "Colors.RedAttribute", false ), "Red" );
			Assert.IsTrue( Reflect.HasAttribute( myType, "Colors.GreenAttribute", false ), "Green" );
			Assert.IsFalse( Reflect.HasAttribute( myType, "Colors.BlueAttribute", false ), "Blue" );
		}

		[Test]
		public void CanDetectInheritedAttributes()
		{
			Assert.IsTrue( Reflect.HasAttribute( myType, "Colors.RedAttribute", true ), "Red" );
			Assert.IsTrue( Reflect.HasAttribute( myType, "Colors.GreenAttribute", true ), "Green" );
			Assert.IsFalse( Reflect.HasAttribute( myType, "Colors.BlueAttribute", true ), "Blue" );
		}

		[Test]
		public void GetAttribute()
		{
			Assert.IsNull( Reflect.GetAttribute( myType, "Colors.RedAttribute", false ), "Red" );
			Assert.AreEqual( "GreenAttribute", 
				Reflect.GetAttribute( myType, "Colors.GreenAttribute", false ).GetType().Name );
			Assert.IsNull( Reflect.GetAttribute( myType, "Colors.BlueAttribute", false ), "Blue" );
		}

		[Test]
		public void GetAttributes()
		{
			Assert.AreEqual( 1, Reflect.GetAttributes( myType, false ).Length );
			Assert.AreEqual( 0, Reflect.GetAttributes( myType, "Colors.RedAttribute", false ).Length, "Red" );
			Assert.AreEqual( 1, Reflect.GetAttributes( myType, "Colors.GreenAttribute", false ).Length, "Green" );
			Assert.AreEqual( 0, Reflect.GetAttributes( myType, "Colors.BlueAttribute", false ).Length, "Blue" );
		}

		[Test]
		public void GetInheritedAttribute()
		{
			Assert.AreEqual( "RedAttribute", 
				Reflect.GetAttribute( myType, "Colors.RedAttribute", true ).GetType().Name );
			Assert.AreEqual( "GreenAttribute", 
				Reflect.GetAttribute( myType, "Colors.GreenAttribute", true ).GetType().Name );
			Assert.IsNull( Reflect.GetAttribute( myType, "Colors.BlueAttribute", true ), "Blue" );
		}
	
		[Test]
		public void GetInheritedAttributes()
		{
			Assert.AreEqual( 2, Reflect.GetAttributes( myType, true ).Length );
			Assert.AreEqual( 1, Reflect.GetAttributes( myType, "Colors.RedAttribute", true ).Length, "Red" );
			Assert.AreEqual( 1, Reflect.GetAttributes( myType, "Colors.GreenAttribute", true ).Length, "Green" );
			Assert.AreEqual( 0, Reflect.GetAttributes( myType, "Colors.BlueAttribute", true ).Length , "Blue" );
		}
	
		[Test]
		public void InheritsFrom()
		{
			Assert.IsTrue( Reflect.InheritsFrom( myType, "Colors.BaseClass" ) );
		}

		[Test]
		public void HasInterface()
		{
			Assert.IsTrue( Reflect.HasInterface( myType, "Colors.MyInterface" ) );
		}

		[Test]
		public void GetConstructor()
		{
			Assert.IsNotNull( Reflect.GetConstructor( myType ) );
		}

		[Test]
		public void GetMethodsWithAttribute()
		{
            MethodInfo[] methods = Reflect.GetMethodsWithAttribute(myType, "Colors.BlueAttribute", false);
            Assert.That(
                List.Map(methods).Property("Name"), 
                Is.EqualTo(new string[] {"BaseBlueMethod", "BlueMethod"} ));
        }

		[Test]
		public void GetNamedMethod()
		{
			Assert.IsNotNull( Reflect.GetNamedMethod( myType, "BlueMethod" ) );
		}

		[Test]
		public void GetNamedMethodWithArgs()
		{
			Assert.IsNotNull( Reflect.GetNamedMethod( myType, "TwoArgs", new string[] { "System.Int32", "System.String" } ) );
		}

		[Test]
		public void GetPropertyWithAttribute()
		{
			Assert.IsNotNull( Reflect.GetPropertyWithAttribute( myType, "Colors.RedAttribute" ) );
		}

		[Test]
		public void GetNamedProperty()
		{
			Assert.IsNotNull( Reflect.GetNamedProperty( myType, "RedProperty", BF ) );
		}

		[Test]
		public void GetPropertyValue()
		{
			Assert.AreEqual( 42, Reflect.GetPropertyValue( new Colors.MyClass(), "RedProperty", BF ) );
		}

		[Test]
		public void Construct()
		{
			Assert.IsNotNull( Reflect.Construct( myType ) );
		}

		[Test] 
		public void InvokeMethod()
		{
			Colors.MyClass myClass = new Colors.MyClass();
			MethodInfo method = Reflect.GetNamedMethod( myType, "BlueMethod" );
			Reflect.InvokeMethod( method, myClass );
			Assert.IsTrue( myClass.BlueInvoked );
		}
	}
}

namespace Colors
{
	class RedAttribute : Attribute { }
	class GreenAttribute : Attribute { }
	class BlueAttribute : Attribute { }
	class BrightRedAttribute : RedAttribute { }

	interface MyInterface
	{
	}

	[Red]
	class BaseClass : MyInterface
	{
        [Blue]
        public void BaseBlueMethod() { }
	}

	[Green]
	class MyClass : BaseClass
	{
		public bool BlueInvoked = false;

		[Blue]
		public void BlueMethod() { BlueInvoked = true; }

		[Red]
		public int RedProperty { get { return 42; } }

		public void TwoArgs( int i, string s ) { }
	}
}
