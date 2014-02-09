// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;

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
            Log(InternalTraceLevel.Error, message);
        }

        public void Error(string message, params object[] args)
        {
            Log(InternalTraceLevel.Error, message, args);
        }

        public void Error(string message, Exception ex)
        {
            if (InternalTrace.Level >= InternalTraceLevel.Error)
            {
                InternalTrace.Log(InternalTraceLevel.Error, message, name, ex);
            }
        }
        #endregion

        #region Warning
        public void Warning(string message)
        {
            Log(InternalTraceLevel.Warning, message);
        }

        public void Warning(string message, params object[] args)
        {
            Log(InternalTraceLevel.Warning, message, args);
        }
        #endregion

        #region Info
        public void Info(string message)
        {
            Log(InternalTraceLevel.Info, message);
        }

        public void Info(string message, params object[] args)
        {
            Log(InternalTraceLevel.Info, message, args);
        }
        #endregion

        #region Debug
        public void Debug(string message)
        {
            Log(InternalTraceLevel.Verbose, message);
        }

        public void Debug(string message, params object[] args)
        {
            Log(InternalTraceLevel.Verbose, message, args);
        }
        #endregion

        #region Helper Methods
        public void Log(InternalTraceLevel level, string message)
        {
            if (InternalTrace.Level >= level)
                InternalTrace.Log(level, message, name);
        }

        private void Log(InternalTraceLevel level, string format, params object[] args)
        {
            if (InternalTrace.Level >= level)
                Log(level, string.Format( format, args ) );
        }
        #endregion
    }
}
