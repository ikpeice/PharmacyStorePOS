using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyStore.Models
{
    internal class Helper
    {
        public Helper()
        {

        }
        public bool search(string text, string in_text)
        {
            string up_in = in_text.ToUpper();
            string low_in = in_text.ToLower();
            int len = in_text.Length;
            int len2 = text.Length;
            int x = 0;
            for (int i = 0; i < len; i++)
            {
                if (up_in[i] == text[x] || low_in[x] == text[x])
                {
                    if (x + 1 == len2) return true;
                    x += 1;
                }
                else { x = 0; }
            }
            return false;
        }
    }

    
}
