#!/usr/bin/env pwsh

$ErrorActionPreference = 'Stop'

# Options
$configuration = 'Release'
$artifactsDir = Join-Path (Resolve-Path .) 'artifacts'
$packagesDir = Join-Path $artifactsDir 'Packages'
$testResultsDir = Join-Path $artifactsDir 'Test results'
$logsDir = Join-Path $artifactsDir 'Logs'

# Ensure directories exist
New-Item -ItemType Directory -Force -Path $artifactsDir, $packagesDir, $testResultsDir, $logsDir | Out-Null

$dotnetArgs = @(
    '--configuration', $configuration
    '/p:CI=' + ($env:CI -or $env:TF_BUILD)
)

# Build
Write-Host "Building..." -ForegroundColor Cyan
dotnet build @dotnetArgs /bl:"$logsDir/build.binlog"
if ($LASTEXITCODE -ne 0) { 
    Write-Error "Build failed with exit code $LASTEXITCODE"
    exit 1 
}

# Pack
Write-Host "Packing..." -ForegroundColor Cyan
if (Test-Path $packagesDir) {
    Remove-Item -Recurse -Force $packagesDir
}
dotnet pack src\Shouldly --no-build --output $packagesDir @dotnetArgs /bl:"$logsDir/pack.binlog"
if ($LASTEXITCODE -ne 0) { 
    Write-Error "Pack failed with exit code $LASTEXITCODE"
    exit 1 
}

# Test
Write-Host "Testing..." -ForegroundColor Cyan
if (Test-Path $testResultsDir) {
    Remove-Item -Recurse -Force $testResultsDir
}

# Define test projects
$testProjects = @("src\Shouldly.Tests\Shouldly.Tests.csproj")

# Add DocumentationExamples project only on Windows
if ($IsWindows) {
    $testProjects += "src\DocumentationExamples\DocumentationExamples.csproj"
    Write-Host "Running on Windows. Including DocumentationExamples project." -ForegroundColor Cyan
} else {
    Write-Host "Not running on Windows. Skipping DocumentationExamples project." -ForegroundColor Yellow
}

# Run tests for each project
foreach ($project in $testProjects) {
    Write-Host "Testing $project..." -ForegroundColor Cyan
    dotnet test $project --no-build @dotnetArgs --logger trx --results-directory $testResultsDir /bl:"$logsDir/test-$(Split-Path $project -Leaf).binlog" --logger "GitHubActions;summary.includePassedTests=true;summary.includeSkippedTests=true" -- RunConfiguration.CollectSourceInformation=true
    if ($LASTEXITCODE -ne 0) { 
        Write-Error "Tests for $project failed with exit code $LASTEXITCODE"
        exit 1 
    }
}

Write-Host "Build completed successfully!" -ForegroundColor Green