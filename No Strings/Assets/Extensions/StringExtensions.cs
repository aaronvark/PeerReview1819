using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Extensions
{
    public static class StringExtensions
    {
        /// <summary>Capitalizes the first character of a string.</summary>
        public static string Capitalize(this string str)
        {
            if(string.IsNullOrWhiteSpace(str))
                return str;

            return Char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>Converts the first character of a string to lowercase.</summary>
        public static string DeCapitalize(this string str)
        {
            if(string.IsNullOrWhiteSpace(str))
                return str;

            return Char.ToLower(str[0]) + str.Substring(1);
        }

        #region ToIdentifier Methods
        /// <summary>Creates a valid identifier out of the passed string.</summary>
        public static string ToIdentifier(this string str) => str.ToIdentifier("");

        /// <summary>Creates a valid identifier out of the passed string.</summary>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        public static string ToIdentifier(this string str, Type enclosingType) => str.ToIdentifier(enclosingType.Name);

        /// <summary>Creates a valid identifier out of the passed string.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static string ToIdentifier(this string str, string enclosingType) => str.ToIdentifier(enclosingType, IdentifierOptions.None);

        /// <summary>Creates a valid identifier out of the passed string.</summary>
        /// <param name="options">Additional options for identifier conversion.</param>
        public static string ToIdentifier(this string str, IdentifierOptions options) => str.ToIdentifier("", options);

        /// <summary>Creates a valid identifier out of the passed string.</summary>
        /// <param name="options">Additional options for identifier conversion.</param>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        public static string ToIdentifier(this string str, Type enclosingType, IdentifierOptions options) => str.ToIdentifier(enclosingType.Name, options);

        /// <summary>Creates a valid identifier out of the passed string.</summary>
        /// <param name="options">Additional options for identifier conversion.</param>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static string ToIdentifier(this string str, string enclosingType, IdentifierOptions options) => str.ToIdentifierBase(options).ToIdentifierCatchEdgeCases(enclosingType);
        #endregion

        #region ToIdentifiers Methods
        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> it) => it.ToIdentifiers(false);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, Type enclosingType) => enumerable.ToIdentifiers(enclosingType.Name);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, string enclosingType) => enumerable.ToIdentifiers(enclosingType, false);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="options">Additional options for identifier conversion.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, IdentifierOptions options) => enumerable.ToIdentifiers("", options);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, bool allowDuplicates) => enumerable.ToIdentifiers("", allowDuplicates);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        /// <param name="options">Additional options for identifier conversion.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, Type enclosingType, IdentifierOptions options) => enumerable.ToIdentifiers(enclosingType.Name, options);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        /// <param name="options">Additional options for identifier conversion.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, string enclosingType, IdentifierOptions options) => enumerable.ToIdentifiers(enclosingType, options, false);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, Type enclosingType, bool allowDuplicates) => enumerable.ToIdentifiers(enclosingType.Name, allowDuplicates);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, string enclosingType, bool allowDuplicates) => enumerable.ToIdentifiers(enclosingType, IdentifierOptions.None, allowDuplicates);

        /// <summary>Creates valid identifiers out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        /// <param name="options">Additional options for identifier conversion.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToIdentifiers(this IEnumerable<string> enumerable, string enclosingType, IdentifierOptions options, bool allowDuplicates)
        {
            enumerable = enumerable.Select(it => it.ToIdentifier(enclosingType, options));
            return enumerable.RemoveDuplicateIdentifiers(allowDuplicates);
        }
        #endregion

        #region ToCase Methods
        /// <summary>Capitalizes each word and removes whitespace.</summary>
        public static string ToPascalCase(this string str) => str.ToPascalCase("");

        /// <summary>Capitalizes each word and removes whitespace.</summary>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        public static string ToPascalCase(this string str, Type enclosingType) => str.ToPascalCase(enclosingType.Name);

        /// <summary>Capitalizes each word and removes whitespace.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static string ToPascalCase(this string str, string enclosingType)
        {
            return str.ToCase(enclosingType, s => (s.Length <= 2 || Char.IsUpper(s[2])) ?
                s.Substring(0, 2).ToUpper() + s.Substring(2) :
                s.Capitalize()
            );
        }

        /// <summary>Capitalizes each word, decapitalizes the first letter or two-letter acronym, and removes whitespace and underscores.</summary>
        public static string ToCamelCase(this string str) => str.ToCamelCase("");

        /// <summary>Capitalizes each word, decapitalizes the first letter or two-letter acronym, and removes whitespace and underscores.</summary>
        /// <param name="enclosingType">The type of the enclosing type.</param>
        public static string ToCamelCase(this string str, Type enclosingType) => str.ToCamelCase(enclosingType.Name);

        /// <summary>Capitalizes each word, decapitalizes the first letter or two-letter acronym, and removes whitespace and underscores.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static string ToCamelCase(this string str, string enclosingType) => str.ToCase(enclosingType, s => s.DeCapitalize());

        private static string ToCase(this string str, string enclosingType, Func<string, string> func)
        {
            Regex regex = new Regex(@"(\p{Ll})(\p{Lu}+)");
            str = regex.Replace(str, "$1 $2");

            str = str.ToLower().ToIdentifierBase(IdentifierOptions.All);
            str = func(str);

            return str.ToIdentifierCatchEdgeCases(enclosingType);
        }
        #endregion

        #region ToCases Methods
        /// <summary>Creates valid Pascal Cases out of the passed enumerator of strings.</summary>
        public static IEnumerable<string> ToPascalCase(this IEnumerable<string> enumerable) => enumerable.ToPascalCase("");

        /// <summary>Creates valid Pascal Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The Type of the enclosing type.</param>
        public static IEnumerable<string> ToPascalCase(this IEnumerable<string> enumerable, Type enclosingType) => enumerable.ToPascalCase(enclosingType.Name);

        /// <summary>Creates valid Pascal Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static IEnumerable<string> ToPascalCase(this IEnumerable<string> enumerable, string enclosingType) => enumerable.ToPascalCase(enclosingType, false);

        /// <summary>Creates valid Pascal Cases out of the passed enumerator of strings.</summary>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToPascalCase(this IEnumerable<string> enumerable, bool allowDuplicates) => enumerable.ToPascalCase("", allowDuplicates);

        /// <summary>Creates valid Pascal Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The Type of the enclosing type.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToPascalCase(this IEnumerable<string> enumerable, Type enclosingType, bool allowDuplicates) => enumerable.ToPascalCase(enclosingType.Name, allowDuplicates);

        /// <summary>Creates valid Pascal Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToPascalCase(this IEnumerable<string> enumerable, string enclosingType, bool allowDuplicates)
        {
            enumerable = enumerable.Select(it => it.ToPascalCase(enclosingType));
            return enumerable.RemoveDuplicateIdentifiers(allowDuplicates);
        }

        /// <summary>Creates valid Camel Cases out of the passed enumerator of strings.</summary>
        public static IEnumerable<string> ToCamelCases(this IEnumerable<string> enumerable) => enumerable.ToCamelCases("");

        /// <summary>Creates valid Camel Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The Type of the enclosing type.</param>
        public static IEnumerable<string> ToCamelCases(this IEnumerable<string> enumerable, Type enclosingType) => enumerable.ToCamelCases(enclosingType.Name);

        /// <summary>Creates valid Camel Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        public static IEnumerable<string> ToCamelCases(this IEnumerable<string> enumerable, string enclosingType) => enumerable.ToCamelCases(enclosingType, false);

        /// <summary>Creates valid Camel Cases out of the passed enumerator of strings.</summary>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToCamelCases(this IEnumerable<string> enumerable, bool allowDuplicates) => enumerable.ToCamelCases("", allowDuplicates);

        /// <summary>Creates valid Camel Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The Type of the enclosing type.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToCamelCases(this IEnumerable<string> enumerable, Type enclosingType, bool allowDuplicates) => enumerable.ToCamelCases(enclosingType.Name, allowDuplicates);

        /// <summary>Creates valid Camel Cases out of the passed enumerator of strings.</summary>
        /// <param name="enclosingType">The name of the enclosing type.</param>
        /// <param name="allowDuplicates">Should duplicate identifiers be allowed.</param>
        public static IEnumerable<string> ToCamelCases(this IEnumerable<string> enumerable, string enclosingType, bool allowDuplicates)
        {
            enumerable = enumerable.Select(it => it.ToCamelCase(enclosingType));
            return enumerable.RemoveDuplicateIdentifiers(allowDuplicates);
        }
        #endregion

        #region ToIdentifier Base Methods
        private static string ToIdentifierBase(this string str, IdentifierOptions options)
        {
            if(string.IsNullOrWhiteSpace(str))
                return string.Empty;

            List<KeyValuePair<Regex, MatchEvaluator>> regexPairs = new List<KeyValuePair<Regex, MatchEvaluator>>();

            if(options.HasFlag(IdentifierOptions.WhitespaceAsSpace))
            {
                regexPairs.Add(new KeyValuePair<Regex, MatchEvaluator>
                (
                    new Regex(@"\s"),
                    new MatchEvaluator(match => " ")
                ));
            }

            if(options.HasFlag(IdentifierOptions.RemoveUnderscores))
            {
                regexPairs.Add(new KeyValuePair<Regex, MatchEvaluator>
                (
                    new Regex(@"_"),
                    new MatchEvaluator(match => " ")
                ));
            }

            if(!options.HasFlag(IdentifierOptions.CapitalizeWords))
            {
                regexPairs.Add(new KeyValuePair<Regex, MatchEvaluator>
                (
                    new Regex(@"\A[\W\d]*|\W"),
                    new MatchEvaluator(match => string.Empty)
                ));
            }
            else
            {
                regexPairs.Add(new KeyValuePair<Regex, MatchEvaluator>
                (
                    new Regex(@"\A[\W\d]*|[\W-[ ]]"),
                    new MatchEvaluator(match => string.Empty)
                ));

                regexPairs.Add(new KeyValuePair<Regex, MatchEvaluator>
                (
                    new Regex(@"(\A|[ _]+)(\w*)"),
                    (options.HasFlag(IdentifierOptions.TwoLettersAreAcronyms)) ?
                        new MatchEvaluator(match =>
                        {
                            string value = match.Groups[2].Value;
                            return (value.Length < 3) ?
                                value :
                                value.Capitalize();
                        }) :
                        new MatchEvaluator(match => match.Groups[2].Value.Capitalize())
                ));
            }

            if(options.HasFlag(IdentifierOptions.UppercaseSingleLetters))
            {
                // Any lowercase between 2 uppercases, or between an uppercase and the start or the end of the string
                regexPairs.Add(new KeyValuePair<Regex, MatchEvaluator>(
                    new Regex(@"(?<=\A|\p{Lu})\p{Ll}(?=\p{Lu}|\z)"),
                    new MatchEvaluator(match => match.Value.ToUpper())
                ));
            }

            foreach(var regexPair in regexPairs)
                str = regexPair.Key.Replace(str, regexPair.Value);

            return str;
        }

        private static IEnumerable<string> RemoveDuplicateIdentifiers(this IEnumerable<string> enumerable, bool allowDuplicates)
        {
            if(allowDuplicates)
                return enumerable;

            List<string> uniqueIdentifiers = new List<string>();
            using(var enumerator = enumerable.GetEnumerator())
            {
                while(enumerator.MoveNext())
                {
                    int? index = null;

                    while(uniqueIdentifiers.Contains(enumerator.Current + index))
                        index = (index != null) ? index + 1 : 0;

                    uniqueIdentifiers.Add(enumerator.Current + index);
                }
            }

            return uniqueIdentifiers.AsEnumerable();
        }

        private static string ToIdentifierCatchEdgeCases(this string str, string enclosingType)
        {
            const string emptyIdentifierReplacement = "_";

            if(str == string.Empty)
                str = emptyIdentifierReplacement;

            if(str == enclosingType)
                str = emptyIdentifierReplacement + str;

            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            str = provider.CreateEscapedIdentifier(str);
            provider.Dispose();

            return str;
        }
        #endregion

        /// <summary>Replaces the strings line endings with the system environments line endings.</summary>
        public static string FixLineEndings(this string str)
        {
            if(string.IsNullOrEmpty(str))
                return str;

            // CRLF line ending, CR line ending or LF line ending
            Regex regex1 = new Regex(@"\r\n?|\n");
            return regex1.Replace(str, Environment.NewLine);
        }
    }
}
