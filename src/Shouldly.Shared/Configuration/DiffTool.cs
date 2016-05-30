#if !PORTABLE
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Shouldly.Configuration
{
    public class DiffTool
    {
        readonly string _path;
        readonly ArgumentGenerator _argGenerator;

        public delegate string ArgumentGenerator(string received, string approved, bool approvedExists);

        public DiffTool(string name, string path, ArgumentGenerator argGenerator)
        {
            Name = name;
            _path = Path.IsPathRooted(path) && File.Exists(path) ? path : Discover(path);
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

        string Discover(string path)
        {
            var exeName = Path.GetFileName(path);
            var fullPathFromPathEnv = GetFullPath(exeName);
            if (!string.IsNullOrEmpty(fullPathFromPathEnv))
                return fullPathFromPathEnv;

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

        static string GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            var processPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
            var userPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            var machinePath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            var values = $"{processPath};{userPath};{machinePath}";
            return values.Split(';')
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(path => path.Trim('"'))
                .Select(path => Path.Combine(path, fileName))
                .FirstOrDefault(File.Exists);
        }
    }
}
#endif