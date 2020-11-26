using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Shouldly.Internals
{
    internal static class StackTraceHelpers
    {
        public static string GetStackTrace(Exception exception, [NotNull] ref string? cachedValue)
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

            var frames = stackTrace.GetFrames();
            foreach (var (startIndex, frame) in frames.AsIndexed())
            {
                if (frame.GetMethod()?.DeclaringType?.Assembly != shouldlyAssembly)
                {
                    if (startIndex == 0)
                    {
                        builder.Append(stackTrace.ToString().TrimEnd());
                    }
                    else
                    {
                        var lines = new string[frames.Length - startIndex];
                        var neededCapacity = builder.Length;

                        for (var i = 0; i < lines.Length; i++)
                        {
                            var line = new StackTrace(frames[i + startIndex]).ToString();
                            if (i == lines.Length - 1) line = line.TrimEnd();
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