#addin "Cake.Json"

using System.Net;
using System.Linq;

var target = Argument("target", "Default");

string Get(string url)
{
    var assetsRequest = WebRequest.CreateHttp(url);
    assetsRequest.Method = "GET";
    assetsRequest.Accept = "application/vnd.github.v3+json";
    assetsRequest.UserAgent = "BuildScript";

    using (var assetsResponse = assetsRequest.GetResponse())
    {
        var assetsStream = assetsResponse.GetResponseStream();
        var assetsReader = new StreamReader(assetsStream);
        var assetsBody = assetsReader.ReadToEnd();
        return assetsBody;
    }
}

Task("EnsureRequirements")
    .Does(() =>
    {
        if (!AppVeyor.IsRunningOnAppVeyor)
           throw new Exception("Deployment should happen via appveyor");
        
        var isTag =
           AppVeyor.Environment.Repository.Tag.IsTag &&
           !string.IsNullOrWhiteSpace(AppVeyor.Environment.Repository.Tag.Name);
        if (!isTag)
           throw new Exception("Deployment should happen from a published GitHub release");
    });

var tag = "";

Task("UpdateVersionInfo")
    .IsDependentOn("EnsureRequirements")
    .Does(() =>
    {
        tag = AppVeyor.Environment.Repository.Tag.Name;
        AppVeyor.UpdateBuildVersion(tag);
    });

Task("DownloadGitHubReleaseArtifacts")
    .IsDependentOn("UpdateVersionInfo")
    .Does(() =>
    {
        var assets_url = ParseJson(Get("https://api.github.com/repos/shouldly/shouldly/releases/tags/" + tag))
            .GetValue("assets_url").Value<string>();
        EnsureDirectoryExists("./releaseArtifacts");
        foreach(var asset in DeserializeJson<JArray>(Get(assets_url)))
        {
            DownloadFile(asset.Value<string>("browser_download_url"), "./releaseArtifacts/" + asset.Value<string>("name"));
        }
    });

Task("DeployNuget")
    .IsDependentOn("DownloadGitHubReleaseArtifacts")
    .Does(() =>
    {
        // Turns .artifacts file into a lookup
        var fileLookup = System.IO.File
            .ReadAllLines("./releaseArtifacts/artifacts")
            .Select(l => l.Split(':'))
            .ToDictionary(v => v[0], v => v[1]);

        NuGetPush("./releaseArtifacts/" + fileLookup["nuget"], new NuGetPushSettings {
            ApiKey = EnvironmentVariable("NuGetApiKey")
        });
    });

Task("Deploy")
    .IsDependentOn("DeployNuget");

Task("Default")
    .IsDependentOn("Deploy");

Task("Verify")
    .Does(() => {
        // Nothing, used to make sure the script compiles
    });

RunTarget(target);