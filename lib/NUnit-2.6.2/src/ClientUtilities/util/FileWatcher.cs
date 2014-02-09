// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Timers;

namespace NUnit.Util
{
    public delegate void FileChangedHandler(string filePath);

    public interface IWatcher
    {
        int Delay { get; set; }

        void Start();
        void Stop();

        event FileChangedHandler Changed;
    }

    public class FileWatcher : IDisposable
    {
        private string filePath;
        private FileSystemWatcher watcher;
        private Timer timer;

        public FileWatcher(string filePath, int delay)
        {
            this.filePath = filePath;
            this.watcher = new FileSystemWatcher();

            watcher.Path = Path.GetDirectoryName(filePath);
            watcher.Filter = Path.GetFileName(filePath);
            watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = false;
            watcher.Changed += new FileSystemEventHandler(OnChange);

            timer = new Timer(delay);
            timer.AutoReset = false;
            timer.Enabled = false;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
        }

        public void Dispose()
        {
            watcher.Dispose();
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

        private void OnChange(object sender, FileSystemEventArgs e)
        {
            if (!timer.Enabled)
                timer.Enabled = true;
            timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;

            if (Changed != null)
                Changed(filePath);
        }

        public event FileChangedHandler Changed;
    }
}
