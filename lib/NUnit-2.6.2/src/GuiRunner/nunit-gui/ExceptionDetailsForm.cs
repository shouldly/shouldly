// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NUnit.Gui
{
	/// <summary>
	/// Summary description for ExceptionDetailsForm.
	/// </summary>
	public class ExceptionDetailsForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RichTextBox stackTrace;
		private System.Windows.Forms.Label message;
		private Exception exception;

		public ExceptionDetailsForm( Exception exception )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.exception = exception;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.okButton = new System.Windows.Forms.Button();
			this.stackTrace = new System.Windows.Forms.RichTextBox();
			this.message = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(434, 512);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(90, 27);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			// 
			// stackTrace
			// 
			this.stackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.stackTrace.Location = new System.Drawing.Point(10, 65);
			this.stackTrace.Name = "stackTrace";
			this.stackTrace.ReadOnly = true;
			this.stackTrace.Size = new System.Drawing.Size(940, 433);
			this.stackTrace.TabIndex = 3;
			this.stackTrace.Text = "";
			// 
			// message
			// 
			this.message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.message.Location = new System.Drawing.Point(19, 9);
			this.message.Name = "message";
			this.message.Size = new System.Drawing.Size(931, 46);
			this.message.TabIndex = 2;
			// 
			// ExceptionDetailsForm
			// 
			this.ClientSize = new System.Drawing.Size(969, 551);
			this.Controls.Add(this.stackTrace);
			this.Controls.Add(this.message);
			this.Controls.Add(this.okButton);
			this.Name = "ExceptionDetailsForm";
			this.Text = "Exception Details";
			this.Resize += new System.EventHandler(this.ExceptionDetailsForm_Resize);
			this.Load += new System.EventHandler(this.ExceptionDetailsForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ExceptionDetailsForm_Load(object sender, System.EventArgs e)
		{
			//TODO: Put some of this in the constructor for easier testing?
			this.message.Text = FormatMessage( exception );
			SetMessageLabelSize();

			this.stackTrace.Text = FormatStackTrace( exception );
		}

		private string FormatMessage( Exception exception )
		{
			StringBuilder sb = new StringBuilder();

			for( Exception ex = exception; ex != null; ex = ex.InnerException )
			{
				if ( ex != exception ) sb.Append( "\r\n----> " );
				sb.Append( ex.GetType().ToString() );
				sb.Append( ": " );
				sb.Append( ex.Message );
			}

			return sb.ToString();
		}

		private string FormatStackTrace( Exception exception )
		{
			StringBuilder sb = new StringBuilder();
			AppendStackTrace( sb, exception );

			return sb.ToString();
		}

		private void AppendStackTrace( StringBuilder sb, Exception ex )
		{
			if ( ex.InnerException != null )
				AppendStackTrace( sb, ex.InnerException );

			sb.Append( ex.GetType().ToString() );
			sb.Append( "...\r\n" );
			sb.Append( ex.StackTrace );
			sb.Append( "\r\n\r\n" );
		}

		private void ExceptionDetailsForm_Resize(object sender, System.EventArgs e)
		{
			SetMessageLabelSize();
		}

		private void SetMessageLabelSize()
		{
			Rectangle rect = message.ClientRectangle;
			Graphics g = Graphics.FromHwnd( Handle );
			SizeF sizeNeeded = g.MeasureString( message.Text, message.Font, rect.Width );
			int delta = sizeNeeded.ToSize().Height - rect.Height;
			
			message.Height += delta;
			stackTrace.Top += delta;
			stackTrace.Height -= delta;
		}
	}
}