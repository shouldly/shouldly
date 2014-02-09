// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace NUnit.Framework.Syntax
{
    [TestFixture]
    public class BinarySerializableTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<binaryserializable>";
            staticSyntax = Is.BinarySerializable;
            inheritedSyntax = Helper().BinarySerializable;
            builderSyntax = Builder().BinarySerializable;
        }
    }

    [TestFixture]
    public class XmlSerializableTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<xmlserializable>";
            staticSyntax = Is.XmlSerializable;
            inheritedSyntax = Helper().XmlSerializable;
            builderSyntax = Builder().XmlSerializable;
        }
    }
}
