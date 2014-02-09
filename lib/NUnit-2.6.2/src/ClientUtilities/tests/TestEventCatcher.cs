// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Core;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// Summary description for TestEventCatcher.
	/// </summary>
	public class TestEventCatcher
	{
		public class TestEventArgsCollection : ReadOnlyCollectionBase
		{
			public EventArgs this[int index]
			{
				get { return (EventArgs)InnerList[index]; }
			}

			public void Add( EventArgs e )
			{
				InnerList.Add( e );
			}
		}

		private TestEventArgsCollection events;

        public bool GotRunFinished = false;

		public TestEventCatcher( ITestEvents source )
		{
			events = new TestEventArgsCollection();

			source.ProjectLoading	+= new TestEventHandler( OnTestEvent );
			source.ProjectLoaded	+= new TestEventHandler( OnTestEvent );
			source.ProjectLoadFailed+= new TestEventHandler( OnTestEvent );
			source.ProjectUnloading	+= new TestEventHandler( OnTestEvent );
			source.ProjectUnloaded	+= new TestEventHandler( OnTestEvent );
			source.ProjectUnloadFailed+= new TestEventHandler( OnTestEvent );

			source.TestLoading		+= new TestEventHandler( OnTestEvent );
			source.TestLoaded		+= new TestEventHandler( OnTestEvent );
			source.TestLoadFailed	+= new TestEventHandler( OnTestEvent );

			source.TestUnloading	+= new TestEventHandler( OnTestEvent );
			source.TestUnloaded		+= new TestEventHandler( OnTestEvent );
			source.TestUnloadFailed	+= new TestEventHandler( OnTestEvent );
		
			source.TestReloading	+= new TestEventHandler( OnTestEvent );
			source.TestReloaded		+= new TestEventHandler( OnTestEvent );
			source.TestReloadFailed	+= new TestEventHandler( OnTestEvent );

			source.RunStarting		+= new TestEventHandler( OnTestEvent );
			source.RunFinished		+= new TestEventHandler( OnTestEvent );

			source.TestStarting		+= new TestEventHandler( OnTestEvent );
			source.TestFinished		+= new TestEventHandler( OnTestEvent );
		
			source.SuiteStarting	+= new TestEventHandler( OnTestEvent );
			source.SuiteFinished	+= new TestEventHandler( OnTestEvent );
		}

		public TestEventArgsCollection Events
		{
			get { return events; }
		}

		private void OnTestEvent( object sender, TestEventArgs e )
		{
			events.Add( e );
            if (e.Action == TestAction.RunFinished)
                GotRunFinished = true;
		}
	}
}
