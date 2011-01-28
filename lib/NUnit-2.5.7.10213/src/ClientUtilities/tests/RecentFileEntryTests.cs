// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
    [TestFixture]
    public class RecentFileEntryTests
    {
        private RecentFileEntry entry;
        private static readonly string entryPath = "a/b/c";
		private static readonly string entryPathWithComma = @"D:\test\test, and further research\program1\program1.exe";
		private static Version entryVersion = new Version("1.2");
		private static Version currentVersion = Environment.Version;

		[Test]
		public void CanCreateFromSimpleFileName()
		{
			entry = new RecentFileEntry( entryPath );
			Assert.AreEqual( entryPath, entry.Path );
			Assert.AreEqual( currentVersion, entry.CLRVersion );
		}

		[Test]
		public void CanCreateFromFileNameAndVersion()
		{
			entry = new RecentFileEntry( entryPath, entryVersion );
			Assert.AreEqual( entryPath, entry.Path );
			Assert.AreEqual( entryVersion, entry.CLRVersion );
		}

        [Test]
        public void EntryCanDisplayItself()
        {
			entry = new RecentFileEntry( entryPath, entryVersion );
			Assert.AreEqual(
                entryPath + RecentFileEntry.Separator + entryVersion.ToString(),
                entry.ToString());
        }

		[Test]
		public void CanParseSimpleFileName()
		{
			entry = RecentFileEntry.Parse(entryPath);
			Assert.AreEqual(entryPath, entry.Path);
			Assert.AreEqual(currentVersion, entry.CLRVersion);
		}

		[Test]
		public void CanParseSimpleFileNameWithComma()
		{
			entry = RecentFileEntry.Parse(entryPathWithComma);
			Assert.AreEqual(entryPathWithComma, entry.Path);
			Assert.AreEqual(currentVersion, entry.CLRVersion);
		}

		[Test]
		public void CanParseFileNamePlusVersionString()
		{
			string text = entryPath + RecentFileEntry.Separator + entryVersion.ToString();
			entry = RecentFileEntry.Parse(text);
			Assert.AreEqual(entryPath, entry.Path);
			Assert.AreEqual(entryVersion, entry.CLRVersion);
		}

		[Test]
		public void CanParseFileNameWithCommaPlusVersionString()
		{
			string text = entryPathWithComma + RecentFileEntry.Separator + entryVersion.ToString();
			entry = RecentFileEntry.Parse(text);
			Assert.AreEqual(entryPathWithComma, entry.Path);
			Assert.AreEqual(entryVersion, entry.CLRVersion);
		}
	}
}
