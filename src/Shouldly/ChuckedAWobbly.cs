using System;

namespace Shouldly
{
    [Serializable]
    internal class ChuckedAWobbly : Exception
    {
        public ChuckedAWobbly(string message) : base(message)
        {
        }
    }
}
