// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.ConsoleRunner.Tests
{
	using System;
	using System.IO;
	using System.Reflection;
	using NUnit.Framework;

	[TestFixture]
	public class CommandLineTests
	{
		[Test]
		public void NoParametersCount()
		{
			ConsoleOptions options = new ConsoleOptions();
			Assert.IsTrue(options.NoArgs);
		}

		[Test]
		public void AllowForwardSlashDefaultsCorrectly()
		{
			ConsoleOptions options = new ConsoleOptions();
			Assert.AreEqual( Path.DirectorySeparatorChar != '/', options.AllowForwardSlash );
		}

		[TestCase( "nologo", "nologo")]
		[TestCase( "help", "help" )]
		[TestCase( "help", "?" )]
		[TestCase( "wait", "wait" )]
		[TestCase( "xmlConsole", "xmlConsole")]
		[TestCase( "labels", "labels")]
		[TestCase( "noshadow", "noshadow" )]
		[TestCase( "nothread", "nothread" )]
		public void BooleanOptionAreRecognized( string fieldName, string option )
		{
			FieldInfo field = typeof(ConsoleOptions).GetField( fieldName );
			Assert.IsNotNull( field, "Field '{0}' not found", fieldName );
			Assert.AreEqual( typeof(bool), field.FieldType, "Field '{0}' is wrong type", fieldName );

			ConsoleOptions options = new ConsoleOptions( "-" + option );
			Assert.AreEqual( true, (bool)field.GetValue( options ), "Didn't recognize -" + option );
			options = new ConsoleOptions( "--" + option );
			Assert.AreEqual( true, (bool)field.GetValue( options ), "Didn't recognize --" + option );
			options = new ConsoleOptions( false, "/" + option );
			Assert.AreEqual( false, (bool)field.GetValue( options ), "Incorrectly recognized /" + option );
			options = new ConsoleOptions( true, "/" + option );
			Assert.AreEqual( true, (bool)field.GetValue( options ), "Didn't recognize /" + option );
		}

		[TestCase( "fixture", "fixture" )]
		[TestCase( "config", "config")]
        [TestCase( "result", "result")]
		[TestCase( "result", "xml" )]
		[TestCase( "output", "output" )]
		[TestCase( "output", "out" )]
		[TestCase( "err", "err" )]
        [TestCase( "include", "include" )]
		[TestCase( "exclude", "exclude" )]
        [TestCase("run", "run")]
        [TestCase("runlist", "runlist")]
        [TestCase("basepath", "basepath")]
        [TestCase("privatebinpath", "privatebinpath")]
		public void StringOptionsAreRecognized( string fieldName, string option )
		{
			FieldInfo field = typeof(ConsoleOptions).GetField( fieldName );
			Assert.IsNotNull( field, "Field {0} not found", fieldName );
			Assert.AreEqual( typeof(string), field.FieldType );

			ConsoleOptions options = new ConsoleOptions( "-" + option + ":text" );
			Assert.AreEqual( "text", (string)field.GetValue( options ), "Didn't recognize -" + option );
			options = new ConsoleOptions( "--" + option + ":text" );
			Assert.AreEqual( "text", (string)field.GetValue( options ), "Didn't recognize --" + option );
			options = new ConsoleOptions( false, "/" + option + ":text" );
			Assert.AreEqual( null, (string)field.GetValue( options ), "Incorrectly recognized /" + option );
			options = new ConsoleOptions( true, "/" + option + ":text" );
			Assert.AreEqual( "text", (string)field.GetValue( options ), "Didn't recognize /" + option );
		}

        [TestCase("domain")]
        [TestCase("trace")]
		public void EnumOptionsAreRecognized( string fieldName )
		{
			FieldInfo field = typeof(ConsoleOptions).GetField( fieldName );
			Assert.IsNotNull( field, "Field {0} not found", fieldName );
			Assert.IsTrue( field.FieldType.IsEnum, "Field {0} is not an enum", fieldName );
		}

		[Test]
		public void AssemblyName()
		{
			ConsoleOptions options = new ConsoleOptions( "nunit.tests.dll" );
			Assert.AreEqual( "nunit.tests.dll", options.Parameters[0] );
		}

		[Test]
		public void FixtureNamePlusAssemblyIsValid()
		{
			ConsoleOptions options = new ConsoleOptions( "-fixture:NUnit.Tests.AllTests", "nunit.tests.dll" );
			Assert.AreEqual("nunit.tests.dll", options.Parameters[0]);
			Assert.AreEqual("NUnit.Tests.AllTests", options.fixture);
			Assert.IsTrue(options.Validate());
		}

		[Test]
		public void AssemblyAloneIsValid()
		{
			ConsoleOptions options = new ConsoleOptions( "nunit.tests.dll" );
			Assert.IsTrue(options.Validate(), "command line should be valid");
		}

		[Test]
		public void InvalidOption()
		{
			ConsoleOptions options = new ConsoleOptions( "-asembly:nunit.tests.dll" );
			Assert.IsFalse(options.Validate());
		}


		[Test]
		public void NoFixtureNameProvided()
		{
			ConsoleOptions options = new ConsoleOptions( "-fixture:", "nunit.tests.dll" );
			Assert.IsFalse(options.Validate());
		}

		[Test] 
		public void InvalidCommandLineParms()
		{
			ConsoleOptions options = new ConsoleOptions( "-garbage:TestFixture", "-assembly:Tests.dll" );
			Assert.IsFalse(options.Validate());
		}

		[Test]
		public void XmlParameter()
		{
			ConsoleOptions options = new ConsoleOptions( "tests.dll", "-xml:results.xml" );
			Assert.IsTrue(options.ParameterCount == 1, "assembly should be set");
			Assert.AreEqual("tests.dll", options.Parameters[0]);
			Assert.AreEqual("results.xml", options.result);
		}

		[Test]
		public void XmlParameterWithFullPath()
		{
			ConsoleOptions options = new ConsoleOptions( "tests.dll", "-xml:C:/nunit/tests/bin/Debug/console-test.xml" );
			Assert.IsTrue(options.ParameterCount == 1, "assembly should be set");
			Assert.AreEqual("tests.dll", options.Parameters[0]);
			Assert.AreEqual("C:/nunit/tests/bin/Debug/console-test.xml", options.result);
		}

		[Test]
		public void XmlParameterWithFullPathUsingEqualSign()
		{
			ConsoleOptions options = new ConsoleOptions( "tests.dll", "-xml=C:/nunit/tests/bin/Debug/console-test.xml" );
			Assert.IsTrue(options.ParameterCount == 1, "assembly should be set");
			Assert.AreEqual("tests.dll", options.Parameters[0]);
			Assert.AreEqual("C:/nunit/tests/bin/Debug/console-test.xml", options.result);
		}

		[Test]
		public void FileNameWithoutXmlParameterLooksLikeParameter()
		{
			ConsoleOptions options = new ConsoleOptions( "tests.dll", "result.xml" );
			Assert.IsTrue(options.Validate());
			Assert.AreEqual(2, options.Parameters.Count);
		}

		[Test]
		public void XmlParameterWithoutFileNameIsInvalid()
		{
			ConsoleOptions options = new ConsoleOptions( "tests.dll", "-xml:" );
			Assert.IsFalse(options.Validate());			
		}

		[Test]
		public void HelpTextUsesCorrectDelimiterForPlatform()
		{
			string helpText = new ConsoleOptions().GetHelpText();
			char delim = System.IO.Path.DirectorySeparatorChar == '/' ? '-' : '/';

			string expected = string.Format( "{0}output=", delim );
			StringAssert.Contains( expected, helpText );
			
			expected = string.Format( "{0}out=", delim );
			StringAssert.Contains( expected, helpText );
		}
	}
}
