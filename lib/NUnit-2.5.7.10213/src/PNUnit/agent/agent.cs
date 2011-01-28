using System;
using System.IO;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Reflection;

using log4net;
using log4net.Config;

using PNUnit.Framework;

#if NUNIT_2_5
using NUnit.Util;
#endif

namespace PNUnit.Agent
{
	class Agent
	{
		[STAThread]
		static void Main(string[] args)
		{
			AgentConfig config = new AgentConfig();

#if NUNIT_2_5
            // Start required services
            ServiceManager.Services.AddService(new SettingsService());
            ServiceManager.Services.AddService(new DomainManager());
            // TODO: We use ProjectService in DomainManager - try to eliminate
            ServiceManager.Services.AddService(new ProjectService());
#endif

			// Load the test configuration file
			if( args.Length != 1 && args.Length != 2)
			{
				Console.WriteLine("Usage: agent [configfile | port path_to_assemblies]");
				return;
			}
			else if (args.Length == 1) 
			{

				string configfile = args[0];
                        
				config = AgentConfigLoader.LoadFromFile(configfile);
      
				if( config == null )
				{
					Console.WriteLine("No agent.conf file found");
				}
			}
			else if (args.Length == 2)
			{
				config.Port = int.Parse(args[0]);
				config.PathToAssemblies = args[1];
			}
            
			ConfigureLogging();

			PNUnitAgent agent = new PNUnitAgent();
			agent.Run(config);
		}

		private static void ConfigureLogging()
		{
			string log4netpath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "agent.log.conf");
			Console.WriteLine(log4netpath);
			XmlConfigurator.Configure(new FileInfo(log4netpath));
		}
	}

	public class PNUnitAgent: MarshalByRefObject, IPNUnitAgent
	{


		private AgentConfig mConfig;
		private static readonly ILog log = LogManager.GetLogger(typeof(PNUnitAgent));

		#region IPNUnitAgent

		public void RunTest(TestInfo info)
		{
			log.InfoFormat("RunTest called for Test {0}, AssemblyName {1}, TestToRun {2}",
				info.TestName, info.AssemblyName, info.TestToRun);

			new PNUnitTestRunner(info, mConfig).Run();
		}

		#endregion

		#region MarshallByRefObject
		// Lives forever
		public override object InitializeLifetimeService()
		{
			return null;
		}
		#endregion

		public void Run(AgentConfig config)
		{
			mConfig = config;
			// init remoting
			BinaryClientFormatterSinkProvider clientProvider = 
				new BinaryClientFormatterSinkProvider();
			BinaryServerFormatterSinkProvider serverProvider = 
				new BinaryServerFormatterSinkProvider();
			serverProvider.TypeFilterLevel = 
				System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                
			IDictionary props = new Hashtable();
			props["port"] = mConfig.Port;
			string s = System.Guid.NewGuid().ToString();
			props["name"] = s;
			props["typeFilterLevel"] = TypeFilterLevel.Full;
			try
			{

				TcpChannel chan = new TcpChannel(
					props,clientProvider,serverProvider);
    
				log.InfoFormat("Registering channel on port {0}", mConfig.Port);
				ChannelServices.RegisterChannel(chan);
			}
			catch( Exception e )
			{
				log.InfoFormat("Can't register channel.\n{0}", e.Message);
				return;
			}

			// publish
			RemotingServices.Marshal(this, PNUnit.Framework.Names.PNUnitAgentServiceName);

			// wait for a key to finish
			Console.ReadLine();

			RemotingServices.Disconnect(this);
		}
	}

}
