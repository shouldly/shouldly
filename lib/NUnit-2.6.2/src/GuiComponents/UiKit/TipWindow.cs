// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CP.Windows.Forms
{
	public class TipWindow : Form
	{
		/// <summary>
		/// Direction in which to expand
		/// </summary>
		public enum ExpansionStyle
		{
			Horizontal,
			Vertical,
			Both
		}

		#region Instance Variables

		/// <summary>
		/// Text we are displaying
		/// </summary>
		private string tipText;

		/// <summary>
		/// The control for which we are showing expanded text
		/// </summary>
		private Control control;

		/// <summary>
		/// Rectangle representing bounds to overlay. For a listbox, this
		/// is a single item rectangle. For other controls, it is usually
		/// the entire client area.
		/// </summary>
		private Rectangle itemBounds;

		/// <summary>
		/// True if we may overlay control or item
		/// </summary>
		private bool overlay = true;
			
		/// <summary>
		/// Directions we are allowed to expand
		/// </summary>
		private ExpansionStyle expansion = ExpansionStyle.Horizontal;

		/// <summary>
		/// Time before automatically closing
		/// </summary>
		private int autoCloseDelay = 0;

		/// <summary>
		/// Timer used for auto-close
		/// </summary>
		private System.Windows.Forms.Timer autoCloseTimer;

		/// <summary>
		/// Time to wait for after mouse leaves
		/// the window or the label before closing.
		/// </summary>
		private int mouseLeaveDelay = 300;

		/// <summary>
		/// Timer used for mouse leave delay
		/// </summary>
		private System.Windows.Forms.Timer mouseLeaveTimer;

		/// <summary>
		/// Rectangle used to display text
		/// </summary>
		private Rectangle textRect;

		/// <summary>
		/// Indicates whether any clicks should be passed to the underlying control
		/// </summary>
		private bool wantClicks = false;

		#endregion

		#region Construction and Initialization

		public TipWindow( Control control )
		{
			InitializeComponent();
			InitControl( control );

			// Note: This causes an error if called on a listbox
			// with no item as yet selected, therefore, it is handled
			// differently in the constructor for a listbox.
			this.tipText = control.Text;
		}

		public TipWindow( ListBox listbox, int index )
		{
			InitializeComponent();
			InitControl( listbox );

			this.itemBounds = listbox.GetItemRectangle( index );
			this.tipText = listbox.Items[ index ].ToString();
		}

		private void InitControl( Control control )
		{
			this.control = control;
			this.Owner = control.FindForm();
			this.itemBounds = control.ClientRectangle;

			this.ControlBox = false;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.BackColor = Color.LightYellow;
			this.FormBorderStyle = FormBorderStyle.None;
			this.StartPosition = FormStartPosition.Manual; 			

			this.Font = control.Font;
		}

		private void InitializeComponent()
		{
			// 
			// TipWindow
			// 
			this.BackColor = System.Drawing.Color.LightYellow;
			this.ClientSize = new System.Drawing.Size(292, 268);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TipWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

		}

		protected override void OnLoad(System.EventArgs e)
		{
			// At this point, further changes to the properties
			// of the label will have no effect on the tip.
			Point origin = control.Parent.PointToScreen( control.Location );
			origin.Offset( itemBounds.Left, itemBounds.Top );
			if ( !overlay )	origin.Offset( 0, itemBounds.Height );
			this.Location = origin;

			Graphics g = Graphics.FromHwnd( Handle );
			Screen screen = Screen.FromControl( control );
			SizeF layoutArea = new SizeF( screen.WorkingArea.Width - 40, screen.WorkingArea.Height - 40 );
			if ( expansion == ExpansionStyle.Vertical )
				layoutArea.Width = itemBounds.Width;
			else if ( expansion == ExpansionStyle.Horizontal )
				layoutArea.Height = itemBounds.Height;

			Size sizeNeeded = Size.Ceiling( g.MeasureString( tipText, Font, layoutArea ) );

			this.ClientSize = sizeNeeded;
			this.Size = sizeNeeded + new Size( 2, 2 );
			this.textRect = new Rectangle( 1, 1, sizeNeeded.Width, sizeNeeded.Height );

			// Catch mouse leaving the control
			control.MouseLeave += new EventHandler( control_MouseLeave );

			// Catch the form that holds the control closing
			control.FindForm().Closed += new EventHandler( control_FormClosed );

			if ( this.Right > screen.WorkingArea.Right )
			{
				this.Left = Math.Max( 
					screen.WorkingArea.Right - this.Width - 20, 
					screen.WorkingArea.Left + 20);
			}

			if ( this.Bottom > screen.WorkingArea.Bottom - 20 )
			{
				if ( overlay )
					this.Top = Math.Max(
						screen.WorkingArea.Bottom - this.Height - 20,
						screen.WorkingArea.Top + 20 );

				if ( this.Bottom > screen.WorkingArea.Bottom - 20 )
					this.Height = screen.WorkingArea.Bottom - 20 - this.Top;

			}

			if ( autoCloseDelay > 0 )
			{
				autoCloseTimer = new System.Windows.Forms.Timer();
				autoCloseTimer.Interval = autoCloseDelay;
				autoCloseTimer.Tick += new EventHandler( OnAutoClose );
				autoCloseTimer.Start();
			}
		}

		#endregion

		#region Properties

		public bool Overlay
		{
			get { return overlay; }
			set { overlay = value; }
		}

		public ExpansionStyle Expansion
		{
			get { return expansion; }
			set { expansion = value; }
		}

		public int AutoCloseDelay
		{
			get { return autoCloseDelay; }
			set { autoCloseDelay = value; }
		}

		public int MouseLeaveDelay
		{
			get { return mouseLeaveDelay; }
			set { mouseLeaveDelay = value; }
		}

		public string TipText
		{
			get { return tipText; }
			set { tipText = value; }
		}

		public Rectangle ItemBounds
		{
			get { return itemBounds; }
			set { itemBounds = value; }
		}

		public bool WantClicks
		{
			get { return wantClicks; }
			set { wantClicks = value; }
		}

		#endregion

		#region Event Handlers

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint( e );
				
			Graphics g = e.Graphics;
			Rectangle outlineRect = this.ClientRectangle;
			outlineRect.Inflate( -1, -1 );
			g.DrawRectangle( Pens.Black, outlineRect );
			g.DrawString( tipText, Font, Brushes.Black, textRect );
		}

		private void OnAutoClose( object sender, System.EventArgs e )
		{
			this.Close();
		}

		protected override void OnMouseEnter(System.EventArgs e)
		{
			if ( mouseLeaveTimer != null )
			{
				mouseLeaveTimer.Stop();
				mouseLeaveTimer.Dispose();
				System.Diagnostics.Debug.WriteLine( "Entered TipWindow - stopped mouseLeaveTimer" );
			}
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			if ( mouseLeaveDelay > 0  )
			{
				mouseLeaveTimer = new System.Windows.Forms.Timer();
				mouseLeaveTimer.Interval = mouseLeaveDelay;
				mouseLeaveTimer.Tick += new EventHandler( OnAutoClose );
				mouseLeaveTimer.Start();
				System.Diagnostics.Debug.WriteLine( "Left TipWindow - started mouseLeaveTimer" );
			}
		}

		/// <summary>
		/// The form our label is on closed, so we should. 
		/// </summary>
		private void control_FormClosed( object sender, System.EventArgs e )
		{
			this.Close();
		}

		/// <summary>
		/// The mouse left the label. We ignore if we are
		/// overlaying the label but otherwise start a
		/// delay for closing the window
		/// </summary>
		private void control_MouseLeave( object sender, System.EventArgs e )
		{
			if ( mouseLeaveDelay > 0 && !overlay )
			{
				mouseLeaveTimer = new System.Windows.Forms.Timer();
				mouseLeaveTimer.Interval = mouseLeaveDelay;
				mouseLeaveTimer.Tick += new EventHandler( OnAutoClose );
				mouseLeaveTimer.Start();
				System.Diagnostics.Debug.WriteLine( "Left Control - started mouseLeaveTimer" );
			}
		}

		#endregion
	
		[DllImport("user32.dll")]
		static extern uint SendMessage(
			IntPtr hwnd,
			int msg,
			IntPtr wparam,
			IntPtr lparam
			);
	
		protected override void WndProc(ref Message m)
		{
			uint WM_LBUTTONDOWN = 0x201;
			uint WM_RBUTTONDOWN = 0x204;
			uint WM_MBUTTONDOWN = 0x207;

			if ( m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN || m.Msg == WM_MBUTTONDOWN )
			{	
				if ( m.Msg != WM_LBUTTONDOWN )
					this.Close();
				SendMessage( control.Handle, m.Msg, m.WParam, m.LParam );
			}
			else
			{
				base.WndProc (ref m);
			}
		}
	}
}
