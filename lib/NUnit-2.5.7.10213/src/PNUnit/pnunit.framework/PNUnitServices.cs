using System;

namespace PNUnit.Framework
{
	[Serializable]
	public class PNUnitServices
	{
		private TestInfo mInfo = null;
		private ITestConsoleAccess mConsole = null;
		private static PNUnitServices mInstance = null;

		// To be used only by the runner
		public PNUnitServices(object info, object consoleaccess)
		{            
			mInfo = info as TestInfo;
			mConsole = consoleaccess as ITestConsoleAccess;
			mInstance = this;            
		}

		public static PNUnitServices Get()
		{
			if( mInstance == null )
			{
				throw new Exception("mInstance is null");
			}
			return mInstance;
		}

		private void CheckInfo()
		{
			if( mInfo == null )
				throw new Exception("TestInfo not initialized");
		}

		// IPNUnitServices

		public void InitBarriers()
		{
			CheckInfo();
			mInfo.Services.InitBarriers(mInfo.TestName);
		}

		public void InitBarrier(string barrier)
		{
			CheckInfo();
			mInfo.Services.InitBarrier(mInfo.TestName, barrier);
		}

		public void InitBarrier(string barrier, int Max)
		{
			CheckInfo();
			mInfo.Services.InitBarrier(mInfo.TestName, barrier, Max);
		}
        
		public void EnterBarrier(string barrier)
		{
			CheckInfo();
			mConsole.WriteLine(
				string.Format(">>>Test {0} entering barrier {1}", 
				mInfo.TestName, barrier));
			mInfo.Services.EnterBarrier(barrier);
			mConsole.WriteLine(
				string.Format("<<<Test {0} leaving barrier {1}", 
				mInfo.TestName, barrier));
		}

		public string GetTestName()
		{
			CheckInfo();
			return mInfo.TestName;
		}

		public string[] GetTestParams()
		{
			CheckInfo();
			return mInfo.TestParams;
		}

		public void WriteLine(string s)
		{
			if( mConsole != null )
				mConsole.WriteLine(s);
		}

		public void Write(char[] buf)
		{
			if( mConsole != null )
				mConsole.Write(buf);
		}

		public void Write(char[] buf, int index, int count)
		{
			if( mConsole != null )
				mConsole.Write(buf, index, count);
		}

	}

}
