// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.TestUtilities;

namespace NUnit.UiKit.Tests
{
	[TestFixture]
	public class ErrorDisplayTests : ControlTester
	{
		[TestFixtureSetUp]
		public void CreateForm()
		{
			this.Control = new ErrorDisplay();
		}

		[TestFixtureTearDown]
		public void CloseForm()
		{
			this.Control.Dispose();
		}

		[Test]
		public void ControlsExist()
		{
			AssertControlExists( "detailList", typeof( ListBox ) );
			AssertControlExists( "tabSplitter", typeof( Splitter ) );
			AssertControlExists( "errorBrowser", typeof( NUnit.UiException.Controls.ErrorBrowser ) );
		}

		[Test]
		public void ControlsArePositionedCorrectly()
		{
			AssertControlsAreStackedVertically( "detailList", "tabSplitter", "errorBrowser" );
		}
	}
}
