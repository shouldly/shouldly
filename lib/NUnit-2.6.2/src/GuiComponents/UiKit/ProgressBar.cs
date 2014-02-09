// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// ColorProgressBar provides a custom progress bar with the
	/// ability to control the color of the bar and to render itself
	/// in either solid or segmented style. The bar can be updated
	/// on the fly and has code to avoid repainting the entire bar
	/// when that occurs.
	/// </summary>
	public class ColorProgressBar : System.Windows.Forms.Control
	{
		#region Instance Variables
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// The current progress value
		/// </summary>
		private int val = 0;

		/// <summary>
		/// The minimum value allowed
		/// </summary>
		private int min = 0;

		/// <summary>
		/// The maximum value allowed
		/// </summary>
		private int max = 100;

		/// <summary>
		/// Amount to advance for each step
		/// </summary>
		private int step = 1;

		/// <summary>
		/// Last segment displayed when displaying asynchronously rather 
		/// than through OnPaint calls.
		/// </summary>
		private int lastSegmentCount=0;

		/// <summary>
		/// The brush to use in painting the progress bar
		/// </summary>
		private Brush foreBrush = null;

		/// <summary>
		/// The brush to use in painting the background of the bar
		/// </summary>
		private Brush backBrush = null;

		/// <summary>
		/// Indicates whether to draw the bar in segments or not
		/// </summary>
		private bool segmented = false;

		#endregion

		#region Constructors & Disposer
		public ColorProgressBar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				this.ReleaseBrushes();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Properties
		
		[Category("Behavior")]
		public int Minimum
		{
			get { return this.min; }
			set
			{
				if (value <= Maximum) 
				{
					if (this.min != value) 
					{
						this.min = value;
						this.Invalidate();
					}
				}
				else
				{
					throw new ArgumentOutOfRangeException("Minimum", value
						,"Minimum must be <= Maximum.");
				}
			}
		}

		[Category("Behavior")]
		public int Maximum 
		{
			get	{ return this.max; }
			set
			{
				if (value >= Minimum) 
				{
					if (this.max != value) 
					{
						this.max = value;
						this.Invalidate();
					}
				}
				else
				{
					throw new ArgumentOutOfRangeException("Maximum", value
						,"Maximum must be >= Minimum.");
				}
			}
		}

		[Category("Behavior")]
		public int Step
		{
			get	{ return this.step; }
			set
			{
				if (value <= Maximum && value >= Minimum) 
				{
					this.step = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException("Step", value
						,"Must fall between Minimum and Maximum inclusive.");
				}
			}
		}
		
		[Browsable(false)]
		private float PercentValue
		{
			get
            {
                if (0 != Maximum - Minimum) // NRG 05/28/03: Prevent divide by zero
                    return((float)this.val / ((float)Maximum - (float)Minimum));
                else
                    return(0);
            }
		}	

		[Category("Behavior")]
		public int Value 
		{
			get { return this.val; }
			set 
			{
				if(value == this.val)
					return;
				else if(value <= Maximum && value >= Minimum)
				{
					this.val = value;
					this.Invalidate();
				}
				else
				{
					throw new ArgumentOutOfRangeException("Value", value
						,"Must fall between Minimum and Maximum inclusive.");
				}
			}
		}

		[Category("Appearance")]
		public bool Segmented
		{
			get { return segmented; }
			set { segmented = value; }
		}

		#endregion

		#region Methods

		protected override void OnCreateControl()
		{
		}

		public void PerformStep()
		{
			int newValue = Value + Step;

			if( newValue > Maximum )
				newValue = Maximum;

			Value = newValue;
		}

		protected override void OnBackColorChanged(System.EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.Refresh();
		}
		protected override void OnForeColorChanged(System.EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.Refresh();
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.lastSegmentCount=0;
			this.ReleaseBrushes();
			PaintBar(e.Graphics);
			ControlPaint.DrawBorder3D(
				e.Graphics
				,this.ClientRectangle
				,Border3DStyle.SunkenOuter);
			//e.Graphics.Flush();
		}

		private void ReleaseBrushes()
		{
			if(foreBrush != null)
			{
				foreBrush.Dispose();
				backBrush.Dispose();
				foreBrush=null;
				backBrush=null;
			}
		}

		private void AcquireBrushes()
		{
			if(foreBrush == null)
			{
				foreBrush = new SolidBrush(this.ForeColor);
				backBrush = new SolidBrush(this.BackColor);
			}
		}

		private void PaintBar(Graphics g)
		{
			Rectangle theBar = Rectangle.Inflate(ClientRectangle, -2, -2);
			int maxRight = theBar.Right-1;
			this.AcquireBrushes();

			if ( segmented )
			{
				int segmentWidth = (int)((float)ClientRectangle.Height * 0.66f);
				int maxSegmentCount = ( theBar.Width + segmentWidth ) / segmentWidth;

				//int maxRight = Bar.Right;
				int newSegmentCount = (int)System.Math.Ceiling(PercentValue*maxSegmentCount);
				if(newSegmentCount > lastSegmentCount)
				{
					theBar.X += lastSegmentCount*segmentWidth;
					while (lastSegmentCount < newSegmentCount )
					{
						theBar.Width = System.Math.Min( maxRight - theBar.X, segmentWidth - 2 );
						g.FillRectangle( foreBrush, theBar );
						theBar.X += segmentWidth;
						lastSegmentCount++;
					}
				}
				else if(newSegmentCount < lastSegmentCount)
				{
					theBar.X += newSegmentCount * segmentWidth;
					theBar.Width = maxRight - theBar.X;
					g.FillRectangle(backBrush, theBar);
					lastSegmentCount = newSegmentCount;
				}
			}
			else
			{
				//g.FillRectangle( backBrush, theBar );
				theBar.Width = theBar.Width * val / max;
				g.FillRectangle( foreBrush, theBar );
			}

			if(Value == Minimum || Value == Maximum)
				this.ReleaseBrushes();
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ProgressBar
			// 
			this.CausesValidation = false;
			this.Enabled = false;
			this.ForeColor = System.Drawing.SystemColors.Highlight;
			this.Name = "ProgressBar";
			this.Size = new System.Drawing.Size(432, 24);
		}
		#endregion
	}

	public class TestProgressBar : ColorProgressBar, TestObserver
	{
		private readonly static Color SuccessColor = Color.Lime;
		private readonly static Color FailureColor = Color.Red;
		private readonly static Color IgnoredColor = Color.Yellow;

		public TestProgressBar()
		{
			Initialize( 100 );
		}

		private void Initialize( int testCount )
		{
			Value = 0;
			Maximum = testCount;
            ForeColor = SuccessColor;
        }

		private void OnRunStarting( object Sender, TestEventArgs e )
		{
			Initialize( e.TestCount );
		}

        private void OnTestLoaded(object sender, TestEventArgs e)
        {
            Initialize(e.TestCount);
        }

        private void OnTestReloaded(object sender, TestEventArgs e)
        {
            if (Services.UserSettings.GetSetting("Options.TestLoader.ClearResultsOnReload", false))
                Initialize(e.TestCount);
            else
                Value = Maximum = e.TestCount;
        }

        private void OnTestUnloaded(object sender, TestEventArgs e)
		{
			Initialize( 100 );
		}

		private void OnTestFinished( object sender, TestEventArgs e )
		{
			PerformStep();

            switch (e.Result.ResultState)
            {
                case ResultState.NotRunnable:
                case ResultState.Failure:
                case ResultState.Error:
                case ResultState.Cancelled:
                    ForeColor = FailureColor;
                    break;
                case ResultState.Ignored:
                    if (ForeColor == SuccessColor)
                        ForeColor = IgnoredColor;
                    break;
                default:
                    break;
            }
        }

		private void OnSuiteFinished( object sender, TestEventArgs e )
		{
			TestResult result = e.Result;
            if ( result.FailureSite == FailureSite.TearDown )
                switch (result.ResultState)
                {
                    case ResultState.Error:
                    case ResultState.Failure:
                    case ResultState.Cancelled:
                        ForeColor = FailureColor;
                        break;
                }
		}

		private void OnTestException(object sender, TestEventArgs e)
		{
			ForeColor = FailureColor;
		}

		#region TestObserver Members

		public void Subscribe(ITestEvents events)
		{
			events.TestLoaded	+= new TestEventHandler( OnTestLoaded );
            events.TestReloaded += new TestEventHandler(OnTestReloaded);
			events.TestUnloaded	+= new TestEventHandler( OnTestUnloaded );
			events.RunStarting	+= new TestEventHandler( OnRunStarting );
			events.TestFinished	+= new TestEventHandler( OnTestFinished );
			events.SuiteFinished += new TestEventHandler( OnSuiteFinished );
			events.TestException += new TestEventHandler(OnTestException);
		}

		#endregion
	}
}
