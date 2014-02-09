// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;

namespace NUnit.Framework.Tests
{
    public class TestDirectory : IDisposable
    {
        private bool _disposedValue = false;
        public string directoryName;
        public DirectoryInfo directoryInformation;
        public DirectoryInfo diSubSubDirectory;

        #region TestDirectory Utility Class

        public TestDirectory(string dirName) : this(dirName, true) { }

        public TestDirectory(string dirName, bool CreateSubDirectory)
        {
            this.directoryName = Path.Combine(Path.GetTempPath(), dirName);

            directoryInformation = Directory.CreateDirectory(this.directoryName);

            if (CreateSubDirectory)
            {
                DirectoryInfo diSubDirectory = directoryInformation.CreateSubdirectory("SubDirectory");
                diSubSubDirectory = diSubDirectory.CreateSubdirectory("SubSubDirectory");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    if (Directory.Exists(directoryName))
                    {
                        Directory.Delete(directoryName,true);
                    }
                }
            }
            this._disposedValue = true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }

    /// <summary>
	/// Summary description for DirectoryAssertTests.
	/// </summary>
	[TestFixture, Obsolete("DirectoryAssert is obsolete")]
    public class DirectoryAssertTests : MessageChecker
    {
        #region AreEqual

        #region Success Tests
        [Test]
        public void AreEqualPassesWhenBothAreNull()
        {
            DirectoryInfo expected = null;
            DirectoryInfo actual = null;
            DirectoryAssert.AreEqual(expected, actual);
        }

