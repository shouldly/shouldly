// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;
using BF = System.Reflection.BindingFlags;

namespace NUnit.Core
{
	/// <summary>
	/// Proxy class for operations on a real log4net appender,
	/// allowing NUnit to work with multiple versions of log4net
	/// and to fail gracefully if no log4net assembly is present.
	/// </summary>
	public class Log4NetCapture : TextCapture
	{
        /// <summary>
        /// The TextWriter to which text is redirected
        /// </summary>
        private TextWriter writer;

        /// <summary>
        /// The threshold for capturing text. A value of "Off"
        /// means that no text is captured. A value of "All"
        /// should be taken to mean the highest possible level
        /// of verbosity supported by the derived class. The 
        /// meaning of any other values is determined by the 
        /// derived class.
        /// </summary>
        private LoggingThreshold threshold = LoggingThreshold.Off;

        private Assembly log4netAssembly;
		private Type appenderType;
		private Type basicConfiguratorType;

		private object appender;
		private bool isInitialized;

		// Layout codes that work for versions from 
		// log4net 1.2.0.30714 to 1.2.10:
		//
		//	%a = domain friendly name
		//	%c = logger name (%c{1} = last component )
		//	%d = date and time
		//	%d{ABSOLUTE} = time only
		//	%l = source location of the error
		//	%m = message
		//	%n = newline
		//	%p = level
		//	%r = elapsed milliseconds since program start
		//	%t = thread
		//	%x = nested diagnostic content (NDC)
		private static readonly string logFormat =
			"%d{ABSOLUTE} %-5p [%4t] %c{1} [%x]- %m%n";

        /// <summary>
        /// Gets or sets the TextWriter to which text is redirected
        /// when captured. The value may only be changed when the
        /// logging threshold is set to "Off"
        /// </summary>
        public override TextWriter Writer
        {
            get { return writer; }
            set
            {
                if (threshold != LoggingThreshold.Off)
                    throw new System.InvalidOperationException(
                        "Writer may not be changed while capture is enabled");

                writer = value;
            }
        }

        /// <summary>
        /// Gets or sets the capture threshold value, which represents
        /// the degree of verbosity of the output text stream.
        /// Derived classes may supply multiple levels of capture but
        /// must retain the use of the "Off" setting to represent 
        /// no logging.
        /// </summary>
        public override LoggingThreshold Threshold
        {
            get { return threshold; }
            set
            {
                if (value != threshold)
                {
                    bool turnOff = value == LoggingThreshold.Off;
                    //bool turnOn = threshold == LoggingThreshold.Off;

                    //if (turnOff)
                        StopCapture();

                    threshold = value;

                    if (!turnOff)
                        StartCapture();
                }
            }
        }
        
        private void StartCapture()
		{
            if (IsLog4netAvailable)
			{
                string threshold = Threshold.ToString();
				if ( !SetLoggingThreshold( threshold ) )
					SetLoggingThreshold( "Error" );

				SetAppenderTextWriter( this.Writer );
				ConfigureAppender();
			}
		}

        private void ResumeCapture()
        {
            if (IsLog4netAvailable)
            {
                SetLoggingThreshold(Threshold.ToString());
                ConfigureAppender();
            }
        }

		private void StopCapture()
		{
            if ( writer != null )
                writer.Flush();

			if ( appender != null )
			{
				SetLoggingThreshold( "Off" );
                //SetAppenderTextWriter( null );
			}
		}

		#region Private Properties and Methods

        private bool IsLog4netAvailable
        {
            get
            {
                if (!isInitialized)
                    InitializeTypes();

                return log4netAssembly != null && basicConfiguratorType != null && appenderType != null;
            }       
        }

        private void InitializeTypes()
        {
            try
            {
                log4netAssembly = Assembly.Load("log4net");

                if (log4netAssembly != null)
                {
                    appenderType = log4netAssembly.GetType(
                        "log4net.Appender.TextWriterAppender", false, false);

                    basicConfiguratorType = log4netAssembly.GetType(
                        "log4net.Config.BasicConfigurator", false, false);

                    appender = TryCreateAppender();
                    if (appender != null)
                        SetAppenderLogFormat(logFormat);
                }
            }
            catch
            {
            }
            finally
            {
                isInitialized = true;
            }
        }

		/// <summary>
		/// Attempt to create a TextWriterAppender using reflection,
		/// failing silently if it is not possible.
		/// </summary>
		private object TryCreateAppender()
		{
			ConstructorInfo ctor = appenderType.GetConstructor( Type.EmptyTypes );
			object appender = ctor.Invoke( new object[0] );

			return appender;
		}

		private void SetAppenderLogFormat( string logFormat )
		{
			Type patternLayoutType = log4netAssembly.GetType( 
				"log4net.Layout.PatternLayout", false, false );
			if ( patternLayoutType == null ) return;

			ConstructorInfo ctor = patternLayoutType.GetConstructor( new Type[] { typeof(string) } );
			if ( ctor != null )
			{
				object patternLayout = ctor.Invoke( new object[] { logFormat } );

				if ( patternLayout != null )
				{
					PropertyInfo prop = appenderType.GetProperty( "Layout", BF.Public | BF.Instance | BF.SetProperty );
					if ( prop != null )
						prop.SetValue( appender, patternLayout, null );
				}
			} 
		}

		private bool SetLoggingThreshold( string threshold )
		{
			PropertyInfo prop = appenderType.GetProperty( "Threshold", BF.Public | BF.Instance | BF.SetProperty );
			if ( prop == null ) return false;

			Type levelType = prop.PropertyType;
			FieldInfo levelField = levelType.GetField( threshold, BF.Public | BF.Static | BF.IgnoreCase );
			if ( levelField == null ) return false;

			object level = levelField.GetValue( null );
			prop.SetValue( appender, level, null );
			return true;
		}

		private void SetAppenderTextWriter( TextWriter writer )
		{
			PropertyInfo prop = appenderType.GetProperty( "Writer", BF.Instance | BF.Public | BF.SetProperty );
			if ( prop != null )
				prop.SetValue( appender, writer, null );
		}

		private void ConfigureAppender()
		{
			MethodInfo configureMethod = basicConfiguratorType.GetMethod( "Configure", new Type[] { appenderType } );
			if ( configureMethod != null )
				configureMethod.Invoke( null, new object[] { appender } );
		}
		#endregion
	}
}
