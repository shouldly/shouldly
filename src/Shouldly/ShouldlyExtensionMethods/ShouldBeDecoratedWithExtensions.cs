using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for attribute decoration assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeDecoratedWithExtensions
{
    extension(Type actual)
    {
        /// <summary>
        /// Asserts that the type is decorated with the specified attribute.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeDecoratedWith<T>(string? customMessage = null)
            where T : Attribute
        {
            if (!actual.HasAttribute(typeof(T)))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(T).Name, customMessage).ToString());
        }

        bool HasAttribute(Type attributeType) =>
            actual.GetCustomAttributes(attributeType, true).Any();
    }
}