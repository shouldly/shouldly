// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Util
{
	using System;
	using System.Globalization;
	using System.IO;
	using System.Xml;
    using System.Collections;
	using System.Reflection;
	using NUnit.Core;

	/// <summary>
	/// Summary description for XmlResultWriter.
	/// </summary>
	public class XmlResultWriter
	{
		private XmlTextWriter xmlWriter;
		private TextWriter writer;
		private MemoryStream memoryStream;

		#region Constructors
		public XmlResultWriter(string fileName)
		{
			xmlWriter = new XmlTextWriter( new StreamWriter(fileName, false, System.Text.Encoding.UTF8) );
		}

        public XmlResultWriter( TextWriter writer )
		{
			this.memoryStream = new MemoryStream();
			this.writer = writer;
			this.xmlWriter = new XmlTextWriter( new StreamWriter( memoryStream, System.Text.Encoding.UTF8 ) );
		}
		#endregion

		private void InitializeXmlFile(TestResult result) 
		{
			ResultSummarizer summaryResults = new ResultSummarizer(result);

			xmlWriter.Formatting = Formatting.Indented;
			xmlWriter.WriteStartDocument(false);
			xmlWriter.WriteComment("This file represents the results of running a test suite");

			xmlWriter.WriteStartElement("test-results");

			xmlWriter.WriteAttributeString("name", summaryResults.Name);
			xmlWriter.WriteAttributeString("total", summaryResults.TestsRun.ToString());
            xmlWriter.WriteAttributeString("errors", summaryResults.Errors.ToString());
            xmlWriter.WriteAttributeString("failures", summaryResults.Failures.ToString());
            xmlWriter.WriteAttributeString("not-run", summaryResults.TestsNotRun.ToString());
            xmlWriter.WriteAttributeString("inconclusive", summaryResults.Inconclusive.ToString());
            xmlWriter.WriteAttributeString("ignored", summaryResults.Ignored.ToString());
            xmlWriter.WriteAttributeString("skipped", summaryResults.Skipped.ToString());
            xmlWriter.WriteAttributeString("invalid", summaryResults.NotRunnable.ToString());

			DateTime now = DateTime.Now;
			xmlWriter.WriteAttributeString("date", XmlConvert.ToString( now, "yyyy-MM-dd" ) );
			xmlWriter.WriteAttributeString("time", XmlConvert.ToString( now, "HH:mm:ss" ));
			WriteEnvironment();
			WriteCultureInfo();
		}

		private void WriteCultureInfo() {
			xmlWriter.WriteStartElement("culture-info");
			xmlWriter.WriteAttributeString("current-culture",
			                               CultureInfo.CurrentCulture.ToString());
			xmlWriter.WriteAttributeString("current-uiculture",
			                               CultureInfo.CurrentUICulture.ToString());
			xmlWriter.WriteEndElement();
		}

		private void WriteEnvironment() {
			xmlWriter.WriteStartElement("environment");
			xmlWriter.WriteAttributeString("nunit-version", 
										   Assembly.GetExecutingAssembly().GetName().Version.ToString());
			xmlWriter.WriteAttributeString("clr-version", 
			                               Environment.Version.ToString());
			xmlWriter.WriteAttributeString("os-version",
			                               Environment.OSVersion.ToString());
			xmlWriter.WriteAttributeString("platform",
				Environment.OSVersion.Platform.ToString());
			xmlWriter.WriteAttributeString("cwd",
			                               Environment.CurrentDirectory);
			xmlWriter.WriteAttributeString("machine-name",
			                               Environment.MachineName);
			xmlWriter.WriteAttributeString("user",
			                               Environment.UserName);
			xmlWriter.WriteAttributeString("user-domain",
			                               Environment.UserDomainName);
			xmlWriter.WriteEndElement();
		}

		#region Public Methods
		public void SaveTestResult( TestResult result )
		{
			InitializeXmlFile( result );
			WriteResultElement( result );
			TerminateXmlFile();
		}

        private void WriteResultElement( TestResult result )
        {
			StartTestElement( result );

			WriteCategoriesElement(result);
			WritePropertiesElement(result);

            switch (result.ResultState)
            {
                case ResultState.Ignored:
                case ResultState.NotRunnable:
                case ResultState.Skipped:
                    WriteReasonElement(result);
                    break;

                case ResultState.Failure:
                case ResultState.Error:
                case ResultState.Cancelled:
                    if (!result.Test.IsSuite || result.FailureSite == FailureSite.SetUp)
                        WriteFailureElement(result);
                    break;
                case ResultState.Success:
                case ResultState.Inconclusive:
                    if (result.Message != null)
                        WriteReasonElement(result);
                    break;
            }

			if ( result.HasResults )
				WriteChildResults( result );

			xmlWriter.WriteEndElement(); // test element
		}

		private void TerminateXmlFile()
		{
			try 
			{
				xmlWriter.WriteEndElement(); // test-results
				xmlWriter.WriteEndDocument();
				xmlWriter.Flush();

				if ( memoryStream != null && writer != null )
				{
					memoryStream.Position = 0;
					using ( StreamReader rdr = new StreamReader( memoryStream ) )
					{
						writer.Write( rdr.ReadToEnd() );
					}
				}

				xmlWriter.Close();
			} 
			finally 
			{
				//writer.Close();
			}
		}
		#endregion

		#region Element Creation Helpers
		private void StartTestElement(TestResult result)
		{
            if (result.Test.IsSuite)
            {
                xmlWriter.WriteStartElement("test-suite");
                xmlWriter.WriteAttributeString("type", result.Test.TestType);
                xmlWriter.WriteAttributeString("name", result.Name);
            }
            else
            {
                xmlWriter.WriteStartElement("test-case");
                xmlWriter.WriteAttributeString("name", result.FullName);
            }

			if (result.Description != null)
				xmlWriter.WriteAttributeString("description", result.Description);

			xmlWriter.WriteAttributeString("executed", result.Executed.ToString());
            xmlWriter.WriteAttributeString("result", result.ResultState.ToString());
			
			if ( result.Executed )
			{
				xmlWriter.WriteAttributeString("success", result.IsSuccess.ToString());
				xmlWriter.WriteAttributeString("time", result.Time.ToString("#####0.000", NumberFormatInfo.InvariantInfo));
				xmlWriter.WriteAttributeString("asserts", result.AssertCount.ToString());
			}
		}

		private void WriteCategoriesElement(TestResult result)
		{
			if (result.Test.Categories != null && result.Test.Categories.Count > 0)
			{
				xmlWriter.WriteStartElement("categories");
				foreach (string category in result.Test.Categories)
				{
					xmlWriter.WriteStartElement("category");
					xmlWriter.WriteAttributeString("name", category);
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
			}
		}

		private void WritePropertiesElement(TestResult result)
		{
            IDictionary props = result.Test.Properties;

			if (result.Test.Properties != null && props.Count > 0)
			{
                int nprops = 0;

				foreach (string key in result.Test.Properties.Keys)
				{
                    if ( !key.StartsWith("_") )
                    {
                        object val = result.Test.Properties[key];
                        if (val != null)
                        {
                            if ( nprops == 0 )
                                xmlWriter.WriteStartElement("properties");

                            xmlWriter.WriteStartElement("property");
                            xmlWriter.WriteAttributeString("name", key);
                            xmlWriter.WriteAttributeString("value", val.ToString());
                            xmlWriter.WriteEndElement();

                            ++nprops;
                        }
                    }
				}

                if ( nprops > 0 )
				    xmlWriter.WriteEndElement();
			}
		}

		private void WriteReasonElement(TestResult result)
		{
			xmlWriter.WriteStartElement("reason");
			xmlWriter.WriteStartElement("message");
			xmlWriter.WriteCData(result.Message);
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
		}

		private void WriteFailureElement(TestResult result)
		{
			xmlWriter.WriteStartElement("failure");

			xmlWriter.WriteStartElement("message");
            WriteCData(result.Message);
			xmlWriter.WriteEndElement();

			xmlWriter.WriteStartElement("stack-trace");
            if (result.StackTrace != null)
                WriteCData(StackTraceFilter.Filter(result.StackTrace));
			xmlWriter.WriteEndElement();

			xmlWriter.WriteEndElement();
		}

		private void WriteChildResults(TestResult result)
		{
			xmlWriter.WriteStartElement("results");

			if ( result.HasResults )
				foreach (TestResult childResult in result.Results)
					WriteResultElement( childResult );

			xmlWriter.WriteEndElement();
		}
		#endregion

		#region Output Helpers
		/// <summary>
		/// Makes string safe for xml parsing, replacing control chars with '?'
		/// </summary>
		/// <param name="encodedString">string to make safe</param>
		/// <returns>xml safe string</returns>
		private static string CharacterSafeString(string encodedString)
		{
			/*The default code page for the system will be used.
			Since all code pages use the same lower 128 bytes, this should be sufficient
			for finding uprintable control characters that make the xslt processor error.
			We use characters encoded by the default code page to avoid mistaking bytes as
			individual characters on non-latin code pages.*/
			char[] encodedChars = System.Text.Encoding.Default.GetChars(System.Text.Encoding.Default.GetBytes(encodedString));
			
			System.Collections.ArrayList pos = new System.Collections.ArrayList();
			for(int x = 0 ; x < encodedChars.Length ; x++)
			{
				char currentChar = encodedChars[x];
				//unprintable characters are below 0x20 in Unicode tables
				//some control characters are acceptable. (carriage return 0x0D, line feed 0x0A, horizontal tab 0x09)
				if(currentChar < 32 && (currentChar != 9 && currentChar != 10 && currentChar != 13))
				{
					//save the array index for later replacement.
					pos.Add(x);
				}
			}
			foreach(int index in pos)
			{
				encodedChars[index] = '?';//replace unprintable control characters with ?(3F)
			}
			return System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(encodedChars));
		}

        private void WriteCData(string text)
        {
            int start = 0;
            while (true)
            {
                int illegal = text.IndexOf("]]>", start);
                if (illegal < 0)
                    break;
                xmlWriter.WriteCData(text.Substring(start, illegal - start + 2));
                start = illegal + 2;
                if (start >= text.Length)
                    return;
            }

            if (start > 0)
                xmlWriter.WriteCData(text.Substring(start));
            else
                xmlWriter.WriteCData(text);
        }

		#endregion
	}
}
