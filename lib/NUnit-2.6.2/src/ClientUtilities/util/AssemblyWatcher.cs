// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Timers;
using NUnit.Core;

namespace NUnit.Util
{
	/// <summary>
	/// AssemblyWatcher keeps track of one or more assemblies to 
	/// see if they have changed. It incorporates a delayed notification
	/// and uses a standard event to notify any interested parties
	/// about the change. The path to the assembly is provided as
	/// an argument to the event handler so that one routine can
	/// be used to handle events from multiple watchers.
	/// </summary>
	public class AssemblyWatcher : IAssemblyWatcher
	{
        static Logger log = InternalTrace.GetLogger(typeof(AssemblyWatcher));

        private FileSystemWatcher[] fileWatchers;
		private FileInfo[] files;

		protected System.Timers.Timer timer;
		protected string changedAssemblyPath;

		protected FileInfo GetFileInfo(int index)
		{
			return files[index];
		}

		public void Setup(int delay, string assemblyFileName)
		{
			Setup(delay, new string[] {assemblyFileName});
		}

#if CLR_2_0 || CLR_4_0
		public void Setup(int delay, System.Collections.Generic.IList<string> assemblies)
#else
        public void Setup(int delay, System.Collections.IList assemblies)
#endif
		{
            log.Info("Setting up watcher");

			files = new FileInfo[assemblies.Count];
			fileWatchers = new FileSystemWatcher[assemblies.Count];

			for (int i = 0; i < assemblies.Count; i++)
			{
                log.Debug("Setting up FileSystemWatcher for {0}", assemblies[i]);
                
				files[i] = new FileInfo((string)assemblies[i]);

				fileWatchers[i] = new FileSystemWatcher();
				fileWatchers[i].Path = files[i].DirectoryName;
				fileWatchers[i].Filter = files[i].Name;
				fileWatchers[i].NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite;
				fileWatchers[i].Changed += new FileSystemEventHandler(OnChanged);
				fileWatchers[i].EnableRaisingEvents = false;
			}

			timer = new System.Timers.Timer(delay);
			timer.AutoReset = false;
			timer.Enabled = false;
			timer.Elapsed += new ElapsedEventHandler(OnTimer);
		}

		public void Start()
		{
			EnableWatchers( true );
		}

		public void Stop()
		{
			EnableWatchers( false );
		}

		private void EnableWatchers( bool enable )
		{
            if (fileWatchers != null)
    			foreach( FileSystemWatcher watcher in fileWatchers )
	    			watcher.EnableRaisingEvents = enable;
		}

		public void FreeResources()
		{
            log.Info("FreeResources");

            Stop();

			if (fileWatchers != null)
			{
				foreach (FileSystemWatcher watcher in fileWatchers)
				{
                    if (watcher != null)
                    {
                        watcher.Changed -= new FileSystemEventHandler(OnChanged);
                        watcher.Dispose();
                    }
				}
			}

			if (timer != null)
			{
				timer.Stop();
				timer.Close();
			}

			fileWatchers = null;
			timer = null;
		}

		public event AssemblyChangedHandler AssemblyChanged;

		protected void OnTimer(Object source, ElapsedEventArgs e)
		{
			lock(this)
			{
                log.Info("Timer expired");
				PublishEvent();
				timer.Enabled=false;
			}
		}
		
		protected void OnChanged(object source, FileSystemEventArgs e)
		{
            log.Info("File {0} changed", e.Name);

			changedAssemblyPath = e.FullPath;
			if ( timer != null )
			{
				lock(this)
				{
					if(!timer.Enabled)
						timer.Enabled=true;
                    log.Info("Setting timer");
					timer.Start();
				}
			}
			else
			{
				PublishEvent();
			}
		}
	
		protected void PublishEvent()
		{
            if (AssemblyChanged != null)
            {
                log.Debug("Publishing Event to {0} listeners", AssemblyChanged.GetInvocationList().Length);
                AssemblyChanged(changedAssemblyPath);
            }
		}
	}
}