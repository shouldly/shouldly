// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Mocks
{
	/// <summary>
	/// The IVerify interface is implemented by objects capable of self-verification.
	/// </summary>
    [Obsolete("NUnit now uses NSubstitute")]
    public interface IVerify
	{
		void Verify();
	}
}
