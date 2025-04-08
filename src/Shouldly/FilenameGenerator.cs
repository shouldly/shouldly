namespace Shouldly;

/// <summary>
/// Delegate for generating filenames for approval tests
/// </summary>
public delegate string FilenameGenerator(
    TestMethodInfo testMethodInfo, string? discriminator, string fileType, string fileExtension);