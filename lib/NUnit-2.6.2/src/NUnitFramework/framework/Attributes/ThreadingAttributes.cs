// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Threading;

namespace NUnit.Framework
{
    /// <summary>
    /// Used on a method, marks the test with a timeout value in milliseconds. 
    /// The test will be run in a separate thread and is cancelled if the timeout 
    /// is exceeded. Used on a method or assembly, sets the default timeout 
    /// for all contained test methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited=false)]
    public class TimeoutAttribute : PropertyAttribute
    {
        /// <summary>
        /// Construct a TimeoutAttribute given a time in milliseconds
        /// </summary>
        /// <param name="timeout">The timeout value in milliseconds</param>
        public TimeoutAttribute(int timeout)
            : base(timeout) { }
    }

    /// <summary>
    /// Marks a test that must run in the STA, causing it
    /// to run in a separate thread if necessary.
    /// 
    /// On methods, you may also use STAThreadAttribute
    /// to serve the same purpose.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited=false)]
    public class RequiresSTAAttribute : PropertyAttribute
    {
        /// <summary>
        /// Construct a RequiresSTAAttribute
        /// </summary>
        public RequiresSTAAttribute()
        {
            this.Properties.Add("APARTMENT_STATE", ApartmentState.STA);
        }
    }

    /// <summary>
    /// Marks a test that must run in the MTA, causing it
    /// to run in a separate thread if necessary.
    /// 
    /// On methods, you may also use MTAThreadAttribute
    /// to serve the same purpose.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited=false)]
    public class RequiresMTAAttribute : PropertyAttribute
    {
        /// <summary>
        /// Construct a RequiresMTAAttribute
        /// </summary>
        public RequiresMTAAttribute()
        {
            this.Properties.Add("APARTMENT_STATE", ApartmentState.MTA);
        }
    }

    /// <summary>
    /// Marks a test that must run on a separate thread.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited=false)]
    public class RequiresThreadAttribute : PropertyAttribute
    {
        /// <summary>
        /// Construct a RequiresThreadAttribute
        /// </summary>
        public RequiresThreadAttribute()
            : base(true) { }

        /// <summary>
        /// Construct a RequiresThreadAttribute, specifying the apartment
        /// </summary>
        public RequiresThreadAttribute(ApartmentState apartment)
            : base(true)
        {
            this.Properties.Add("APARTMENT_STATE", apartment);
        }
    }
}
