using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiView.Code
{
    public class StringHelper
    {
        public static string myReplace(string strSource, string strRe, string strTo)
        {
            string strSl, strRl;
            strSl = strSource.ToLower();
            strRl = strRe.ToLower();
            int start = strSl.IndexOf(strRl);
            if (start != -1)
            {
                strSource = strSource.Substring(0, start) + strTo
                + myReplace(strSource.Substring(start + strRe.Length), strRe, strTo);
            }
            return strSource;
        }

        public static string HighlightKeyword(string str, string keyword)
        {
            var ks = keyword.Trim().Split(' ');
            foreach (var k in ks)
            {
                int index;
                int startIndex = 0;
                string highlightBegin = "<em>";
                string highlightEnd = "</em>";
                int length = highlightBegin.Length + k.Length;
                int lengthHighlight = length + highlightEnd.Length;

                while ((index = str.IndexOf(k, startIndex, StringComparison.OrdinalIgnoreCase)) > -1)
                {
                    str = str.Insert(index, highlightBegin).Insert(index + length, highlightEnd);
                    startIndex = index + lengthHighlight;
                }
            }

            return str;
        }
    }
}