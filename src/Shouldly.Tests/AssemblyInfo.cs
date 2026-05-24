// Some tests mutate global ShouldlyConfiguration state (e.g. EscapeStyle).
// xUnit parallelizes test classes by default, which races those mutations
// against tests reading the same config. Disable assembly-wide parallelization
// so global-config tests don't leak into concurrent tests.
[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]
