namespace NUnit.Framework.Constraints;

/// <summary>
///     The Numerics class contains common operations on numeric values.
/// </summary>
static class Numerics
{
    /// <summary>
    ///     Checks the type of the object, returning true if
    ///     the object is a numeric type.
    /// </summary>
    /// <param name="obj">The object to check</param>
    /// <returns>true if the object is a numeric type</returns>
    public static bool IsNumericType([NotNullWhen(true)] object? obj)
    {
        return obj is double or float or byte or sbyte or decimal or int or uint or long or ulong or short or ushort
#if NET5_0_OR_GREATER
            or Half
#endif
            ;
    }

    /// <summary>
    ///     Test two numeric values for equality, performing the usual numeric
    ///     conversions and using a provided or default tolerance. If the tolerance
    ///     provided is Empty, this method may set it to a default tolerance.
    /// </summary>
    /// <param name="expected">The expected value</param>
    /// <param name="actual">The actual value</param>
    /// <param name="tolerance">A reference to the tolerance in effect</param>
    /// <returns>True if the values are equal</returns>
    public static bool AreEqual(object expected, object actual, ref Tolerance tolerance)
    {
        // A floating-point value compared with an integer or decimal, with no tolerance in effect, must hold exactly
        // the same value: converting either side to the other's type can round it and make distinct values equal
        // (#1427). Floating-point types widen into each other exactly, and integer types widen exactly too, so only
        // this mix needs the exact comparison.
        if (IsFloatingPoint(expected) != IsFloatingPoint(actual) &&
            tolerance.IsEmpty && !(ShouldlyConfiguration.DefaultFloatingPointTolerance > 0.0d))
        {
            return IsFloatingPoint(expected)
                ? FloatingPointEqualsExactly(expected, actual)
                : FloatingPointEqualsExactly(actual, expected);
        }

#if NET5_0_OR_GREATER
        // Half doesn't implement IConvertible, so it can't go through the Convert calls below. Every Half
        // value is exactly representable as a double, so widen it and compare like the other floating-point
        // types (#1405). Decided before the float branch so the other operand isn't narrowed to float.
        if (expected is Half || actual is Half)
            return AreEqual(ToDouble(expected), ToDouble(actual), ref tolerance);
#endif

        if (expected is double || actual is double)
            return AreEqual(Convert.ToDouble(expected), Convert.ToDouble(actual), ref tolerance);

        if (expected is float || actual is float)
            return AreEqual(Convert.ToSingle(expected), Convert.ToSingle(actual), ref tolerance);

        if (tolerance.Mode == ToleranceMode.Ulps)
            throw new InvalidOperationException("Ulps may only be specified for floating point arguments");

        if (expected is decimal || actual is decimal || MixesSignedWithUnsigned(expected, actual))
            return AreEqual(Convert.ToDecimal(expected), Convert.ToDecimal(actual), tolerance);

        if (expected is ulong || actual is ulong)
            return AreEqual(Convert.ToUInt64(expected), Convert.ToUInt64(actual), tolerance);

        if (expected is long || actual is long)
            return AreEqual(Convert.ToInt64(expected), Convert.ToInt64(actual), tolerance);

        if (expected is uint || actual is uint)
            return AreEqual(Convert.ToUInt32(expected), Convert.ToUInt32(actual), tolerance);

        return AreEqual(Convert.ToInt32(expected), Convert.ToInt32(actual), tolerance);
    }

