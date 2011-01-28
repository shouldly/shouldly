// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Diagnostics;

namespace NUnit.Core
{
	/// <summary>
	/// Summary description for Logger.
	/// </summary>
	public class InternalTrace
	{
		private readonly static string NL = Environment.NewLine;
        private readonly static string TIME_FMT = "HH:mm:ss.fff";

		private static bool initialized;

        private static InternalTraceWriter writer;
        public static InternalTraceWriter Writer
        {
            get { return writer; }
        }

		public static TraceLevel Level;

        public static void Initialize(string logName)
        {
			Initialize(logName, new TraceSwitch( "NTrace", "NUnit internal trace" ).Level);
        }

        public static void Initialize(string logName, TraceLevel level)
        {
			if (!initialized)
			{
				Level = level;

				if (writer == null && Level > TraceLevel.Off)
				{
					writer = new InternalTraceWriter(logName);
					writer.WriteLine("InternalTrace: Initializing at level " + Level.ToString());
				}

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

        public static void Log(TraceLevel level, string message, string category)
        {
            Log(level, message, category, null);
        }

        public static void Log(TraceLevel level, string message, string category, Exception ex)
        {
            Writer.WriteLine("{0} {1,-5} [{2,2}] {3}: {4}",
                DateTime.Now.ToString(TIME_FMT),
                level == TraceLevel.Verbose ? "Debug" : level.ToString(),
#if NET_2_0
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
