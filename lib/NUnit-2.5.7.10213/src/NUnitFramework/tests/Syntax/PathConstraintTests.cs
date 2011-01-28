// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;

namespace NUnit.Framework.Syntax
{
    public class SamePathTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";
            string defaultCaseSensitivity = Path.DirectorySeparatorChar == '\\'
                ? "ignorecase" : "respectcase";

            parseTree = string.Format(@"<samepath ""{0}"" {1}>", path, defaultCaseSensitivity);
            staticSyntax = Is.SamePath(path);
            inheritedSyntax = Helper().SamePath(path);
            builderSyntax = Builder().SamePath(path);
        }
    }

    public class SamePathTest_IgnoreCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<samepath ""{0}"" ignorecase>", path);
            staticSyntax = Is.SamePath(path).IgnoreCase;
            inheritedSyntax = Helper().SamePath(path).IgnoreCase;
            builderSyntax = Builder().SamePath(path).IgnoreCase;
        }
    }

    public class NotSamePathTest_IgnoreCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<not <samepath ""{0}"" ignorecase>>", path);
            staticSyntax = Is.Not.SamePath(path).IgnoreCase;
            inheritedSyntax = Helper().Not.SamePath(path).IgnoreCase;
            builderSyntax = Builder().Not.SamePath(path).IgnoreCase;
        }
    }

    public class SamePathTest_RespectCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<samepath ""{0}"" respectcase>", path);
            staticSyntax = Is.SamePath(path).RespectCase;
            inheritedSyntax = Helper().SamePath(path).RespectCase;
            builderSyntax = Builder().SamePath(path).RespectCase;
        }
    }

    public class NotSamePathTest_RespectCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<not <samepath ""{0}"" respectcase>>", path);
            staticSyntax = Is.Not.SamePath(path).RespectCase;
            inheritedSyntax = Helper().Not.SamePath(path).RespectCase;
            builderSyntax = Builder().Not.SamePath(path).RespectCase;
        }
    }

    public class SamePathOrUnderTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";
            string defaultCaseSensitivity = Path.DirectorySeparatorChar == '\\'
                ? "ignorecase" : "respectcase";

            parseTree = string.Format(@"<samepathorunder ""{0}"" {1}>", path, defaultCaseSensitivity);
            staticSyntax = Is.SamePathOrUnder(path);
            inheritedSyntax = Helper().SamePathOrUnder(path);
            builderSyntax = Builder().SamePathOrUnder(path);
        }
    }

    public class SamePathOrUnderTest_IgnoreCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<samepathorunder ""{0}"" ignorecase>", path);
            staticSyntax = Is.SamePathOrUnder(path).IgnoreCase;
            inheritedSyntax = Helper().SamePathOrUnder(path).IgnoreCase;
            builderSyntax = Builder().SamePathOrUnder(path).IgnoreCase;
        }
    }

    public class NotSamePathOrUnderTest_IgnoreCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<not <samepathorunder ""{0}"" ignorecase>>", path);
            staticSyntax = Is.Not.SamePathOrUnder(path).IgnoreCase;
            inheritedSyntax = Helper().Not.SamePathOrUnder(path).IgnoreCase;
            builderSyntax = Builder().Not.SamePathOrUnder(path).IgnoreCase;
        }
    }

    public class SamePathOrUnderTest_RespectCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<samepathorunder ""{0}"" respectcase>", path);
            staticSyntax = Is.SamePathOrUnder(path).RespectCase;
            inheritedSyntax = Helper().SamePathOrUnder(path).RespectCase;
            builderSyntax = Builder().SamePathOrUnder(path).RespectCase;
        }
    }

    public class NotSamePathOrUnderTest_RespectCase : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            string path = "/path/to/match";

            parseTree = string.Format(@"<not <samepathorunder ""{0}"" respectcase>>", path);
            staticSyntax = Is.Not.SamePathOrUnder(path).RespectCase;
            inheritedSyntax = Helper().Not.SamePathOrUnder(path).RespectCase;
            builderSyntax = Builder().Not.SamePathOrUnder(path).RespectCase;
        }
    }
}
