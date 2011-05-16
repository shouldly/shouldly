// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Collections;
using NUnit.Core;

namespace NUnit.UiKit
{
	/// <summary>
	/// Summary description for TestPropertiesDialog.
	/// </summary>
	public class TestPropertiesDialog : System.Windows.Forms.Form
	{
		#region Instance Variables;

		private TestSuiteTreeNode node;
		private ITest test;
		private TestResult result;

		private Image pinnedImage;
		private Image unpinnedImage;
		private System.Windows.Forms.CheckBox pinButton;
		private System.Windows.Forms.Label testResult;
		private System.Windows.Forms.Label testName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label7;
		private CP.Windows.Forms.ExpandingLabel description;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label testCaseCount;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label shouldRun;
		private System.Windows.Forms.Label label2;
		private CP.Windows.Forms.ExpandingLabel fullName;
		private System.Windows.Forms.Label label1;
		private CP.Windows.Forms.ExpandingLabel ignoreReason;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label testType;
		private System.Windows.Forms.Label label3;
		private CP.Windows.Forms.ExpandingLabel stackTrace;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label elapsedTime;
		private CP.Windows.Forms.ExpandingLabel message;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label assertCount;
		private System.Windows.Forms.ListBox properties;
		private System.Windows.Forms.ListBox categories;
		private System.ComponentModel.IContainer components = null;

		#endregion

		#region Construction and Disposal

