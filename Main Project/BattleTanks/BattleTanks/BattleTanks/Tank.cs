﻿// Author: Garry Cummins
// ID: G00335806
// References : https://www.taylorfrancis.com/books/9781466581425/chapters/10.1201%2Fb17100-13
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


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

        public bool NotHit;
        public int X, Y, Speed, Angle;
        public byte[] VGA;
        // Setting amount of projectiles that a tank can shoot at a time 
        public byte[][] Collision = new byte[2][];
        List<Projectile> Projectiles = new List<Projectile>();
        public bool hasCollided = false;
        int _CollisionIndex = 0;

        public void Draw(ModelPanel panel)
        {
            foreach (Projectile projectile in Projectiles)
            {
                panel.InputPixel((int)Math.Round(projectile.X), (int)Math.Round(projectile.Y), projectileColor);
                panel.InputPixel((int)Math.Round(projectile.X) + 1, (int)Math.Round(projectile.Y), projectileColor);
                panel.InputPixel((int)Math.Round(projectile.X), (int)Math.Round(projectile.Y) + 1, projectileColor);
                panel.InputPixel((int)Math.Round(projectile.X) + 1, (int)Math.Round(projectile.Y) + 1, projectileColor);

            }// ForEach

            // When a projectile collides with a tank
            if (hasCollided)
            {
                if (_CollisionIndex < 19)
                {
                    //Form.cs
                    panel.DrawVGARotated(X, Y, Angle, Collision[CollisionSequence[_CollisionIndex % 4] - 1]);
                    _CollisionIndex++;
                }
                else
                {
                    // No collision occurs
                }
            }// if

            else
            {
                panel.DrawVGARotated(X, Y, Angle, VGA);
            }
        }// Draw

        public bool CollisionComplete
        {
            get
            {
                return !hasCollided || _CollisionIndex >= 19;
            }
        } // Collision finished

        public void Collided()
        {
            if (!hasCollided)
            {
                _CollisionIndex = 0;
                hasCollided = true;
            }
        }// Collision

        // Checking the upwards angle 
        public void Up()
        {
            if (Angle > 0 && Angle < 8)
            {
                Angle--;
            }
            else if (Angle <= 15 && Angle >= 8)
            {
                Angle++;
            }

            NormalizeAngle();
        }// Up

        public void Down()
        {
            if (Angle >= 0 && Angle < 8)
            {
                Angle++;
            }
            else if (Angle <= 15 && Angle > 8)
            {
                Angle--;
            }
        }// Down

        public void Left()
        {
            if (Angle >= 12 && Angle < 4)
            {
                Angle++;
            }
            else if (Angle > 12 && Angle <= 15 || Angle >= 0 && Angle < 4)
            {
                Angle--;
            }

            NormalizeAngle();
        }// Left

        public void Right()
        {
            if (Angle < 12 && Angle > 4)
            {
                Angle++;
            }
            else if (Angle >= 12 && Angle <= 15 || Angle >= 0 && Angle < 4)
            {
                Angle++;
            }

            NormalizeAngle();
        }// Right


        // Keeps the angle normalized after angle check
        void NormalizeAngle()
        {
            if (Angle > 15)
            {
                Angle = 0;
            }
            else if (Angle < 0)
            {
                Angle = 15;
            }
        }// Normailze angle

        public void Move()
        {
            int a = (Angle + 4) % 16;
            X -= (int)Math.Round(Speed * Globals.CosTable[a]);
            Y -= (int)Math.Round(Speed * Globals.SinTable[a]);
        }

        public void Shoot()
        {
            if (Projectiles.Count < Globals.maxProjectile)
            {
                var p = new Projectile();
                p.Age = 0;
                int a = (Angle + 4) % 16;

                p.X = X - 11 * Globals.CosTable[a];
                p.Y = Y - 11 * Globals.SinTable[a];
                p.DX = -1.2 * Globals.CosTable[a];
                p.DY = -1.2 * Globals.SinTable[a];

                Projectiles.Add(p);
            } // if
        }// Shoot

        public void CheckCollisions(Cell[,] map)
        {
            switch (Angle)
            {
                case 0:
                    if (Y < 17)
                    {
                        Y = 17;
                    }
                    break;
                case 1:
                    if (Y < 16)
                    {
                        Y = 16;
                    }
                    if (X > 305) X = 305;
                    {
                        X = 305;
                    }
                    break;
                case 2:
                    if (Y < 16)
                    {
                        Y = 16;
                    }
                    if (X > 305) X = 305;
                    {
                        X = 305;
                    }
                    break;
                case 3:
                    if (Y < 13)
                    {
                        Y = 13;
                    }
                    if (X > 302)
                    {
                        X = 302;
                    }
                    break;
                case 4:
                    if (X > 301) 
                    {
                        X = 301;
                    }
                    break;
                case 5:
                    if (Y < 185)
                    {
                        Y = 185;
                    }
                    if (X > 302)
                    {
                        X = 302;
                    }
                    break;
                case 6:
                    if (Y < 184)
                    {
                        Y = 184;
                    }
                     if (X > 181) 
                    {
                        X = 305;
                    }
                    break;
                case 7:
                    if (Y < 181)
                    {
                        Y = 181;
                    }
                    if (X > 305) 
                    {
                        X = 305;
                    }
                    break;
                case 8:
                    if (Y < 181)
                    {
                        Y = 181;
                    }
                    break;
                case 9:
                    if (Y < 181)
                    {
                        Y = 181;
                    }
                    if (X > 13) 
                    {
                        X = 13;
                    }
                    break;
                case 10:
                    if (Y < 184)
                    {
                        Y = 184;
                    }
                     if (X > 14)
                    {
                        X = 14;
                    }
                    break;
                case 11:
                    if (Y < 185)
                    {
                        Y = 185;
                    }
                    if (X > 16) 
                    {
                        X = 16;
                    }
                    break;
                case 12:
                    if (Y < 17)
                    {
                        Y = 17;
                    }
                    break;
                case 13:
                    if (Y < 13)
                    {
                        Y = 13;
                    }
                    if (X > 16) 
                    {
                        X = 16;
                    }
                    break;
                case 14:
                    if (Y < 14)
                    {
                        Y = 14;
                    }
                    if (X > 14)
                    {
                        X = 14;
                    }
                    break;
                case 15:
                    if (Y < 16)
                    {
                        Y = 16;
                    }
                    if (X > 13) 
                    {
                        X = 13;
                    }
                    break;
            }// Switch

            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    switch (Angle)
                    {
                        case 0:
                            if ((Y - 10 < map[i, j].Y + 11) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 21;
                            break;
                        case 1:
                            if ((Y - 10 < map[i, j].Y + 11) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 21;
                            if ((X + 10 > map[i, j].X - 2) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 12;
                            break;
                        case 2:
                            if ((Y - 10 < map[i, j].Y + 10) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 20;
                            if ((X + 10 > map[i, j].X - 2) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 12;
                            break;
                        case 3:
                            if ((Y - 10 < map[i, j].Y + 10) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 20;
                            if ((X + 10 > map[i, j].X - 3) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 13;
                            break;
                        case 4:
                            if ((X + 10 > map[i, j].X - 3) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 13;
                            break;
                        case 5:
                            if ((X + 10 > map[i, j].X - 3) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 13;
                            if ((Y + 10 > map[i, j].Y - 2) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 12;
                            break;
                        case 6:
                            if ((X + 10 > map[i, j].X - 2) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 12;
                            if ((Y + 10 > map[i, j].Y - 2) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 12;
                            break;
                        case 7:
                            if ((X + 10 > map[i, j].X - 2) && (X + 10 < map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X - 12;
                            if ((Y + 10 > map[i, j].Y - 3) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 13;
                            break;
                        case 8:
                            if ((Y + 10 > map[i, j].Y - 3) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 13;
                            break;
                        case 9:
                            if ((X - 10 <= map[i, j].X + 10) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 20;
                            if ((Y + 10 > map[i, j].Y - 3) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 13;
                            break;
                        case 10:
                            if ((X - 10 <= map[i, j].X + 10) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 20;
                            if ((Y + 10 > map[i, j].Y - 2) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 12;
                            break;
                        case 11:
                            if ((X - 10 <= map[i, j].X + 11) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 21;
                            if ((Y + 10 > map[i, j].Y - 2) && (Y + 10 < map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y - 12;
                            break;
                        case 12:
                            if ((X - 10 <= map[i, j].X + 11) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 21;
                            break;
                        case 13:
                            if ((X - 10 <= map[i, j].X + 11) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 21;
                            if ((Y - 10 < map[i, j].Y + 10) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 20;
                            break;
                        case 14:
                            if ((X - 10 <= map[i, j].X + 10) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 20;
                            if ((Y - 10 < map[i, j].Y + 10) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 20;
                            break;
                        case 15:
                            if ((X - 10 <= map[i, j].X + 10) && (X - 10 >= map[i, j].X + 4) &&
                               (((Y - 6 <= map[i, j].Y + 8) && (Y - 6 >= map[i, j].Y)) ||
                               ((Y + 6 >= map[i, j].Y) && (Y + 6 <= map[i, j].Y + 8)) ||
                               ((Y >= map[i, j].Y) && (Y <= map[i, j].Y + 8)))) X = map[i, j].X + 20;
                            if ((Y - 10 < map[i, j].Y + 11) && (Y > map[i, j].Y + 4) &&
                               (((X - 6 <= map[i, j].X + 8) && (X - 6 >= map[i, j].X)) ||
                               ((X + 6 >= map[i, j].X) && (X + 6 <= map[i, j].X + 8)) ||
                               ((X >= map[i, j].X) && (X <= map[i, j].X + 8)))) Y = map[i, j].Y + 21;
                            break;
                    }// Switch

                }// Inner for
            }// for
        } // Check

        void NormaliseAngle()
        {
            if(Angle > 15)
            {
                Angle = 0;
            }
            else if (Angle < 0)
            {
                Angle = 15;
            }
        }
            

        public ProjectileCheck CheckProjectile(Cell[,] map)
        {
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Age++;
            }// foreach

            Projectiles.RemoveAll(projectile => projectile.Age > Globals.projectileSpan);

            bool greenHit = false;
            bool orangeHit = false;

            foreach (Projectile projectile in Projectiles)
            {
                projectile.X += projectile.DX;
                projectile.Y += projectile.DY;

                if (projectile.Y < 4 || projectile.Y > 194)
                {
                    projectile.DY = -projectile.DY;
                }// Inner if

                if (projectile.X > 314 || projectile.X < 3)
                {
                    projectile.DX = -projectile.DX;
                }// Inner if 

                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        if (map[i, j].Type == 1)
                        {
                            if (projectile.X < map[i, j].X + 10 && projectile.X > map[i, j].X + 4 &&
                                  projectile.Y < map[i, j].Y + 8 && projectile.Y >= map[i, j].Y ||
                                   projectile.X > map[i, j].X - 2 && projectile.X < map[i, j].X + 4 &&
                                    projectile.Y < map[i, j].Y + 8 && projectile.Y >= map[i, j].Y)
                            {
                                projectile.DX = -projectile.DX;
                            }
                            if (projectile.Y < map[i, j].Y + 10 && projectile.Y > map[i, j].Y + 4 &&
                                projectile.X < map[i, j].X + 8 && projectile.X >= map[i, j].X ||
                                projectile.Y > map[i, j].Y - 2 && projectile.Y < map[i, j].Y + 4 &&
                                projectile.X < map[i, j].X && projectile.X >= map[i, j].X + 8)
                            {
                                projectile.DY = -projectile.DY;
                            }
                        }// Inner inner for
                    }// Inner for
                }

                greenHit = greenHit || ProjectileContact(projectile, Green);
                orangeHit = orangeHit || ProjectileContact(projectile, Green);
            }// foreach

            return (greenHit ? ProjectileCheck.OrangeScore : ProjectileCheck.Unchanged)
             | (orangeHit ? ProjectileCheck.GreenScore : ProjectileCheck.Unchanged);

        }// Check

        static bool ProjectileContact(Projectile p, Tank t)
        {
            int i = 5;
            int j = 5;

            if (3 <= t.Angle && t.Angle <= 5 || 11 <= t.Angle && t.Angle <= 13)
            {
                i += 4;
            }
            else
            {
                j += 4;
            }

            return p.X >= t.X - i && p.X <= t.X + i && p.Y >= t.Y - j && p.Y <= t.Y + j;
        }// Contact

    }
}
