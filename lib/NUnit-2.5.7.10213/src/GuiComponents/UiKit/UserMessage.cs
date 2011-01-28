// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Text;
using System.Windows.Forms;

namespace NUnit.UiKit
{
	/// <summary>
	/// Summary description for UserMessage.
	/// </summary>
	public class UserMessage
	{
		private static readonly string nunitCaption = "NUnit";

		public static DialogResult Display( string message )
		{
			return Display( message, nunitCaption, MessageBoxButtons.OK, MessageBoxIcon.None );
		}

		public static DialogResult Display( string message, string caption )
		{
			return Display( message, caption, MessageBoxButtons.OK, MessageBoxIcon.None );
		}

		public static DialogResult Display( string message, MessageBoxButtons buttons )
		{
			return Display( message, nunitCaption, buttons, MessageBoxIcon.None );
		}

		public static DialogResult Display( string message, MessageBoxButtons buttons, MessageBoxIcon icon )
		{
			return Display( message, nunitCaption, buttons, icon );
		}

		public static DialogResult Display( string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
		{
			return MessageBox.Show( message, caption, buttons, icon );
		}

		public static DialogResult DisplayFailure( string message )
		{
			return DisplayFailure( message, nunitCaption );
		}

		public static DialogResult DisplayFailure( string message, string caption )
		{
			return Display( message, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
		}

		public static DialogResult DisplayFailure( Exception exception, string caption )
		{
//			if ( exception is NUnitException )
//				return DisplayFailure( exception.InnerException, exception.Message, caption );
//			else
			return DisplayFailure( exception, null, caption );
		}

        public static DialogResult DisplayFailure(Exception exception, string message, string caption)
        {
            Exception ex = exception;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} : {1}", ex.GetType().ToString(), ex.Message);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sb.AppendFormat("\r----> {0} : {1}", ex.GetType().ToString(), ex.Message);
            }

            if (message != null)
                sb.AppendFormat("\r\r{0}", message);

            sb.Append("\r\rFor further information, use the Exception Details menu item.");

            return DisplayFailure(sb.ToString(), caption);
        }

        public static DialogResult DisplayFatalError(Exception exception, string message, string caption)
        {
            Exception ex = exception;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} : {1}", ex.GetType().ToString(), ex.Message);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sb.AppendFormat("\r----> {0} : {1}", ex.GetType().ToString(), ex.Message);
            }

            if (message != null)
                sb.AppendFormat("\r\r{0}", message);

            return DisplayFailure(sb.ToString(), caption);
        }

        public static DialogResult DisplayInfo(string message)
		{
			return DisplayInfo( message, nunitCaption );
		}

		public static DialogResult DisplayInfo( string message, string caption )
		{
			return Display( message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
		}

		public static DialogResult Ask( string message, MessageBoxButtons buttons )
		{
			return Ask( message, nunitCaption, buttons );
		}

		public static DialogResult Ask( string message )
		{
			return Ask( message, nunitCaption, MessageBoxButtons.YesNo );
		}

		public static DialogResult Ask( string message, string caption )
		{
			return Display( message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
		}

		public static DialogResult Ask( string message, string caption, MessageBoxButtons buttons )
		{
			return Display( message, caption, buttons, MessageBoxIcon.Question );
		}

	}
}
