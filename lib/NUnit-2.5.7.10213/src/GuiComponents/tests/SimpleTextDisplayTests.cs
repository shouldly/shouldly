// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;

namespace NUnit.UiKit.Tests
{
	/// <summary>
	/// Summary description for SimpleTextDisplayTests.
	/// </summary>
	[TestFixture]
	public class SimpleTextDisplayTests
	{
		SimpleTextDisplay textDisplay;
		static readonly string NL = Environment.NewLine;
		static readonly string myText = "Here is one line" + NL + "Here is another" + NL;
		static readonly string fiveLines = 
			"This is line 1" + NL + "This is line 2" + NL + "This is line 3" + NL + "This is line 4" + NL +"This is line 5" + NL;
		static readonly string twoParts =
			"This line written in two parts" + NL + "The final line" + NL;
		static readonly string threeParts =
			"This line written in three parts" + NL + "The final line" + NL;

		[SetUp]
		public void Init()
		{
			textDisplay = new SimpleTextDisplay(); 
		}

		[TearDown]
		public void CleanUp()
		{
			textDisplay.Dispose();
		}

		private void AppendLines( int count )
		{
			for( int index = 1; index <= count; ++index )
				textDisplay.WriteLine( string.Format( "This is line {0}", index ) );
		}

		private Size getTextSize( string text )
		{
			return Graphics.FromHwnd( textDisplay.Handle ).MeasureString(text, textDisplay.Font ).ToSize();
		}

		[Test]
		public void SetText_BeforeCreation()
		{
			textDisplay.Write( myText );
			Assert.AreEqual( myText, textDisplay.GetText() );
			Assert.AreEqual( Size.Empty, textDisplay.AutoScrollMinSize );
			textDisplay.CreateControl();
			Assert.AreEqual( myText, textDisplay.GetText() );
			Assert.AreEqual( getTextSize( myText ), textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void SetText_AfterCreation()
		{
			textDisplay.CreateControl();
			textDisplay.Write( myText );
			Assert.AreEqual( myText, textDisplay.GetText() );
			Assert.AreEqual( getTextSize( myText ), textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void ClearText_BeforeCreation()
		{
			textDisplay.Write( "text that should be cleared" );
			textDisplay.Clear();

			Assert.AreEqual( "", textDisplay.GetText() );
			Assert.AreEqual( Size.Empty, textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void ClearText_AfterCreation()
		{
			textDisplay.Write( "text that should be cleared" );
			textDisplay.CreateControl();
			textDisplay.Clear();

			Assert.AreEqual( "", textDisplay.GetText() );
			Assert.AreEqual( Size.Empty, textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void AppendText_BeforeCreation()
		{
			AppendLines( 5 );
			textDisplay.Write( "This line written" );
			textDisplay.WriteLine( " in two parts" );
			textDisplay.WriteLine( "The final line" );

			string expected = fiveLines + twoParts;
			Assert.AreEqual( expected, textDisplay.GetText() );
			Assert.AreEqual( Size.Empty, textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void AppendText_AfterCreation()
		{
			textDisplay.CreateControl();
			AppendLines( 5 );
			Assert.AreEqual( getTextSize( textDisplay.GetText() ), textDisplay.AutoScrollMinSize );
			textDisplay.Write( "This line written" );
			Assert.AreEqual( getTextSize( textDisplay.GetText() ), textDisplay.AutoScrollMinSize );
			textDisplay.Write( " in three" );
			Assert.AreEqual( getTextSize( textDisplay.GetText() ), textDisplay.AutoScrollMinSize );
			textDisplay.WriteLine( " parts" );
			Assert.AreEqual( getTextSize( textDisplay.GetText() ), textDisplay.AutoScrollMinSize );
			textDisplay.WriteLine( "The final line" );
			Assert.AreEqual( getTextSize( textDisplay.GetText() ), textDisplay.AutoScrollMinSize );

			string expected = fiveLines + threeParts;
			Assert.AreEqual( expected, textDisplay.GetText() );
			Assert.AreEqual( getTextSize( expected ), textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void AppendText_BeforeAndAfterCreation()
		{
			AppendLines( 5 );
			textDisplay.Write( "This line written" );
			textDisplay.CreateControl();
			textDisplay.WriteLine( " in two parts" );
			textDisplay.WriteLine( "The final line" );

			string expected = fiveLines + twoParts;
			Assert.AreEqual( expected, textDisplay.GetText() );
			Assert.AreEqual( getTextSize( expected ), textDisplay.AutoScrollMinSize );
		}

		[Test]
		public void StressTest()
		{
			textDisplay.CreateControl();
			DateTime startTime = DateTime.Now;
			AppendLines( 1000 );
			DateTime endTime = DateTime.Now;
			Assert.AreEqual( 9*14 + 90*15 + 900*16 + 17 + 1000*NL.Length, textDisplay.GetText().Length );
			Assert.AreEqual( getTextSize( textDisplay.GetText() ), textDisplay.AutoScrollMinSize );
		}
	}
}
