using System;

namespace PNUnit.Framework
{
    [Serializable]
    public class PNUnitServices
    {
        private PNUnitTestInfo mInfo = null;
        private ITestConsoleAccess mConsole = null;
        private static PNUnitServices mInstance = null;

        // To be used only by the runner
        public PNUnitServices(object info, object consoleaccess)
        {
            mInfo = info as PNUnitTestInfo;
            mConsole = consoleaccess as ITestConsoleAccess;
            mInstance = this;
        }

        public static PNUnitServices Get()
        {
            if (mInstance == null)
            {
                throw new Exception("mInstance is null");
            }
            return mInstance;
        }

        private void CheckInfo()
        {
            if (mInfo == null)
                throw new Exception("TestInfo not initialized");
        }

        // IPNUnitServices

        public void InitBarriers()
        {
            CheckInfo();
            mInfo.Services.InitBarriers();
        }
        
        public void InitBarrier(string name, int max)
        {
            CheckInfo();
            mInfo.Services.InitBarrier(name, max);
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

        public void SendMessage(string tag, int receivers, object message)
        {
            CheckInfo();
            mConsole.WriteLine(
                string.Format(
                ">>>Message sending (tag:{1} receivers:{2} message:{3}) by test {0} ", 
                mInfo.TestName, tag, receivers, 
                message == null ? string.Empty : message.ToString()));
            mInfo.Services.SendMessage(tag, receivers, message);
            mConsole.WriteLine(
                string.Format(
                "<<<Message send (tag:{1} receivers:{2} message:{3}) by test {0} & all receivers confirm reception", 
                mInfo.TestName, tag, receivers, 
                message == null ? string.Empty : message.ToString()));
        }

        public object ReceiveMessage(string tag)
        {
            CheckInfo();
            mConsole.WriteLine(
                string.Format(">>>Receiving message (tag:{1}) by test {0}", 
                mInfo.TestName, tag));
            object message = mInfo.Services.ReceiveMessage(tag);
            mConsole.WriteLine(
                string.Format("<<<Received message (tag:{1} message:{2}) by test {0}", 
                mInfo.TestName, tag, message.ToString()));
            return message;
        }

        public void ISendMessage(string tag, int receivers, object message)
        {
            CheckInfo();
            CheckInfo();
            mConsole.WriteLine(
                string.Format(
                ">>>Message sending (tag:{1} message:{2}) by test {0} ", 
                mInfo.TestName, tag, 
                message == null ? string.Empty : message.ToString()));
            mInfo.Services.ISendMessage(tag, receivers, message);
            mConsole.WriteLine(
                string.Format(
                "<<<Message sent (tag:{1} message:{2}) by test {0} & all receivers confirm reception", 
                mInfo.TestName, tag, 
                message == null ? string.Empty : message.ToString()));
        }

        public object IReceiveMessage(string tag)
        {
            CheckInfo();
            mConsole.WriteLine(
                string.Format(">>>Looking for message (tag:{1}) by test {0}", 
                mInfo.TestName, tag));
            object msg = mInfo.Services.IReceiveMessage(tag);
            mConsole.WriteLine(
                string.Format("<<<Search for message (tag{1}) by test {0}", 
                mInfo.TestName, tag));
            return msg;
        }

        public string[] GetTestWaitBarriers()
        {
            CheckInfo();
            return mInfo.WaitBarriers;
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
            if (mConsole != null)
                mConsole.WriteLine(s);
        }

        public void Write(char[] buf)
        {
            if (mConsole != null)
                mConsole.Write(buf);
        }

        public void Write(char[] buf, int index, int count)
        {
            if (mConsole != null)
                mConsole.Write(buf, index, count);
        }

        public string GetTestStartBarrier()
        {
            CheckInfo();
            if (mInfo.StartBarrier == null || mInfo.StartBarrier == string.Empty)
                mInfo.StartBarrier = Names.ServerBarrier;
            return mInfo.StartBarrier;
        }

        public string GetTestEndBarrier()
        {
            CheckInfo();
            if (mInfo.EndBarrier == null || mInfo.EndBarrier == string.Empty)
                mInfo.EndBarrier = Names.EndBarrier;
            return mInfo.EndBarrier;
        }

        public string GetUserValue(string key)
        {
            if( mInfo == null || mInfo.UserValues == null )
                return string.Empty;

            return (string) mInfo.UserValues[key];
        }
    }
}
