// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;

namespace NUnit.Framework.Tests
{
    public class TestDelegates
    {
        public static void ThrowsArgumentException()
        {
            throw new ArgumentException("myMessage", "myParam");
        }

        public static void ThrowsApplicationException()
        {
            throw new ApplicationException("my message");
        }

        public static void ThrowsSystemException()
        {
            throw new Exception("my message");
        }

        public static void ThrowsNothing()
        {
        }

        public static void ThrowsDerivedApplicationException()
        {
            throw new DerivedApplicationException();
        }

        public class DerivedApplicationException : ApplicationException
        {
        }
    }
}
