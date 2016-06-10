var target = Argument("target", "Default");
var shouldlyProj = "./src/Shouldly/project.json";
var outputDir = "./artifacts/";

Task("Clean")
    .Does(() => {
        if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }
    });

Task("Restore")
    .Does(() => {
        NuGetRestore("./src/Shouldly.sln");
    });

Task("Version")
    .Does(() => {
        var versionInfo = GitVersion(new GitVersionSettings {
            UpdateAssemblyInfo = true
        });
        // Update project.json
        var updatedProjectJson = System.IO.File.ReadAllText(shouldlyProj)
            .Replace("1.0.0-*", versionInfo.NuGetVersion);

        System.IO.File.WriteAllText(shouldlyProj, updatedProjectJson);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Version")
    .IsDependentOn("Restore")
    .Does(() => {
        MSBuild("./src/Shouldly.sln");
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTest("./src/Shouldly.Tests");
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() => {
        var settings = new DotNetCorePackSettings
        {
            OutputDirectory = outputDir,
            NoBuild = true
        };
    
        DotNetCorePack(shouldlyProj, settings);
    });

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);