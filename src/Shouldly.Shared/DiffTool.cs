#if !PORTABLE
using System;
using System.Diagnostics;

namespace Shouldly
{
    public class DiffTool
    {
        readonly string _name;
        readonly string _path;
        readonly Func<string, string, string> _argGenerator;

        public DiffTool(string name, string path, Func<string, string, string> argGenerator)
        {
            _name = name;
            _path = path;
            _argGenerator = argGenerator;
        }

        public string Name => _name;

        public void Open(string receivedPath, string approvedPath)
        {
            Process.Start(_path, _argGenerator(receivedPath, approvedPath));
        }
    }
}
#endif