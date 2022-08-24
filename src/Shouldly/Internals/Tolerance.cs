namespace NUnit.Framework.Constraints;

/// <summary>
///     The Tolerance class generalizes the notion of a tolerance
///     within which an equality test succeeds. Normally, it is
///     used with numeric types, but it can be used with any
///     type that supports taking a difference between two
///     objects and comparing that difference to a value.
/// </summary>
internal class Tolerance
{
    private const string ModeMustFollowTolerance = "Tolerance amount must be specified before setting mode";
    private const string MultipleToleranceModes = "Tried to use multiple tolerance modes at the same time";
    private const string NumericToleranceRequired = "A numeric tolerance is required";
    private readonly object amount;
    private readonly ToleranceMode mode;

    /// <summary>
    ///     Constructs a linear tolerance of a specified amount
    /// </summary>
    public Tolerance(object amount) : this(amount, ToleranceMode.Linear)
    {
    }

    /// <summary>
    ///     Constructs a tolerance given an amount and ToleranceMode
    /// </summary>
    private Tolerance(object amount, ToleranceMode mode)
    {
        this.amount = amount;
        this.mode = mode;
    }

    /// <summary>
    ///     Returns an empty Tolerance object, equivalent to
    ///     specifying no tolerance. In most cases, it results
    ///     in an exact match but for floats and doubles a
    ///     default tolerance may be used.
    /// </summary>
    public static Tolerance Empty => new(0, ToleranceMode.None);

    /// <summary>
    ///     Returns a zero Tolerance object, equivalent to
    ///     specifying an exact match.
    /// </summary>
    public static Tolerance Zero => new(0, ToleranceMode.Linear);

    /// <summary>
    ///     Gets the ToleranceMode for the current Tolerance
    /// </summary>
    public ToleranceMode Mode => mode;

    /// <summary>
    ///     Gets the value of the current Tolerance instance.
    /// </summary>
    public object Value => amount;

    /// <summary>
    ///     Returns a new tolerance, using the current amount as a percentage.
    /// </summary>
    public Tolerance Percent
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(amount, ToleranceMode.Percent);
        }
    }

    /// <summary>
    ///     Returns a new tolerance, using the current amount in Ulps.
    /// </summary>
    public Tolerance Ulps
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(amount, ToleranceMode.Ulps);
        }
    }

    /// <summary>
    ///     Returns a new tolerance with a TimeSpan as the amount, using
    ///     the current amount as a number of days.
    /// </summary>
    public Tolerance Days
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(TimeSpan.FromDays(Convert.ToDouble(amount)));
        }
    }

    /// <summary>
    ///     Returns a new tolerance with a TimeSpan as the amount, using
    ///     the current amount as a number of hours.
    /// </summary>
    public Tolerance Hours
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(TimeSpan.FromHours(Convert.ToDouble(amount)));
        }
    }

    /// <summary>
    ///     Returns a new tolerance with a TimeSpan as the amount, using
    ///     the current amount as a number of minutes.
    /// </summary>
    public Tolerance Minutes
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(TimeSpan.FromMinutes(Convert.ToDouble(amount)));
        }
    }

    /// <summary>
    ///     Returns a new tolerance with a TimeSpan as the amount, using
    ///     the current amount as a number of seconds.
    /// </summary>
    public Tolerance Seconds
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(TimeSpan.FromSeconds(Convert.ToDouble(amount)));
        }
    }

    /// <summary>
    ///     Returns a new tolerance with a TimeSpan as the amount, using
    ///     the current amount as a number of milliseconds.
    /// </summary>
    public Tolerance Milliseconds
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(TimeSpan.FromMilliseconds(Convert.ToDouble(amount)));
        }
    }

    /// <summary>
    ///     Returns a new tolerance with a TimeSpan as the amount, using
    ///     the current amount as a number of clock ticks.
    /// </summary>
    public Tolerance Ticks
    {
        get
        {
            CheckLinearAndNumeric();
            return new Tolerance(TimeSpan.FromTicks(Convert.ToInt64(amount)));
        }
    }

    /// <summary>
    ///     Returns true if the current tolerance is empty.
    /// </summary>
    public bool IsEmpty => mode == ToleranceMode.None;

    /// <summary>
    ///     Tests that the current Tolerance is linear with a
    ///     numeric value, throwing an exception if it is not.
    /// </summary>
    private void CheckLinearAndNumeric()
    {
        if (mode != ToleranceMode.Linear)
        {
            throw new InvalidOperationException(mode == ToleranceMode.None
                ? ModeMustFollowTolerance
                : MultipleToleranceModes);
        }

        if (!Numerics.IsNumericType(amount))
            throw new InvalidOperationException(NumericToleranceRequired);
    }
}