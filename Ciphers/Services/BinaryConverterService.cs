using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Services
{
    public static class BinaryConverterService
    {
        public static bool[] StringToBinary(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Текст не может быть пустым или null.");

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            List<bool> bitList = new List<bool>();

            foreach (byte b in bytes)
            {
                for (int i = 7; i >= 0; i--)
                    bitList.Add((b & (1 << i)) != 0);
            }

            return bitList.ToArray();
        }

        public static string BinaryToString(bool[] bitArray)
        {
            if (bitArray == null || bitArray.Length == 0)
                throw new ArgumentException("Массив битов не может быть пустым или null.");
            if (bitArray.Length % 8 != 0)
                throw new ArgumentException("Длина массива битов должна быть кратна 8.");

            int byteCount = bitArray.Length / 8;
            byte[] bytes = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                byte b = 0;
                for (int j = 0; j < 8; j++)
                    if (bitArray[i * 8 + j])
                        b |= (byte)(1 << (7 - j));
                bytes[i] = b;
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
