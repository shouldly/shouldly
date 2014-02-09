// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.UiException.CodeFormatters;
using NUnit.UiException.Tests.data;

namespace NUnit.UiException.Tests.CodeFormatters
{
    [TestFixture]
    public class TestGeneralCodeFormatter
    {
        private GeneralCodeFormatter _formatter;

        [SetUp]
        public void SetUp()
        {
            _formatter = new GeneralCodeFormatter();

            return;
        }

        [Test]
        public void Test_Default()
        {
            Assert.That(_formatter.DefaultFormatter, Is.Not.Null);

            Assert.That(_formatter.Formatters, Is.Not.Null);
            Assert.That(_formatter.Formatters.Extensions, Is.EquivalentTo(new string[] { "cs" }));

            return;
        }

        [Test]
        public void LanguageFromExtension()
        {
            Assert.That(_formatter.LanguageFromExtension("cs"), Is.EqualTo("C#"));
            Assert.That(_formatter.LanguageFromExtension(""), Is.EqualTo("Plain text"));
            Assert.That(_formatter.LanguageFromExtension(null), Is.EqualTo("Plain text"));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultFormatter_Can_Throw_FormatterNullException()
        {
            _formatter.DefaultFormatter = null; // throws exception
        }

        [Test]
        public void DefaultFormatter()
        {
            CSharpCodeFormatter csFormatter;

            csFormatter = new CSharpCodeFormatter();
            _formatter.DefaultFormatter = csFormatter;
            Assert.That(_formatter.DefaultFormatter, Is.EqualTo(csFormatter));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFormatterFromExtension_Can_Throw_ExtensionNullException()
        {
            _formatter.GetFormatterFromExtension(null); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFormatterFromLanguage_Can_Throw_LanguageNullException()
        {
            _formatter.GetFormatterFromLanguage(null); // throws exception
        }

        [Test]
        public void GetFormatterFroms()
        {
            // using extension first

            Assert.That(_formatter.GetFormatterFromExtension("cs"), Is.EqualTo(_formatter.Formatters["C#"]));
            Assert.That(_formatter.GetFormatterFromExtension("txt"), Is.EqualTo(_formatter.DefaultFormatter));

            // using language name

            Assert.That(_formatter.GetFormatterFromLanguage("C#"), Is.EqualTo(_formatter.Formatters["C#"]));
            Assert.That(_formatter.GetFormatterFromLanguage("txt"), Is.EqualTo(_formatter.DefaultFormatter));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "code",
            MatchType = MessageMatch.Contains)]
        public void FormatFromExtension_Can_Throw_CodeNullException()
        {
            _formatter.FormatFromExtension(null, "cs"); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "extension",
            MatchType = MessageMatch.Contains)]
        public void FormatFromExtension_Can_Throw_ExtensionNullException()
        {
            _formatter.FormatFromExtension("test", null); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "code",
            MatchType = MessageMatch.Contains)]
        public void Format_Can_Throw_CodeNullException()
        {
            _formatter.Format(null, "C#"); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "language",
            MatchType = MessageMatch.Contains)]
        public void Format_Can_Throw_LanguageNameNullException()
        {
            _formatter.Format("test", null); // throws exception
        }

        public void FormatResource(TestResource res)
        {
            ErrorItem error;
            FormattedCode code;
            List<ICodeFormatter> array = GetAllFormatters();

            foreach (ICodeFormatter item in array)
            {
                error = new ErrorItem(res.Path, 1);
                code = item.Format(error.ReadFile());

                Assert.That(code, Is.Not.Null,
                    "Formatter: " + item + " failed to format resource.");

                try
                {
                    FormattedCode.CheckData(code);
                }
                catch (Exception e)
                {
                    Assert.Fail("Formatter: " + item + " has created an ill-formed data. Error: " + e.Message);
                }
            }

            return;
        }

