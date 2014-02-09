// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Net.Sockets;

namespace NUnit.Framework.Tests
{
    #region Nested TestFile Utility Class
    public class TestFile : IDisposable
	{
		private bool _disposedValue = false;
		private string _resourceName;
		private string _fileName;

		public TestFile(string fileName, string resourceName)
		{
			_resourceName = "NUnit.Framework.Tests.data." + resourceName;
            _fileName = Path.Combine(Path.GetTempPath(), fileName);

			Assembly a = Assembly.GetExecutingAssembly();
			using (Stream s = a.GetManifestResourceStream(_resourceName))
			{
				if (s == null) throw new Exception("Manifest Resource Stream " + _resourceName + " was not found.");

				byte[] buffer = new byte[1024];
				using (FileStream fs = File.Create(_fileName))
				{
					while(true)
					{
						int count = s.Read(buffer, 0, buffer.Length);
						if(count == 0) break;
						fs.Write(buffer, 0, count);
					}
				}
			}
		}

        public string FileName
        {
            get { return _fileName; }
        }

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					if(File.Exists(_fileName))
					{
						File.Delete(_fileName);
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
    }
    #endregion

    /// <summary>
	/// Summary description for FileAssertTests.
	/// </summary>
	[TestFixture]
	public class FileAssertTests : MessageChecker
	{
		#region AreEqual

		#region Success Tests
		[Test]
		public void AreEqualPassesWhenBothAreNull()
		{
			FileStream expected = null;
			FileStream actual = null;
			FileAssert.AreEqual( expected, actual );
		}

        [Test]
        public void AreEqualPassesWithSameStream()
        {
            Stream exampleStream = new MemoryStream(new byte[] { 1, 2, 3 });
            Assert.That(exampleStream, Is.EqualTo(exampleStream));
        }

        [Test]
        public void AreEqualPassesWithEqualStreams()
        {
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
            using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage1.jpg"))
            {
                using (FileStream expected = File.OpenRead(tf1.FileName))
                {
                    using (FileStream actual = File.OpenRead(tf2.FileName))
                    {
                        FileAssert.AreEqual(expected, actual);
                    }
                }
            }
        }

        [Test, ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "not readable", MatchType = MessageMatch.Contains)]
        public void NonReadableStreamGivesException()
        {
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
            using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage1.jpg"))
            {
                using (FileStream expected = File.OpenRead(tf1.FileName))
                {
                    using (FileStream actual = File.OpenWrite(tf2.FileName))
                    {
                        FileAssert.AreEqual(expected, actual);
                    }
                }
            }
        }

        [Test, ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "not seekable", MatchType = MessageMatch.Contains)]
        public void NonSeekableStreamGivesException()
        {
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
            {
                using (FileStream expected = File.OpenRead(tf1.FileName))
                {
                    using (FakeStream actual = new FakeStream())
                    {
                        FileAssert.AreEqual(expected, actual);
                    }
                }
            }
        }

        private class FakeStream : MemoryStream
        {
            public override bool CanSeek
            {
                get { return false; }
            }
        }

