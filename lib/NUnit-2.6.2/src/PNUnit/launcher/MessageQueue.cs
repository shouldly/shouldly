using System;
using System.Collections;
using System.Threading;

namespace PNUnit.Launcher
{
    public class MessageQueue
    {
        private Object mLock = new Object();
        private ArrayList mMessages = new ArrayList();

        public MessageQueue()
        {            
        }

        public void Send(string tag, int receivers, object message)
        {
            MessageData msg = null;
            Monitor.Enter(mLock);
            try
            {
                msg = AddMessage(tag, receivers, message);

                Monitor.PulseAll(mLock);
            }
            finally
            {
                Monitor.Exit(mLock);
            }

            msg.WaitForReceptions();
        }

        public object Receive(string tag)
        {
            Monitor.Enter(mLock);
            try
            {
                object msg = null;
                while ((msg = GetMessage(tag)) == null)
                {
                    Monitor.Wait(mLock);
                }
                return msg;
            }
            finally
            {
                Monitor.Exit(mLock);
            } 
        }

        public void ISend(string tag, int receivers, object message)
        {
            Monitor.Enter(mLock);
            try
            {
               AddMessage(tag, receivers, message);
               Monitor.PulseAll(mLock);
            }
            finally
            {
                Monitor.Exit(mLock);
            } 
        }

        public object IReceive(string tag)
        {
            Monitor.Enter(mLock);
            try
            {
               return GetMessage(tag);
            }
            finally
            {
                Monitor.Exit(mLock);
            } 
        }

        private MessageData AddMessage(string tag, int receivers, object message)
        {
            MessageData msg = new MessageData(tag, receivers, message);
            mMessages.Add(msg);

            return msg;
        }

        private object GetMessage(string tag)
        {
            int index = 0;
            MessageData msg = null;
            for (;index< mMessages.Count; index++)
            {
                msg = mMessages[index] as MessageData;

                if (!msg.Tag.Equals(tag)) continue;

                msg.ConfirmReception();
                
                break;
            }

            if (msg == null) return null;

            if (!msg.HasPendingReceivers())
                mMessages.RemoveAt(index);

            return msg.Message;
        }

        private class MessageData
        {
            private object mLock = new object();
            private string mTag;
            private int mReceivers;
            private object mMessage;

            internal MessageData(string tag, int receivers, object message)
            {
                this.mTag = tag;
                this.mReceivers = receivers;
                this.mMessage = message;
            }

            internal string Tag
            {
                get
                {
                    return mTag;
                }
            }

            internal object Message
            {
                get
                {
                    return mMessage;
                }
            }

            internal void WaitForReceptions()
            {
                Monitor.Enter(mLock);
                try
                {                    
                    while(mReceivers > 0)
                    {
                        Monitor.Wait(mLock);
                    }  
                }
                finally
                {
                    Monitor.Exit(mLock);
                }   
            }

            internal void ConfirmReception()
            {
                Monitor.Enter(mLock);
                try
                {
                    mReceivers--;
                   
                    Monitor.Pulse(mLock);
                }
                finally
                {
                    Monitor.Exit(mLock);
                }
            }

            internal bool HasPendingReceivers()
            {
                Monitor.Enter(mLock);
                try
                {
                    return mReceivers > 0;
                }
                finally
                {
                    Monitor.Exit(mLock);
                }
            }
        }
    }    
}