        /// <summary>
        /// Returns a list containing all formatters in GeneralCodeFormatter
        /// and the one in DefaultFormatter.
        /// </summary>
        /// <returns></returns>
        private List<ICodeFormatter> GetAllFormatters()
        {
            List<ICodeFormatter> array;

            array = new List<ICodeFormatter>();
            foreach (ICodeFormatter item in _formatter.Formatters)
                array.Add(item);
            array.Add(_formatter.DefaultFormatter);

            return (array);
        }

        [Test]
        public void All_Formatters_Have_Unique_Language_Value()
        {
            List<ICodeFormatter> formatters;
            List<string> languages;

            formatters = GetAllFormatters();
            languages = new List<string>();

            foreach (ICodeFormatter item in formatters)
                languages.Add(item.Language);

            Assert.That(languages, Is.All.Not.Null);
            Assert.That(languages, Is.Unique);

            return;
        }

        [Test]
        public void All_Formatters_Should_PreProcess_Tab_Character()
        {
            List<ICodeFormatter> formatters;
            FormattedCode res;

            // We don't have reliable way to measure '\t' at drawing time,
            // hence, each textFormatter should replace '\t' by one or more white spaces.

            formatters = GetAllFormatters();
            foreach (ICodeFormatter formatter in formatters)
            {
                res = formatter.Format("hi\tthere!");

                Assert.That(
                    res.Text.IndexOf("\t") == -1,
                    "Formatter: " + formatter + " failed to remove initial '\\t' characters.");
            }

            return;
        }

        [Test]
        public void All_Formatters_Should_Have_Overwrite_Behavior()
        {
            List<ICodeFormatter> formatters;
            FormattedCode res;

            // check that formatters reset their state before
            // processing new data. So new data overwrite former one.

            formatters = GetAllFormatters();
            foreach (ICodeFormatter formatter in formatters)
            {
                // process this text first
                formatter.Format("line 1\r\nline 2\r\nline 3");

                // after processing "hi", we expect res to contain
                // just one line of text.
                res = formatter.Format("hi");
                Assert.That(
                    res.CopyInfo().LineArray.Count,
                    Is.EqualTo(1),
                    "Formatter: " + formatter + " failed to reset its state.");
            }

            return;
        }

        [Test]
        public void Any_Formatter_Should_Format_Any_Text()
        {
            TestResource res;

            using (res = new TestResource("HelloWorld.txt"))
            {
                FormatResource(res);
            }

            using (res = new TestResource("Basic.cs"))
            {
                FormatResource(res);
            }

            using (res = new TestResource("TextCode.txt"))
            {
                FormatResource(res);
            }


            return;
        }

        [Test]
        public void Format()
        {
            Assert.That(_formatter.Format("test", "C#"), Is.Not.Null);
            Assert.That(_formatter.Format("test", "txt"), Is.Not.Null);

            return;
        }

        [Test]
        public void Format_Pick_Best_Formatter()
        {
            ErrorItem itemHelloTxt;
            ErrorItem itemBasicCs;
            ICodeFormatter txtFormatter;
            ICodeFormatter csFormatter;
            FormattedCode exp;

            using (TestResource resource = new TestResource("HelloWorld.txt"))
            {
                itemHelloTxt = new ErrorItem(resource.Path, 1);
                txtFormatter = new PlainTextCodeFormatter();
                exp = txtFormatter.Format(itemHelloTxt.ReadFile());
                Assert.That(
                    _formatter.FormatFromExtension(itemHelloTxt.ReadFile(), itemHelloTxt.FileExtension),
                    Is.EqualTo(exp));
                FormattedCode.CheckData(exp);
            }

            using (TestResource resource = new TestResource("Basic.cs"))
            {
                itemBasicCs = new ErrorItem(resource.Path, 1);
                csFormatter = new CSharpCodeFormatter();
                exp = csFormatter.Format(itemBasicCs.ReadFile());
                Assert.That(
                    _formatter.FormatFromExtension(itemBasicCs.ReadFile(), itemBasicCs.FileExtension),
                    Is.EqualTo(exp));
                FormattedCode.CheckData(exp);
            }

            return;
        }
    }    
}
