using Ciphers.Enums;
using Ciphers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Services
{
    public static class BinaryConverterService
    {
        public static bool[] StringToBinary(string text, TextLanguage language)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Текст не может быть пустым или null.");

            string alphabet = AlphabetHelper.GetLowerAlphaForLanguageType(language);
            int bitLength = Convert.ToString(alphabet.Length, 2).Length;
            List<bool> bitList = new List<bool>();

            foreach (char c in text.ToLower())
            {
                int index = alphabet.IndexOf(c) + 1;

                string binary = Convert.ToString(index, 2).PadLeft(bitLength, '0');
                foreach (char bit in binary)
                {
                    bitList.Add(bit == '1');
                }
            }

            return bitList.ToArray();
        }

        public static string BinaryToString(bool[] bitArray, TextLanguage language)
        {
            if (bitArray == null || bitArray.Length == 0)
                throw new ArgumentException("Массив битов не может быть пустым или null.");

            string alphabet = AlphabetHelper.GetLowerAlphaForLanguageType(language);
            int bitLength = Convert.ToString(alphabet.Length, 2).Length;

            if (bitArray.Length % bitLength != 0)
                throw new ArgumentException($"Длина массива битов должна быть кратна {bitLength}.");

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < bitArray.Length; i += bitLength)
            {
                int index = 0;
                for (int j = 0; j < bitLength; j++)
                {
                    if (bitArray[i + j])
                        index |= 1 << (bitLength - j - 1);
                }

                result.Append(alphabet[index - 1]);
            }

            return result.ToString();
        }
    }
}
