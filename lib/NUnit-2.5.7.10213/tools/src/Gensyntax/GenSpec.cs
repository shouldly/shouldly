using System;
using System.Collections.Generic;
using System.Text;

namespace GenSyntax
{
    public class GenSpec
    {
        private string fullSpec;
        bool isVoid;

        private string specType;
        private string leftPart;
        private string className;
        private string methodName;
        private string attributes;
        private string rightPart;

        public GenSpec(string spec) : this(spec, false) { }

        public GenSpec(string spec, bool isVoid)
        {
            this.fullSpec = spec;
            this.isVoid = isVoid;

            int colon = spec.IndexOf(':');
            int arrow = spec.IndexOf("=>", colon + 1);
            if (colon <= 0 || arrow <= 0)
                throw new ArgumentException(string.Format("Invalid generation spec: {0}", spec), "spec");

            this.specType = spec.Substring(0, colon + 1);
            this.leftPart = spec.Substring(colon+1, arrow-colon-1).Trim();
            this.rightPart = spec.Substring(arrow + 2).Trim();

            int nest = 0; int attributeLength = 0;
            foreach( char c in leftPart )
            {
                if ( c == '[' ) nest++;
                if ( nest == 0 ) break;
                attributeLength++;
                if ( c == ']' ) nest--;
            }

            //this.attributes = leftPart.Substring(0, attributeLength);

            int dot = leftPart.IndexOf('.', attributeLength);
            if (dot <= 0)
                throw new ArgumentException(string.Format("Invalid generation spec: {0}", spec), "spec");

            this.className = leftPart.Substring(0, dot).Trim();
            this.methodName = leftPart.Substring(dot + 1).Trim();

            if (attributeLength > 0)
            {
                this.attributes = className.Substring(0, attributeLength);
                this.className = className.Substring(attributeLength);
            }
        }

        public string SpecType
        {
            get { return this.specType; }
        }

        public string LeftPart
        {
            get { return this.leftPart; }
        }

        public string RightPart
        {
            get { return this.rightPart; }
        }

        public string MethodName
        {
            get { return methodName; }
        }

        public string ClassName
        {
            get { return className; }
        }

        public string Attributes
        {
            get { return attributes; }
        }

        public bool IsGeneric
        {
            get
            { 
                return this.MethodName.IndexOf('<') > 0
                    || this.MethodName.IndexOf('?') > 0;
            }
        }

        public bool IsProperty
        {
            get { return !IsGeneric && !methodName.EndsWith(")"); }
        }

        public override string ToString()
        {
            return fullSpec;
        }
    }
}
