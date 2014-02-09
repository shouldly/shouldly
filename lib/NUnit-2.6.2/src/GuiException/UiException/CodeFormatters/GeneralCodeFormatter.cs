// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    ///   GeneralCodeFormatter is the front class responsible for making a basic
    /// syntax coloring of a string of text for a set of specific languages.
    ///  The class encapsulates a set of algorithms where each of them addresses
    /// a specific language formatting. The use of one or another algorithm at
    /// analysing time is made through an language registering mechanism.
    /// For instance C# files are covered by CSharpCodeFormatter which has
    /// been assigned to "cs" language.
    ///  If a query is made to GeneralCodeFormatter while there is no formatter
    /// that fit the given file language a default formatting is applyied
    /// through the use of the formatter registered into the property
    /// DefaultFormatter.
    /// </summary>
    public class GeneralCodeFormatter :
        IFormatterCatalog
    {
        /// <summary>
        /// The set of formatting algorithms.
        /// </summary>
        private CodeFormatterCollection _formatters;

        /// <summary>
        /// The default formatter to be used as last resort.
        /// </summary>
        private ICodeFormatter _default;

        /// <summary>
        /// Build and initialises GeneralCodeFormatter.
        /// </summary>
        public GeneralCodeFormatter()
        {
            _formatters = new CodeFormatterCollection();
            _formatters.Register(new CSharpCodeFormatter(), "cs");

            _default = new PlainTextCodeFormatter();

            return;
        }

        /// <summary>
        /// Gets or sets the formatter to be used as last resort when
        /// no formatter fit the given source language.
        ///   The value cannot be null.
        /// </summary>
        public ICodeFormatter DefaultFormatter
        {
            get { return (_default); }
            set 
            {
                UiExceptionHelper.CheckNotNull(value, "value");
                _default = value;
            }
        }

        /// <summary>
        /// Gives access to the underlying formatter collection.
        /// </summary>
        public CodeFormatterCollection Formatters
        {
            get { return (_formatters); }
        }
        
        /// <summary>
        /// Gets the best formatter that fits the given language. If there
        /// is no such formatter, a default one is returned.
        /// </summary>
        /// <param name="language">
        /// The language name. Ex: "C#", "Java. This parameter cannot be null.
        /// </param>
        /// <returns>
        /// A non-null ICodeFormatter that best fits the request.
        /// </returns>
        public ICodeFormatter GetFormatterFromLanguage(string languageName)
        {
            UiExceptionHelper.CheckNotNull(languageName, "language");

            if (_formatters.HasLanguage(languageName))
                return (_formatters[languageName]);

            return (DefaultFormatter);
        }

        /// <summary>
        /// Gets the formatter assigned to the given extension. If there
        /// is no such assignment, the default formatter is returned.
        /// </summary>
        /// <param name="extension">
        /// A file extension. Ex: "cs", "txt". This parameter cannot be null.
        /// </param>
        /// <returns>A non-null ICodeFormatter.</returns>
        public ICodeFormatter GetFormatterFromExtension(string extension)
        {
            UiExceptionHelper.CheckNotNull(extension, "extension");

            if (_formatters.HasExtension(extension))
                return (_formatters.GetFromExtension(extension));

            return (DefaultFormatter);
        }

        /// <summary>
        /// A convenient method to make the formatting of a piece of code when
        /// only the file extension is known. 
        /// </summary>
        /// <param name="code">The piece of code to be formatted. This parameter
        /// cannot be null.</param>
        /// <param name="extension">The file extension associated to this piece of code.
        /// Ex: "cs", "cpp". This is used to pick the formatter assigned to. If no such
        /// formatter exists, the default one is picked up.</param>
        /// <returns>The FormattedCode for this piece of code.</returns>
        public FormattedCode FormatFromExtension(string code, string extension)
        {
            UiExceptionHelper.CheckNotNull(code, "code");
            UiExceptionHelper.CheckNotNull(extension, "extension");

            if (_formatters.HasExtension(extension))
                return (_formatters.GetFromExtension(extension).Format(code));

            return (DefaultFormatter.Format(code));
        }

        #region IFormatterCatalog Membres

        /// <summary>
        /// Pick the best available formatter to format the given piece of code.
        /// </summary>
        /// <param name="code">The code to be formatted. This parameter cannot be null.</param>
        /// <param name="language">
        /// The language into which code has been written. Ex: "C#", "Java".
        /// If no such formatter is available, a default formatting is applied.
        /// This parameter cannot be null.
        /// </param>
        /// <returns>
        /// The formatting for this piece of code.
        /// </returns>
        public FormattedCode Format(string code, string language)
        {
            UiExceptionHelper.CheckNotNull(code, "code");
            UiExceptionHelper.CheckNotNull(language, "language");

            if (_formatters.HasLanguage(language))
                return (_formatters[language].Format(code));

            return (DefaultFormatter.Format(code));
        }

        public string LanguageFromExtension(string extension)
        {
            if (_formatters.HasExtension(extension))
                return (_formatters.GetFromExtension(extension).Language);
            return (_default.Language);
        }

        #endregion
    }
}
