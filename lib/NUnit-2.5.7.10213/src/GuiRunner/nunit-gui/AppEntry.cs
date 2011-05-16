// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NUnit.UiKit;
using NUnit.Util;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace NUnit.Gui
{
    /// <summary>
    /// Class to manage application startup.
    /// </summary>
    public class AppEntry
    {
        static Logger log = InternalTrace.GetLogger(typeof(AppEntry));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static int Main(string[] args)
        {
            InternalTrace.Initialize("nunit-gui_%p.log");

            log.Info("Starting NUnit GUI");

            GuiOptions guiOptions = new GuiOptions(args);

            GuiAttachedConsole attachedConsole = null;
            if (guiOptions.console)
            {
                log.Info("Creating attached console");
                attachedConsole = new GuiAttachedConsole();
            }

            if (!guiOptions.Validate() || guiOptions.help)
            {
                string message = guiOptions.GetHelpText();
                UserMessage.DisplayFailure(message, "Help Syntax");
                log.Error("Command line error: " + message);
                return 2;
            }

            if (guiOptions.cleanup)
            {
                log.Info("Performing cleanup of shadow copy cache");
                DomainManager.DeleteShadowCopyPath();
                return 0;
            }

            if (!guiOptions.NoArgs)
            {
                if (guiOptions.lang != null)
                {
                    log.Info("Setting culture to " + guiOptions.lang);
                    Thread.CurrentThread.CurrentUICulture =
                        new CultureInfo(guiOptions.lang);
                }
            }

            try
            {
                // Add Standard Services to ServiceManager
                log.Info("Adding Services");
                ServiceManager.Services.AddService(new SettingsService());
                ServiceManager.Services.AddService(new DomainManager());
                ServiceManager.Services.AddService(new RecentFilesService());
                ServiceManager.Services.AddService(new ProjectService());
                ServiceManager.Services.AddService(new TestLoader(new GuiTestEventDispatcher()));
                ServiceManager.Services.AddService(new AddinRegistry());
                ServiceManager.Services.AddService(new AddinManager());
                ServiceManager.Services.AddService(new TestAgency());

                // Initialize Services
                log.Info("Initializing Services");
                ServiceManager.Services.InitializeServices();
            }
            catch (Exception ex)
            {
                UserMessage.DisplayFatalError(ex, null, "Unable to Initialize Services");
                log.Error("Unable to initialize services", ex);
                return 2;
            }

            // Create container in order to allow ambient properties
            // to be shared across all top-level forms.
            log.Info("Initializing AmbientProperties");
            AppContainer c = new AppContainer();
            AmbientProperties ambient = new AmbientProperties();
            c.Services.AddService(typeof(AmbientProperties), ambient);

            log.Info("Constructing Form");
            NUnitForm form = new NUnitForm(guiOptions);
            c.Add(form);

            try
            {
                log.Info("Starting Gui Application");
                Application.Run(form);
                log.Info("Application Exit");
            }
            catch( Exception ex )
            {
                log.Error("Gui Application threw an excepion", ex );
                throw;
            }
            finally
            {
                log.Info("Stopping Services");
                ServiceManager.Services.StopAllServices();
            }

            if (attachedConsole != null)
            {
                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
                attachedConsole.Close();
            }

            log.Info("Exiting NUnit GUI");
            InternalTrace.Close();

            return 0;
        }
    }
}
