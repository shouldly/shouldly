// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace NUnit.UiKit.Tests
{
	[TestFixture]
	public class LongRunningOperationDisplayTests : AssertionHelper
	{
        // This test was intermittently throwing the following exception under .NET 1.0.
        // System.Runtime.InteropServices.ExternalException: A generic error occurred in GDI+.
        //  at System.Drawing.Graphics.DrawRectangle(Pen pen, Int32 x, Int32 y, Int32 width, Int32 height)
        //  at System.Drawing.Graphics.DrawRectangle(Pen pen, Rectangle rect)
        //  at NUnit.UiKit.LongRunningOperationDisplay.OnPaint(PaintEventArgs e) in .\src\GuiComponents\UiKit\LongRunningOperationDisplay.cs:line 117
        [Test, Platform(Exclude = "NET-1.0")]
        public void CreateDisplay()
		{
			Form form = new Form();
			LongRunningOperationDisplay display = new LongRunningOperationDisplay( form, "Loading..." );
			Expect( display.Owner, EqualTo( form ) );
			Expect( GetOperationText( display ), EqualTo( "Loading..." ) );
		}

		private string GetOperationText( Control display )
		{
			foreach( Control control in display.Controls )
				if ( control.Name == "operation" )
					return control.Text;

			return null;
		}
	}
}