        [Test]
		public void AreEqualPassesWithFiles()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
            using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage1.jpg"))
			{
				FileAssert.AreEqual( tf1.FileName, tf2.FileName, "Failed using file names" );
			}
		}

		[Test]
		public void AreEqualPassesUsingSameFileTwice()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
				FileAssert.AreEqual( tf1.FileName, tf1.FileName );
			}
		}

		[Test]
		public void AreEqualPassesWithFileInfos()
		{
			using(TestFile tf1 = new TestFile("Test1.jpg","TestImage1.jpg"))
			using(TestFile tf2 = new TestFile("Test2.jpg","TestImage1.jpg"))
			{
				FileInfo expected = new FileInfo( tf1.FileName );
				FileInfo actual = new FileInfo( tf2.FileName );
				FileAssert.AreEqual( expected, actual );
				FileAssert.AreEqual( expected, actual );
			}
		}

		[Test]
		public void AreEqualPassesWithTextFiles()
		{
			using(TestFile tf1 = new TestFile("Test1.txt","TestText1.txt"))
			{
				using(TestFile tf2 = new TestFile("Test2.txt","TestText1.txt"))
				{
					FileAssert.AreEqual( tf1.FileName, tf2.FileName );
				}
			}
		}
		#endregion

		#region Failure Tests
		[Test,ExpectedException(typeof(AssertionException))]
		public void AreEqualFailsWhenOneIsNull()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
				using(FileStream expected = File.OpenRead(tf1.FileName))
				{
					expectedMessage = 
						"  Expected: <System.IO.FileStream>" + Environment.NewLine +
						"  But was:  null" + Environment.NewLine;
					FileAssert.AreEqual( expected, null );
				}
			}
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void AreEqualFailsWithStreams()
		{
			string expectedFile = "Test1.jpg";
			string actualFile = "Test2.jpg";
            using (TestFile tf1 = new TestFile(expectedFile, "TestImage1.jpg"))
			{
                using (TestFile tf2 = new TestFile(actualFile, "TestImage2.jpg"))
				{
					using(FileStream expected = File.OpenRead(tf1.FileName))
					{
						using(FileStream actual = File.OpenRead(tf2.FileName))
						{
							expectedMessage =
								string.Format("  Expected Stream length {0} but was {1}." + Environment.NewLine,
									new FileInfo(tf1.FileName).Length, new FileInfo(tf2.FileName).Length);
							FileAssert.AreEqual( tf1.FileName, tf2.FileName);
						}
					}
				}
			}
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void AreEqualFailsWithFileInfos()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
                using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage2.jpg"))
				{
					FileInfo expected = new FileInfo( tf1.FileName );
					FileInfo actual = new FileInfo( tf2.FileName );
					expectedMessage =
						string.Format("  Expected Stream length {0} but was {1}." + Environment.NewLine,
							expected.Length, actual.Length);
					FileAssert.AreEqual( expected, actual );
				}
			}
		}


		[Test,ExpectedException(typeof(AssertionException))]
		public void AreEqualFailsWithFiles()
		{
			string expected = "Test1.jpg";
			string actual = "Test2.jpg";
            using (TestFile tf1 = new TestFile(expected, "TestImage1.jpg"))
			{
                using (TestFile tf2 = new TestFile(actual, "TestImage2.jpg"))
				{
					expectedMessage =
						string.Format("  Expected Stream length {0} but was {1}." + Environment.NewLine,
							new FileInfo(tf1.FileName).Length, new FileInfo(tf2.FileName).Length);
					FileAssert.AreEqual( tf1.FileName, tf2.FileName );
				}
			}
		}

		[Test]
        [ExpectedException(typeof(AssertionException), 
            ExpectedMessage="Stream lengths are both",
            MatchType=MessageMatch.Contains)]
		public void AreEqualFailsWithTextFilesAfterReadingBothFiles()
		{
            using (TestFile tf1 = new TestFile("Test1.txt", "TestText1.txt"))
			{
                using (TestFile tf2 = new TestFile("Test2.txt", "TestText2.txt"))
				{
					FileAssert.AreEqual( tf1.FileName, tf2.FileName );
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
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
				using(FileStream expected = File.OpenRead(tf1.FileName))
				{
					FileAssert.AreNotEqual( expected, null );
				}
			}
		}

		[Test]
		public void AreNotEqualPassesWithStreams()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
                using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage2.jpg"))
				{
					using(FileStream expected = File.OpenRead(tf1.FileName))
					{
						using(FileStream actual = File.OpenRead(tf2.FileName))
						{
							FileAssert.AreNotEqual( expected, actual);
						}
					}
				}
			}
		}

		[Test]
		public void AreNotEqualPassesWithFiles()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
                using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage2.jpg"))
				{
					FileAssert.AreNotEqual( tf1.FileName, tf2.FileName );
				}
			}
		}

		[Test]
		public void AreNotEqualPassesWithFileInfos()
		{
            using (TestFile tf1 = new TestFile("Test1.jpg", "TestImage1.jpg"))
			{
                using (TestFile tf2 = new TestFile("Test2.jpg", "TestImage2.jpg"))
				{
					FileInfo expected = new FileInfo( tf1.FileName );
					FileInfo actual = new FileInfo( tf2.FileName );
					FileAssert.AreNotEqual( expected, actual );
				}
			}
		}

		[Test]
		public void AreNotEqualIteratesOverTheEntireFile()
		{
            using (TestFile tf1 = new TestFile("Test1.txt", "TestText1.txt"))
			{
                using (TestFile tf2 = new TestFile("Test2.txt", "TestText2.txt"))
				{
					FileAssert.AreNotEqual( tf1.FileName, tf2.FileName );
				}
			}
		}
		#endregion

		#region Failure Tests
		[Test, ExpectedException(typeof(AssertionException))]
		public void AreNotEqualFailsWhenBothAreNull()
		{
			FileStream expected = null;
			FileStream actual = null;
			expectedMessage =
				"  Expected: not null" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
			FileAssert.AreNotEqual( expected, actual );
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void AreNotEqualFailsWithStreams()
		{
			using(TestFile tf1 = new TestFile("Test1.jpg","TestImage1.jpg"))
			using(TestFile tf2 = new TestFile("Test2.jpg","TestImage1.jpg"))
			using(FileStream expected = File.OpenRead(tf1.FileName))
			using(FileStream actual = File.OpenRead(tf2.FileName))
			{
				expectedMessage = 
					"  Expected: not <System.IO.FileStream>" + Environment.NewLine +
					"  But was:  <System.IO.FileStream>" + Environment.NewLine;
				FileAssert.AreNotEqual( expected, actual );
			}
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void AreNotEqualFailsWithFileInfos()
		{
			using(TestFile tf1 = new TestFile("Test1.jpg","TestImage1.jpg"))
			{
				using(TestFile tf2 = new TestFile("Test2.jpg","TestImage1.jpg"))
				{
					FileInfo expected = new FileInfo( tf1.FileName );
					FileInfo actual = new FileInfo( tf2.FileName );
					expectedMessage = 
						"  Expected: not <System.IO.FileStream>" + Environment.NewLine +
						"  But was:  <System.IO.FileStream>" + Environment.NewLine;
					FileAssert.AreNotEqual( expected, actual );
				}
			}
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void AreNotEqualFailsWithFiles()
		{
			using(TestFile tf1 = new TestFile("Test1.jpg","TestImage1.jpg"))
			{
				expectedMessage = 
					"  Expected: not <System.IO.FileStream>" + Environment.NewLine +
					"  But was:  <System.IO.FileStream>" + Environment.NewLine;
				FileAssert.AreNotEqual( tf1.FileName, tf1.FileName );
			}
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void AreNotEqualIteratesOverTheEntireFileAndFails()
		{
			using(TestFile tf1 = new TestFile("Test1.txt","TestText1.txt"))
			{
				using(TestFile tf2 = new TestFile("Test2.txt","TestText1.txt"))
				{
					expectedMessage = 
						"  Expected: not <System.IO.FileStream>" + Environment.NewLine +
						"  But was:  <System.IO.FileStream>" + Environment.NewLine;
					FileAssert.AreNotEqual( tf1.FileName, tf2.FileName );
				}
			}
		}
		#endregion

		#endregion
	}
}
