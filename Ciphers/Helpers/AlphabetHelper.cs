using Ciphers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Helpers
{
    public static class AlphabetHelper
    {
        private static readonly string _alphaRus = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        private static readonly string _alhaEng = "abcdefghijklmnopqrstuvwxyz";


        public static string GetEnglishLowerAlpha()
        {
            return _alhaEng;
        }

        public static string GetRussianLowerAlpha()
        {
            return _alphaRus;
        }

        public static string GetLowerAlphaForLanguageType(TextLanguage language)
        {
            if (language == TextLanguage.English)
            {
                return GetEnglishLowerAlpha();
            }
            else
            {
                return GetRussianLowerAlpha();
            }
        }
    }
}
