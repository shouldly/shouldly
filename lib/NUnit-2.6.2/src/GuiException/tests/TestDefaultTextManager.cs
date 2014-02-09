// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit.UiException.Tests
{
    [TestFixture]
    public class TestDefaultTextManager
    {
        private DefaultTextManager _textBlocks;

        [SetUp]
        public void SetUp()
        {
            _textBlocks = new DefaultTextManager();            
        }       

        [Test]
        public void Test_Default()
        {
            Assert.That(_textBlocks.Text, Is.EqualTo(""));
            Assert.That(_textBlocks.LineCount, Is.EqualTo(0));
            Assert.That(_textBlocks.MaxLength, Is.EqualTo(0));

            return;
        }

        [Test]
        public void Test_CodeBlockCollection()
        {
            List<string> lst;

            Assert.That(_textBlocks.LineCount, Is.EqualTo(0));

            _textBlocks.Text = "01\r\n02\r\n03\r\n";

            Assert.That(_textBlocks.Text, Is.EqualTo("01\r\n02\r\n03\r\n"));
            Assert.That(_textBlocks.LineCount, Is.EqualTo(3));
            Assert.That(_textBlocks.GetTextAt(0), Is.EqualTo("01"));
            Assert.That(_textBlocks.GetTextAt(1), Is.EqualTo("02"));
            Assert.That(_textBlocks.GetTextAt(2), Is.EqualTo("03"));

            _textBlocks.Text = "01";
            Assert.That(_textBlocks.LineCount, Is.EqualTo(1));
            Assert.That(_textBlocks.GetTextAt(0), Is.EqualTo("01"));

            _textBlocks.Text = "01\r\n02";
            Assert.That(_textBlocks.LineCount, Is.EqualTo(2));
            Assert.That(_textBlocks.GetTextAt(0), Is.EqualTo("01"));
            Assert.That(_textBlocks.GetTextAt(1), Is.EqualTo("02"));

            lst = new List<string>();
            foreach (string line in _textBlocks)
                lst.Add(line);
            Assert.That(lst.Count, Is.EqualTo(2));
            Assert.That(lst[0], Is.EqualTo(_textBlocks.GetTextAt(0)));
            Assert.That(lst[1], Is.EqualTo(_textBlocks.GetTextAt(1)));

            _textBlocks.Text = null;
            Assert.That(_textBlocks.Text, Is.EqualTo(""));            

            return;
        }

        [Test]
        public void Test_MaxLength()
        {
            _textBlocks.Text = null;
            Assert.That(_textBlocks.MaxLength, Is.EqualTo(0));

            _textBlocks.Text = "a\r\nabc\r\nab";
            Assert.That(_textBlocks.MaxLength, Is.EqualTo(3));

            _textBlocks.Text = "a\r\nab\r\nabc";
            Assert.That(_textBlocks.MaxLength, Is.EqualTo(3));

            return;
        }
    }
}
