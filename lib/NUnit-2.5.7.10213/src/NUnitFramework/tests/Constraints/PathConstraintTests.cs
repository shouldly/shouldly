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

        object[] SuccessData = new object[] 
            { 
                @"C:\folder1\file.tmp", 
                @"C:\Folder1\File.TMP",
                @"C:\folder1\.\file.tmp",
                @"C:\folder1\folder2\..\file.tmp",
                @"C:\FOLDER1\.\folder2\..\File.TMP",
                @"C:/folder1/file.tmp"
            };
        object[] FailureData = new object[] 
            { 
                123,
                @"C:\folder2\file.tmp",
                @"C:\folder1\.\folder2\..\file.temp"
            };
        string[] ActualValues = new string[] 
            { 
                "123",
                "\"C:\\folder2\\file.tmp\"",
                "\"C:\\folder1\\.\\folder2\\..\\file.temp\""
            };
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

        object[] SuccessData = new object[] 
            { 
                @"/folder1/file.tmp", 
                @"/folder1/./file.tmp",
                @"/folder1/folder2/../file.tmp",
                @"/folder1/./folder2/../file.tmp",
                @"\folder1\file.tmp"
            };
        object[] FailureData = new object[] 
            { 
                123,
                @"/folder2/file.tmp",
                @"/folder1/./folder2/../file.temp",
                @"/Folder1/File.TMP",
                @"/FOLDER1/./folder2/../File.TMP",
            };
        string[] ActualValues = new string[] 
            { 
                "123",
                "\"/folder2/file.tmp\"",
                "\"/folder1/./folder2/../file.temp\"",
                "\"/Folder1/File.TMP\"",
                "\"/FOLDER1/./folder2/../File.TMP\"",
            };
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

        object[] SuccessData = new object[]
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
        object[] FailureData = new object[]
            {
                123,
                @"C:\folder1\folder3",
                @"C:\folder1\.\folder2\..\file.temp"
            };
        string[] ActualValues = new string[]
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

        object[] SuccessData = new object[]
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
        object[] FailureData = new object[]
            {
                123,
                "/Folder1/Folder2",
                "/FOLDER1/./junk/../Folder2",
                "/FOLDER1/./junk/../Folder2/temp/../Folder3",
                "/folder1/folder3",
                "/folder1/./folder2/../folder3",
				"/folder1"
            };
        string[] ActualValues = new string[]
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