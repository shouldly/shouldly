using System;

namespace Shouldly.Tests
{
    public class Widget
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return String.Format("Name({0}) Enabled({1})", Name, Enabled);
        }    
    }
}