using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    internal class FairRandomGenerator
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

        public static (int number, string hmac, byte[] key) GenerateFairNumber(int maxValue)
        {
            byte[] key = new byte[32];
            rng.GetBytes(key);

            int number = RandomNumber(maxValue);
            string hmac = ComputeHMAC(key, number);

            return (number, hmac, key);
        }

        private static int RandomNumber(int maxValue)
        {
            if (maxValue <= 0)
                throw new ArgumentException("maxValue must be greater than 0");

            byte[] buffer = new byte[4];
            int number;

            do
            {
                rng.GetBytes(buffer);
                number = Math.Abs(BitConverter.ToInt32(buffer, 0)) % maxValue;
            } while (number < 0 || number >= maxValue); // Гарантируем корректный диапазон

            return number;
        }


        private static string ComputeHMAC(byte[] key, int number)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(number.ToString()));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
