// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Diagnostics;

namespace NUnit.Core
{
    public class Logger
    {
        private string name;
        private string fullname;

        public Logger(string name)
        {
            this.fullname = this.name = name;
            int index = fullname.LastIndexOf('.');
            if (index >= 0)
                this.name = fullname.Substring(index + 1);
        }

        #region Error
        public void Error(string message)
        {
            Log(TraceLevel.Error, message);
        }

        public void Error(string message, params object[] args)
        {
            Log(TraceLevel.Error, message, args);
        }

        public void Error(string message, Exception ex)
        {
            if (InternalTrace.Level >= TraceLevel.Error)
            {
                InternalTrace.Log(TraceLevel.Error, message, name, ex);
            }
        }
        #endregion

        #region Warning
        public void Warning(string message)
        {
            Log(TraceLevel.Warning, message);
        }

        public void Warning(string message, params object[] args)
        {
            Log(TraceLevel.Warning, message, args);
        }
        #endregion

        #region Info
        public void Info(string message)
        {
            Log(TraceLevel.Info, message);
        }

        public void Info(string message, params object[] args)
        {
            Log(TraceLevel.Info, message, args);
        }
        #endregion

        #region Debug
        public void Debug(string message)
        {
            Log(TraceLevel.Verbose, message);
        }

        public void Debug(string message, params object[] args)
        {
            Log(TraceLevel.Verbose, message, args);
        }
        #endregion

        #region Helper Methods
        public void Log(TraceLevel level, string message)
        {
            if (InternalTrace.Level >= level)
                InternalTrace.Log(level, message, name);
        }

        private void Log(TraceLevel level, string format, params object[] args)
        {
            if (InternalTrace.Level >= level)
                Log(level, string.Format( format, args ) );
        }
        #endregion
    }
}