		public TestPropertiesDialog( TestSuiteTreeNode node )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.node = node;
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

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pinButton = new System.Windows.Forms.CheckBox();
            this.testResult = new System.Windows.Forms.Label();
            this.testName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.categories = new System.Windows.Forms.ListBox();
            this.properties = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ignoreReason = new CP.Windows.Forms.ExpandingLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.testType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.description = new CP.Windows.Forms.ExpandingLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.testCaseCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.shouldRun = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fullName = new CP.Windows.Forms.ExpandingLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stackTrace = new CP.Windows.Forms.ExpandingLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.assertCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.elapsedTime = new System.Windows.Forms.Label();
            this.message = new CP.Windows.Forms.ExpandingLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pinButton
            // 
            this.pinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pinButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.pinButton.Location = new System.Drawing.Point(440, 8);
            this.pinButton.Name = "pinButton";
            this.pinButton.Size = new System.Drawing.Size(20, 20);
            this.pinButton.TabIndex = 14;
            this.pinButton.Click += new System.EventHandler(this.pinButton_Click);
            this.pinButton.CheckedChanged += new System.EventHandler(this.pinButton_CheckedChanged);
            // 
            // testResult
            // 
            this.testResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.testResult.Location = new System.Drawing.Point(16, 16);
            this.testResult.Name = "testResult";
            this.testResult.Size = new System.Drawing.Size(120, 16);
            this.testResult.TabIndex = 46;
            this.testResult.Text = "Failure";
            this.testResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // testName
            // 
            this.testName.Location = new System.Drawing.Point(144, 16);
            this.testName.Name = "testName";
            this.testName.Size = new System.Drawing.Size(280, 16);
            this.testName.TabIndex = 49;
            this.testName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.categories);
            this.groupBox1.Controls.Add(this.properties);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.ignoreReason);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.testType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.description);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.testCaseCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.shouldRun);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.fullName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 376);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Details";
            // 
            // categories
            // 
            this.categories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.categories.ItemHeight = 16;
            this.categories.Location = new System.Drawing.Point(104, 152);
            this.categories.Name = "categories";
            this.categories.Size = new System.Drawing.Size(320, 52);
            this.categories.TabIndex = 58;
            // 
            // properties
            // 
            this.properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.properties.ItemHeight = 16;
            this.properties.Location = new System.Drawing.Point(104, 304);
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(320, 52);
            this.properties.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(24, 312);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 16);
            this.label11.TabIndex = 56;
            this.label11.Text = "Properties:";
            // 
            // ignoreReason
            // 
            this.ignoreReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ignoreReason.CopySupported = true;
            this.ignoreReason.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Vertical;
            this.ignoreReason.Location = new System.Drawing.Point(112, 248);
            this.ignoreReason.Name = "ignoreReason";
            this.ignoreReason.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ignoreReason.Size = new System.Drawing.Size(312, 48);
            this.ignoreReason.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 248);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 43;
            this.label5.Text = "Reason:";
            // 
            // testType
            // 
            this.testType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.testType.Location = new System.Drawing.Point(112, 32);
            this.testType.Name = "testType";
            this.testType.Size = new System.Drawing.Size(312, 16);
            this.testType.TabIndex = 55;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(24, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 54;
            this.label8.Text = "Test Type:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 52;
            this.label7.Text = "Categories:";
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.description.CopySupported = true;
            this.description.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Both;
            this.description.Location = new System.Drawing.Point(112, 96);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(312, 48);
            this.description.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(24, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 50;
            this.label6.Text = "Description:";
            // 
            // testCaseCount
            // 
            this.testCaseCount.Location = new System.Drawing.Point(112, 216);
            this.testCaseCount.Name = "testCaseCount";
            this.testCaseCount.Size = new System.Drawing.Size(48, 15);
            this.testCaseCount.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Test Count:";
            // 
            // shouldRun
            // 
            this.shouldRun.Location = new System.Drawing.Point(304, 216);
            this.shouldRun.Name = "shouldRun";
            this.shouldRun.Size = new System.Drawing.Size(88, 15);
            this.shouldRun.TabIndex = 47;
            this.shouldRun.Text = "Yes";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(188, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 46;
            this.label2.Text = "Should Run?";
            // 
            // fullName
            // 
            this.fullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fullName.CopySupported = true;
            this.fullName.Location = new System.Drawing.Point(112, 63);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(312, 17);
            this.fullName.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 44;
            this.label1.Text = "Full Name:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 47;
            this.label3.Text = "Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // stackTrace
            // 
            this.stackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stackTrace.CopySupported = true;
            this.stackTrace.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Both;
            this.stackTrace.Location = new System.Drawing.Point(112, 128);
            this.stackTrace.Name = "stackTrace";
            this.stackTrace.Size = new System.Drawing.Size(312, 49);
            this.stackTrace.TabIndex = 45;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.assertCount);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.elapsedTime);
            this.groupBox2.Controls.Add(this.message);
            this.groupBox2.Controls.Add(this.stackTrace);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(16, 432);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 192);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // assertCount
            // 
            this.assertCount.Location = new System.Drawing.Point(240, 32);
            this.assertCount.Name = "assertCount";
            this.assertCount.Size = new System.Drawing.Size(176, 16);
            this.assertCount.TabIndex = 61;
            this.assertCount.Text = "Assert Count:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(24, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 17);
            this.label10.TabIndex = 60;
            this.label10.Text = "Message:";
            // 
            // elapsedTime
            // 
            this.elapsedTime.Location = new System.Drawing.Point(24, 32);
            this.elapsedTime.Name = "elapsedTime";
            this.elapsedTime.Size = new System.Drawing.Size(192, 16);
            this.elapsedTime.TabIndex = 58;
            this.elapsedTime.Text = "Execution Time:";
            // 
            // message
            // 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.message.CopySupported = true;
            this.message.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Both;
            this.message.Location = new System.Drawing.Point(112, 63);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(312, 49);
            this.message.TabIndex = 57;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(24, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 16);
            this.label12.TabIndex = 56;
            this.label12.Text = "Stack:";
            // 
            // TestPropertiesDialog
            // 
            this.ClientSize = new System.Drawing.Size(472, 634);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.testName);
            this.Controls.Add(this.testResult);
            this.Controls.Add(this.pinButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestPropertiesDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Test Properties";
            this.Load += new System.EventHandler(this.TestPropertiesDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		#region Properties

		[Browsable( false )]
		public bool Pinned
		{
			get { return pinButton.Checked; }
			set { pinButton.Checked = value; }
		}

		#endregion

		#region Methods

		private void SetTitleBarText()
		{
			string name = test.TestName.Name;
			int index = name.LastIndexOfAny( new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar } );
			if ( index >= 0 )
				name = name.Substring( index + 1 );
			this.Text = string.Format( "{0} Properties - {1}", node.TestType, name );
		}

		/// <summary>
		/// Set up all dialog fields when it loads
		/// </summary>
		private void TestPropertiesDialog_Load(object sender, System.EventArgs e)
		{
			pinnedImage = new Bitmap( typeof( TestPropertiesDialog ), "pinned.gif" );
			unpinnedImage = new Bitmap( typeof( TestPropertiesDialog ), "unpinned.gif" );
			pinButton.Image = unpinnedImage;

			DisplayProperties();

			node.TreeView.AfterSelect += new TreeViewEventHandler( OnSelectedNodeChanged );	
		}

		private void OnSelectedNodeChanged( object sender, TreeViewEventArgs e )
		{
			if ( pinButton.Checked )
			{
				DisplayProperties( (TestSuiteTreeNode)e.Node );
			}
			else
				this.Close();
		}

		public void DisplayProperties( )
		{
			DisplayProperties( this.node );
		}

		public void DisplayProperties( TestSuiteTreeNode node)
		{
			this.node = node;
			this.test = node.Test;
			this.result = node.Result;

			SetTitleBarText();

			categories.Items.Clear();
			foreach( string cat in test.Categories )
				categories.Items.Add( cat );

			testResult.Text = node.StatusText;
			testName.Text = test.TestName.Name;

			testType.Text = node.TestType;
			fullName.Text = test.TestName.FullName;
			switch( test.RunState )
			{
				case RunState.Explicit:
					shouldRun.Text = "Explicit";
					break;
				case RunState.Runnable:
					shouldRun.Text = "Yes";
					break;
				default:
					shouldRun.Text = "No";
					break;
			}
			description.Text = test.Description;
			ignoreReason.Text = test.IgnoreReason;
			testCaseCount.Text = test.TestCount.ToString();
			properties.Items.Clear();
            foreach (DictionaryEntry entry in test.Properties)
            {
                if (entry.Value is ICollection)
                {
                    ICollection items = (ICollection)entry.Value;
                    if (items.Count == 0) continue;

                    StringBuilder sb = new StringBuilder();
                    foreach (object item in items)
                    {
                        if (sb.Length > 0)
                            sb.Append(",");
                        sb.Append(item.ToString());
                    }
                    
                    properties.Items.Add( entry.Key.ToString() + "=" +sb.ToString() );
                }
                else
                    properties.Items.Add( entry.Key.ToString() + "=" + entry.Value.ToString());
            }

			message.Text = "";
			elapsedTime.Text = "Execution Time:";
			assertCount.Text = "Assert Count:";
			stackTrace.Text = "";

			if ( result != null )
			{
				// message may have a leading blank line
				// TODO: take care of this in label?
                if (result.Message != null)
                {
                    if (result.Message.Length > 64000)
                        message.Text = TrimLeadingBlankLines(result.Message.Substring(0, 64000));
                    else
                        message.Text = TrimLeadingBlankLines(result.Message);
                }

				elapsedTime.Text = string.Format( "Execution Time: {0}", result.Time );
				assertCount.Text = string.Format( "Assert Count: {0}", result.AssertCount );
				stackTrace.Text = result.StackTrace;
			}
		}

		private string TrimLeadingBlankLines( string s )
		{
			if ( s == null ) return s;

			int start = 0;
			for( int i = 0; i < s.Length; i++ )
			{
				switch( s[i] )
				{
					case ' ':
					case '\t':
						break;
					case '\r':
					case '\n':
						start = i + 1;
						break;

					default:
						goto getout;
				}
			}

			getout:
			return start == 0 ? s : s.Substring( start );
		}

		protected override bool ProcessKeyPreview(ref System.Windows.Forms.Message m)
		{
			const int ESCAPE = 27;
			const int WM_CHAR = 258;

			if (m.Msg == WM_CHAR && m.WParam.ToInt32() == ESCAPE )
			{
				this.Close();
				return true;
			}

			return base.ProcessKeyEventArgs( ref m ); 
		}

		private void pinButton_Click(object sender, System.EventArgs e)
		{
			if ( pinButton.Checked )
				pinButton.Image = pinnedImage;
			else
				pinButton.Image = unpinnedImage;
		}

		private void pinButton_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}
	}

	#endregion
}
