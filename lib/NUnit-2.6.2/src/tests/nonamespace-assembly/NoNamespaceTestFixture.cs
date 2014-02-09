// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using NUnit.Framework;

[TestFixture]
public class NoNamespaceTestFixture
{
	public static readonly int Tests = 3;

    public static readonly string AssemblyPath = NUnit.Core.AssemblyHelper.GetAssemblyPath(typeof(NoNamespaceTestFixture));

	[Test]
	public void Test1()
	{
	}

	[Test]
	public void Test2()
	{
	}

	[Test]
	public void Test3()
	{
	}
}
