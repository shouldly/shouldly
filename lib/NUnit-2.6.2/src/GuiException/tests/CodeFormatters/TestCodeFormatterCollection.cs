// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Tests.CodeFormatters
{
    [TestFixture]
    public class TestCodeFormatterCollection
    {
        private CodeFormatterCollection _empty;
        private CodeFormatterCollection _filled;
        private ICodeFormatter _csFormatter;
        private ICodeFormatter _defaultFormatter;

        [SetUp]
        public void SetUp()
        {
            _empty = new CodeFormatterCollection();

            _csFormatter = new CSharpCodeFormatter();
            _defaultFormatter = new PlainTextCodeFormatter();
            _filled = new CodeFormatterCollection();
            _filled.Register(_csFormatter, "cs");
            _filled.Register(_defaultFormatter, "txt");

            return;
        }

        [Test]
        public void Test_Default()
        {
            List<string> extensions;
//            ErrorItem errorCS;
//            ErrorItem errorCS_Upper;
//            ErrorItem errorTxt;

//            errorCS = new ErrorItem("C:\\dir\\file.cs", 1);
//            errorCS_Upper = new ErrorItem("C:\\dir\\file.CS", 1);
//            errorTxt = new ErrorItem("C:\\dir\\file.txt", 1);

            Assert.That(_empty.Count, Is.EqualTo(0));
            Assert.That(_empty.HasExtension("cs"), Is.False);
            Assert.That(_empty.HasLanguage("C#"), Is.False);
            Assert.That(_empty, Is.EquivalentTo(new List<string>()));                
                
            Assert.That(_filled.Count, Is.EqualTo(2));

            Assert.That(_filled["C#"], Is.EqualTo(_csFormatter));
            Assert.That(_filled["Plain text"], Is.EqualTo(_defaultFormatter));

            Assert.That(_filled.HasExtension("cs"), Is.True);
            Assert.That(_filled.HasLanguage("C#"), Is.True);

            extensions = new List<string>();
            extensions.Add("cs");
            extensions.Add("txt");
            Assert.That(_filled.Extensions, Is.EquivalentTo(extensions));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "formatter",
            MatchType = MessageMatch.Contains)]
        public void Register_Can_Throw_NullFormatterException()
        {
            _empty.Register(null, "cs"); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "language",
            MatchType = MessageMatch.Contains)]
        public void Register_Can_Throw_NullExtensionException()
        {
            _empty.Register(_csFormatter, null); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "language cannot be empty",
            MatchType = MessageMatch.Contains)]
        public void Register_Check_Extension_Is_Not_Empty()
        {
            _empty.Register(_csFormatter, ""); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "language cannot contain '.'",
            MatchType = MessageMatch.Contains)]
        public void Register_Check_Extension_Not_Contain_Dot_Character()
        {
            _empty.Register(_csFormatter, ".cs"); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "language 'cs' has already an handler. Remove handler first.",
            MatchType = MessageMatch.Contains)]
        public void Register_Check_Multiple_Extension_Definition()
        {
            _empty.Register(_csFormatter, "cs"); // OK
            _empty.Register(_defaultFormatter, "cs"); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "language 'cs' has already an handler. Remove handler first.",
            MatchType = MessageMatch.Contains)]
        public void Register_Check_Extension_Case()
        {
            _empty.Register(_csFormatter, "cs"); // OK
            _empty.Register(_csFormatter, "CS"); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringIndexer_Can_Throw_NullExtensionException()
        {
            if (_empty[(string)null] == null) // throws exception
				return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ItemIndexer_Can_Throw_NullExtensionException()
        {
            if (_empty[null] == null) // throws exception
				return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "unknown language: 'unk'",
            MatchType = MessageMatch.Contains)]
        public void Indexer_Can_Throw_UnknownExtensionException()
        {
            if (_empty["unk"] == null) // throws exception
				return;
        }

        [Test]
        public void Remove()
        {
            _filled.Remove("cs");
            Assert.That(_filled.Count, Is.EqualTo(1));
            Assert.That(_filled.HasExtension("cs"), Is.False);

            _filled.Remove("txt");
            Assert.That(_filled.Count, Is.EqualTo(0));
            Assert.That(_filled, Is.EquivalentTo(new List<string>()));

            // should not fail
            _filled.Remove(null);
            _filled.Remove("unknown");

            return;
        }

        [Test]
        public void Remove_Is_Not_Case_Sensitive()
        {
            _filled.Remove("CS");
            _filled.Remove("TxT");
            Assert.That(_filled.Count, Is.EqualTo(0));

            return;
        }


        [Test]
        public void Clear()
        {
            _filled.Clear();
            Assert.That(_filled.Count, Is.EqualTo(0));
        }

        [Test]
        public void ContainsFormatterFromExtension()
        {
            Assert.That(_filled.HasExtension((string)null), Is.False);
        }
    }
}
