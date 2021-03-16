using System.Text.RegularExpressions;

namespace FN.Testing.Common.Core
{
    public static class RegularExpression
    {
        public static readonly Regex OnlyDigits = new Regex(@"^\d+$");
    }
}
