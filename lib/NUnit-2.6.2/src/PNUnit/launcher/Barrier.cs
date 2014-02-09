using System;
using System.Threading;

namespace PNUnit.Launcher
{
    public class Barrier
    {
        public int mCount;
        public int mMaxCount;
        private Object mLock = new Object();
        private string mName;

        private static int MAX_WAIT_TIME = 300 * 1000; //milliseconds

        public Barrier(string name, int maxCount)
        {
            mCount = 0;
            mName = name;
            mMaxCount = maxCount;
        }

        public void Enter()
        {
            lock( mLock )
            {
                ++mCount;
                if( mCount == mMaxCount )
                {
                    Monitor.PulseAll(mLock);
                }
                else if( mCount < mMaxCount )
                {
                    if (!Monitor.Wait(mLock, MAX_WAIT_TIME))
                    {
                        Console.WriteLine("Barrier {0} abandoned due to timeout!!!",
                            mName);
                    }
                }
                else
                {
                    //nothing to do, entering in a barrier already used
                }
            }
        }

        public void Abandon()
        {
            lock( mLock )
            {
                --mMaxCount;
                if( mCount >= mMaxCount )
                {
                    mCount = 0;
                    Monitor.PulseAll(mLock);
                }
            }
        }
    }
}
