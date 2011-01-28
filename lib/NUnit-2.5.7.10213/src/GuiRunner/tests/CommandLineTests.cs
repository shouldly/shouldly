// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Gui.Tests
{
	using System;
	using NUnit.Framework;

	[TestFixture]
	public class CommandLineTests
	{
		[Test]
		public void NoParametersCount()
		{
			GuiOptions options = new GuiOptions(new string[] {});
			Assert.IsTrue(options.NoArgs);
		}

		[Test]
		public void Help()
		{
			GuiOptions options = new GuiOptions(new string[] {"-help"});
			Assert.IsTrue(options.help);
		}

		[Test]
		public void ShortHelp()
		{
			GuiOptions options = new GuiOptions(new string[] {"-?"});
			Assert.IsTrue(options.help);
		}

		[Test]
		public void AssemblyName()
		{
			string assemblyName = "nunit.tests.dll";
			GuiOptions options = new GuiOptions(new string[]{ assemblyName });
			Assert.AreEqual(assemblyName, options.Parameters[0]);
		}

		[Test]
		public void ValidateSuccessful()
		{
			GuiOptions options = new GuiOptions(new string[] { "nunit.tests.dll" });
			Assert.IsTrue(options.Validate(), "command line should be valid");
		}

		[Test]
		public void InvalidArgs()
		{
			GuiOptions options = new GuiOptions(new string[] { "-asembly:nunit.tests.dll" });
			Assert.IsFalse(options.Validate());
		}


		[Test] 
		public void InvalidCommandLineParms()
		{
			GuiOptions parser = new GuiOptions(new String[]{"-garbage:TestFixture", "-assembly:Tests.dll"});
			Assert.IsFalse(parser.Validate());
		}

		[Test] 
		public void NoNameValuePairs()
		{
			GuiOptions parser = new GuiOptions(new String[]{"TestFixture", "Tests.dll"});
			Assert.IsFalse(parser.Validate());
		}

		[Test]
		public void HelpTextUsesCorrectDelimiterForPlatform()
		{
			string helpText = new GuiOptions(new String[] {"Tests.dll"} ).GetHelpText();
			char delim = System.IO.Path.DirectorySeparatorChar == '/' ? '-' : '/';

			string expected = string.Format( "{0}config=", delim );
			StringAssert.Contains( expected, helpText );
		}
	}
}

