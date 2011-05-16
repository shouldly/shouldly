// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework
{
    /// <summary>
    /// Summary description for SetUICultureAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Assembly, AllowMultiple = true)]
    public class SetUICultureAttribute : PropertyAttribute
    {
        /// <summary>
        /// Construct given the name of a culture
        /// </summary>
        /// <param name="culture"></param>
        public SetUICultureAttribute(string culture) : base("_SETUICULTURE", culture) { }
    }
}
