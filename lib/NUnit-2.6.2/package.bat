@echo off

rem PACKAGE - Packages NUnit

setlocal

set NANT=tools\nant\bin\nant.exe
set OPTIONS=-f:scripts/nunit.package.targets
set CONFIG=
set RUNTIME=
set COMMANDS=
set PASSTHRU=
set CHECK=
goto start

:shift
shift /1

:start

IF "%1" EQU "" goto execute

IF "%PASSTHRU%" NEQ "" set COMMANDS=%COMMANDS% %1&goto shift

IF /I "%1" EQU "?"	goto usage
IF /I "%1" EQU "/h"	goto usage
IF /I "%1" EQU "/help"	goto usage

IF /I "%1" EQU "debug"	set CONFIG=debug&goto shift
IF /I "%1" EQU "release" set CONFIG=release&goto shift

IF /I "%1" EQU "net-1.0" set RUNTIME=net-1.0&goto shift
IF /I "%1" EQU "net-1.1" set RUNTIME=net-1.1&goto shift
IF /I "%1" EQU "net-2.0" set RUNTIME=net-2.0&goto shift
IF /I "%1" EQU "net-3.0" set RUNTIME=net-3.0&goto shift
IF /I "%1" EQU "net-3.5" set RUNTIME=net-3.5&goto shift
IF /I "%1" EQU "net-4.0" set RUNTIME=net-4.0&goto shift

IF /I "%1" EQU "mono-1.0" set RUNTIME=mono-1.0&goto shift
IF /I "%1" EQU "mono-2.0" set RUNTIME=mono-2.0&goto shift
IF /I "%1" EQU "mono-3.5" set RUNTIME=mono-3.5&goto shift
IF /I "%1" EQU "mono-4.0" set RUNTIME=mono-4.0&goto shift

IF /I "%1" EQU "check" set CHECK=1&goto shift

IF /I "%1" EQU "all"		set COMMANDS=%COMMANDS% package-all&goto shift
IF /I "%1" EQU "docs"		set COMMANDS=%COMMANDS% package-docs&goto shift
IF /I "%1" EQU "source"		set COMMANDS=%COMMANDS% package-src&goto shift
IF /I "%1" EQU "samples"	set COMMANDS=%COMMANDS% package-samples&goto shift
IF /I "%1" EQU "src"		set COMMANDS=%COMMANDS% package-src&goto shift
IF /I "%1" EQU "zip"		set COMMANDS=%COMMANDS% package-zip&goto shift
IF /I "%1" EQU "msi"		set COMMANDS=%COMMANDS% package-msi&goto shift
IF /I "%1" EQU "nuget"		set COMMANDS=%COMMANDS% package-nuget&goto shift

IF "%1" EQU "--" set PASSTHRU=1&goto shift

echo Invalid option: %1
echo.
echo Use PACKAGE /help for more information
echo.

goto done

:execute

IF "%CONFIG%" NEQ "" set OPTIONS=%OPTIONS% -D:build.config=%CONFIG%
IF "%RUNTIME%" NEQ "" set OPTIONS=%OPTIONS% -D:runtime.config=%RUNTIME%
IF "%CHECK%" EQU "" set OPTIONS=%OPTIONS% -D:light.suppressices=ICE69

if "%COMMANDS%" EQU "" set COMMANDS=package

%NANT% %OPTIONS% %COMMANDS%

goto done

:usage

echo Builds one or more NUnit packages for distribution
echo.
echo usage: PACKAGE [option [...] ] [ -- nantoptions ]
echo.
echo Options may be any of the following, in any order...
echo.
echo   debug          Builds debug packages (default)
echo   release        Builds release packages
echo.
echo   net-4.0        Builds package using .NET 4.0 build (future)
echo   net-3.5        Builds package using .NET 3.5 build (default)
echo   net-2.0        Builds package using .NET 2.0 build
echo   net-1.1        Builds package using .NET 1.1 build
echo   net-1.0        Builds package using .NET 1.0 build
echo   mono-4.0       Builds package using Mono 4.0 profile (future)
echo   mono-3.5       Builds package using Mono 3.5 profile (default)
echo   mono-2.0       Builds package using Mono 2.0 profile
echo   mono-1.0       Builds package using Mono 1.0 profile
echo.
echo   src, source    Builds the source package
echo   docs           Builds the documentation package
echo   zip            Builds a binary package in zipped form
echo   msi            Builds a windows installer (msi) package
echo   all            Builds source, documentation, 3.5 and 1.1 packages
echo.
echo   check          Causes all ICE verifications to run when building
echo                  the msi, including those normally suppressed.
echo.
echo   ?, /h, /help   Displays this help message
echo.
echo Notes:
echo.
echo   1. The PACKAGE script assumes that the build to be packaged
echo      has already been created and is located in the output
echo      directory for the configuration and runtime specified.
echo.
echo   2. If no specific package type is specified, both zip and
echo      msi packages are created.
echo.
echo   3. If the runtime is not specified, the net-3.5 and mono-3.5
echo      builds are tried and the first found is used. If neither
echo      is found an error is reported.
echo.
echo   4. Any arguments following '--' on the command line are passed
echo      directly to the NAnt script.
echo.

:done
