// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// Summary description for PathConstraintTests.
    /// </summary>]
    [TestFixture]
    public class SamePathTest_Windows : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathConstraint( @"C:\folder1\file.tmp" ).IgnoreCase;
            expectedDescription = @"Path matching ""C:\folder1\file.tmp""";
            stringRepresentation = "<samepath \"C:\\folder1\\file.tmp\" ignorecase>";
        }

        internal object[] SuccessData = new object[] 
            { 
                @"C:\folder1\file.tmp", 
                @"C:\Folder1\File.TMP",
                @"C:\folder1\.\file.tmp",
                @"C:\folder1\folder2\..\file.tmp",
                @"C:\FOLDER1\.\folder2\..\File.TMP",
                @"C:/folder1/file.tmp"
            };
        internal object[] FailureData = new object[] 
            { 
                123,
                @"C:\folder2\file.tmp",
                @"C:\folder1\.\folder2\..\file.temp"
            };
        internal string[] ActualValues = new string[] 
            { 
                "123",
                "\"C:\\folder2\\file.tmp\"",
                "\"C:\\folder1\\.\\folder2\\..\\file.temp\""
            };
		
		[Test]
		public void RootPathEquality()
		{
			Assert.That("c:\\", Is.SamePath("C:\\junk\\..\\").IgnoreCase);
		}
    }

    [TestFixture]
    public class SamePathTest_Linux : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathConstraint(@"/folder1/file.tmp").RespectCase;
            expectedDescription = @"Path matching ""/folder1/file.tmp""";
            stringRepresentation = @"<samepath ""/folder1/file.tmp"" respectcase>";
        }

        internal object[] SuccessData = new object[] 
            { 
                @"/folder1/file.tmp", 
                @"/folder1/./file.tmp",
                @"/folder1/folder2/../file.tmp",
                @"/folder1/./folder2/../file.tmp",
                @"\folder1\file.tmp"
            };
        internal object[] FailureData = new object[] 
            { 
                123,
                @"/folder2/file.tmp",
                @"/folder1/./folder2/../file.temp",
                @"/Folder1/File.TMP",
                @"/FOLDER1/./folder2/../File.TMP",
            };
        internal string[] ActualValues = new string[] 
            { 
                "123",
                "\"/folder2/file.tmp\"",
                "\"/folder1/./folder2/../file.temp\"",
                "\"/Folder1/File.TMP\"",
                "\"/FOLDER1/./folder2/../File.TMP\"",
            };

		[Test]
		public void RootPathEquality()
		{
			Assert.That("/", Is.SamePath("/junk/../"));
		}
	}

    [TestFixture]
    public class SubPathTest_Windows : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SubPathConstraint( @"C:\folder1\folder2" ).IgnoreCase;
            expectedDescription = @"Path under ""C:\folder1\folder2""";
            stringRepresentation = @"<subpath ""C:\folder1\folder2"" ignorecase>";
        }

        internal object[] SuccessData = new object[]
            {
                @"C:\folder1\folder2\folder3",
                @"C:\folder1\.\folder2\folder3",
                @"C:\folder1\junk\..\folder2\folder3",
                @"C:\FOLDER1\.\junk\..\Folder2\temp\..\Folder3",
                @"C:/folder1/folder2/folder3",
            };
        internal object[] FailureData = new object[]
            {
                123,
                @"C:\folder1\folder3",
                @"C:\folder1\.\folder2\..\file.temp",
                @"C:\folder1\folder2",
                @"C:\Folder1\Folder2",
                @"C:\folder1\.\folder2",
                @"C:\folder1\junk\..\folder2",
                @"C:\FOLDER1\.\junk\..\Folder2",
                @"C:/folder1/folder2"
            };
        internal string[] ActualValues = new string[]
            {
                "123",
                "\"C:\\folder1\\folder3\"",
                "\"C:\\folder1\\.\\folder2\\..\\file.temp\"",
                "\"C:\\folder1\\folder2\"",
                "\"C:\\Folder1\\Folder2\"",
                "\"C:\\folder1\\.\\folder2\"",
                "\"C:\\folder1\\junk\\..\\folder2\"",
                "\"C:\\FOLDER1\\.\\junk\\..\\Folder2\"",
                "\"C:/folder1/folder2\""
            };
		
		[Test]
		public void SubPathOfRoot()
		{
			Assert.That("C:\\junk\\file.temp", new SubPathConstraint("C:\\"));
		}
    }

    [TestFixture]
    public class SubPathTest_Linux : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SubPathConstraint( @"/folder1/folder2"  ).RespectCase;
            expectedDescription = @"Path under ""/folder1/folder2""";
            stringRepresentation = @"<subpath ""/folder1/folder2"" respectcase>";
        }

        internal object[] SuccessData = new object[]
            {
                @"/folder1/folder2/folder3",
                @"/folder1/./folder2/folder3",
                @"/folder1/junk/../folder2/folder3",
                @"\folder1\folder2\folder3",
            };
        internal object[] FailureData = new object[]
            {
                123,
                "/Folder1/Folder2",
                "/FOLDER1/./junk/../Folder2",
                "/FOLDER1/./junk/../Folder2/temp/../Folder3",
                "/folder1/folder3",
                "/folder1/./folder2/../folder3",
				"/folder1",
                "/folder1/folder2",
                "/folder1/./folder2",
                "/folder1/junk/../folder2",
                @"\folder1\folder2"
            };
        internal string[] ActualValues = new string[]
            {
                "123",
                "\"/Folder1/Folder2\"",
                "\"/FOLDER1/./junk/../Folder2\"",
                "\"/FOLDER1/./junk/../Folder2/temp/../Folder3\"",
                "\"/folder1/folder3\"",
                "\"/folder1/./folder2/../folder3\"",
				"\"/folder1\"",
                "\"/folder1/folder2\"",
                "\"/folder1/./folder2\"",
                "\"/folder1/junk/../folder2\"",
                "\"\\folder1\\folder2\""
            };

		[Test]
		public void SubPathOfRoot()
		{
			Assert.That("/junk/file.temp", new SubPathConstraint("/"));
		}
}
	
    [TestFixture]
    public class SamePathOrUnderTest_Windows : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathOrUnderConstraint( @"C:\folder1\folder2" ).IgnoreCase;
            expectedDescription = @"Path under or matching ""C:\folder1\folder2""";
            stringRepresentation = @"<samepathorunder ""C:\folder1\folder2"" ignorecase>";
        }

        internal object[] SuccessData = new object[]
            {
                @"C:\folder1\folder2",
                @"C:\Folder1\Folder2",
                @"C:\folder1\.\folder2",
                @"C:\folder1\junk\..\folder2",
                @"C:\FOLDER1\.\junk\..\Folder2",
                @"C:/folder1/folder2",
                @"C:\folder1\folder2\folder3",
                @"C:\folder1\.\folder2\folder3",
                @"C:\folder1\junk\..\folder2\folder3",
                @"C:\FOLDER1\.\junk\..\Folder2\temp\..\Folder3",
                @"C:/folder1/folder2/folder3",
            };
        internal object[] FailureData = new object[]
            {
                123,
                @"C:\folder1\folder3",
                @"C:\folder1\.\folder2\..\file.temp"
            };
        internal string[] ActualValues = new string[]
            {
                "123",
                "\"C:\\folder1\\folder3\"",
                "\"C:\\folder1\\.\\folder2\\..\\file.temp\""
            };
    }

    [TestFixture]
    public class SamePathOrUnderTest_Linux : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathOrUnderConstraint( @"/folder1/folder2"  ).RespectCase;
            expectedDescription = @"Path under or matching ""/folder1/folder2""";
            stringRepresentation = @"<samepathorunder ""/folder1/folder2"" respectcase>";
        }

        internal object[] SuccessData = new object[]
            {
                @"/folder1/folder2",
                @"/folder1/./folder2",
                @"/folder1/junk/../folder2",
                @"\folder1\folder2",
                @"/folder1/folder2/folder3",
                @"/folder1/./folder2/folder3",
                @"/folder1/junk/../folder2/folder3",
                @"\folder1\folder2\folder3",
            };
        internal object[] FailureData = new object[]
            {
                123,
                "/Folder1/Folder2",
                "/FOLDER1/./junk/../Folder2",
                "/FOLDER1/./junk/../Folder2/temp/../Folder3",
                "/folder1/folder3",
                "/folder1/./folder2/../folder3",
				"/folder1"
            };
        internal string[] ActualValues = new string[]
            {
                "123",
                "\"/Folder1/Folder2\"",
                "\"/FOLDER1/./junk/../Folder2\"",
                "\"/FOLDER1/./junk/../Folder2/temp/../Folder3\"",
                "\"/folder1/folder3\"",
                "\"/folder1/./folder2/../folder3\"",
				"\"/folder1\""
            };
    }
}