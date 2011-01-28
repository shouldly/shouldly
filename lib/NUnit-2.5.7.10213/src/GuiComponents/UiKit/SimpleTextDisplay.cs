// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// Summary description for SimpleTextDisplay.
	/// </summary>
	public class SimpleTextDisplay : System.Windows.Forms.ScrollableControl, TextDisplay
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private StringBuilder builder;

		private SizeF exactSize = SizeF.Empty;

		private float fastPathHeightAdjust;

		private TextDisplayContent content;

		public SimpleTextDisplay()
		{
			this.builder = new StringBuilder();

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			string text = builder.ToString();
			Rectangle clip = pe.ClipRectangle;

			g.DrawString( text, this.Font, Brushes.Black, this.AutoScrollPosition );

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			if ( builder.Length > 0 )
			{
				this.exactSize = MeasureString( builder.ToString() );
				this.AutoScrollMinSize = exactSize.ToSize();
			}

			this.OnFontChanged(e);

			base.OnHandleCreated (e);
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged (e);

			SizeF measuredSize = this.MeasureString( "abc" );
			this.fastPathHeightAdjust = measuredSize.Height - Font.GetHeight();
		}

	
		#region TextDisplay Members
		public TextDisplayContent Content
		{
			get { return content; }
			set { content = value; }
		}

		public void Clear()
		{
			builder.Length = 0;
			this.AutoScrollMinSize = new Size(0,0);
			this.Invalidate();
		}

		public void Write( string text )
		{
			int lengthSoFar = builder.Length;

			builder.Append( text );

			if ( this.IsHandleCreated )
			{
				bool isUpdateVisible = this.AutoScrollPosition.Y + this.AutoScrollMinSize.Height <= this.ClientRectangle.Height;

				if ( text != null && lengthSoFar > 0 && builder[lengthSoFar-1] == '\n' ) // use fast path
				{

					SizeF textSize = MeasureString( text );
					this.exactSize = new SizeF( 
						Math.Max( this.exactSize.Width, textSize.Width ),
						this.exactSize.Height + textSize.Height - this.fastPathHeightAdjust );
					this.AutoScrollMinSize = exactSize.ToSize();
				}
				else
				{
					this.exactSize = MeasureString( builder.ToString() );
					this.AutoScrollMinSize = exactSize.ToSize();
				}
				
				if ( isUpdateVisible ) this.Invalidate();
			}
		}

		public void Write(TestOutput output)
		{
			Write(output.Text);
		}

		public void WriteLine( string text )
		{
			Write( text + Environment.NewLine );
		}

		public string GetText()
		{
			return this.builder.ToString();
		}
		#endregion

		#region TestObserver Methods
		public void Subscribe(ITestEvents events)
		{
		}
		#endregion

		#region Private Methods
		private SizeF MeasureString(string text)
		{
			return Graphics.FromHwnd(this.Handle).MeasureString( text, this.Font );
		}
		#endregion
	}
}
