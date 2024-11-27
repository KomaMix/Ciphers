using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Services
{
    public static class GammaGeneratorService
    {
        private static readonly Random _random = new Random();

        public static bool[] GenerateRandomGamma(int length)
        {
            if (length <= 0)
                throw new ArgumentException("Длина гаммы должна быть положительным числом.");

            bool[] gamma = new bool[length];

            for (int i = 0; i < length; i++)
            {
                gamma[i] = _random.Next(2) == 1;
            }

            return gamma;
        }
    }
}
