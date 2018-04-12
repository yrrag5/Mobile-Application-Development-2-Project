using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTanks
{
    public static class Globals
    {
        public static readonly double[] SinTable = new double[16];
        public static readonly double[] CosTable = new double[16];

        public static int gameLoop = 4;

        // Setting the number projectiles on screen and
        // for a certain amount of time 
        public static int maxProjectile = 3;
        public static int projectileSpan = 500;

        // Number of wins set to 5
        public static int winCondition = 5;

        static Globals()
        {
            for(int i = 0; i < 16; i++)
            {
                SinTable[i] = Math.Sin(22.5 * i * Math.PI / 180);
                CosTable[i] = Math.Cos(22.5 * i * Math.PI / 180);
            }// For
        }// Globals
    }// Main
}
