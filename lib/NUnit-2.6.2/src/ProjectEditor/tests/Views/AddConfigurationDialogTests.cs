// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.TestUtilities;

namespace NUnit.ProjectEditor.Tests.Views
{
	[TestFixture]
	public class AddConfigurationDialogTests : FormTester
	{
		private AddConfigurationDialog dlg;

		[SetUp]
		public void SetUp()
		{
            dlg = new AddConfigurationDialog();
            dlg.ConfigList = new string[] { "Debug", "Release" };
            this.Form = dlg;
		}

		[TearDown]
		public void TearDown()
		{
			dlg.Close();
		}

        [Test]
        public void CheckForControls()
        {
            AssertControlExists("configurationNameTextBox", typeof(TextBox));
            AssertControlExists("configurationComboBox", typeof(ComboBox));
            AssertControlExists("okButton", typeof(Button));
            AssertControlExists("cancelButton", typeof(Button));
        }

        [Test]
        public void TextBox_OnLoad_IsEmpty()
        {
            TextBox configBox = TextBoxes["configurationNameTextBox"];
            Assert.AreEqual("", configBox.Text);
        }

        [Test]
        public void ComboBox_OnLoad_IsInitializedCorrectly()
        {
            ComboBox combo = Combos["configurationComboBox"];
            dlg.Show();
            Assert.AreEqual(3, combo.Items.Count);
            Assert.AreEqual("<none>", combo.Items[0]);
            Assert.AreEqual("Debug", combo.Items[1]);
            Assert.AreEqual("Release", combo.Items[2]);
            Assert.AreEqual("<none>", combo.SelectedItem);
        }

        [Test]
        public void TestSimpleEntry()
        {
            dlg.Show();
            TextBox config = TextBoxes["configurationNameTextBox"];
            Button okButton = Buttons["okButton"];
            config.Text = "Super";
            okButton.PerformClick();
            Assert.AreEqual("Super", dlg.ConfigToCreate);
            Assert.AreEqual(null, dlg.ConfigToCopy);
        }

        [Test]
        public void TestComplexEntry()
        {
            dlg.Show();
            TextBox config = TextBoxes["configurationNameTextBox"];
            Button okButton = Buttons["okButton"];
            ComboBox combo = Combos["configurationComboBox"];

            config.Text = "Super";
            combo.SelectedIndex = combo.FindStringExact("Release");

            okButton.PerformClick();
            Assert.AreEqual("Super", dlg.ConfigToCreate);
            Assert.AreEqual("Release", dlg.ConfigToCopy);
        }
	}
}
