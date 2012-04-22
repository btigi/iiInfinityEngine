using System.IO;
using System;

namespace iiInfinityEngine.Core
{
    /// <summary>
    /// Loads strings from an external reference file.
    /// </summary>
    public static class Translator
    {
        const char separator = '~';

        /// <summary>
        /// Retrieve a string from the specified translation file/
        /// </summary>
        /// <param name="file">The full path and filename of the translation file.</param>
        /// <param name="lookup">The unique identifier of the required string, e.g. @1</param>
        /// <returns>The string or an empty string if the unique identifier or translation file could not be found</returns>
        /// <remarks>The string must be encased in ~ characters. Other markers are not supported.</remarks>
        public static string Text(string file, string lookup)
        {
            string line = String.Empty;
            if (File.Exists(file))
            {
                var text = File.ReadAllText(file);
                var startSection = text.IndexOf(lookup);
                if (startSection > -1)
                {
                    var startActual = text.IndexOf(separator, startSection);
                    if (startActual > -1)
                    {
                        var endActual = text.IndexOf(separator, startActual + 1);
                        if (endActual > startActual)
                        {
                            line = text.Substring(startActual + 1, endActual - startActual - 1);
                        }
                    }
                }
            }
            return line;
        }
    }
}