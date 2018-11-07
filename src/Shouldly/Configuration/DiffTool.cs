#if ShouldMatchApproved
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Shouldly.Configuration
{
    public class DiffToolPath
    {
        public string Windows { private get; set; }

        public string Mac { private get; set; }

        public string TruePath
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Windows : Mac;                
            }
        }
    }
    
    public class DiffTool
    {
        readonly string _path;
        readonly ArgumentGenerator _argGenerator;

        public delegate string ArgumentGenerator(string received, string approved, bool approvedExists);

        public DiffTool(string name, string path, ArgumentGenerator argGenerator)
        {
            Name = name;
            _path = path == null ? null : (Path.IsPathRooted(path) && File.Exists(path) ? path : Discover(path));
            _argGenerator = argGenerator;
        }

        public DiffTool(string name, DiffToolPath path, ArgumentGenerator argGenerator)
        {
            Name = name;
            _path = path == null ? null : (Path.IsPathRooted(path.TruePath) && File.Exists(path.TruePath) ? path.TruePath : Discover(path.TruePath));
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

            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var home = "/Applications/";
                var result = new[]
                    {
                        home
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
                .Select(path => TryCombine(fileName, path))
                .FirstOrDefault(File.Exists);
        }

        private static string TryCombine(string fileName, string path)
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

#endif