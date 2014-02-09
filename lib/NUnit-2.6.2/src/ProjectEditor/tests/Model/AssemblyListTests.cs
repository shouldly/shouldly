// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Xml;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Model
{
	/// <summary>
	/// This fixture tests AssemblyList
	/// </summary>
	[TestFixture]
	public class AssemblyListTests
	{
		private AssemblyList assemblies;

        private string path1;
        private string path2;
        private string path3;

		[SetUp]
		public void CreateAssemblyList()
		{
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Config/>");
			assemblies = new AssemblyList(doc.FirstChild);

            path1 = CleanPath("/tests/bin/debug/assembly1.dll");
            path2 = CleanPath("/tests/bin/debug/assembly2.dll");
            path3 = CleanPath("/tests/bin/debug/assembly3.dll");
        }

		[Test]
		public void EmptyList()
		{
			Assert.AreEqual( 0, assemblies.Count );
		}

        [Test]
        public void CanAddAssemblies()
        {
            assemblies.Add(path1);
            assemblies.Add(path2);

            Assert.AreEqual(2, assemblies.Count);
            Assert.AreEqual(path1, assemblies[0]);
            Assert.AreEqual(path2, assemblies[1]);
        }

        [Test]
        public void CanInsertAssemblies()
        {
            assemblies.Add(path1);
            assemblies.Add(path3);
            assemblies.Insert(1, path2);

            Assert.AreEqual(3, assemblies.Count);
            Assert.AreEqual(path1, assemblies[0]);
            Assert.AreEqual(path2, assemblies[1]);
            Assert.AreEqual(path3, assemblies[2]);
        }

        [Test]
        public void CanInsertAssemblyAtEnd()
        {
            assemblies.Add(path1);
            assemblies.Add(path2);
            assemblies.Insert(2, path3);

            Assert.AreEqual(3, assemblies.Count);
            Assert.AreEqual(path1, assemblies[0]);
            Assert.AreEqual(path2, assemblies[1]);
            Assert.AreEqual(path3, assemblies[2]);
        }

        [Test]
        public void CanInsertAssemblyAtStart()
        {
            assemblies.Add(path2);
            assemblies.Add(path3);
            assemblies.Insert(0, path1);

            Assert.AreEqual(3, assemblies.Count);
            Assert.AreEqual(path1, assemblies[0]);
            Assert.AreEqual(path2, assemblies[1]);
            Assert.AreEqual(path3, assemblies[2]);
        }

        [Test]
        public void CanRemoveAssemblies()
        {
            assemblies.Add(path1);
            assemblies.Add(path2);
            assemblies.Add(path3);
            
            assemblies.Remove(path2);

            Assert.AreEqual(2, assemblies.Count);
            Assert.AreEqual(path1, assemblies[0]);
            Assert.AreEqual(path3, assemblies[1]);
        }

        //[Test]
        //public void CanRemoveAssemblyAtIndex()
        //{
        //    assemblies.Add(path1);
        //    assemblies.Add(path2);
        //    assemblies.Add(path3);
        //    assemblies.RemoveAt(1);

        //    Assert.AreEqual(2, assemblies.Count);
        //    Assert.AreEqual(path1, assemblies[0]);
        //    Assert.AreEqual(path3, assemblies[1]);
        //}

        //[Test]
        //public void CanFindIndexOfAssembly()
        //{
        //    assemblies.Add(path1);
        //    assemblies.Add(path2);
        //    assemblies.Add(path3);

        //    Assert.AreEqual(1, assemblies.IndexOf(path2));
        //    Assert.AreEqual(-1, assemblies.IndexOf("/Not/in/list"));
        //}

        private string CleanPath( string path )
        {
            return path.Replace( '/', System.IO.Path.DirectorySeparatorChar );
        }
	}
}
