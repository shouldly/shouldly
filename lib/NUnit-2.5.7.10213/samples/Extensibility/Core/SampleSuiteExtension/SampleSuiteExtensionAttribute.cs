// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;

namespace NUnit.Core.Extensions
{
	/// <summary>
	/// SampleSuiteExtensionAttribute is used to identify a SampleSuiteExtension fixture
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public sealed class SampleSuiteExtensionAttribute : Attribute
	{
	}
}
