// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class BinarySerializableTest : ConstraintTestBaseWithArgumentException
    {
		[SetUp]
        public void SetUp()
        {
            theConstraint = new BinarySerializableConstraint();
            expectedDescription = "binary serializable";
            stringRepresentation = "<binaryserializable>";
        }

        object[] SuccessData = new object[] { 1, "a", new ArrayList(), new InternalWithSerializableAttributeClass() };
        
        object[] FailureData = new object[] { new InternalClass() };

        string[] ActualValues = new string[] { "<InternalClass>" }; 

        object[] InvalidData = new object[] { null };
    }

    [TestFixture]
    public class XmlSerializableTest : ConstraintTestBaseWithArgumentException
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new XmlSerializableConstraint();
            expectedDescription = "xml serializable";
            stringRepresentation = "<xmlserializable>";
        }

        object[] SuccessData = new object[] { 1, "a", new ArrayList() };

#if NET_2_0
		object[] FailureData = new object[] { new Dictionary<string, string>(), new InternalClass(), new InternalWithSerializableAttributeClass() };
        string[] ActualValues = new string[] { "<Dictionary`2>", "<InternalClass>", "<InternalWithSerializableAttributeClass>" };
#else
		object[] FailureData = new object[] { new InternalClass(), new InternalWithSerializableAttributeClass() };
		string[] ActualValues = new string[] { "<InternalClass>", "<InternalWithSerializableAttributeClass>" };
#endif


        object[] InvalidData = new object[] { null };
    }

    internal class InternalClass
    {}

    [Serializable]
    internal class InternalWithSerializableAttributeClass
    {}
}
