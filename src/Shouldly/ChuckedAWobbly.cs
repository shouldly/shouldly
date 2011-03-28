using System;

namespace Shouldly
{
    internal class ChuckedAWobbly : Exception
    {
        public ChuckedAWobbly(string message) : base(message)
        {
        }
    }
}