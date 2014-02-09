@echo off

rem BUILD - Builds and tests NUnit

setlocal

set NANT=tools\nant\bin\nant.exe
set OPTIONS=-f:scripts\nunit.build.targets
set CONFIG=
set RUNTIME=
set CLEAN=
set COMMANDS=
set PASSTHRU=
goto start

:shift
shift /1

:start

IF "%1" EQU "" goto execute

IF "%PASSTHRU%" EQU "TRUE" set COMMANDS=%COMMANDS% %1&goto shift

IF /I "%1" EQU "?"	goto usage
IF /I "%1" EQU "/h"	goto usage
IF /I "%1" EQU "/help"	goto usage

IF /I "%1" EQU "debug" set CONFIG=debug&goto shift
IF /I "%1" EQU "release" set CONFIG=release&goto shift

IF /I "%1" EQU "net" set RUNTIME=net&goto shift
IF /I "%1" EQU "net-1.0" set RUNTIME=net-1.0&goto shift
IF /I "%1" EQU "net-1.1" set RUNTIME=net-1.1&goto shift
IF /I "%1" EQU "net-2.0" set RUNTIME=net-2.0&goto shift
IF /I "%1" EQU "net-3.0" set RUNTIME=net-3.0&goto shift
IF /I "%1" EQU "net-3.5" set RUNTIME=net-3.5&goto shift
IF /I "%1" EQU "net-4.0" set RUNTIME=net-4.0&goto shift
IF /I "%1" EQU "net-4.5" set RUNTIME=net-4.5&goto shift

IF /I "%1" EQU "mono" set RUNTIME=mono&goto shift
IF /I "%1" EQU "mono-1.0" set RUNTIME=mono-1.0&goto shift
IF /I "%1" EQU "mono-2.0" set RUNTIME=mono-2.0&goto shift
IF /I "%1" EQU "mono-3.5" set RUNTIME=mono-3.5&goto shift
IF /I "%1" EQU "mono-4.0" set RUNTIME=mono-4.0&goto shift

if /I "%1" EQU "clean" set CLEAN=clean&goto shift
if /I "%1" EQU "clean-all" set CLEAN=clean-all&goto shift
IF /I "%1" EQU "samples" set COMMANDS=%COMMANDS% build-samples&goto shift
IF /I "%1" EQU "tools" set COMMANDS=%COMMANDS% build-tools&goto shift
IF /I "%1" EQU "test" set COMMANDS=%COMMANDS% test&goto shift
IF /I "%1" EQU "test45" set COMMANDS=%COMMANDS% test45&goto shift
IF /I "%1" EQU "gui-test" set COMMANDS=%COMMANDS% gui-test&goto shift
IF /I "%1" EQU "gen-syntax" set COMMANDS=%COMMANDS% gen-syntax&goto shift

IF "%1" EQU "--" set PASSTHRU=TRUE&goto shift

echo Invalid option: %1
echo.
echo Use BUILD /help for more information.
echo.

goto done

: execute

if "%CONFIG%" NEQ "" set OPTIONS=%OPTIONS% -D:build.config=%CONFIG%
if "%RUNTIME%" NEQ "" set OPTIONS=%OPTIONS% -D:runtime.config=%RUNTIME%

if "%COMMANDS%" EQU "" set COMMANDS=build

%NANT% %OPTIONS% %CLEAN% %COMMANDS%

goto done

: usage

echo Builds and tests NUnit for various targets
echo.
echo usage: BUILD [option [...] ] [ -- nantoptions ]
echo.
echo Options may be any of the following, in any order...
echo.
echo   debug          Builds debug configuration (default)
echo   release        Builds release configuration
echo.
echo   net-4.5        Builds using .NET 4.5 framework (future)
echo   net-4.0        Builds using .NET 4.0 framework (future)
echo   net-3.5        Builds using .NET 3.5 framework (default)
echo   net-2.0        Builds using .NET 2.0 framework
echo   net-1.1        Builds using .NET 1.1 framework
echo   net-1.0        Builds using .NET 1.0 framework
echo   mono-4.0       Builds using Mono 4.0 profile (future)
echo   mono-3.5       Builds using Mono 3.5 profile (default)
echo   mono-2.0       Builds using Mono 2.0 profile
echo   mono-1.0       Builds using Mono 1.0 profile
echo.
echo   net            Builds using default .NET version
echo   mono           Builds using default Mono profile
echo.
echo   clean          Cleans the output directory before building
echo   clean-all      Removes output directories for all runtimes
echo.
echo   samples        Builds the NUnit samples
echo   tools          Builds the NUnit tools
echo.
echo   test           Runs tests for a build using the console runner
echo   test45         Runs the .NET 4.5 async tests using the console runner
echo   gui-test       Runs tests for a build using the NUnit gui
echo.
echo   ?, /h, /help   Displays this help message
echo.
echo Notes:
echo.
echo   1. The default .NET or Mono version to be used is selected
echo      automatically by the NAnt script from those installed.
echo.
echo   2. When building under a framework version of 3.5 or higher,
echo      the 2.0 framework is targeted for NUnit itself. Tests use
echo      the specified higher level framework.
echo.
echo   3. Any arguments following '--' on the command line are passed
echo      directly to the NAnt script.
echo.

: done
