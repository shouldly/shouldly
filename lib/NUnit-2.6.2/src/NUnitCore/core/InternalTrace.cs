// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Core
{
    /// <summary>
    /// InternalTraceLevel is an enumeration controlling the
    /// level of detailed presented in the internal log.
    /// </summary>
    public enum InternalTraceLevel
    {
        Default,
        Off,
        Error,
        Warning,
        Info,
        Verbose
    }
    
    /// <summary>
	/// Summary description for Logger.
	/// </summary>
	public class InternalTrace
	{
        private readonly static string TIME_FMT = "HH:mm:ss.fff";

		private static bool initialized;
		private static InternalTraceLevel level;
        private static string logName;

        private static InternalTraceWriter writer;
        public static InternalTraceWriter Writer
        {
            get { return writer; }
        }

        private static string LogName
        {
            get { return logName; }
            set { logName = value; }
        }

        public static InternalTraceLevel Level
        {
            get { return level; }
            set 
            {
                if (level != value)
                {
                    level = value;

                    if (writer == null && Level > InternalTraceLevel.Off)
                    {
                        writer = new InternalTraceWriter(logName);
                        writer.WriteLine("InternalTrace: Initializing at level " + Level.ToString());
                    }
                }
            }
        }

        //public static void Initialize(string logName)
        //{
        //    int lev = (int) new System.Diagnostics.TraceSwitch("NTrace", "NUnit internal trace").Level;
        //    Initialize(logName, (InternalTraceLevel)lev);
        //}

        public static void Initialize(string logName, InternalTraceLevel level)
        {
			if (!initialized)
			{
                LogName = logName;
				Level = level;

				initialized = true;
			}
        }

        public static void Flush()
        {
            if (writer != null)
                writer.Flush();
        }

        public static void Close()
        {
            if (writer != null)
                writer.Close();

            writer = null;
        }

        public static Logger GetLogger(string name)
		{
			return new Logger( name );
		}

		public static Logger GetLogger( Type type )
		{
			return new Logger( type.FullName );
		}

        public static void Log(InternalTraceLevel level, string message, string category)
        {
            Log(level, message, category, null);
        }

        public static void Log(InternalTraceLevel level, string message, string category, Exception ex)
        {
            Writer.WriteLine("{0} {1,-5} [{2,2}] {3}: {4}",
                DateTime.Now.ToString(TIME_FMT),
                level == InternalTraceLevel.Verbose ? "Debug" : level.ToString(),
#if CLR_2_0 || CLR_4_0
                System.Threading.Thread.CurrentThread.ManagedThreadId,
#else
                AppDomain.GetCurrentThreadId(),
#endif
                category,
                message);

            if (ex != null)
                Writer.WriteLine(ex.ToString());
        }
    }
}
