// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Collections;
using System.Reflection;
using NUnit.Core.Builders;
using NUnit.Core.Extensibility;

namespace NUnit.Core
{
	/// <summary>
	/// CoreExtensions is a singleton class that groups together all 
	/// the extension points that are supported in the test domain.
	/// It also provides access to the test builders and decorators
	/// by other parts of the NUnit core.
	/// </summary>
	public class CoreExtensions : ExtensionHost, IService
	{
		static Logger log = InternalTrace.GetLogger("CoreExtensions");

		#region Instance Fields
		private IAddinRegistry addinRegistry;
		private bool initialized;

		private SuiteBuilderCollection suiteBuilders;
		private TestCaseBuilderCollection testBuilders;
		private TestDecoratorCollection testDecorators;
		private EventListenerCollection listeners;
		private FrameworkRegistry frameworks;
	    private TestCaseProviders testcaseProviders;
        private DataPointProviders dataPointProviders;

		#endregion

		#region CoreExtensions Singleton
		private static CoreExtensions host;
		public static CoreExtensions Host
		{
			get
			{
				if (host == null)
					host = new CoreExtensions();

				return host;
			}
		}
		#endregion

		#region Constructors
		public CoreExtensions() 
		{
			this.suiteBuilders = new SuiteBuilderCollection(this);
			this.testBuilders = new TestCaseBuilderCollection(this);
			this.testDecorators = new TestDecoratorCollection(this);
			this.listeners = new EventListenerCollection(this);
			this.frameworks = new FrameworkRegistry(this);
            this.testcaseProviders = new TestCaseProviders(this);
            this.dataPointProviders = new DataPointProviders(this);

		    this.extensions = new ArrayList();
		    extensions.Add(suiteBuilders);
		    extensions.Add(testBuilders);
		    extensions.Add(testDecorators);
		    extensions.Add(listeners);
		    extensions.Add(frameworks);
		    extensions.Add(testcaseProviders);
            extensions.Add(dataPointProviders);

			this.supportedTypes = ExtensionType.Core;

			// TODO: This should be somewhere central
//			string logfile = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
//			logfile = Path.Combine( logfile, "NUnit" );
//			logfile = Path.Combine( logfile, "NUnitTest.log" );
//
//			appender = new log4net.Appender.ConsoleAppender();
////			appender.File = logfile;
////			appender.AppendToFile = true;
////			appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
//			appender.Layout = new log4net.Layout.PatternLayout(
//				"%date{ABSOLUTE} %-5level [%4thread] %logger{1}: PID=%property{PID} %message%newline" );
//			appender.Threshold = log4net.Core.Level.All;
//			log4net.Config.BasicConfigurator.Configure(appender);
		}
		#endregion

		#region Public Properties

		public bool Initialized
		{
			get { return initialized; }
		}

		/// <summary>
		/// Our AddinRegistry may be set from outside or passed into the domain
		/// </summary>
		public IAddinRegistry AddinRegistry
		{
			get 
			{
				if ( addinRegistry == null )
					addinRegistry = AppDomain.CurrentDomain.GetData( "AddinRegistry" ) as IAddinRegistry;

				return addinRegistry; 
			}
			set { addinRegistry = value; }
		}
		#endregion

		#region Internal Properties
		internal ISuiteBuilder SuiteBuilders
		{
			get { return suiteBuilders; }
		}

		internal ITestCaseBuilder2 TestBuilders
		{
			get { return testBuilders; }
		}

		internal ITestDecorator TestDecorators
		{
			get { return testDecorators; }
		}

		internal EventListener Listeners
		{
			get { return listeners; }
		}

		internal FrameworkRegistry TestFrameworks
		{
			get { return frameworks; }
		}

        internal TestCaseProviders TestCaseProviders
	    {
            get { return testcaseProviders; }
	    }

	    #endregion

		#region Public Methods	
		public void InstallBuiltins()
		{
			log.Info( "Installing Builtins" );

			// Define NUnit Frameworks
			frameworks.Register( "NUnit", "nunit.framework" );
            frameworks.Register("NUnitLite", "NUnitLite");

			// Install builtin SuiteBuilders
			suiteBuilders.Install( new NUnitTestFixtureBuilder() );
			suiteBuilders.Install( new SetUpFixtureBuilder() );

            // Install builtin TestCaseBuilder
            testBuilders.Install( new NUnitTestCaseBuilder() );
            //testBuilders.Install(new TheoryBuilder());

            // Install builtin TestCaseProviders
            testcaseProviders.Install(new TestCaseParameterProvider());
            testcaseProviders.Install(new TestCaseSourceProvider());
            testcaseProviders.Install(new CombinatorialTestCaseProvider());

            // Install builtin DataPointProvider
            dataPointProviders.Install(new InlineDataPointProvider());
            dataPointProviders.Install(new ValueSourceProvider());
            dataPointProviders.Install(new DatapointProvider());
		}

		public void InstallAddins()
		{
			log.Info( "Installing Addins" );

			if( AddinRegistry != null )
			{
				foreach (Addin addin in AddinRegistry.Addins)
				{
					if ( (this.ExtensionTypes & addin.ExtensionType) != 0 )
					{
						AddinStatus status = AddinStatus.Unknown;
						string message = null;

						try
						{
							Type type = Type.GetType(addin.TypeName);
							if ( type == null )
							{
								status = AddinStatus.Error;
								message = string.Format( "Unable to locate {0} Type", addin.TypeName );
							}
							else if ( !InstallAddin( type ) )
							{
								status = AddinStatus.Error;
								message = "Install method returned false";
							}
							else
							{
								status = AddinStatus.Loaded;
							}
						}
						catch( Exception ex )
						{
							status = AddinStatus.Error;
							message = ex.ToString(); 				
						}

						AddinRegistry.SetStatus( addin.Name, status, message );
						if ( status != AddinStatus.Loaded )
						{
							log.Error( "Failed to load {0}", addin.Name );
							log.Error( message );
						}
					}
				}
			}
		}

		public void InstallAdhocExtensions( Assembly assembly )
		{
			foreach ( Type type in assembly.GetExportedTypes() )
			{
				if ( type.GetCustomAttributes(typeof(NUnitAddinAttribute), false).Length == 1 )
					InstallAddin( type );
			}
		}
		#endregion

		#region Helper Methods
		private bool InstallAddin( Type type )
		{
			ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
			object obj = ctor.Invoke( new object[0] );
			IAddin theAddin = (IAddin)obj;

			return theAddin.Install(this);
		}
		#endregion

		#region IService Members

		public void UnloadService()
		{
			// TODO:  Add CoreExtensions.UnloadService implementation
		}

		public void InitializeService()
		{
			InstallBuiltins();
			InstallAddins();

			initialized = true;
		}

		#endregion
	}
}