        [Test]
        public void AreEqualPassesWithDirectoryInfos()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryAssert.AreEqual(td.directoryInformation, td.directoryInformation);
            }
        }

        [Test]
        public void AreEqualPassesWithStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryAssert.AreEqual(td.directoryName, td.directoryName);
            }
        }
        #endregion

        #region Failure Tests
        [Test, ExpectedException(typeof(AssertionException))]
        public void AreEqualFailsWhenOneIsNull()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {               
                DirectoryAssert.AreEqual(td.directoryInformation, null);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AreEqualFailsWhenOneDoesNotExist()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryInfo actual = new DirectoryInfo("NotExistingDirectoryName");
                DirectoryAssert.AreEqual(td.directoryInformation, actual);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AreEqualFailsWithDirectoryInfos()
        {
            using (TestDirectory td1 = new TestDirectory("ParentDirectory1"))
            {
                using (TestDirectory td2 = new TestDirectory("ParentDirectory2"))
                {
                    DirectoryAssert.AreEqual(td1.directoryInformation, td2.directoryInformation);
                }
            }
        }


        [Test, ExpectedException(typeof(AssertionException))]
        public void AreEqualFailsWithStringPath()
        {
            using (TestDirectory td1 = new TestDirectory("ParentDirectory1"))
            {
                using (TestDirectory td2 = new TestDirectory("ParentDirectory2"))
                {
                    DirectoryAssert.AreEqual(td1.directoryName, td2.directoryName);
                }
            }
        }
        #endregion

        #endregion

        #region AreNotEqual

        #region Success Tests
        [Test]
        public void AreNotEqualPassesIfOneIsNull()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryAssert.AreNotEqual(td.directoryInformation, null);
            }
        }

        [Test]
        public void AreNotEqualPassesWhenOneDoesNotExist()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryInfo actual = new DirectoryInfo("NotExistingDirectoryName");
                DirectoryAssert.AreNotEqual(td.directoryInformation, actual);
            }
        }

        public void AreNotEqualPassesWithDirectoryInfos()
        {
            using (TestDirectory td1 = new TestDirectory("ParentDirectory1"))
            {
                using (TestDirectory td2 = new TestDirectory("ParentDirectory2"))
                {
                    DirectoryAssert.AreNotEqual(td1.directoryInformation, td2.directoryInformation);
                }
            }
        }

        [Test]
        public void AreNotEqualPassesWithStringPath()
        {
            using (TestDirectory td1 = new TestDirectory("ParentDirectory1"))
            {
                using (TestDirectory td2 = new TestDirectory("ParentDirectory2"))
                {
                    DirectoryAssert.AreNotEqual(td1.directoryName, td2.directoryName);
                }
            }
        }
        #endregion

        #region Failure Tests
        [Test, ExpectedException(typeof(AssertionException))]
        public void AreNotEqualFailsWhenBothAreNull()
        {
            DirectoryInfo expected = null;
            DirectoryInfo actual = null;
            expectedMessage =
    "  Expected: not null" + Environment.NewLine +
    "  But was:  null" + Environment.NewLine;
            DirectoryAssert.AreNotEqual(expected, actual);
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AreNotEqualFailsWithDirectoryInfos()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryAssert.AreNotEqual(td.directoryInformation, td.directoryInformation);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AreNotEqualFailsWithStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory"))
            {
                DirectoryAssert.AreNotEqual(td.directoryName, td.directoryName);
            }
        }
        #endregion

        #endregion

        #region IsEmpty

        [Test]
        public void IsEmptyPassesWithEmptyDirectoryUsingDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", false))
            {
                DirectoryAssert.IsEmpty(td.directoryInformation);
                Assert.That(td.directoryInformation, Is.Empty);
            }
        }

        [Test]
        public void IsEmptyPassesWithEmptyDirectoryUsingStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", false))
            {
                DirectoryAssert.IsEmpty(td.directoryName);
            }
        }

        [Test, ExpectedException(typeof(DirectoryNotFoundException))]
        public void IsEmptyFailsWithInvalidDirectory()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", false))
            {
                DirectoryAssert.IsEmpty(td.directoryName + "INVALID");
            }
        }

        [Test,ExpectedException(typeof(ArgumentException))]
        public void IsEmptyThrowsUsingNull()
        {
            DirectoryAssert.IsEmpty((DirectoryInfo)null);
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsEmptyFailsWithNonEmptyDirectoryUsingDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsEmpty(td.directoryInformation);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsEmptyFailsWithNonEmptyDirectoryUsingStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsEmpty(td.directoryName);
            }
        }

        #endregion

        #region IsNotEmpty

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsNotEmptyFailsWithEmptyDirectoryUsingDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", false))
            {
                DirectoryAssert.IsNotEmpty(td.directoryInformation);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsNotEmptyFailsWithEmptyDirectoryUsingStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", false))
            {
                DirectoryAssert.IsNotEmpty(td.directoryName);
            }
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void IsNotEmptyThrowsUsingNull()
        {
            DirectoryAssert.IsNotEmpty((DirectoryInfo) null);
        }

        [Test, ExpectedException(typeof(DirectoryNotFoundException))]
        public void IsNotEmptyFailsWithInvalidDirectory()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", false))
            {
                DirectoryAssert.IsNotEmpty(td.directoryName + "INVALID");
            }
        }

        [Test]
        public void IsNotEmptyPassesWithNonEmptyDirectoryUsingDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsNotEmpty(td.directoryInformation);
                Assert.That(td.directoryInformation, Is.Not.Empty);
            }
        }

        [Test]
        public void IsNotEmptyPassesWithNonEmptyDirectoryUsingStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsNotEmpty(td.directoryName);
            }
        }

        #endregion

        #region IsWithin

        [Test, ExpectedException(typeof(ArgumentException))]
        public void IsWithinThrowsWhenBothAreNull()
        {
            DirectoryInfo expected = null;
            DirectoryInfo actual = null;
            DirectoryAssert.IsWithin(expected, actual);
        }

        [Test]
        public void IsWithinPassesWithDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory",true))
            {
                DirectoryAssert.IsWithin(td.directoryInformation, td.diSubSubDirectory);
            }
        }

        [Test]
        public void IsWithinPassesWithStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsWithin(td.directoryName, td.diSubSubDirectory.FullName);
            }
        }

        [Test]
        public void IsWithinPassesWithTempPath()
        {
            // Special case because GetTempPath() returns with a trailing slash
            string tempPath = Path.GetTempPath();
            string tempPathParent = Path.GetDirectoryName(Path.GetDirectoryName(tempPath));

            DirectoryAssert.IsWithin(tempPathParent, tempPath);
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsWithinFailsWhenOutsidePathUsingDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryInfo diSystemFolder = new DirectoryInfo(Environment.SpecialFolder.System.ToString());
                DirectoryAssert.IsWithin(td.directoryInformation, diSystemFolder);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsWithinFailsWhenOutsidePathUsingStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsWithin(td.directoryName, Environment.SpecialFolder.System.ToString());
            }
        }

        #endregion

        #region IsNotWithin

        [Test, ExpectedException(typeof(ArgumentException))]
        public void IsNotWithinThrowsWhenBothAreNull()
        {
            DirectoryInfo expected = null;
            DirectoryInfo actual = null;
            DirectoryAssert.IsNotWithin(expected, actual);
        }

        [Test]
        public void IsNotWithinPassesWhenOutsidePathUsingDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryInfo diSystemFolder = new DirectoryInfo(Environment.SpecialFolder.System.ToString());
                DirectoryAssert.IsNotWithin(td.directoryInformation, diSystemFolder);
            }
        }

        [Test]
        public void IsNotWithinPassesWhenOutsidePathUsingStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsNotWithin(td.directoryName, Environment.SpecialFolder.System.ToString());
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsNotWithinFailsWithDirectoryInfo()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsNotWithin(td.directoryInformation, td.diSubSubDirectory);
            }
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsNotWithinFailsWithStringPath()
        {
            using (TestDirectory td = new TestDirectory("ParentDirectory", true))
            {
                DirectoryAssert.IsNotWithin(td.directoryName, td.diSubSubDirectory.FullName);
            }
        }

        #endregion
    }
}
