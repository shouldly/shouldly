// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NUnit.UiException
{    
    /// <summary>
    /// (Formerly named ExceptionItem)
    /// 
    /// This is the output analysis of one error line coming from
    /// a stack trace that still gathers the same data but in more
    /// convenient way to be read from.
    ///   An ErrorItem represents one error with possible location
    /// informations such as:
    ///   - filename where the error has occured
    ///   - file's line number
    ///   - method name
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// An access path to the source file referred by this item.
        /// </summary>
        private string _path;

        /// <summary>
        /// The full qualified name of the member method referred by this item.
        /// </summary>
        private string _fullyQualifiedMethodName;

        /// <summary>
        /// The line index where the exception occured.
        /// </summary>
        private int _line;

        /// <summary>
        /// Store the content of the file pointed by _path.
        /// </summary>
        private string _text;
      
        /// <summary>
        /// Create an instance of ErrorItem that
        /// has source code attachments.
        /// </summary>
        public ErrorItem(string path, int lineNumber)
        {
            UiExceptionHelper.CheckNotNull(path, "path");

            _path = path;
            _line = lineNumber;

            return;
        }

        /// <summary>
        /// Create a new exception item.
        /// </summary>
        /// <param name="path">An absolute path to the source code file.</param>
        /// <param name="fullMethodName">A full qualified name of a member method.</param>
        /// <param name="lineNumber">A line index where the exception occured.</param>
        public ErrorItem(string path, string fullMethodName, int lineNumber)
        {
            _path = path;
            _fullyQualifiedMethodName = fullMethodName;
            _line = lineNumber;

            return;
        }

        /// <summary>
        /// Create an instance of ErrorItem that doesn't have
        /// any source code attachments.
        /// </summary>
        public ErrorItem()
        {
            // nothing to do
        }

        /// <summary>
        /// Reads and returns the part of Path that contains the filename
        /// of the source code file.
        /// </summary>
        public string FileName 
        {
            get { return (System.IO.Path.GetFileName(_path)); }
        }

        /// <summary>
        /// Gets the absolute path to the source code file.
        /// </summary>
        public string Path 
        {
            get { return (_path); }
        }

        /// <summary>
        /// Returns the file language - e.g.: the string after
        /// the last dot or null -
        /// </summary>
        public string FileExtension
        {
            get 
            {
                int dotIndex;

                if (_path == null)
                    return (null);

                dotIndex = _path.LastIndexOf(".", StringComparison.Ordinal);
                if (dotIndex > -1 && dotIndex < _path.Length - 1)
                    return (_path.Substring(dotIndex + 1));

                return (null); 
            }
        }

        /// <summary>
        /// Gets the full qualified name of the member method.
        /// </summary>
        public string FullyQualifiedMethodName
        {
            get { return (_fullyQualifiedMethodName); }
        }

        /// <summary>
        /// Reads and return the method part from the FullyQualifiedMethodName.
        /// The value contains the signature of the method.
        /// </summary>
        public string MethodName
        {
            get
            {
                int index;

                if (FullyQualifiedMethodName == null)
                    return ("");

                if ((index = FullyQualifiedMethodName.LastIndexOf(".", 
                            StringComparison.Ordinal)) == -1)
                    return (FullyQualifiedMethodName);

                return (FullyQualifiedMethodName.Substring(index + 1));
            }
        }

        /// <summary>
        /// Gets the method name without the argument list.
        /// </summary>
        public string BaseMethodName
        {
            get
            {
                string method = MethodName;
                int index = method.IndexOf("(", StringComparison.Ordinal);

                if (index > 0)
                    return (method.Substring(0, index));

                return (method);
            }
        }

        /// <summary>
        /// Reads and returns the class part from the FullyQualifiedMethodName.
        /// </summary>
        public string ClassName
        {
            get
            {
                int end_index;
                int start_index;

                if (FullyQualifiedMethodName == null)
                    return ("");

                if ((end_index = FullyQualifiedMethodName.LastIndexOf(".", 
                                StringComparison.Ordinal)) == -1)
                    return ("");

                start_index = end_index - 1;
                while (start_index > 0 && FullyQualifiedMethodName[start_index] != '.')
                    start_index--;

                if (start_index >= 0 && FullyQualifiedMethodName[start_index] == '.')
                    start_index++;

                return (FullyQualifiedMethodName.Substring(start_index, end_index - start_index));
            }
        }

        /// <summary>
        /// Gets the line number where the exception occured.
        /// </summary>
        public int LineNumber 
        {
            get { return (_line); }
        }

        /// <summary>
        /// Gets a boolean that says whether this item has source
        /// code localization attachments.
        /// </summary>
        public bool HasSourceAttachment {
            get { return (_path != null); }
        }

        /// <summary>
        /// Read and return the content of the underlying file. If the file
        /// cannot be found or read an exception is raised.
        /// </summary>
        public string ReadFile()
        {
            if (!System.IO.File.Exists(_path))
                throw new FileNotFoundException("File does not exist. File: " + _path);

            if (_text == null)
            {
                StreamReader rder = new StreamReader(_path);
                _text = rder.ReadToEnd();
                rder.Close();
            }

            return (_text);
        }

        public override string ToString() {
            return ("TraceItem: {'" + _path + "', " + _fullyQualifiedMethodName + ", " + _line + "}");
        }

        public override bool Equals(object obj)
        {
            ErrorItem item = obj as ErrorItem;

            if (item == null)
                return (false);

            return (_path == item._path &&
                    _fullyQualifiedMethodName == item._fullyQualifiedMethodName &&
                    _line == item._line);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
