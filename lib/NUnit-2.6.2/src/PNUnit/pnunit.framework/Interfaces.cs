using System;
using System.Collections;
using System.Runtime.Serialization;

using NUnit.Core;

namespace PNUnit.Framework
{
    public class Names
    {
        public const string PNUnitAgentServiceName = "IPNUnitAgent";
        public const string ServerBarrier = "SERVERSTART";
        public const string EndBarrier = "ENDBARRIER";
        public const string RestartBarrier = "SERVERRESTART";
        public const string RestartedOkBarrier = "SERVERRESTARTEDOK";
    }

    public interface ITestConsoleAccess
    {
        void WriteLine(string s);
        void Write(char[] buf);
        void Write(char[] buf, int index, int count);
    }


    public interface IPNUnitServices
    {
        void NotifyResult(string TestName, TestResult result);

        void InitBarriers();

        void InitBarrier(string name, int max);

        void EnterBarrier(string barrier);

        void SendMessage(string tag, int receivers, object message);

        object ReceiveMessage(string tag);

        void ISendMessage(string tag, int receivers, object message);

        object IReceiveMessage(string tag);
    }

    [Serializable]
    public class PNUnitTestInfo
    {
        public string TestName;
        public string AssemblyName;
        public string TestToRun;
        public string[] TestParams;
        public IPNUnitServices Services;
        public string StartBarrier;
        public string EndBarrier;
        public string[] WaitBarriers;
        public Hashtable UserValues = new Hashtable();

        public PNUnitTestInfo(
            string TestName,
            string AssemblyName,
            string TestToRun,
            string[] TestParams,
            IPNUnitServices Services)
        {
            this.TestName = TestName;
            this.AssemblyName = AssemblyName;
            this.TestToRun = TestToRun;
            this.TestParams = TestParams;
            this.Services = Services;
        }

        public PNUnitTestInfo(
            string TestName, string AssemblyName,
            string TestToRun, string[] TestParams,
            IPNUnitServices Services,
            string StartBarrier, string EndBarrier, string[] WaitBarriers)
        {
            this.TestName = TestName;
            this.AssemblyName = AssemblyName;
            this.TestToRun = TestToRun;
            this.TestParams = TestParams;
            this.Services = Services;
            this.StartBarrier = StartBarrier;
            this.EndBarrier = EndBarrier;
            this.WaitBarriers = WaitBarriers;
        }
    }

    public interface IPNUnitAgent
    {
        void RunTest(PNUnitTestInfo info);
    }

    [Serializable]
    public class PNUnitRetryException : Exception
    {
        public static string RETRY_EXCEPTION = "RETRY_EXCEPTION:";
        #region "constructors"
        public PNUnitRetryException(string message)
            : base(RETRY_EXCEPTION + message)
        {
        }

        public PNUnitRetryException(string message, Exception innerException)
            : base(RETRY_EXCEPTION + message, innerException)
        {
        }

        public PNUnitRetryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }

    [Serializable]
    public class PNUnitTestResult : TestResult
    {
        private string mOutput = string.Empty;
        private bool mRetryTest = false;

        public PNUnitTestResult(TestName testName, string output)
            : base(testName)
        {
            mOutput = output;
        }

        public PNUnitTestResult(TestResult testResult, string output): base(testResult.Test)
        {
            mOutput = output;
            if (testResult.Message != null && (testResult.Message.IndexOf(PNUnitRetryException.RETRY_EXCEPTION) >= 0))
                this.mRetryTest = true;

            if (testResult.IsSuccess)
                this.Success(testResult.Message);
            else
                this.Failure(testResult.Message, testResult.StackTrace);
            this.Time = testResult.Time;
        }

        public string Output { get { return mOutput; } }
        public bool RetryTest { get { return mRetryTest; } }
    }


}

