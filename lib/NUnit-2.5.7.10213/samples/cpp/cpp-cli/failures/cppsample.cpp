// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

#include "cppsample.h"

namespace NUnitSamples {

	void SimpleCPPSample::Init() {
		fValue1 = 2;
		fValue2 = 3;
	}

	void SimpleCPPSample::Add() {
		int result = fValue1 + fValue2;
		Assert::AreEqual(6,result);
	}

	void SimpleCPPSample::DivideByZero()
	{
		int zero= 0;
		int result= 8/zero;
	}

	void SimpleCPPSample::Equals() {
		Assert::AreEqual(12, 12, "Integer");
		Assert::AreEqual(12L, 12L, "Long");
		Assert::AreEqual('a', 'a', "Char");


		Assert::AreEqual(12, 13, "Expected Failure (Integer)");
		Assert::AreEqual(12.0, 11.99, 0.0, "Expected Failure (Double)");
	}

	void SimpleCPPSample::IgnoredTest()
	{
		throw gcnew InvalidCastException();
	}

	void SimpleCPPSample::ExpectAnException()
	{
		throw gcnew InvalidCastException();
	}

}

