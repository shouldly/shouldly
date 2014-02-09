// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.UiException.StackTraceAnalyzers
{
    public class RawError
    {
        private string _function;
        private string _path;
        private int _line;
        private string _input;

        public RawError(string input)
        {
            UiExceptionHelper.CheckNotNull(input, "input");
            _input = input;

            return;
        }

        public string Input
        {
            get { return (_input); }
        }

        public string Function
        {
            get { return (_function); }
            set { _function = value; }
        }

        public string Path
        {
            get { return (_path); }
            set { _path = value; }
        }

        public int Line
        {
            get { return (_line); }
            set { _line = value; }
        }

        public ErrorItem ToErrorItem()
        {
            UiExceptionHelper.CheckTrue(
                _function != null,
                "Cannot create instance of ErrorItem without a valid value in Function",
                "Function");

            return (new ErrorItem(_path, _function, _line));
        }
    }

    public interface IErrorParser
    {
        bool TryParse(StackTraceParser parser, RawError args);
    }
}
