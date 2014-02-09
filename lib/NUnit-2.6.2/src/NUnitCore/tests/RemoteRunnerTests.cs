// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Collections;

using NUnit.Framework;
using NUnit.Core;
using NUnit.Util;
using NUnit.Tests.Assemblies;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class RemoteRunnerTests : BasicRunnerTests
	{
		protected override TestRunner CreateRunner( int runnerID )
		{
			return new RemoteTestRunner( runnerID );
		}
	}
}
