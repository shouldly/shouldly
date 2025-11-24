namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    extension(decimal actual)
    {
        /// <summary>
        /// Asserts that the decimal value is positive.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBePositive(string? customMessage = null)
        {
            var expected = default(decimal);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the decimal value is negative.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeNegative(string? customMessage = null)
        {
            var expected = default(decimal);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }

    extension(double actual)
    {
        /// <summary>
        /// Asserts that the double value is positive.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBePositive(string? customMessage = null)
        {
            var expected = default(double);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the double value is negative.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeNegative(string? customMessage = null)
        {
            var expected = default(double);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }

    extension(float actual)
    {
        /// <summary>
        /// Asserts that the float value is positive.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBePositive(string? customMessage = null)
        {
            var expected = default(float);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the float value is negative.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeNegative(string? customMessage = null)
        {
            var expected = default(float);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }

    extension(int actual)
    {
        /// <summary>
        /// Asserts that the integer value is positive.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBePositive(string? customMessage = null)
        {
            var expected = default(int);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the integer value is negative.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeNegative(string? customMessage = null)
        {
            var expected = default(int);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }

    extension(long actual)
    {
        /// <summary>
        /// Asserts that the long value is positive.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBePositive(string? customMessage = null)
        {
            var expected = default(long);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the long value is negative.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeNegative(string? customMessage = null)
        {
            var expected = default(long);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }

    extension(short actual)
    {
        /// <summary>
        /// Asserts that the short value is positive.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBePositive(string? customMessage = null)
        {
            var expected = default(short);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the short value is negative.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeNegative(string? customMessage = null)
        {
            var expected = default(short);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }
}