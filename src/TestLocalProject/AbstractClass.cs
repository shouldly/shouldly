using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLocalProject
{

    public abstract class AbstractClass
    {
        public int intNumerator { get; set; }

        public void TestMethod()
        {
            throw new PersonalizedException("test message", 1);
        }
    }
}
