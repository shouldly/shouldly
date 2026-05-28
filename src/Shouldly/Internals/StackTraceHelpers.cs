namespace Shouldly.Internals;

static class StackTraceHelpers
{
    public static string GetStackTrace(Exception exception, [NotNull] ref string? cachedValue)
    {
        if (cachedValue == null)
        {
            var builder = new StringBuilder();
            WriteFilteredStackTrace(builder, new(exception, fNeedFileInfo: true));
            cachedValue = builder.ToString();
        }

        return cachedValue;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026",
        Justification = "Reads StackFrame.GetMethod() only to compare DeclaringType.Assembly identity against the Shouldly assembly. No members are invoked or introspected. If the trimmer strips method metadata, the worst case is that a frame is not filtered — a cosmetic regression in failure messages, not a functional break.")]
    public static void WriteFilteredStackTrace(StringBuilder builder, StackTrace stackTrace)
    {
        var shouldlyAssembly = Assembly.GetExecutingAssembly();

        var frames = stackTrace.GetFrames();
        foreach (var (startIndex, frame) in frames.AsIndexed())
        {
            if (frame.GetMethod()?.DeclaringType?.Assembly == shouldlyAssembly)
            {
                continue;
            }

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
                    var nextFrame = frames[i + startIndex];
                    if (nextFrame == null)
                    {
                        continue;
                    }

                    var line = new StackTrace(nextFrame).ToString();
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
