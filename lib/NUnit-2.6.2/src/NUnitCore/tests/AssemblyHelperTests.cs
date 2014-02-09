// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using NUnit.Framework;
using System.Reflection;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class AssemblyHelperTests
    {
        [Test]
        public void GetPathForAssembly()
        {
            string path = AssemblyHelper.GetAssemblyPath(this.GetType().Assembly);
            Assert.That(Path.GetFileName(path), Is.EqualTo("nunit.core.tests.dll").IgnoreCase);
            Assert.That(File.Exists(path));
        }

        [Test]
        public void GetPathForType()
        {
            string path = AssemblyHelper.GetAssemblyPath(this.GetType());
            Assert.That(Path.GetFileName(path), Is.EqualTo("nunit.core.tests.dll").IgnoreCase);
            Assert.That(File.Exists(path));
        }
		
        // The following tests are only useful to the extent that the test cases
        // match what will actually be provided to the method in production.
        // As currently used, NUnit's codebase can only use the file: schema,
        // since we don't load assemblies from anything but files. The uri's
        // provided can be absolute file paths or UNC paths.

#if CLR_2_0 || CLR_4_0
        [Platform("Win")]
        [TestCase(@"file:///C:/path/to/assembly.dll", @"C:\path\to\assembly.dll")]
        [TestCase(@"file://C:/path/to/assembly.dll", @"C:\path\to\assembly.dll")]
        [TestCase(@"file://C:/my%20path/to%20my/assembly.dll", @"C:\my path\to my\assembly.dll")]
        [TestCase(@"file:///C:/dev/C%23/assembly.dll", @"C:\dev\C#\assembly.dll")]
        [TestCase(@"file:///C:/dev/funnychars?:=/assembly.dll", @"C:\dev\funnychars?:=\assembly.dll")]
        [TestCase(@"file:///path/to/assembly.dll", @"/path/to/assembly.dll")]
        [TestCase(@"file://server/path/to/assembly.dll", @"\\server\path\to\assembly.dll")]
        [TestCase(@"file:///my%20path/to%20my/assembly.dll", @"/my path/to my/assembly.dll")]
        [TestCase(@"file:///dev/C%23/assembly.dll", @"/dev/C#/assembly.dll")]
        [TestCase(@"file:///dev/funnychars?:=/assembly.dll", @"/dev/funnychars?:=/assembly.dll")]
        public void GetAssemblyPathFromEscapedCodeBase_Windows(string uri, string expectedPath)
        {
            string localPath = AssemblyHelper.GetAssemblyPathFromEscapedCodeBase(uri);
            Assert.That(localPath, Is.SamePath(expectedPath));
        }

        [Platform("Linux")]
        [TestCase(@"file:///path/to/assembly.dll", Result = @"/path/to/assembly.dll")]
        [TestCase(@"file://path/to/assembly.dll", Result = @"/path/to/assembly.dll")]
        [TestCase(@"file:///my path/to my/assembly.dll", Result = @"/my path/to my/assembly.dll")]
        [TestCase(@"file://my path/to my/assembly.dll", Result = @"/my path/to my/assembly.dll")]
        [TestCase(@"file:///dev/C#/assembly.dll", Result = @"/dev/C#/assembly.dll")]
        [TestCase(@"file:///dev/funnychars?:=/assembly.dll", Result = @"/dev/funnychars?:=/assembly.dll")]
        //[TestCase(@"http://server/path/to/assembly.dll", Result="//server/path/to/assembly.dll")]
        public string GetAssemblyPathFromEscapedCodeBase_Linux(string uri)
        {
            return AssemblyHelper.GetAssemblyPathFromEscapedCodeBase(uri);
        }
#else
        [Platform("Win")]
        [TestCase(@"file:///C:/path/to/assembly.dll", @"C:\path\to\assembly.dll")]
        [TestCase(@"file://C:/path/to/assembly.dll", @"C:\path\to\assembly.dll")]
        [TestCase(@"file://C:/my path/to my/assembly.dll", @"C:\my path\to my\assembly.dll")]
        [TestCase(@"file:///C:/dev/C#/assembly.dll", @"C:\dev\C#\assembly.dll")]
        [TestCase(@"file:///C:/dev/funnychars?:=/assembly.dll", @"C:\dev\funnychars?:=\assembly.dll")]
        [TestCase(@"file:///path/to/assembly.dll", @"/path/to/assembly.dll")]
        [TestCase(@"file://server/path/to/assembly.dll", @"\\server\path\to\assembly.dll")]
        [TestCase(@"file:///my path/to my/assembly.dll", @"/my path/to my/assembly.dll")]
        [TestCase(@"file:///dev/C#/assembly.dll", @"/dev/C#/assembly.dll")]
        [TestCase(@"file:///dev/funnychars?:=/assembly.dll", @"/dev/funnychars?:=/assembly.dll")]
        public void GetAssemblyPathFromCodeBase_Windows(string uri, string expectedPath)
        {
            string localPath = AssemblyHelper.GetAssemblyPathFromCodeBase(uri);
            Assert.That(localPath, Is.SamePath(expectedPath));
        }
#endif
    }
}