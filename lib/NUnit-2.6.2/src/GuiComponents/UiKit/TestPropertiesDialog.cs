// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Collections;
using NUnit.Core;

namespace NUnit.UiKit
{
    public partial class TestPropertiesDialog : Form
    {
        private TestSuiteTreeNode node;
        private ITest test;
        private TestResult result;
        private int maxY;
        private int nextY;
        private int clientWidth;

        private Image pinnedImage;
        private Image unpinnedImage;

        public TestPropertiesDialog(TestSuiteTreeNode node)
        {
            InitializeComponent();

            this.node = node;
        }

        #region Properties

        [Browsable(false)]
        public bool Pinned
        {
            get { return pinButton.Checked; }
            set { pinButton.Checked = value; }
        }

        #endregion

        #region Public Methods

        public void DisplayProperties()
        {
            DisplayProperties(this.node);
        }

        public void DisplayProperties(TestSuiteTreeNode node)
        {
            this.node = node;
            this.test = node.Test;
            this.result = node.Result;

            SetTitleBarText();

            testResult.Text = node.StatusText;
            testResult.Font = new Font(this.Font, FontStyle.Bold);
            if (node.TestType == "Project" || node.TestType == "Assembly")
                testName.Text = Path.GetFileName(test.TestName.Name);
            else
                testName.Text = test.TestName.Name;

            testType.Text = node.TestType;
            fullName.Text = test.TestName.FullName;
            description.Text = test.Description;

            StringBuilder sb1 = new StringBuilder();
            foreach (string cat in test.Categories)
                if (sb1.Length > 0)
                {
                    sb1.Append(", ");
                    sb1.Append(cat);
                }
            categories.Text = sb1.ToString();

            testCaseCount.Text = test.TestCount.ToString();

            switch (test.RunState)
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
            ignoreReason.Text = test.IgnoreReason;

            FillPropertyList();

            elapsedTime.Text = "Execution Time:";
            assertCount.Text = "Assert Count:";
            message.Text = "";
            stackTrace.Text = "";

            if (result != null)
            {
                elapsedTime.Text = string.Format("Execution Time: {0}", result.Time);

                assertCount.Text = string.Format("Assert Count: {0}", result.AssertCount);
                // message may have a leading blank line
                // TODO: take care of this in label?
                if (result.Message != null)
                {
                    if (result.Message.Length > 64000)
                        message.Text = TrimLeadingBlankLines(result.Message.Substring(0, 64000));
                    else
                        message.Text = TrimLeadingBlankLines(result.Message);
                }

                stackTrace.Text = result.StackTrace;
            }

            BeginPanel();

            CreateRow(testTypeLabel, testType);
            CreateRow(fullNameLabel, fullName);
            CreateRow(descriptionLabel, description);
            CreateRow(categoriesLabel, categories);
            CreateRow(testCaseCountLabel, testCaseCount, shouldRunLabel, shouldRun);
            CreateRow(ignoreReasonLabel, ignoreReason);
            CreateRow(propertiesLabel, properties);
            CreateRow(hiddenProperties);

            groupBox1.ClientSize = new Size(
                groupBox1.ClientSize.Width, maxY + 12);

            groupBox2.Location = new Point(
                groupBox1.Location.X, groupBox1.Bottom + 12);

            BeginPanel();

            CreateRow(elapsedTime, assertCount);
            CreateRow(messageLabel, message);
            CreateRow(stackTraceLabel, stackTrace);

            groupBox2.ClientSize = new Size(
                groupBox2.ClientSize.Width, this.maxY + 12);

            this.ClientSize = new Size(
                this.ClientSize.Width, groupBox2.Bottom + 12);
        }

        private void FillPropertyList()
        {
            properties.Items.Clear();
            foreach (DictionaryEntry entry in test.Properties)
            {
                if (hiddenProperties.Checked || !entry.Key.ToString().StartsWith("_"))
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

                        properties.Items.Add(entry.Key.ToString() + "=" + sb.ToString());
                    }
                    else
                        properties.Items.Add(entry.Key.ToString() + "=" + entry.Value.ToString());
                }
            }
        }

        #endregion

        #region Event Handlers and Overrides

        private void TestPropertiesDialog_Load(object sender, System.EventArgs e)
        {
            pinnedImage = new Bitmap(typeof(TestPropertiesDialog), "Images.pinned.gif");
            unpinnedImage = new Bitmap(typeof(TestPropertiesDialog), "Images.unpinned.gif");
            pinButton.Image = unpinnedImage;

            if (!this.DesignMode)
                DisplayProperties();

            node.TreeView.AfterSelect += new TreeViewEventHandler(OnSelectedNodeChanged);
        }

        private void pinButton_Click(object sender, System.EventArgs e)
        {
            if (pinButton.Checked)
                pinButton.Image = pinnedImage;
            else
                pinButton.Image = unpinnedImage;
        }

        private void TestPropertiesDialog_SizeChanged(object sender, EventArgs e)
        {
            if (clientWidth != this.ClientSize.Width)
            {
				if (this.node != null)
                	this.DisplayProperties();
                clientWidth = this.ClientSize.Width;
            }
        }

        private void TestPropertiesDialog_ResizeEnd(object sender, EventArgs e)
        {
            this.ClientSize = new Size(
                this.ClientSize.Width, groupBox2.Bottom + 12);

            clientWidth = this.ClientSize.Width;
        }

        private void OnSelectedNodeChanged(object sender, TreeViewEventArgs e)
        {
            if (pinButton.Checked)
            {
                DisplayProperties((TestSuiteTreeNode)e.Node);
            }
            else
                this.Close();
        }

        protected override bool ProcessKeyPreview(ref System.Windows.Forms.Message m)
        {
            const int ESCAPE = 27;
            const int WM_CHAR = 258;

            if (m.Msg == WM_CHAR && m.WParam.ToInt32() == ESCAPE)
            {
                this.Close();
                return true;
            }

            return base.ProcessKeyEventArgs(ref m);
        }

        private void hiddenProperties_CheckedChanged(object sender, EventArgs e)
        {
            FillPropertyList();
        }

        #endregion

        #region Helper Methods

        private void SetTitleBarText()
        {
            string name = test.TestName.Name;
            int index = name.LastIndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            if (index >= 0)
                name = name.Substring(index + 1);
            this.Text = string.Format("{0} Properties - {1}", node.TestType, name);
        }

        private void BeginPanel()
        {
            this.maxY = 20;
            this.nextY = 24;
        }

        private void SizeToFitText(Label label)
        {
            string text = label.Text;
            if (text == "") 
                text = "Ay"; // Include descender to be sure of size

            Graphics g = Graphics.FromHwnd(label.Handle);
            SizeF size = g.MeasureString(text, label.Font, label.Parent.ClientSize.Width - label.Left - 8);
            label.ClientSize = new Size(
                (int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
        }

        private void CreateRow(params Control[] controls)
        {
            this.nextY = this.maxY + 4;

            foreach (Control control in controls)
            {
                InsertInRow(control);
            }
        }

        private void InsertInRow(Control control)
        {
            Label label = control as Label;
            if (label != null)
                SizeToFitText(label);

            control.Location = new Point(control.Location.X, this.nextY);
            this.maxY = Math.Max(this.maxY, control.Bottom);
        }

        private string TrimLeadingBlankLines(string s)
        {
            if (s == null) return s;

            int start = 0;
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
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
            return start == 0 ? s : s.Substring(start);
        }

        #endregion
    }
}
