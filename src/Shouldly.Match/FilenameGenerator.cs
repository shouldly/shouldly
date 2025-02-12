namespace Shouldly.Configuration;

public delegate string FilenameGenerator(
    TestMethodInfo testMethodInfo, string? discriminator, string fileType, string fileExtension);