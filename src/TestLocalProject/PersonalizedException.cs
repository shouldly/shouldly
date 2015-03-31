using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestLocalProject
{
    [Serializable]
    public class PersonalizedException : Exception, IPersonalizedException
    {
        public int NumberException { get; set; }

        public string ClassMessage
        {
            get
            {
                return "PersonalizedException class message ";
            }
        }

        public PersonalizedException(string pMessage, int pNumberException)
            : base(pMessage)
        {
            NumberException = pNumberException;
        }

        public PersonalizedException(string pMessage, Exception pException, int pNumberException)
            : base(pMessage, pException)
        {
            NumberException = pNumberException;
        }

        public PersonalizedException(SerializationInfo pSerializationInfo, StreamingContext pStreamingContext, int pNumberException)
            : base(pSerializationInfo, pStreamingContext)
        {
            NumberException = pNumberException;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override void GetObjectData(SerializationInfo pOInfo, StreamingContext pOContext)
        {
            base.GetObjectData(pOInfo, pOContext);
            pOInfo.AddValue("NumberException", NumberException);
        }
    }
}

