using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Shouldly
{
    internal class TestEnvironment
    {

        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public StackFrame OriginatingFrame  { get; set; }
        public MethodBase UnderlyingShouldMethod { get; set; }

        public object Key { get; set; }
        public object Expected { get; set; }
        public object Actual { get; set; }
        public object Tolerance { get; set; }

        // For now, this property cannot just check to see if "Actual != null". The term is overloaded. 
        // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
        // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where 
        // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
        // is relevant.
        public bool HasActual { get; set; }
        public bool HasKey { get; set; }

        public bool IsNegatedAssertion { get { return ShouldMethod.Contains("Not"); } }

        internal TestEnvironment(object expected, object actual = null)
        {
            var stackTrace = new StackTrace(true);
            var i = 0;
            var currentFrame = stackTrace.GetFrame(i);

            if (currentFrame == null) throw new Exception("Unable to find test method");

            var shouldlyFrame = default(StackFrame);
            while (shouldlyFrame == null || IsShouldlyMethod(currentFrame.GetMethod()))
            {
                if (IsShouldlyMethod(currentFrame.GetMethod()))
                    shouldlyFrame = currentFrame;

                currentFrame = stackTrace.GetFrame(++i);

                // Required to support the DynamicShould.HaveProperty method that takes in a dynamic as a parameter.
                // Having a method that takes a dynamic really stuffs up the stack trace because the runtime binder
                // has to inject a whole heap of methods. Our normal way of just taking the next frame doesn't work.
                // The following two lines seem to work for now, but this feels like a hack. The conditions to be able to 
                // walk up stack trace until we get to the calling method might have to be updated regularly as we find more
                // scanarios. Alternately, it could be replaced with a more robust implementation.
                while ( currentFrame.GetMethod().DeclaringType == null ||
                        currentFrame.GetMethod().DeclaringType.FullName.StartsWith("System.Dynamic"))
                {
                    currentFrame = stackTrace.GetFrame(++i);
                }
            }

            var originatingFrame = currentFrame;

            var fileName = originatingFrame.GetFileName();

           DeterminedOriginatingFrame = fileName != null && File.Exists(fileName);
           ShouldMethod = shouldlyFrame.GetMethod().Name;
           UnderlyingShouldMethod = shouldlyFrame.GetMethod();
           FileName = fileName;
           LineNumber = originatingFrame.GetFileLineNumber() - 1;
           OriginatingFrame = originatingFrame;
           Expected = expected;
           Actual = actual;
        }

        private bool IsShouldlyMethod(MethodBase method)
        {
            if (method.DeclaringType == null)
                return false;

            return method.DeclaringType.GetCustomAttributes(typeof(ShouldlyMethodsAttribute), true).Any();
        }

        public bool IgnoreOrder { get; set; }

        public string GetCodePart()
        {
            var codePart = "The provided expression";

            if (DeterminedOriginatingFrame)
            {
                var codeLines = String.Join("\n", File.ReadAllLines(FileName).Skip(LineNumber).ToArray());

                codePart = codeLines.Substring(0, codeLines.IndexOf(ShouldMethod) - 1).Trim();
            }
            return codePart;
        }


    }
}