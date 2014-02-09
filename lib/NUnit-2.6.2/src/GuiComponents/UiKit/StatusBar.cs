// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.UiKit
{
	public class StatusBar : System.Windows.Forms.StatusBar, TestObserver
	{
		private StatusBarPanel statusPanel = new StatusBarPanel();
		private StatusBarPanel testCountPanel = new StatusBarPanel();
		private StatusBarPanel testsRunPanel = new StatusBarPanel();
		private StatusBarPanel errorsPanel = new StatusBarPanel();
		private StatusBarPanel failuresPanel = new StatusBarPanel();
		private StatusBarPanel timePanel = new StatusBarPanel();

		private int testCount = 0;
		private int testsRun = 0;
		private int errors = 0;
		private int failures = 0;
		private double time = 0.0;

		private bool displayProgress = false;

		public bool DisplayTestProgress
		{
			get { return displayProgress; }
			set { displayProgress = value; }
		}

		public StatusBar()
		{
			Panels.Add( statusPanel );
			Panels.Add( testCountPanel );
			Panels.Add( testsRunPanel );
			Panels.Add( errorsPanel );
			Panels.Add( failuresPanel );
			Panels.Add( timePanel );

			statusPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			statusPanel.BorderStyle = StatusBarPanelBorderStyle.None;
			statusPanel.Text = "Status";
			
			testCountPanel.AutoSize = StatusBarPanelAutoSize.Contents;
			testsRunPanel.AutoSize = StatusBarPanelAutoSize.Contents;
			errorsPanel.AutoSize = StatusBarPanelAutoSize.Contents;
			failuresPanel.AutoSize = StatusBarPanelAutoSize.Contents;
			timePanel.AutoSize = StatusBarPanelAutoSize.Contents;

			ShowPanels = true;
			InitPanels();
		}

		// Kluge to keep VS from generating code that sets the Panels for
		// the statusbar. Really, our status bar should be a user control
		// to avoid this and shouldn't allow the panels to be set except
		// according to specific protocols.
		[System.ComponentModel.DesignerSerializationVisibility( 
			 System.ComponentModel.DesignerSerializationVisibility.Hidden )]
		public new System.Windows.Forms.StatusBar.StatusBarPanelCollection Panels
		{
			get 
			{ 
				return base.Panels;
			}
		}

	
		public override string Text
		{
			get { return statusPanel.Text; }
			set { statusPanel.Text = value; }
		}

		public void Initialize( int testCount )
		{
			Initialize( testCount, testCount > 0 ? "Ready" : "" );
		}

		public void Initialize( int testCount, string text )
		{
			this.statusPanel.Text = text;

			this.testCount = testCount;
			this.testsRun = 0;
			this.errors = 0;
			this.failures = 0;
			this.time = 0.0;

			InitPanels();
		}

		private void InitPanels()
		{
			this.testCountPanel.MinWidth = 50;
			DisplayTestCount();

			this.testsRunPanel.MinWidth = 50;
			this.testsRunPanel.Text = "";

			this.errorsPanel.MinWidth = 50;
			this.errorsPanel.Text = "";
			
			this.failuresPanel.MinWidth = 50;
			this.failuresPanel.Text = "";
			
			this.timePanel.MinWidth = 50;
			this.timePanel.Text = "";
		}

		private void DisplayTestCount()
		{
            testCountPanel.Text = "Test Cases : " + testCount.ToString();
		}

		private void DisplayTestsRun()
		{
            testsRunPanel.Text = "Tests Run : " + testsRun.ToString();
		}

		private void DisplayErrors()
		{
            errorsPanel.Text = "Errors : " + errors.ToString();
		}

		private void DisplayFailures()
		{
            failuresPanel.Text = "Failures : " + failures.ToString();
		}

		private void DisplayTime()
		{
            timePanel.Text = "Time : " + time.ToString();
		}

		private void DisplayResult(TestResult result)
		{
			ResultSummarizer summarizer = new ResultSummarizer(result);

            //this.testCount = summarizer.ResultCount;
            this.testsRun = summarizer.TestsRun;
			this.errors = summarizer.Errors;
            this.failures = summarizer.Failures;
            this.time = summarizer.Time;

            DisplayTestCount();
            DisplayTestsRun();
            DisplayErrors();
            DisplayFailures();
            DisplayTime();
        }

		public void OnTestLoaded( object sender, TestEventArgs e )
		{
			Initialize( e.TestCount );
		}

		public void OnTestReloaded( object sender, TestEventArgs e )
		{
			Initialize( e.TestCount, "Reloaded" );
		}

		public void OnTestUnloaded( object sender, TestEventArgs e )
		{
			Initialize( 0, "Unloaded" );
		}

		private void OnRunStarting( object sender, TestEventArgs e )
		{
			Initialize( e.TestCount, "Running :" + e.Name );
			DisplayTestCount();
            DisplayTestsRun();
            DisplayErrors();
            DisplayFailures();
            DisplayTime();
        }

		private void OnRunFinished(object sender, TestEventArgs e )
		{
			if ( e.Exception != null )
				statusPanel.Text = "Failed";
			else
			{
				statusPanel.Text = "Completed";
				DisplayResult( e.Result );
			}
		}

		public void OnTestStarting( object sender, TestEventArgs e )
		{
			string fullText = "Running : " + e.TestName.FullName;
			string shortText = "Running : " + e.TestName.Name;

			Graphics g = Graphics.FromHwnd( Handle );
			SizeF sizeNeeded = g.MeasureString( fullText, Font );
			if ( statusPanel.Width >= (int)sizeNeeded.Width )
			{
				statusPanel.Text = fullText;
				statusPanel.ToolTipText = "";
			}
			else
			{
				sizeNeeded = g.MeasureString( shortText, Font );
				statusPanel.Text = statusPanel.Width >= (int)sizeNeeded.Width
					? shortText : e.TestName.Name;
				statusPanel.ToolTipText = e.TestName.FullName;
			}
		}

		private void OnTestFinished( object sender, TestEventArgs e )
		{
			if ( DisplayTestProgress && e.Result.Executed )
			{
				++testsRun;
				DisplayTestsRun();
                switch ( e.Result.ResultState )
                {
                    case ResultState.Error:
                    case ResultState.Cancelled:
    					++errors;
	    				DisplayErrors();
                        break;
                    case ResultState.Failure:
    					++failures;
	    				DisplayFailures();
                        break;
				}
			}
		}

//        protected override void OnFontChanged(EventArgs e)
//        {
//            base.OnFontChanged(e);
//
//            this.Height = (int)(this.Font.Height * 1.6);
//        }

		#region TestObserver Members

		public void Subscribe(ITestEvents events)
		{
			events.TestLoaded	+= new TestEventHandler( OnTestLoaded );
			events.TestReloaded	+= new TestEventHandler( OnTestReloaded );
			events.TestUnloaded	+= new TestEventHandler( OnTestUnloaded );

			events.TestStarting	+= new TestEventHandler( OnTestStarting );
			events.TestFinished	+= new TestEventHandler( OnTestFinished );
			events.RunStarting	+= new TestEventHandler( OnRunStarting );
			events.RunFinished	+= new TestEventHandler( OnRunFinished );
		}

		#endregion
	}
}
