using System;
using System.Collections;
using System.IO;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
    public class DomainManagerTests
    {
        static string path1 = TestPath("/test/bin/debug/test1.dll");
        static string path2 = TestPath("/test/bin/debug/test2.dll");
        static string path3 = TestPath("/test/utils/test3.dll");

        [Test]
        public void GetPrivateBinPath()
        {
            string[] assemblies = new string[] { path1, path2, path3 };

            Assert.AreEqual(
                TestPath("bin/debug") + Path.PathSeparator + TestPath("utils"),
                DomainManager.GetPrivateBinPath(TestPath("/test"), assemblies));
        }

        [Test]
        public void GetCommonAppBase_OneElement()
        {
            string[] assemblies = new string[] { path1 };

            Assert.AreEqual(
                TestPath("/test/bin/debug"),
                DomainManager.GetCommonAppBase(assemblies));
        }

        [Test]
        public void GetCommonAppBase_TwoElements_SameDirectory()
        {
            string[] assemblies = new string[] { path1, path2 };

            Assert.AreEqual(
                TestPath("/test/bin/debug"),
                DomainManager.GetCommonAppBase(assemblies));
        }

        [Test]
        public void GetCommonAppBase_TwoElements_DifferentDirectories()
        {
            string[] assemblies = new string[] { path1, path3 };

            Assert.AreEqual(
                TestPath("/test"),
                DomainManager.GetCommonAppBase(assemblies));
        }

        [Test]
        public void GetCommonAppBase_ThreeElements_DiferentDirectories()
        {
            string[] assemblies = new string[] { path1, path2, path3 };

            Assert.AreEqual(
                TestPath("/test"),
                DomainManager.GetCommonAppBase(assemblies));
        }

        /// <summary>
        /// Take a valid Linux path and make a valid windows path out of it
        /// if we are on Windows. Change slashes to backslashes and, if the
        /// path starts with a slash, add C: in front of it.
        /// </summary>
        private static string TestPath(string path)
        {
            if (Path.DirectorySeparatorChar != '/')
            {
                path = path.Replace('/', Path.DirectorySeparatorChar);
                if (path[0] == Path.DirectorySeparatorChar)
                    path = "C:" + path;
            }

            return path;
        }
    }
}
