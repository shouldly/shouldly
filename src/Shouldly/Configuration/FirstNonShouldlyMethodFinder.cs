#if ShouldMatchApproved
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Shouldly.Configuration
{
    public class FirstNonShouldlyMethodFinder : ITestMethodFinder
    {
        static readonly Regex AnonMethod = new Regex(@"<(\w|_)+>b_.+");

        /// <summary>
        /// Increasing the offset will move past the first non-shouldly method
        /// Anonymous methods are not counted in the offset.
        /// This is useful when you have created a reusable method which is calling ShouldMatchApproved
        /// </summary>
        public int Offset { get; set; }

        public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
        {
            var i = startAt;
            StackFrame callingFrame;
            do
            {
                callingFrame = stackTrace.GetFrame(i++);
            } while (callingFrame.GetMethod().IsShouldlyMethod() || IsCompilerGenerated(callingFrame.GetMethod()));

            callingFrame = stackTrace.GetFrame(i + Offset - 1);
            return new TestMethodInfo(callingFrame);
        }

        static bool IsCompilerGenerated(MethodBase method)
        {
            return method.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any() || AnonMethod.IsMatch(method.Name);
        }
    }
}
#endif