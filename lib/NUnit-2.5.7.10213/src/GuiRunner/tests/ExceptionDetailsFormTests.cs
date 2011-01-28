// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.TestUtilities;

namespace NUnit.Gui.Tests
{
	[TestFixture]	
	public class ExceptionDetailsFormTests : FormTester
	{
		[TestFixtureSetUp]
		public void CreateForm()
		{
			this.Form = new ExceptionDetailsForm( new Exception( "My message" ) );
		}

		[TestFixtureTearDown]
		public void CloseForm()
		{
			this.Form.Close();
		}

		[Test]
		public void ControlsExist()
		{
			AssertControlExists( "message", typeof( Label ) );
			AssertControlExists( "stackTrace", typeof( RichTextBox ) );
			AssertControlExists( "okButton", typeof( Button ) );
		}

		[Test]
		public void ControlsArePositionedCorrectly()
		{
			AssertControlsAreStackedVertically( "message", "stackTrace", "okButton" );
		}

		[Test]
		public void MessageDisplaysCorrectly()
		{
			this.Form.Show();
			Assert.AreEqual( "System.Exception: My message", GetText( "message" ) );
		}
	}
}
