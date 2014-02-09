// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CP.Windows.Forms
{
	/// <summary>
	/// A special type of textbox which can display a tooltip-like
	/// window to show the full extent of any text which doesn't 
	/// fit. The window may be placed directly over the label
	/// or immediately beneath it and will expand to fit in
	/// a horizontal, vertical or both directions as needed.
	/// 
	/// TODO: This control is virtually identical to ExpandingLabel.
	/// We need to have an extension provider that works like a 
	/// ToolTip in order to eliminate the duplication.
	/// </summary>
	public class ExpandingTextBox : System.Windows.Forms.TextBox
	{
		#region Instance Variables

		/// <summary>
		/// Our window for displaying expanded text
		/// </summary>
		private TipWindow tipWindow;
		
		/// <summary>
		/// Direction of expansion
		/// </summary>
		private TipWindow.ExpansionStyle expansion = TipWindow.ExpansionStyle.Horizontal;
		
		/// <summary>
		/// Time in milliseconds that the mouse must
		/// be stationary over an item before the
		/// tip window will display.
		/// </summary>
		private int mouseHoverDelay = 300;

		/// <summary>
		/// True if tipWindow may overlay the label
		/// </summary>
		private bool overlay = true;
		
		/// <summary>
		/// Time in milliseconds that the tip window
		/// will remain displayed.
		/// </summary>
		private int autoCloseDelay = 0;

		/// <summary>
		/// Time in milliseconds that the window stays
		/// open after the mouse leaves the control.
		/// </summary>
		private int mouseLeaveDelay = 300;

		/// <summary>
		/// If true, a context menu with Copy is displayed which
		/// allows copying contents to the clipboard.
		/// </summary>
		private bool copySupported = false;

		/// <summary>
		/// Timer used to control display behavior on hover.
		/// </summary>
		private System.Windows.Forms.Timer hoverTimer;

		/// <summary>
		/// True if control should expand automatically on hover.
		/// </summary>
		private bool autoExpand = true;

		#endregion

		#region Properties

		[Browsable( false )]
		public bool Expanded
		{
			get { return tipWindow != null && tipWindow.Visible; }
		}

		[Category ( "Behavior"  ), DefaultValue( TipWindow.ExpansionStyle.Horizontal )]
		public TipWindow.ExpansionStyle Expansion
		{
			get { return expansion; }
			set { expansion = value; }
		}

		[Category ( "Behavior" ), DefaultValue( true )]
		public bool AutoExpand
		{
			get { return autoExpand; }
			set { autoExpand = value; }
		}

		[Category( "Behavior" ), DefaultValue( true )]
		[Description("Indicates whether the tip window should overlay the label")]
		public bool Overlay
		{
			get { return overlay; }
			set { overlay = value; }
		}

		/// <summary>
		/// Time in milliseconds that the mouse must
		/// be stationary over an item before the
		/// tip window will display.
		/// </summary>
		[Category( "Behavior" ), DefaultValue( 300 )]
		[Description("Time in milliseconds that mouse must be stationary over an item before the tip is displayed.")]
		public int MouseHoverDelay
		{
			get { return mouseHoverDelay; }
			set { mouseHoverDelay = value; }
		}

		/// <summary>
		/// Time in milliseconds that the tip window
		/// will remain displayed.
		/// </summary>
		[Category( "Behavior" ), DefaultValue( 0 )]
		[Description("Time in milliseconds that the tip is displayed. Zero indicates no automatic timeout.")]
		public int AutoCloseDelay
		{
			get { return autoCloseDelay; }
			set { autoCloseDelay = value; }
		}

		/// <summary>
		/// Time in milliseconds that the window stays
		/// open after the mouse leaves the control.
		/// Reentering the control resets this.
		/// </summary>
		[Category( "Behavior" ), DefaultValue( 300 )]
		[Description("Time in milliseconds that the tip is displayed after the mouse levaes the control")]
		public int MouseLeaveDelay
		{
			get { return mouseLeaveDelay; }
			set { mouseLeaveDelay = value; }
		}

		[Category( "Behavior"), DefaultValue( false )]
		[Description("If true, displays a context menu with Copy")]
		public bool CopySupported
		{
			get { return copySupported; }
			set 
			{ 
				copySupported = value; 
				if ( copySupported )
					base.ContextMenu = null;
			}
		}

		/// <summary>
		/// Override Text property to set up copy menu if
		/// the value is non-empty.
		/// </summary>
		public override string Text
		{
			get { return base.Text; }
			set 
			{ 
				base.Text = value;

				if ( copySupported )
				{
					if ( value == null || value == string.Empty )
					{
						if ( this.ContextMenu != null )
						{
							this.ContextMenu.Dispose();
							this.ContextMenu = null;
						}
					}
					else
					{
						this.ContextMenu = new System.Windows.Forms.ContextMenu();
						MenuItem copyMenuItem = new MenuItem( "Copy", new EventHandler( CopyToClipboard ) );
						this.ContextMenu.MenuItems.Add( copyMenuItem );
					}
				}
			}
		}

		#endregion

		#region Public Methods

		public void Expand()
		{
			if ( !Expanded )
			{
				tipWindow = new TipWindow( this );
				tipWindow.Closed += new EventHandler( tipWindow_Closed );
				tipWindow.Expansion = this.Expansion;
				tipWindow.Overlay = this.Overlay;
				tipWindow.AutoCloseDelay = this.AutoCloseDelay;
				tipWindow.MouseLeaveDelay = this.MouseLeaveDelay;
				tipWindow.WantClicks = this.CopySupported;
				tipWindow.Show();
			}
		}

		public void Unexpand()
		{
			if ( Expanded )
			{
				tipWindow.Close();
			}
		}

		#endregion

		#region Event Handlers

		private void tipWindow_Closed( object sender, EventArgs e )
		{
			tipWindow = null;
			ClearTimer();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			ClearTimer();
		}

		private void OnMouseHover( object sender, System.EventArgs e )
		{
			if ( autoExpand )
			{
				Graphics g = Graphics.FromHwnd( Handle );
				SizeF sizeNeeded = g.MeasureString( Text, Font );
				bool expansionNeeded = 
					Width < (int)sizeNeeded.Width ||
					Height < (int)sizeNeeded.Height;

				if ( expansionNeeded ) Expand();
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			ClearTimer();

			int textExtent = this.Lines.Length * this.FontHeight;
			if ( e.Y <= textExtent )
			{
				hoverTimer = new System.Windows.Forms.Timer();
				hoverTimer.Interval = mouseHoverDelay;
				hoverTimer.Tick += new EventHandler( OnMouseHover );
				hoverTimer.Start();
			}

			base.OnMouseMove (e);
		}

		private void ClearTimer()
		{
			if ( hoverTimer != null )
			{
				hoverTimer.Stop();
				hoverTimer.Dispose();
			}
		}

		/// <summary>
		/// Copy contents to clipboard
		/// </summary>
		private void CopyToClipboard( object sender, EventArgs e )
		{
			Clipboard.SetDataObject( this.Text );
		}
	
		#endregion
	}

//	public class HoverDetector
//	{
//		private Control control;
//
//		private Timer hoverTimer;
//
//		private int hoverDelay;
//
//		public int HoverDelay
//		{
//			get { return hoverDelay; }
//			set { hoverDelay = value; }
//		}
//
//		public event System.EventHandler Hover;
//
//		public HoverDetector( Control control )
//		{
//			this.control = control;
//			
//			control.MouseLeave += new EventHandler( OnMouseLeave );
//			control.MouseMove += new MouseEventHandler( OnMouseMove );
//		}
//
//		private void OnMouseLeave( object sender, System.EventArgs e )
//		{
//			ClearTimer();
//		}
//
//		private void OnMouseMove( object sender, MouseEventArgs e )
//		{
//			ClearTimer();
//
//			hoverTimer = new System.Windows.Forms.Timer();
//			hoverTimer.Interval = hoverDelay;
//			hoverTimer.Tick += new EventHandler( OnMouseHover );
//			hoverTimer.Start();	
//		}
//
//		private void OnMouseHover( object sender, System.EventArgs e )
//		{
//			if ( Hover != null )
//				Hover( this, e );
//		}
//
//		private void ClearTimer()
//		{
//			if ( hoverTimer != null )
//			{
//				hoverTimer.Stop();
//				hoverTimer.Dispose();
//			}
//		}
//	}
}
