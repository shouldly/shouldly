// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Diagnostics;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.Agent
{
	/// <summary>
	/// Summary description for Program.
	/// </summary>
	public class NUnitTestAgent
	{
		static Logger log = InternalTrace.GetLogger(typeof(NUnitTestAgent));

        static Guid AgentId;
        static string AgencyUrl;
        static TestAgency Agency;

        /// <summary>
        /// Channel used for communications with the agency
        /// and with clients
        /// </summary>
        static TcpChannel Channel;

        /// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
            AgentId = new Guid(args[0]);
            AgencyUrl = args[1];

#if DEBUG
            if (args.Length > 2 && args[2] == "--launch-debugger")
                Debugger.Launch();
#endif

            // Create SettingsService early so we know the trace level right at the start
            SettingsService settingsService = new SettingsService(false);
            InternalTrace.Initialize("nunit-agent_%p.log", (InternalTraceLevel)settingsService.GetSetting("Options.InternalTraceLevel", InternalTraceLevel.Default));

			log.Info("Agent process {0} starting", Process.GetCurrentProcess().Id);
            log.Info("Running under version {0}, {1}", 
                Environment.Version, 
                RuntimeFramework.CurrentFramework.DisplayName);

			// Add Standard Services to ServiceManager
            log.Info("Adding Services");
            ServiceManager.Services.AddService(settingsService);
            ServiceManager.Services.AddService(new ProjectService());
			ServiceManager.Services.AddService( new DomainManager() );
			//ServiceManager.Services.AddService( new RecentFilesService() );
			//ServiceManager.Services.AddService( new TestLoader() );
			ServiceManager.Services.AddService( new AddinRegistry() );
			ServiceManager.Services.AddService( new AddinManager() );

			// Initialize Services
            log.Info("Initializing Services");
            ServiceManager.Services.InitializeServices();

            Channel = ServerUtilities.GetTcpChannel();

            log.Info("Connecting to TestAgency at {0}", AgencyUrl);
            try
            {
                Agency = Activator.GetObject(typeof(TestAgency), AgencyUrl) as TestAgency;
            }
            catch (Exception ex)
            {
                log.Error("Unable to connect", ex);
            }

            if (Channel != null)
            {
                log.Info("Starting RemoteTestAgent");
                RemoteTestAgent agent = new RemoteTestAgent(AgentId, Agency);

                try
                {
                    if (agent.Start())
                    {
                        log.Debug("Waiting for stopSignal");
                        agent.WaitForStop();
                        log.Debug("Stop signal received");
                    }
                    else
                        log.Error("Failed to start RemoteTestAgent");
                }
                catch (Exception ex)
                {
                    log.Error("Exception in RemoteTestAgent", ex);
                }

                log.Info("Unregistering Channel");
                try
                {
                    ChannelServices.UnregisterChannel(Channel);
                }
                catch (Exception ex)
                {
                    log.Error("ChannelServices.UnregisterChannel threw an exception", ex);
                }
            }

            log.Info("Stopping all services");
            ServiceManager.Services.StopAllServices();
            log.Info("Agent process {0} exiting", Process.GetCurrentProcess().Id);
            InternalTrace.Close();

			return 0;
		}
	}
}
