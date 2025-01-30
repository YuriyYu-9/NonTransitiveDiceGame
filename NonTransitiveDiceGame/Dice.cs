using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public class Dice
    {
        private readonly int[] faces;
        private readonly Random random;

        public Dice(int[] faces)
        {
            if (faces.Length < 2) throw new ArgumentException("A dice must have at least 2 faces.");
            this.faces = faces;
            this.random = new Random();
        }

        public int Roll() => faces[random.Next(faces.Length)];

        public int[] GetFaces() => faces;

        public override string ToString() => $"[{string.Join(",", faces)}]";
    }
}