    private static bool AreEqual(double expected, double actual, ref Tolerance tolerance)
    {
        if (double.IsNaN(expected) && double.IsNaN(actual))
            return true;

        // Handle infinity specially since subtracting two infinite values gives
        // NaN and the following test fails. mono also needs NaN to be handled
        // specially although ms.net could use either method. Also, handle
        // situation where no tolerance is used.
        if (double.IsInfinity(expected) || double.IsNaN(expected) || double.IsNaN(actual))
        {
            return expected.Equals(actual);
        }

        if (tolerance.IsEmpty && ShouldlyConfiguration.DefaultFloatingPointTolerance > 0.0d)
            tolerance = new(ShouldlyConfiguration.DefaultFloatingPointTolerance);

        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                return Math.Abs(expected - actual) <= Convert.ToDouble(tolerance.Value);

            case ToleranceMode.Percent:
                if (expected == 0.0)
                    return expected.Equals(actual);

                var relativeError = Math.Abs((expected - actual) / expected);
                return relativeError <= Convert.ToDouble(tolerance.Value) / 100.0;
            case ToleranceMode.Ulps:
                return FloatingPointNumerics.AreAlmostEqualUlps(
                    expected, actual, Convert.ToInt64(tolerance.Value));
            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    private static bool AreEqual(float expected, float actual, ref Tolerance tolerance)
    {
        if (float.IsNaN(expected) && float.IsNaN(actual))
            return true;

        // handle infinity specially since subtracting two infinite values gives
        // NaN and the following test fails. mono also needs NaN to be handled
        // specially although ms.net could use either method.
        if (float.IsInfinity(expected) || float.IsNaN(expected) || float.IsNaN(actual))
        {
            return expected.Equals(actual);
        }

        if (tolerance.IsEmpty && ShouldlyConfiguration.DefaultFloatingPointTolerance > 0.0d)
            tolerance = new(ShouldlyConfiguration.DefaultFloatingPointTolerance);

        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                return Math.Abs(expected - actual) <= Convert.ToDouble(tolerance.Value);

            case ToleranceMode.Percent:
                if (expected == 0.0f)
                    return expected.Equals(actual);
                var relativeError = Math.Abs((expected - actual) / expected);
                return relativeError <= Convert.ToSingle(tolerance.Value) / 100.0f;
            case ToleranceMode.Ulps:
                return FloatingPointNumerics.AreAlmostEqualUlps(
                    expected, actual, Convert.ToInt32(tolerance.Value));
            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    private static bool AreEqual(decimal expected, decimal actual, Tolerance tolerance)
    {
        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                var decimalTolerance = Convert.ToDecimal(tolerance.Value);
                if (decimalTolerance > 0m)
                    return Math.Abs(expected - actual) <= decimalTolerance;

                return expected.Equals(actual);

            case ToleranceMode.Percent:
                if (expected == 0m)
                    return expected.Equals(actual);

                var relativeError = Math.Abs(
                    (double)(expected - actual) / (double)expected);
                return relativeError <= Convert.ToDouble(tolerance.Value) / 100.0;

            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    private static bool AreEqual(ulong expected, ulong actual, Tolerance tolerance)
    {
        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                var ulongTolerance = Convert.ToUInt64(tolerance.Value);
                if (ulongTolerance > 0ul)
                {
                    var diff = expected >= actual ? expected - actual : actual - expected;
                    return diff <= ulongTolerance;
                }

                return expected.Equals(actual);

            case ToleranceMode.Percent:
                if (expected == 0ul)
                    return expected.Equals(actual);

                // Can't do a simple Math.Abs() here since it's unsigned
                var difference = Math.Max(expected, actual) - Math.Min(expected, actual);
                var relativeError = Math.Abs(difference / (double)expected);
                return relativeError <= Convert.ToDouble(tolerance.Value) / 100.0;

            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    private static bool AreEqual(long expected, long actual, Tolerance tolerance)
    {
        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                var longTolerance = Convert.ToInt64(tolerance.Value);
                if (longTolerance > 0L)
                    return Math.Abs(expected - actual) <= longTolerance;

                return expected.Equals(actual);

            case ToleranceMode.Percent:
                if (expected == 0L)
                    return expected.Equals(actual);

                var relativeError = Math.Abs(
                    (expected - actual) / (double)expected);
                return relativeError <= Convert.ToDouble(tolerance.Value) / 100.0;

            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    private static bool AreEqual(uint expected, uint actual, Tolerance tolerance)
    {
        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                var uintTolerance = Convert.ToUInt32(tolerance.Value);
                if (uintTolerance > 0)
                {
                    var diff = expected >= actual ? expected - actual : actual - expected;
                    return diff <= uintTolerance;
                }

                return expected.Equals(actual);

            case ToleranceMode.Percent:
                if (expected == 0u)
                    return expected.Equals(actual);

                // Can't do a simple Math.Abs() here since it's unsigned
                var difference = Math.Max(expected, actual) - Math.Min(expected, actual);
                var relativeError = Math.Abs(difference / (double)expected);
                return relativeError <= Convert.ToDouble(tolerance.Value) / 100.0;

            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    private static bool AreEqual(int expected, int actual, Tolerance tolerance)
    {
        switch (tolerance.Mode)
        {
            case ToleranceMode.None:
                return expected.Equals(actual);

            case ToleranceMode.Linear:
                var intTolerance = Convert.ToInt32(tolerance.Value);
                if (intTolerance > 0)
                    return Math.Abs(expected - actual) <= intTolerance;

                return expected.Equals(actual);

            case ToleranceMode.Percent:
                if (expected == 0)
                    return expected.Equals(actual);

                var relativeError = Math.Abs(
                    (expected - actual) / (double)expected);
                return relativeError <= Convert.ToDouble(tolerance.Value) / 100.0;

            default:
                throw new ArgumentException("Unknown tolerance mode specified", nameof(tolerance));
        }
    }

    /// <summary>
    ///     Compare two numeric values, performing the usual numeric conversions.
    /// </summary>
    /// <param name="expected">The expected value</param>
    /// <param name="actual">The actual value</param>
    /// <returns>The relationship of the values to each other</returns>
    public static int Compare(object expected, object actual)
    {
        if (!IsNumericType(expected) || !IsNumericType(actual))
            throw new ArgumentException("Both arguments must be numeric");

        if (expected is double or float || actual is double or float)
            return Convert.ToDouble(expected).CompareTo(Convert.ToDouble(actual));

        if (expected is decimal || actual is decimal || MixesSignedWithUnsigned(expected, actual))
            return Convert.ToDecimal(expected).CompareTo(Convert.ToDecimal(actual));

        if (expected is ulong || actual is ulong)
            return Convert.ToUInt64(expected).CompareTo(Convert.ToUInt64(actual));

        if (expected is long || actual is long)
            return Convert.ToInt64(expected).CompareTo(Convert.ToInt64(actual));

        if (expected is uint || actual is uint)
            return Convert.ToUInt32(expected).CompareTo(Convert.ToUInt32(actual));

        return Convert.ToInt32(expected).CompareTo(Convert.ToInt32(actual));
    }

    /// <summary>
    ///     True when one value is a <see cref="uint"/> or <see cref="ulong"/> and the other a signed integer. Neither
    ///     type holds both ranges, so converting a negative value to the unsigned type throws (#1426). Such pairs are
    ///     compared in <see cref="decimal"/> instead, which holds every value of both exactly (as ShouldBeEquivalentTo
    ///     does for mixed numeric types), so they stay exact and a linear tolerance may span zero. Smaller unsigned
    ///     types already widen to <see cref="int"/> or <see cref="long"/> alongside a signed value.
    /// </summary>
    private static bool MixesSignedWithUnsigned(object expected, object actual) =>
        (expected is uint or ulong && actual is sbyte or short or int or long) ||
        (actual is uint or ulong && expected is sbyte or short or int or long);

    private static bool IsFloatingPoint(object value) =>
        value is double or float
#if NET5_0_OR_GREATER
            or Half
#endif
            ;

    /// <summary>
    ///     Whether a floating-point value holds exactly the same value as an integer or decimal. Every integer and
    ///     decimal is exact as a decimal, so the floating-point value is converted to decimal, and only counts if
    ///     that conversion is exact.
    /// </summary>
    private static bool FloatingPointEqualsExactly(object floatingPoint, object other) =>
        TryConvertToDecimalExactly(ToDouble(floatingPoint), out var value) &&
        value == Convert.ToDecimal(other);

    /// <summary>
    ///     Converts a double to the decimal holding exactly the same value. Fails for NaN, infinities and values a
    ///     decimal can't represent exactly; <see cref="Convert.ToDecimal(double)"/> rounds to 15 significant digits
    ///     instead.
    /// </summary>
    private static bool TryConvertToDecimalExactly(double value, out decimal result)
    {
        result = 0m;
        if (double.IsNaN(value) || double.IsInfinity(value))
            return false;

        if (value == 0.0d)
            return true;

        // value = ±mantissa × 2^exponent
        var bits = BitConverter.DoubleToInt64Bits(value);
        var biasedExponent = (int)((bits >> 52) & 0x7FF);
        var mantissa = bits & 0xFFFFFFFFFFFFFL;
        if (biasedExponent == 0)
            biasedExponent = 1;
        else
            mantissa |= 1L << 52;
        var exponent = biasedExponent - 1075;

        while ((mantissa & 1) == 0)
        {
            mantissa >>= 1;
            exponent++;
        }

        decimal magnitude = mantissa;
        if (exponent >= 0)
        {
            // The value has (bits in mantissa + exponent) bits; a decimal holds integers below 2^96.
            var mantissaBits = 0;
            for (var m = mantissa; m != 0; m >>= 1)
                mantissaBits++;
            if (mantissaBits + exponent > 96)
                return false;

            for (var i = 0; i < exponent; i++)
                magnitude *= 2;
        }
        else
        {
            // mantissa (odd) × 2^-k = mantissa × 5^k / 10^k needs exactly k decimal places, and a decimal has at
            // most 28, with a 96-bit integer before scaling.
            var scale = -exponent;
            if (scale > 28)
                return false;

            for (var i = 0; i < scale; i++)
            {
                if (magnitude > decimal.MaxValue / 5)
                    return false;
                magnitude *= 5;
            }

            var parts = decimal.GetBits(magnitude);
            magnitude = new(parts[0], parts[1], parts[2], false, (byte)scale);
        }

        result = value < 0 ? -magnitude : magnitude;
        return true;
    }

    private static double ToDouble(object value) =>
#if NET5_0_OR_GREATER
        value is Half half ? (double)half :
#endif
        Convert.ToDouble(value);
}
