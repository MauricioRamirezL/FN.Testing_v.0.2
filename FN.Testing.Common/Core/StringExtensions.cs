using System.Collections.Generic;
using System.Linq;

namespace FN.Testing.Common.Core
{
    internal static class StringExtensions
    {
        internal static string ToCamelCase(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            if (!char.IsUpper(name[0]))
                return name;

            var chars = name.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                if ((0 < i) && HasNotUpperCharacter(i + 1, chars))
                    break;
                chars[i] = char.ToLowerInvariant(chars[i]);
            }
            return new string(chars);
        }
        public static string ToDottedCamelCase(this string path)
        {
            return string.IsNullOrEmpty(path) ? path : string.Join(".", path.Split('.').Select(ToCamelCase));
        }
        private static bool HasNotUpperCharacter(int i, IReadOnlyList<char> chars) => (i < chars.Count) && !char.IsUpper(chars[i]);
    }
}
