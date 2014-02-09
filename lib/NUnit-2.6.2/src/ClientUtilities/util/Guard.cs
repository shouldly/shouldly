// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Util
{
#if CLR_2_0 || CLR_4_0
    public static class Guard
#else
    public class Guard
#endif
    {
        public static void ArgumentNotNull(object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException("Argument " + name + " must not be null", name);
        }

        public static void ArgumentNotNullOrEmpty(string value, string name)
        {
            ArgumentNotNull(value, name);

            if (value == string.Empty)
                throw new ArgumentException("Argument " + name +" must not be the empty string", name);
        }

        public static void NotNull(object value, string name)
        {
            if (value == null)
                throw new InvalidOperationException("Invalid object state: " + name + " is null");
        }

        public static void Validate(ISelfValidating obj)
        {
            if (!obj.Validate())
                throw new InvalidOperationException(obj.Message);
        }
    }

    public interface ISelfValidating
    {
        bool Validate();
        string Message { get; }
    }
}
