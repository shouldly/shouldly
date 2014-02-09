// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Util;
using NUnit.Core;

namespace NUnit.UiKit
{
    public class ScrollingTextDisplayForm : Form
    {
		private System.Windows.Forms.Button okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.Label message;

		public ScrollingTextDisplayForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.textBox = new System.Windows.Forms.RichTextBox();
            this.message = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(284, 441);
            this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(90, 27);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Close";
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.Location = new System.Drawing.Point(10, 65);
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(640, 362);
            this.textBox.TabIndex = 3;
			this.textBox.Text = "";
			// 
			// message
			// 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.message.Location = new System.Drawing.Point(19, 9);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(631, 46);
            this.message.TabIndex = 2;
            this.message.Text = "";
            // 
			// TestAssemblyInfoForm
			// 
            this.ClientSize = new System.Drawing.Size(669, 480);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.message);
			this.Controls.Add(this.okButton);
			this.Name = "ScrollingTextDisplayForm";
			this.Text = "NUnit";
			this.Resize += new System.EventHandler(this.ScrollingTextDisplayForm_Resize);
			this.ResumeLayout(false);

		}
		#endregion

        protected RichTextBox TextBox
        {
            get { return textBox; }
        }

        protected Label Message
        {
            get { return message; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetMessageLabelSize();
        }

        private void ScrollingTextDisplayForm_Resize(object sender, System.EventArgs e)
        {
            SetMessageLabelSize();
        }

        private void SetMessageLabelSize()
        {
            Rectangle rect = message.ClientRectangle;
            Graphics g = Graphics.FromHwnd(Handle);
            SizeF sizeNeeded = g.MeasureString(message.Text, message.Font, rect.Width);
            int delta = sizeNeeded.ToSize().Height - rect.Height;

            message.Height += delta;
            textBox.Top += delta;
            textBox.Height -= delta;
        }
    }
}
