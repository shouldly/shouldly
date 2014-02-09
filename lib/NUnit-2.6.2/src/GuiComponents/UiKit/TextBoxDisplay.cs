// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using NUnit.Core;
using NUnit.Util;
using System.Diagnostics;

namespace NUnit.UiKit
{
	/// <summary>
	/// TextBoxDisplay is an adapter that allows accessing a 
	/// System.Windows.Forms.TextBox using the TextDisplay interface.
	/// </summary>
	public class TextBoxDisplay : System.Windows.Forms.RichTextBox, TextDisplay, TestObserver
	{
		private MenuItem copyMenuItem;
		private MenuItem selectAllMenuItem;
		private MenuItem wordWrapMenuItem;
		private MenuItem fontMenuItem;
		private MenuItem increaseFontMenuItem;
		private MenuItem decreaseFontMenuItem;
		private MenuItem restoreFontMenuItem;

		private TextDisplayContent content;

		public TextBoxDisplay()
		{
			this.Multiline = true;
			this.ReadOnly = true;
			this.WordWrap = false;

			this.ContextMenu = new ContextMenu();
			this.copyMenuItem = new MenuItem( "&Copy", new EventHandler( copyMenuItem_Click ) );
			this.selectAllMenuItem = new MenuItem( "Select &All", new EventHandler( selectAllMenuItem_Click ) );
			this.wordWrapMenuItem = new MenuItem( "&Word Wrap", new EventHandler( wordWrapMenuItem_Click ) );
			this.fontMenuItem = new MenuItem( "Font" );
			this.increaseFontMenuItem = new MenuItem( "Increase", new EventHandler( increaseFontMenuItem_Click ) );
			this.decreaseFontMenuItem = new MenuItem( "Decrease", new EventHandler( decreaseFontMenuItem_Click ) );
			this.restoreFontMenuItem = new MenuItem( "Restore", new EventHandler( restoreFontMenuItem_Click ) );
			this.fontMenuItem.MenuItems.AddRange( new MenuItem[] { increaseFontMenuItem, decreaseFontMenuItem, new MenuItem("-"), restoreFontMenuItem } );
			this.ContextMenu.MenuItems.AddRange( new MenuItem[] { copyMenuItem, selectAllMenuItem, wordWrapMenuItem, fontMenuItem } );
			this.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);
		}

		private void copyMenuItem_Click(object sender, EventArgs e)
		{
			this.Copy();
		}

		private void selectAllMenuItem_Click(object sender, EventArgs e)
		{
			this.SelectAll();
		}

		private void wordWrapMenuItem_Click(object sender, EventArgs e)
		{
			this.WordWrap = this.wordWrapMenuItem.Checked = !this.wordWrapMenuItem.Checked;
		}

		private void increaseFontMenuItem_Click(object sender, EventArgs e)
		{
			applyFont( new Font( this.Font.FontFamily, this.Font.SizeInPoints * 1.2f, this.Font.Style ) );
		}

		private void decreaseFontMenuItem_Click(object sender, EventArgs e)
		{
			applyFont( new Font( this.Font.FontFamily, this.Font.SizeInPoints / 1.2f, this.Font.Style ) );
		}

		private void restoreFontMenuItem_Click(object sender, EventArgs e)
		{
			applyFont( new Font( FontFamily.GenericMonospace, 8.0f ) );
		}

		private void applyFont( Font font )
		{
			this.Font = font;
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			Services.UserSettings.SaveSetting( "Gui.FixedFont", 
                converter.ConvertToString( null, System.Globalization.CultureInfo.InvariantCulture, font ) );
		}
		
		private void ContextMenu_Popup(object sender, EventArgs e)
		{
			this.copyMenuItem.Enabled = this.SelectedText != "";
			this.selectAllMenuItem.Enabled = this.TextLength > 0;
		}

		private string pendingTestCaseLabel = null;
		private void OnTestOutput( object sender, TestEventArgs e )
		{
			if ( ShouldInclude(e.TestOutput.Type) )
			{
				if ( pendingTestCaseLabel != null )
				{
					WriteLine( pendingTestCaseLabel );
					pendingTestCaseLabel = null;
				}

				Write( e.TestOutput.Text );
			}
		}

        // TODO: We determine whether to include output
        // based solely on the output type. This works
        // well for everything but logging. Because we
        // are unable - at this stage of processing - 
        // to determine the logging level of the output
        // all tabs displaying log output will show
        // output at the most verbose level specified
        // on any of the tabs. Since it's not likely
        // that anyone will display logging on multiple
        // tabs, this is not seen as a serious issue.
        // It may be resolved in a future release by
        // limiting the options available when specifying
        // the content of the output displayed.
        private bool ShouldInclude(TestOutputType type)
        {
            switch (type)
            {
                default:
                case TestOutputType.Out:
                    return content.Out;

                case TestOutputType.Error:
                    return content.Error;

                case TestOutputType.Log:
                    return true;// content.LogLevel != LoggingThreshold.Off;

                case TestOutputType.Trace:
                    return content.Trace;
            }
        }

		private void OnTestStarting(object sender, TestEventArgs args)
		{
			if (this.content.Labels != TestLabelLevel.Off)
			{
				string label = string.Format( "***** {0}", args.TestName.FullName );

				if (this.content.Labels == TestLabelLevel.On)
					this.pendingTestCaseLabel = label;
				else
					WriteLine(label);
			}
		}

		protected override void OnFontChanged(EventArgs e)
		{
			// Do nothing - this control uses it's own font
		}


		#region TextDisplay Members
		public TextDisplayContent Content
		{
			get { return content; }
			set { content = value; }
		}

		public void Write( string text )
		{
			this.AppendText( text );
		}

		public void Write( NUnit.Core.TestOutput output )
		{
			Write( output.Text );
		}

		public void WriteLine( string text )
		{
			Write( text + Environment.NewLine );
		}

		public string GetText()
		{
			return this.Text;
		}
		#endregion

		#region TestObserver Members
		public void Subscribe(ITestEvents events)
		{
			events.TestOutput += new TestEventHandler(OnTestOutput);
			events.TestStarting += new TestEventHandler(OnTestStarting);
		}
		#endregion
	}
}
