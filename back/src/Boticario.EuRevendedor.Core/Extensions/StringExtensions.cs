using System.Text.RegularExpressions;

namespace Boticario.EuRevendedor.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Required(this string field)
        {
            return string.Format("O campo {0} é obrigatório", field);
        }

        public static string NumberOnly(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return new Regex("\\D").Replace(value, "");
        }
    }
}
