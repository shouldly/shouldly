namespace Shouldly;

/// <summary>
/// Shared <see cref="ObsoleteAttribute"/> text for the exception assertions that accept a
/// <see cref="Task"/> instance rather than a <see cref="Func{Task}"/>. See shouldly/shouldly#789.
/// </summary>
internal static class TaskOverloadObsolescence
{
    public const string Message =
        "Pass a Func<Task> instead of a Task instance, e.g. Should.Throw<T>(DoWorkAsync) or Should.Throw<T>(() => task). " +
        "A Task argument is evaluated before Shouldly runs, so an exception thrown synchronously " +
        "(argument validation, for example) escapes the assertion instead of being caught by it.";
}
