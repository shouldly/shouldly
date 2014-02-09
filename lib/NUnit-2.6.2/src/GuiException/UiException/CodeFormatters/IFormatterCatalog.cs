// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    /// The interface through which SourceCodeDisplay interacts to guess
    /// the language from a file extension.
    /// 
    /// Direct implementation is:
    ///     - GeneralCodeFormatter
    /// </summary>
    public interface IFormatterCatalog
    {
        /// <summary>
        /// Format the text using the given language formatting.
        /// </summary>
        /// <param name="text">A text to be formatted</param>
        /// <param name="language">The language with which formatting the text</param>
        /// <returns>A FormatterCode object</returns>
        FormattedCode Format(string text, string language);

        /// <summary>
        /// Gets the language from the given extension.
        /// </summary>
        /// <param name="extension">An extension without the dot, like 'cs'</param>
        /// <returns>A language name, like 'C#'</returns>
        string LanguageFromExtension(string extension);
    }
}
