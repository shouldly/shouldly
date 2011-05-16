// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

#pragma once

using namespace System;
using namespace NUnit::Framework;

namespace NUnitSamples
{
	[TestFixture]
	public ref class SimpleCPPSample
	{
		int fValue1;
		int fValue2;
	public:
		[SetUp] void Init();

		[Test] void Add();
		[Test] void DivideByZero();
		[Test] void Equals();
		[Test] [Ignore("ignored test")] void IgnoredTest();
		[Test] [ExpectedException(InvalidOperationException::typeid)] void ExpectAnException();
	};
}
