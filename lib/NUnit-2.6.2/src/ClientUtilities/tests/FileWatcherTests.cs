// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using NUnit.Framework;
using System.Text;
using System.Timers;

namespace NUnit.Util.Tests
{
    [TestFixture]
    [Platform(Exclude = "Win95,Win98,WinMe")]
    public class FileWatcherTests
    {
        private FileWatcher watcher;
        private CounterEventHandler handler;
        private static int watcherDelayMs = 100;
        private string fileName;
        private string tempFileName;

        [SetUp]
        public void CreateFile()
        {
            string tempDir = Path.GetTempPath();
            fileName = Path.Combine(tempDir, "temp.txt");
            tempFileName = Path.Combine(tempDir, "newTempFile.txt");

            StreamWriter writer = new StreamWriter(fileName);
            writer.Write("Hello");
            writer.Close();

            handler = new CounterEventHandler();
            watcher = new FileWatcher(fileName, watcherDelayMs);
            watcher.Changed += new FileChangedHandler(handler.OnChanged);
            watcher.Start();
        }

        [TearDown]
        public void DeleteFile()
        {
            watcher.Stop();
            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.Delete();

            FileInfo temp = new FileInfo(tempFileName);
            if (temp.Exists) temp.Delete();
        }

        [Test]
        [Platform("Linux, Net", Reason="Fails under Mono on Windows")]
        public void MultipleCloselySpacedChangesTriggerWatcherOnlyOnce()
        {
            for (int i = 0; i < 3; i++)
            {
                StreamWriter writer = new StreamWriter(fileName, true);
                writer.WriteLine("Data");
                writer.Close();
                System.Threading.Thread.Sleep(20);
            }
            WaitForTimerExpiration();
            Assert.AreEqual(1, handler.Counter);
            Assert.AreEqual(Path.GetFullPath(fileName), handler.FileName);
        }

        [Test]
        [Platform("Linux, Net", Reason="Fails under Mono on Windows")]
        public void ChangingFileTriggersWatcher()
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.Write("Goodbye");
            writer.Close();

            WaitForTimerExpiration();
            Assert.AreEqual(1, handler.Counter);
            Assert.AreEqual(Path.GetFullPath(fileName), handler.FileName);
        }

        [Test]
        [Platform(Exclude = "Linux", Reason = "Attribute change triggers watcher")]
        public void ChangingAttributesDoesNotTriggerWatcher()
        {
            FileInfo fi = new FileInfo(fileName);
            FileAttributes attr = fi.Attributes;
            fi.Attributes = FileAttributes.Hidden | attr;

            WaitForTimerExpiration();
            Assert.AreEqual(0, handler.Counter);
        }

        [Test]
        public void CopyingFileDoesNotTriggerWatcher()
        {
            FileInfo fi = new FileInfo(fileName);
            fi.CopyTo(tempFileName);
            fi.Delete();

            WaitForTimerExpiration();
            Assert.AreEqual(0, handler.Counter);
        }

        private static void WaitForTimerExpiration()
        {
            System.Threading.Thread.Sleep(watcherDelayMs * 2);
        }

        private class CounterEventHandler
        {
            int counter;
            String fileName;
            public int Counter
            {
                get { return counter; }
            }
            public String FileName
            {
                get { return fileName; }
            }

            public void OnChanged(String fullPath)
            {
                fileName = fullPath;
                counter++;
            }
        }
    }
}
