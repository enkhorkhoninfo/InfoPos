using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
namespace InfoPos.Messages
{
    public class MSG
    {
        public static string Messages(Hashtable lang, int MsgID)
        {
            string value = "";
            if (lang.ContainsKey(MsgID))
            {
                value = Convert.ToString(lang[MsgID]);
            }
            return value;
        }
        public static string Words(Hashtable wordlang, int WordID)
        {
            string value = "";
            if (wordlang.ContainsKey(WordID))
            {
                value = Convert.ToString(wordlang[WordID]);
            }
            return value;
        }
    }
}
