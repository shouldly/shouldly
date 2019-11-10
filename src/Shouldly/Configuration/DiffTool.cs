using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Shouldly.Configuration
{
    public class DiffToolConfig
    {
        public string? WindowsPath { get; set; }

        public string? MacPath { get; set; }

        public string? LinuxPath { get; set; }

        public string? ResolvePath()
        {
            if (ShouldlyEnvironmentContext.IsWindows())
                return WindowsPath;
            if (ShouldlyEnvironmentContext.IsMac())
                return MacPath;
            if (ShouldlyEnvironmentContext.IsLinux())
                return LinuxPath;

            return string.Empty;
        }
    }

    public class DiffTool
    {
        private readonly string? _path;
        private readonly ArgumentGenerator _argGenerator;

        public delegate string ArgumentGenerator(string received, string approved, bool approvedExists);

        public DiffTool(string name, DiffToolConfig config, ArgumentGenerator argGenerator)
        {
            Name = name;
            _path = GetPath(config);
            _argGenerator = argGenerator;
        }

        public string Name { get; }

        public bool Exists()
        {
            return _path != null;
        }

        public void Open(string receivedPath, string approvedPath, bool approvedExists)
        {
            Process.Start(_path, _argGenerator(receivedPath, approvedPath, approvedExists));
        }

        protected static void CreateEmptyFileIfNotExists(string path)
        {
            if (!File.Exists(path))
                File.AppendAllText(path, string.Empty);
        }

        private string GetPath(DiffToolConfig config)
        {
            if (config == null)
                return null;

            var resolvedPath = config.ResolvePath();

            if (Path.IsPathRooted(resolvedPath) && File.Exists(resolvedPath))
                return resolvedPath;

            return Discover(resolvedPath);
        }

        private static string? Discover(string? path)
        {
            if (path == null)
                return null;

            var exeName = Path.GetFileName(path);
            var fullPathFromPathEnv = GetFullPath(exeName);

            if (!string.IsNullOrEmpty(fullPathFromPathEnv))
                return fullPathFromPathEnv;

            if (ShouldlyEnvironmentContext.IsMac())
            {
                var result = new[]
                {
                    "/Applications/"
                }
                .Where(p => p != null)
                .Select(pf => Path.Combine(pf, path))
                .FirstOrDefault(File.Exists);

                return result;
            }

            return new[]
            {
                Environment.GetEnvironmentVariable("ProgramFiles(x86)"),
                Environment.GetEnvironmentVariable("ProgramFiles"),
                Environment.GetEnvironmentVariable("ProgramW6432")
            }
            .Where(p => p != null)
            .Select(pf => Path.Combine(pf, path))
            .FirstOrDefault(File.Exists);
        }

        private static string? GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            var processPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
            var userPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            var machinePath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            var values = $"{processPath};{userPath};{machinePath}";
            var separator = ShouldlyEnvironmentContext.IsWindows() ? ';' : ':';
            return values.Split(separator)
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(path => path.Trim('"'))
                .Select(path => TryCombine(fileName, path))
                .FirstOrDefault(File.Exists);
        }

        private static string? TryCombine(string fileName, string path)
        {
            try
            {
                return Path.Combine(path, fileName);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}