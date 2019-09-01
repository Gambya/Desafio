using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Desafio.CrossCutting.Helpers
{
    public class CleamMessages
    {
        public static string CleamMessage(string msg)
        {
            string cleam = Regex.Replace(msg, @".+?:\s*", "", RegexOptions.Compiled);

            return cleam;
        }
    }
}
