using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Helper
{
    public static class Cripto
    {
        public static string GerarHash(this string value)
        {
            var hash = SHA1.Create();
            var encode = new ASCIIEncoding();
            var array =  encode.GetBytes(value);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }
            return strHexa.ToString();
        }
    }
}
