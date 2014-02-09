// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using NUnit.Framework;

namespace NUnit.UiException.Tests
{
    [TestFixture]
    public class TestStackTraceParser
    {
        private StackTraceParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new StackTraceParser();

            return;
        }

        [Test]
        public void Test_Default()
        {
            Assert.That(_parser.Items, Is.Not.Null);
            Assert.That(_parser.Items.Count, Is.EqualTo(0));

            return;
        }

        [Test]
        public void Test_Parse()
        {
            _parser.Parse("à NUnit.UiException.TraceItem.get_Text() dans C:\\TraceItem.cs:ligne 43");

            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0], 
                Is.EqualTo(new ErrorItem("C:\\TraceItem.cs", "NUnit.UiException.TraceItem.get_Text()", 43)));

            // TryParse should clear previous textFormatter

            _parser.Parse("");
            Assert.That(_parser.Items.Count, Is.EqualTo(0));

            return;
        }

        [Test]
        public void Test_Parse_MultipleExtension()
        {
            _parser.Parse("à get_Text() dans C:\\TraceItem.cs.cs.cs.cs:ligne 43");
            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0].Path, Is.EqualTo("C:\\TraceItem.cs.cs.cs.cs"));

            _parser.Parse("à get_Text() dans C:\\my Document1\\my document2 containing space\\file.cs:line 1");
            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0].Path,
                Is.EqualTo("C:\\my Document1\\my document2 containing space\\file.cs"));

            _parser.Parse("à get_Text() dans C:\\my doc\\my doc2.cs\\file.cs:line 1");
            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0].Path,
                Is.EqualTo("C:\\my doc\\my doc2.cs\\file.cs"));

            return;
        }

        [Test]
        public void Test_Parse_With_Real_Life_Samples()
        {
            // test ability to extract one trace

            _parser.Parse(
                "à Test.TestTraceItem.Can_Set_Properties() dans " +
                "C:\\Documents and Settings\\ihottier\\Mes documents\\" +
                "NUnit_Stacktrace\\Test\\TestTraceItem.cs:ligne 42\r\n");

            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0],
                Is.EqualTo(new ErrorItem(
                    "C:\\Documents and Settings\\ihottier\\Mes documents\\" +
                    "NUnit_Stacktrace\\Test\\TestTraceItem.cs",
                    "Test.TestTraceItem.Can_Set_Properties()",
                    42)));

            // test ability to extract two traces

            _parser.Parse(
                "à NUnit.UiException.TraceItem.get_Text() " +
                "dans C:\\Documents and Settings\\ihottier\\Mes documents\\" +
                "NUnit.UiException\\TraceItem.cs:ligne 43\r\n" +
                "à Test.TestTaggedText.SetUp() dans C:\\Documents and Settings\\" +
                "ihottier\\Mes documents\\NUnit_Stacktrace\\Test\\TestTaggedText.cs:ligne 30\r\n");

            Assert.That(_parser.Items.Count, Is.EqualTo(2));

            Assert.That(_parser.Items[0],
                Is.EqualTo(
                new ErrorItem(
                    "C:\\Documents and Settings\\ihottier\\Mes documents\\" +
                    "NUnit.UiException\\TraceItem.cs",
                    "NUnit.UiException.TraceItem.get_Text()",
                    43)));

            Assert.That(_parser.Items[1],
                Is.EqualTo(
                new ErrorItem(
                    "C:\\Documents and Settings\\" +
                    "ihottier\\Mes documents\\NUnit_Stacktrace\\Test\\TestTaggedText.cs",
                    "Test.TestTaggedText.SetUp()",
                    30)));

            return;
        }        

        [Test]
        public void Test_Trace_When_Missing_File()
        {
            //
            // NUnit.UiException.Tests ability to not be confused
            // if source code attachment is missing
            //

            _parser.Parse(
                "à System.String.InternalSubStringWithChecks(Int32 startIndex, Int32 length, Boolean fAlwaysCopy)\r\n" +
                "à NUnit.UiException.StackTraceParser.Parse(String stackTrace) dans C:\\StackTraceParser.cs:ligne 55\r\n" +
                "à Test.TestStackTraceParser.Test_Parse() dans C:\\TestStackTraceParser.cs:ligne 36\r\n"
                );

            Assert.That(_parser.Items.Count, Is.EqualTo(3));

            Assert.That(_parser.Items[0].HasSourceAttachment, Is.False);
            Assert.That(_parser.Items[0].FullyQualifiedMethodName,
                Is.EqualTo("System.String.InternalSubStringWithChecks(Int32 startIndex, Int32 length, Boolean fAlwaysCopy)"));

            Assert.That(_parser.Items[1], 
                Is.EqualTo(
                    new ErrorItem(
                        "C:\\StackTraceParser.cs",
                        "NUnit.UiException.StackTraceParser.Parse(String stackTrace)",
                        55)));
            Assert.That(_parser.Items[2],
                Is.EqualTo(
                    new ErrorItem(
                        "C:\\TestStackTraceParser.cs",
                        "Test.TestStackTraceParser.Test_Parse()",
                        36)));

            return;
        }

        [Test]
        public void Test_Missing_Line_Number()
        {
            //
            // NUnit.UiException.Tests ability to not be confused
            // if line number is missing
            //

            _parser.Parse("à Test.TestStackTraceParser.Test_Parse() dans C:\\TestStackTraceParser.cs:\r\n");

            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0], 
                Is.EqualTo(new ErrorItem(
                    "C:\\TestStackTraceParser.cs", 
                    "Test.TestStackTraceParser.Test_Parse()",
                    0)));

            return;
        }

        [Test]
        public void Test_English_Stack()
        {
            //
            // NUnit.UiException.Tests ability of the parser to not depend
            // of the language
            //

            _parser.Parse("at Test.TestStackTraceParser.Test_Parse() in C:\\TestStackTraceParser.cs:line 36\r\n");

            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0], Is.EqualTo(
                new ErrorItem("C:\\TestStackTraceParser.cs", 
                    "Test.TestStackTraceParser.Test_Parse()", 36)));

            return;
        }

        [Test]
        public void Test_Ability_To_Handle_Different_Path_System_Syntaxes()
        {
            //
            // NUnit.UiException.Tests ability to not depend of one file system
            //

            // here, an hypothetic stack containing UNIX and Windows like filePath values...
           
            _parser.Parse(
                "at Test.TestStackTraceParser.Test_Parse() in /home/ihottier/work/stacktrace/test/TestStackTraceParser.cs:line 36\r\n" +
                "at Test.TestStackTraceParser2.Text_Parse2() in C:\\folder\\file1:line 42"
                );

            Assert.That(_parser.Items.Count, Is.EqualTo(2));
            Assert.That(_parser.Items[0], Is.EqualTo(
                new ErrorItem(
                    "/home/ihottier/work/stacktrace/test/TestStackTraceParser.cs",
                    "Test.TestStackTraceParser.Test_Parse()",
                    36)));
            Assert.That(_parser.Items[1], Is.EqualTo(
                new ErrorItem(
                    "C:\\folder\\file1",
                    "Test.TestStackTraceParser2.Text_Parse2()",
                    42)));

            return;
        }

        [Test]
        public void Test_Ability_To_Handle_Files_With_Unknown_Extension()
        {
            _parser.Parse("à Test.TestStackTraceParser.Test_Parse() in C:\\TestStackTraceParser.vb:line 36");

            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0], Is.EqualTo(
                new ErrorItem(
                    "C:\\TestStackTraceParser.vb",
                    "Test.TestStackTraceParser.Test_Parse()",
                    36)));

            return;
        }

        [Test]
        public void Test_Analysis_Does_Not_Depend_Upon_File_Extension()
        {
            //
            // NUnit.UiException.Tests that Stack Analyzer should not
            // be not aware of file language.
            //

            _parser.Parse("à Test.TestStackTraceParser.Test_Parse() in C:\\TestStackTraceParser.vb:line 36");

            Assert.That(_parser.Items.Count, Is.EqualTo(1));
            Assert.That(_parser.Items[0], Is.EqualTo(
                new ErrorItem(
                    "C:\\TestStackTraceParser.vb",
                    "Test.TestStackTraceParser.Test_Parse()",
                    36)));

            return;
        }
               
        [Test]
        public void Test_Parse_Null()
        {
            _parser.Parse(null);
        }
    }
}
