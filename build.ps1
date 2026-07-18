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
$packProjects = @("src\Shouldly", "src\Shouldly.DiffEngine")
foreach ($project in $packProjects) {
    dotnet pack $project --no-build --output $packagesDir @dotnetArgs /bl:"$logsDir/pack-$(Split-Path $project -Leaf).binlog"
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Pack for $project failed with exit code $LASTEXITCODE"
        exit 1
    }
}

# Test
Write-Host "Testing..." -ForegroundColor Cyan
if (Test-Path $testResultsDir) {
    Remove-Item -Recurse -Force $testResultsDir
}

# Define test projects
$testProjects = @(
    "src/Shouldly.Tests/Shouldly.Tests.csproj"
    "src/EquivalencyComparisonTests/EquivalencyComparisonTests.csproj"
    "src/DocumentationExamples/DocumentationExamples.csproj"
)

# Run tests for each project
foreach ($project in $testProjects) {
    Write-Host "Testing $project..." -ForegroundColor Cyan
    dotnet test --project $project --no-build @dotnetArgs --results-directory $testResultsDir --report-xunit-trx --report-github --report-github-summary-include-passed --report-github-summary-include-skipped /bl:"$logsDir/test-$(Split-Path $project -Leaf).binlog"
    if ($LASTEXITCODE -ne 0) { 
        Write-Error "Tests for $project failed with exit code $LASTEXITCODE"
        exit 1 
    }
}

Write-Host "Build completed successfully!" -ForegroundColor Green