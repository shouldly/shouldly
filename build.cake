#tool "nuget:?package=GitReleaseNotes"
#tool nuget:?package=GitVersion.CommandLine

var target = Argument("target", "Default");
var shouldlyProj = "./src/Shouldly/Shouldly.csproj";
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

GitVersion versionInfo = null;
Task("Version")
    .Does(() => {
        GitVersion(new GitVersionSettings{
            UpdateAssemblyInfo = false,
            OutputType = GitVersionOutput.BuildServer
        });
        versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });
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
        DotNetCoreTest("./src/Shouldly.Tests/Shouldly.Tests.csproj");
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() => {

        // TODO not sure why this isn't working
        // GitReleaseNotes("outputDir/releasenotes.md", new GitReleaseNotesSettings {
        //     WorkingDirectory         = ".",
        //     AllTags                  = false
        // });

        // var releaseNotesExitCode = StartProcess(
        //     @"tools\GitReleaseNotes\tools\gitreleasenotes.exe", 
        //     new ProcessSettings { Arguments = ". /o artifacts/releasenotes.md" });
        // if (string.IsNullOrEmpty(System.IO.File.ReadAllText("./artifacts/releasenotes.md")))
        //     System.IO.File.WriteAllText("./artifacts/releasenotes.md", "No issues closed since last release");

        // if (releaseNotesExitCode != 0) throw new Exception("Failed to generate release notes");

        // System.IO.File.WriteAllLines(outputDir + "artifacts", new[]{
        //     "nuget:Shouldly." + versionInfo.NuGetVersion + ".nupkg",
        //     "nugetSymbols:Shouldly." + versionInfo.NuGetVersion + ".symbols.nupkg",
        //     "releaseNotes:releasenotes.md"
        // });

        // if (AppVeyor.IsRunningOnAppVeyor)
        // {
        //     foreach (var file in GetFiles(outputDir + "**/*"))
        //         AppVeyor.UploadArtifact(file.FullPath);
        // }
    });

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);