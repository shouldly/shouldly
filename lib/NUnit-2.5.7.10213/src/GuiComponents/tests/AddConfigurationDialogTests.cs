// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Util;
using NUnit.TestUtilities;

namespace NUnit.UiKit.Tests
{
	[TestFixture]
	public class AddConfigurationDialogTests : FormTester
	{
		private NUnitProject project;
		private AddConfigurationDialog dlg;

		[SetUp]
		public void SetUp()
		{
			project = new NUnitProject( "path" );
			project.Configs.Add( "Debug" );
			project.Configs.Add( "Release" );
			dlg = new AddConfigurationDialog( project );
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
			AssertControlExists( "configurationNameTextBox", typeof( TextBox ) );
			AssertControlExists( "configurationComboBox", typeof( ComboBox ) );
			AssertControlExists( "okButton", typeof( Button ) );
			AssertControlExists( "cancelButton", typeof( Button ) );
		}

		[Test]
		public void CheckTextBox()
		{
			TextBox configBox = TextBoxes["configurationNameTextBox"];
			Assert.AreEqual( "", configBox.Text );
		}

		[Test]
		public void CheckComboBox()
		{
			ComboBox combo = Combos["configurationComboBox"];
			dlg.Show();
			Assert.AreEqual( 3, combo.Items.Count );
			Assert.AreEqual( "<none>", combo.Items[0] );
			Assert.AreEqual( "Debug", combo.Items[1] );
			Assert.AreEqual( "Release", combo.Items[2] );
			Assert.AreEqual( "Debug", combo.SelectedItem );
		}

		[Test]
		public void TestSimpleEntry()
		{
			dlg.Show();
			TextBox config = TextBoxes["configurationNameTextBox"];
			Button okButton = Buttons["okButton"];
			config.Text = "Super";
			okButton.PerformClick();
			Assert.AreEqual( "Super", dlg.ConfigurationName );
			Assert.AreEqual( "Debug", dlg.CopyConfigurationName );
		}

		[Test]
		public void TestComplexEntry()
		{
			dlg.Show();
			TextBox config = TextBoxes["configurationNameTextBox"];
			Button okButton = Buttons["okButton"];
			ComboBox combo = Combos["configurationComboBox"];

			config.Text = "Super";
			combo.SelectedIndex = combo.FindStringExact( "<none>" );

			okButton.PerformClick();
			Assert.AreEqual( "Super", dlg.ConfigurationName );
			Assert.IsNull( dlg.CopyConfigurationName );
		}
	}
}
