// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    /// Makes the link between a file language and an ICodeFormatter.
    /// This class is used to know which formatter need to be call
    /// when displaying an ErrorItem.
    /// </summary>
    public class CodeFormatterCollection :
        IEnumerable
    {
        /// <summary>
        /// Maps language files to ICodeFormatters.
        /// </summary>
        private Dictionary<string, ICodeFormatter> _toFormatter;

        /// <summary>
        /// Builds an empty CodeFormatterCollection.
        /// </summary>
        public CodeFormatterCollection()
        {
            _toFormatter = new Dictionary<string, ICodeFormatter>();

            return;
        }

        /// <summary>
        /// Gets the size of the collection.
        /// </summary>
        public int Count
        {
            get { return (_toFormatter.Count); }
        }

        /// <summary>
        /// Returns the ICodeFormatter that fit the given language.
        /// </summary>
        /// <param name="language">
        /// A language name, such as: "C#" or "Java".
        /// This parameter cannot be null.
        /// </param>
        /// <returns>
        /// The ICodeFormatter that fit this language.
        /// </returns>
        /// <see cref="HasExtension"/>
        public ICodeFormatter this[string language]
        {
            get
            {
                UiExceptionHelper.CheckNotNull(language, "language");

                foreach (ICodeFormatter item in _toFormatter.Values)
                    if (item.Language == language)
                        return (item);

                throw new ArgumentException("unknown language: '" + language + "'");
            }
        }
        
        /// <summary>
        /// Checks whether there is a formatter that has been
        /// assigned to the given file extension.
        /// </summary>
        /// <param name="language">A file extension such as: "cs".</param>
        /// <returns>True if there is such formatter, false otherwise.</returns>
        public bool HasExtension(string extension)
        {
            if (extension == null)
                return (false);

            extension = extension.ToLower();

            return (_toFormatter.ContainsKey(extension));
        }

        /// <summary>
        /// Tests whether the collection contains a formatter for the given language.
        /// </summary>
        /// <param name="language">
        /// A language name. Ex: "C#", "Java"</param>
        /// <returns>True if such formatter exists.</returns>
        public bool HasLanguage(string languageName)
        {
            if (languageName == null)
                return (false);

            foreach (ICodeFormatter item in _toFormatter.Values)
                if (item.Language == languageName)
                    return (true);

            return (false);
        }

        /// <summary>
        /// Gets the ICodeFormatter that has been assigned to this extension.
        /// </summary>
        /// <param name="extension">The file extension. This parameter
        /// cannot be null.</param>
        /// <returns>The ICodeFormatter assigned to.</returns>
        public ICodeFormatter GetFromExtension(string extension)
        {
            UiExceptionHelper.CheckNotNull(extension, "extension");
            extension = extension.ToLower();
            return (_toFormatter[extension]);
        }
       
        /// <summary>
        /// Registers an ICodeFormatter for the given language. The system
        /// is not case sensitive.
        /// </summary>
        /// <param name="formatter">
        /// A non null formatter.
        /// </param>
        /// <param name="language">
        /// A non null file language.
        /// The value must not be empty nor contain '.' and
        /// must not have been already registered.
        /// </param>
        public void Register(ICodeFormatter formatter, string extension)
        {
            UiExceptionHelper.CheckNotNull(formatter, "formatter");
            UiExceptionHelper.CheckNotNull(extension, "language");

            extension = extension.ToLower();

            UiExceptionHelper.CheckTrue(extension.Length > 0,
                "language cannot be empty", "language");
            UiExceptionHelper.CheckTrue(extension.LastIndexOf('.') == -1,
                "language cannot contain '.'", "language");
            UiExceptionHelper.CheckFalse(_toFormatter.ContainsKey(extension),
                "language '" + extension + "' has already an handler. Remove handler first.",
                "language");

            _toFormatter.Add(extension, formatter);

            return;
        }

        /// <summary>
        /// Removes the formatter for the given file language.
        /// The language is not case sensitive.
        /// </summary>
        /// <param name="language">A file language.</param>
        public void Remove(string extension)
        {
            if (extension == null)
                return;

            extension = extension.ToLower();

            _toFormatter.Remove(extension);

            return;
        }

        /// <summary>
        /// Removes all formatters.
        /// </summary>
        public void Clear()
        {
            _toFormatter.Clear();
        }

        /// <summary>
        /// Returns a string collection with all registered extensions.
        /// </summary>
        public StringCollection Extensions
        {
            get
            {
                StringCollection res;

                res = new StringCollection();
                foreach (string extension in _toFormatter.Keys)
                    res.Add(extension);

                return (res);
            }
        }

        #region IEnumerable Membres

        /// <summary>
        /// Returns an IEnumerator on all registered ICodeFormatter.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return (_toFormatter.Values.GetEnumerator());
        }

        #endregion
    }
}
