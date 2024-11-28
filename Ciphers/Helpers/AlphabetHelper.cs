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
        private static readonly string _alphaRus = "абвгдежзийклмнопрстуфхцчшщъыьэюя" + "абвгдежзийклмнопрстуфхцчшщъыьэюя".ToUpper() + "0123456789";
        private static readonly string _alhaEng = "abcdefghijklmnopqrstuvwxyz" + "abcdefghijklmnopqrstuvwxyz".ToUpper() + "0123456789";


        public static string GetEnglishAlpha()
        {
            return _alhaEng;
        }

        public static string GetRussianAlpha()
        {
            return _alphaRus;
        }

        public static string GetAlphaForLanguageType(TextLanguage language)
        {
            if (language == TextLanguage.English)
            {
                return GetEnglishAlpha();
            }
            else
            {
                return GetRussianAlpha();
            }
        }
    }
}
