﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Services
{
    public static class GammaCipherWithIterativeVectorService
    {
        public static bool[] Encrypt(bool[] bitArray, bool[] keyBits, bool[] initialVector)
        {
            if (bitArray == null || bitArray.Length == 0)
                throw new ArgumentException("Массив битов не может быть пустым или null.");
            if (keyBits == null || keyBits.Length == 0)
                throw new ArgumentException("Ключ в виде массива битов не может быть пустым или null.");
            if (initialVector == null || initialVector.Length == 0)
                throw new ArgumentException("Итерационный вектор в виде массива битов не может быть пустым или null.");

            bool[] result = new bool[bitArray.Length];
            bool[] currentVector = (bool[])initialVector.Clone();

            for (int i = 0; i < bitArray.Length; i++)
            {
                // Генерация гаммы на основе ключа и текущего итерационного вектора
                bool gammaBit = keyBits[i % keyBits.Length] ^ currentVector[i % currentVector.Length];

                // Шифрование текущего бита
                result[i] = bitArray[i] ^ gammaBit;

                // Обновление итерационного вектора
                currentVector[i % currentVector.Length] = gammaBit;
            }

            return result;
        }

        public static bool[] Decrypt(bool[] bitArray, bool[] keyBits, bool[] initialVector)
        {
            // Расшифрование идентично шифрованию
            return Encrypt(bitArray, keyBits, initialVector);
        }

        public static bool[] GetIVGamma(bool[] keyBits, bool[] initialVector)
        {
            bool[] bitArray = new bool[keyBits.Length];

            return Encrypt(bitArray, keyBits, initialVector);
        }

        public static bool[] GetIV2(bool[] keyBits, bool[] initialVector)
        {
            bool[] result = new bool[keyBits.Length];
            bool[] currentVector = (bool[])initialVector.Clone();

            for (int i = 0; i < keyBits.Length; i++)
            {
                // Генерация гаммы на основе ключа и текущего итерационного вектора
                bool gammaBit = keyBits[i % keyBits.Length] ^ currentVector[i % currentVector.Length];

                // Шифрование текущего бита
                result[i] = keyBits[i] ^ gammaBit;

                // Обновление итерационного вектора
                currentVector[i % currentVector.Length] = gammaBit;
            }

            return currentVector;
        }
    }

}
