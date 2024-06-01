$ErrorActionPreference = 'Stop'

# Options
$configuration = 'Release'
$artifactsDir = Join-Path (Resolve-Path .) 'artifacts'
$packagesDir = Join-Path $artifactsDir 'Packages'
$testResultsDir = Join-Path $artifactsDir 'Test results'
$logsDir = Join-Path $artifactsDir 'Logs'

$dotnetArgs = @(
    '--configuration', $configuration
    '/p:CI=' + ($env:CI -or $env:TF_BUILD)
)

# Build
dotnet build /bl:$logsDir\build.binlog @dotnetArgs
if ($LastExitCode) { exit 1 }

# Pack
Remove-Item -Recurse -Force $packagesDir -ErrorAction Ignore

dotnet pack src\Shouldly --no-build --output $packagesDir /bl:$logsDir\pack.binlog @dotnetArgs
if ($LastExitCode) { exit 1 }

# Test
Remove-Item -Recurse -Force $testResultsDir -ErrorAction Ignore

dotnet test --no-build --configuration $configuration --logger trx --results-directory $testResultsDir /bl:"$logsDir\test.binlog"
if ($LastExitCode) { exit 1 }
