// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace NUnit.Gui
{
	public class AboutBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label copyright;
		private System.Windows.Forms.Label dotNetVersionLabel;
		private System.Windows.Forms.Label clrTypeLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string versionText = executingAssembly.GetName().Version.ToString();

            object [] objectAttrs = executingAssembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
            if ( objectAttrs.Length > 0 )
				versionText = ((AssemblyInformationalVersionAttribute)objectAttrs[0]).InformationalVersion;

			objectAttrs = executingAssembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
			if ( objectAttrs.Length > 0 )
			{
				string configText = ((AssemblyConfigurationAttribute)objectAttrs[0]).Configuration;
                if ( configText != "" )
				    versionText += string.Format(" ({0})",configText);
			}
			
			string copyrightText = "Copyright (C) 2002-2012 Charlie Poole.\r\nCopyright (C) 2002-2004 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov.\r\nCopyright (C) 2000-2002 Philip Craig.\r\nAll Rights Reserved.";
			objectAttrs = executingAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
			if ( objectAttrs.Length > 0 )
				copyrightText = ((AssemblyCopyrightAttribute)objectAttrs[0]).Copyright;

			versionLabel.Text = versionText;
			copyright.Text = copyrightText;
			dotNetVersionLabel.Text = NUnit.Core.RuntimeFramework.CurrentFramework.DisplayName;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.OkButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.copyright = new System.Windows.Forms.Label();
            this.clrTypeLabel = new System.Windows.Forms.Label();
            this.dotNetVersionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Location = new System.Drawing.Point(368, 304);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(96, 29);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "NUnit Version:";
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(164, 240);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(156, 23);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "label2";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Developers:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(164, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(287, 48);
            this.label3.TabIndex = 4;
            this.label3.Text = "James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Philip Craig, Ethan Smith," +
                " Doug de la Torre, Charlie Poole";
            // 
            // linkLabel1
            // 
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 21);
            this.linkLabel1.Location = new System.Drawing.Point(164, 112);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(266, 16);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.nunit.org ";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(31, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Information:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(31, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 29);
            this.label5.TabIndex = 7;
            this.label5.Text = "Thanks to:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(164, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(215, 29);
            this.label6.TabIndex = 8;
            this.label6.Text = "Kent Beck and Erich Gamma";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(31, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 28);
            this.label7.TabIndex = 9;
            this.label7.Text = "Copyright:";
            // 
            // copyright
            // 
            this.copyright.Location = new System.Drawing.Point(164, 20);
            this.copyright.Name = "copyright";
            this.copyright.Size = new System.Drawing.Size(297, 84);
            this.copyright.TabIndex = 10;
            this.copyright.Text = "label8";
            // 
            // clrTypeLabel
            // 
            this.clrTypeLabel.Location = new System.Drawing.Point(31, 272);
            this.clrTypeLabel.Name = "clrTypeLabel";
            this.clrTypeLabel.Size = new System.Drawing.Size(127, 23);
            this.clrTypeLabel.TabIndex = 11;
            this.clrTypeLabel.Text = "Framework Version:";
            // 
            // dotNetVersionLabel
            // 
            this.dotNetVersionLabel.Location = new System.Drawing.Point(164, 272);
            this.dotNetVersionLabel.Name = "dotNetVersionLabel";
            this.dotNetVersionLabel.Size = new System.Drawing.Size(284, 23);
            this.dotNetVersionLabel.TabIndex = 12;
            this.dotNetVersionLabel.Text = "label9";
            // 
            // AboutBox
            // 
            this.AcceptButton = this.OkButton;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(490, 346);
            this.Controls.Add(this.dotNetVersionLabel);
            this.Controls.Add(this.clrTypeLabel);
            this.Controls.Add(this.copyright);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About NUnit";
            this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://nunit.org");
			linkLabel1.LinkVisited = true;
        }
	}
}
