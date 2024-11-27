using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Services
{
    public static class GammaCipherService
    {
        public static bool[] Encrypt(bool[] bitArray, bool[] keyBits)
        {
            if (bitArray == null || bitArray.Length == 0)
                throw new ArgumentException("Массив битов не может быть пустым или null.");
            if (keyBits == null || keyBits.Length == 0)
                throw new ArgumentException("Ключ в виде массива битов не может быть пустым или null.");

            bool[] result = new bool[bitArray.Length];

            for (int i = 0; i < bitArray.Length; i++)
            {
                result[i] = bitArray[i] ^ keyBits[i % keyBits.Length];
            }

            return result;
        }

        public static bool[] Decrypt(bool[] bitArray, bool[] keyBits)
        {
            return Encrypt(bitArray, keyBits);
        }
    }
}
