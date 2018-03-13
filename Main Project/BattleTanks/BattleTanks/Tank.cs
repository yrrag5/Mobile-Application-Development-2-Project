// Author: Garry Cummins
// ID: G00335806
// References : https://www.taylorfrancis.com/books/9781466581425/chapters/10.1201%2Fb17100-13
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace BattleTanks
{
    class Tank
    {
        // Creating tank objects and setting score for both to 0 for 
        // scoreboard track
        public static Tank Green;
        public static Tank Orange;
        public static int GreenScore = 0;
        public static int OrangeScore = 0;

        static readonly Color projectileColor;
        static readonly int[] CollisionSequence = { 1, 2, 3, 2 };

        static Tank()
        {
            // Giving the projectile of the tanks a value
            //projectileColor = Model3hPanel.GetDefaultedPaletteColor(8);
        }

        //[Flags]
        // Checks the progress of the game 
        public enum ProjectileCheck
        {
            Unchanged = 0,
            GreenScore = 1,
            OrangeScore = 2,
        }

        public bool gotHit;
        public int X, Y, Speed, Angle;
        public byte[] VGA;
        // Setting amount of projectiles that a tank can shoot at a time 
        public byte[][] Collision = new byte[2][];
        List<Projectile> Projectiles = new List<Projectile>();
        public bool hasCollided = false;
        int _CollisionIndex = 0;

        public void Draw(MainPage panel)
        {
            foreach(Projectile projectile in Projectiles)
            {
                panel.PutPixel((int)Math.Round(projectile.X), (int)Math.Round(projectile.Y), projectileColor);
                panel.PutPixel((int)Math.Round(projectile.X) + 1, (int)Math.Round(projectile.Y), projectileColor);
                panel.PutPixel((int)Math.Round(projectile.X), (int)Math.Round(projectile.Y), projectileColor);
                panel.PutPixel((int)Math.Round(projectile.X) + 1, (int)Math.Round(projectile.Y), projectileColor);

            }// ForEach

            // When a projectile collides with a tank
            if (hasCollided)
            {
                if(_CollisionIndex < 19)
                {
                    panel.DrawVGARRotated(X, Y, Angle, Collision[CollisionSequence[_CollisionIndex % 4] - 1]);
                    _CollisionIndex++;
                }
                else
                {
                    // No collision
                }
            }// if
        }// Draw



    }
}
