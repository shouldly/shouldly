#if StackTrace
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Shouldly.Internals
{
    internal static class StackTraceHelpers
    {
        public static string GetStackTrace(Exception exception, ref string cachedValue)
        {
            if (cachedValue == null)
            {
                var builder = new StringBuilder();
                WriteFilteredStackTrace(builder, new StackTrace(exception, fNeedFileInfo: true));
                cachedValue = builder.ToString();
            }

            return cachedValue;
        }

        public static void WriteFilteredStackTrace(StringBuilder builder, StackTrace stackTrace)
        {
            var shouldlyAssembly = Assembly.GetExecutingAssembly();

            for (var startIndex = 0; startIndex < stackTrace.FrameCount; startIndex++)
            {
                var frame = stackTrace.GetFrame(startIndex);
                if (frame.GetMethod().DeclaringType?.Assembly != shouldlyAssembly)
                {
                    if (startIndex == 0)
                    {
                        builder.Append(stackTrace);
                    }
                    else
                    {
                        var lines = new string[stackTrace.FrameCount - startIndex];
                        var neededCapacity = builder.Length;

                        for (var i = 0; i < lines.Length; i++)
                        {
                            var line = new StackTrace(stackTrace.GetFrame(i + startIndex)).ToString();
                            lines[i] = line;
                            neededCapacity += line.Length;
                        }

                        builder.EnsureCapacity(neededCapacity);

                        foreach (var line in lines)
                            builder.Append(line);
                    }

                    return;
                }
            }
        }
    }
}
#endif
