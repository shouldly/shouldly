# Copilot Agent Instructions for Shouldly Repository

## Overview

This document provides instructions for AI agents (like GitHub Copilot) working on this Shouldly repository fork.

## Running Unit Tests in Copilot Agent Environment

The Shouldly project is configured to target .NET 9.0, but many environments (including CI/CD runners) may only have .NET 8.0 SDK available. Here's how to handle this:

### Prerequisites

Check if .NET 9.0 SDK is available:
```bash
dotnet --version
```

If you see an error like "A compatible .NET SDK was not found" or it shows .NET 8.0, follow the steps below.

### Steps to Run Tests with .NET 8.0

1. **Temporarily modify `global.json`** to use available SDK:
   ```json
   {
     "sdk": {
       "version": "8.0.118",
       "rollForward": "latestFeature"
     }
   }
   ```

2. **Update Language Version** in `src/Directory.Build.props`:
   ```xml
   <LangVersion>12</LangVersion>
   ```
   (Change from `13` to `12`)

3. **Update Target Frameworks** in project files:
   - `src/Shouldly/Shouldly.csproj`: Change `net9.0` to `net8.0` in TargetFrameworks
   - `src/Shouldly.DiffEngine/Shouldly.DiffEngine.csproj`: Remove `net9.0` from TargetFrameworks
   - `src/Shouldly.Tests/Shouldly.Tests.csproj`: Change `net9.0` to `net8.0`
   - `src/DocumentationExamples/DocumentationExamples.csproj`: Change to `net8.0`
   - `src/DeterministicTests/DeterministicTests.csproj`: Change to `net8.0`

4. **Fix C# 12 compatibility issue** in `src/Shouldly.Tests/StackTraceTests.cs`:
   ```csharp
   // Change this line:
   var stackTraceLines = exception.StackTrace!.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
   
   // To this:
   var stackTraceLines = exception.StackTrace!.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
   ```

5. **Build and Test**:
   ```bash
   dotnet build
   dotnet test --no-build
   ```

### Expected Test Results

- Most tests should pass successfully
- Some approval tests may fail due to path differences (this is expected in different environments)
- Core functionality tests should all pass

### Important Notes

- **DO NOT COMMIT** these temporary changes for .NET 8.0 compatibility
- These changes are only for testing purposes in environments without .NET 9.0
- Always revert these changes after testing if you're working on the actual codebase
- The original project should remain targeting .NET 9.0 for production

### Reverting Changes After Testing

```bash
git checkout -- global.json src/Directory.Build.props src/Shouldly/Shouldly.csproj src/Shouldly.DiffEngine/Shouldly.DiffEngine.csproj src/Shouldly.Tests/Shouldly.Tests.csproj src/DocumentationExamples/DocumentationExamples.csproj src/DeterministicTests/DeterministicTests.csproj src/Shouldly.Tests/StackTraceTests.cs
```

## Alternative: Installing .NET 9.0 SDK

If your environment supports it, you can install .NET 9.0 SDK instead:

1. Visit https://dotnet.microsoft.com/download/dotnet/9.0
2. Download and install the SDK for your platform
3. Verify installation: `dotnet --version`
4. Build and test normally: `dotnet build && dotnet test`

## Test Project Structure

- **Shouldly.Tests**: Main unit test project
- **DocumentationExamples**: Example code used in documentation (some tests may fail in different environments)
- **DeterministicTests**: Tests for deterministic behavior
- **Shouldly**: Main library project
- **Shouldly.DiffEngine**: Diff engine integration library

## Common Issues

1. **Path separator differences**: Some tests expect Windows paths (`\`) but run on Linux (`/`) - this is normal
2. **Approval test failures**: Tests that compare output files may fail due to environment differences
3. **Missing diff tools**: Some tests require diff tools that may not be available in all environments

These issues are expected when running in different environments and don't indicate problems with your code changes.
