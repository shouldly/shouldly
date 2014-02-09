// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using NUnit.UiKit;
using NUnit.Util;
using NUnit.Core;

namespace NUnit.Gui
{
    public class TestAssemblyInfoForm : ScrollingTextDisplayForm
    {
        protected override void OnLoad(EventArgs e)
        {
            this.Text = "Test Assemblies";
            this.TextBox.WordWrap = false;
            //this.TextBox.ContentsResized += new ContentsResizedEventHandler(TextBox_ContentsResized);
            this.TextBox.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8.25F);

            base.OnLoad(e);

            Process p = Process.GetCurrentProcess();
            int currentProcessId = p.Id;
            string currentDomainName = "";

            AppendProcessInfo(
			      currentProcessId, 
			      Path.GetFileName(Assembly.GetEntryAssembly().Location), 
			      RuntimeFramework.CurrentFramework );

            foreach (TestAssemblyInfo info in Services.TestLoader.AssemblyInfo)
            {
                if (info.ProcessId != currentProcessId)
                {
                    this.TextBox.AppendText("\r\n");
                    AppendProcessInfo(info);
                    currentProcessId = info.ProcessId;
                }

                if (info.DomainName != currentDomainName)
                {
                    AppendDomainInfo(info);
                    currentDomainName = info.DomainName;
                }

                AppendAssemblyInfo(info);
            }
			
			TextBox.Select(0,0);
			TextBox.ScrollToCaret();
        }

        private void AppendProcessInfo(TestAssemblyInfo info)
        {
            AppendProcessInfo(info.ProcessId, info.ModuleName, info.RunnerRuntimeFramework);
        }

        private void AppendProcessInfo( int pid, string moduleName, RuntimeFramework framework )
        {
            AppendBoldText(string.Format("{0} ( {1} )\r\n", moduleName, pid));

            TextBox.AppendText(string.Format(
                "  Framework Version: {0}\r\n",
                framework.DisplayName));

            TextBox.AppendText(string.Format(
                "  CLR Version: {0}\r\n",
                framework.ClrVersion.ToString()));
        }

        private void AppendDomainInfo(TestAssemblyInfo info)
        {
            AppendBoldText(string.Format("\r\n  {0}\r\n", info.DomainName));

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat( "    ApplicationBase: {0}\r\n", info.ApplicationBase );

            if (info.PrivateBinPath != null)
            {
                string prefix = "    PrivateBinPath: ";
                foreach (string s in info.PrivateBinPath.Split(new char[] { ';' }))
                {
                    sb.AppendFormat("{0}{1}\r\n", prefix, s);
                    prefix = "                    ";
                }
            }

            sb.AppendFormat("    Configuration File: {0}\r\n", info.ConfigurationFile); 

            TextBox.AppendText(sb.ToString());
        }

        private void AppendAssemblyInfo(TestAssemblyInfo info)
        {
            AppendBoldText(
                string.Format("    {0}\r\n", Path.GetFileNameWithoutExtension(info.Name)));

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("      Path: {0}\r\n", info.Name);
            sb.AppendFormat("      Image Runtime Version: {0}\r\n", info.ImageRuntimeVersion.ToString());

            if (info.TestFrameworks != null)
            {
                string prefix = "      Uses: ";
                foreach (AssemblyName framework in info.TestFrameworks)
                {
                    sb.AppendFormat("{0}{1}\r\n", prefix, framework.FullName);
                    prefix = "            ";
                }
            }

            TextBox.AppendText(sb.ToString());
        }

        private void AppendBoldText(string text)
        {
            TextBox.Select(TextBox.Text.Length, 0);
            TextBox.SelectionFont = new Font(TextBox.Font, FontStyle.Bold);

            TextBox.SelectedText += text;
        }

		void TextBox_ContentsResized(object sender, ContentsResizedEventArgs e)
		{
            int increase = e.NewRectangle.Width - TextBox.ClientSize.Width;
            if (increase > 0)
            {
                TextBox.Width += increase;
                this.Width += increase;
            }
		}
	}
}
