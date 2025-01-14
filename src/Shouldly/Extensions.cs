namespace Shouldly;

/// <summary>
/// Common extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Casts the subject to the specified type if possible, otherwise returns null.
    /// </summary>
    /// <typeparam name="TTo">The type to cast the subject to.</typeparam>
    /// <param name="subject">The subject to be casted.</param>
    public static TTo? As<TTo>(this object? subject)
    {
        return subject is TTo to ? to : default;
    }
}
