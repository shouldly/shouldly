// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Core;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
    [TestFixture]
    public class RuntimeFrameworkSelectorTests
    {
        TestPackage package = new TestPackage("/dummy.dll");

        [Datapoints]
        internal RuntimeFramework[] frameworks = new RuntimeFramework[] { 
            RuntimeFramework.Parse("net-1.0"), 
            RuntimeFramework.Parse("net-1.1"), 
            RuntimeFramework.Parse("net-2.0"),
            RuntimeFramework.Parse("net-4.0"),
            RuntimeFramework.Parse("mono-1.0"),
            RuntimeFramework.Parse("mono-2.0"),
            RuntimeFramework.Parse("v1.1"),
            RuntimeFramework.Parse("v2.0"),
            RuntimeFramework.Parse("v4.0")
            // TODO: Figure out a way to run these
            //RuntimeFramework.Parse("net"),
            //RuntimeFramework.Parse("mono"),
            //RuntimeFramework.Parse("any")
        };

        [Theory]
        public void RequestForSpecificFrameworkIsHonored(RuntimeFramework requestedFramework)
        {
            Assume.That(requestedFramework.Runtime, Is.Not.EqualTo(RuntimeType.Any));

            RuntimeFrameworkSelector selector = new RuntimeFrameworkSelector();
            package.Settings["RuntimeFramework"] = requestedFramework;

            RuntimeFramework selectedFramework = selector.SelectRuntimeFramework(package);
            Assert.That(selectedFramework.Runtime, Is.EqualTo(requestedFramework.Runtime));
            Assert.That(selectedFramework.ClrVersion, Is.EqualTo(requestedFramework.ClrVersion));
        }

        [Theory]
        public void RequestForSpecificVersionIsHonored(RuntimeFramework requestedFramework)
        {
            Assume.That(requestedFramework.Runtime, Is.EqualTo(RuntimeType.Any));

            RuntimeFrameworkSelector selector = new RuntimeFrameworkSelector();
            package.Settings["RuntimeFramework"] = requestedFramework;

            RuntimeFramework selectedFramework = selector.SelectRuntimeFramework(package);
            Assert.That(selectedFramework.Runtime, Is.EqualTo(RuntimeFramework.CurrentFramework.Runtime));
            Assert.That(selectedFramework.ClrVersion, Is.EqualTo(requestedFramework.ClrVersion));
        }
    }
}
