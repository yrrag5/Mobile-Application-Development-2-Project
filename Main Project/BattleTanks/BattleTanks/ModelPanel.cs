using Windows.UI;

namespace BattleTanks
{
    public class ModelPanel : Panel
    {
        delegate void VoidDelegate();

        public ModelPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, true);
        }

        // Designing map with color background and screen
        public static void CreateMap(Cell[,] map, Color[,] screen)
        {
            Clear(screen);

            for(int i =0; i < 320; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    screen[i, j] = Color.Black;
                }
            }// for

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    screen[i, j] = Color.Black;
                }
            }// for

            for (int i = 313; i < 320; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    screen[i, j] = Color.Black;
                }
            }// for

            for (int i = 0; i < 320; i++)
            {
                for (int j = 193; j < 200; j++)
                {
                    screen[i, j] = Color.Black;
                }
            }// for

            for(int i = 0; i < 40; i++)
            {
                for(int j = 0; j < 25; j++)
                {
                    if(map[i, j].Type == 1)
                    {
                        for(int k = 0; k < 8; k++)
                        {
                            screen[map[i, j].X + k, map[i, j].Y + 1] = Color.Black;
                        }
                    }// if
                }
            }// for
        }// Map

        // Setting game over text font size 
        Font _Font = new Font("Arial", 10);

        public void Score(Graphics g)
        {
            string green = Tank.GreenScore.ToString();
            g.DrawString(green, _Font, Brushes.Green, new PointF(10, 10));

            string orange = Tank.OrangeScore.ToString();
            SizeH size = g.MeasureString(orange, _Font);
            g.DrawString(orange, _Font, Brushes.Orange, new PointF(Width - size.Width - 10, 10));

            if (GameOver)
            {
                string gOver = "Game Over";
                size = g.MeasureString(gameOver, _Font);

                g.DrawString(gOver, _Font, Brushes.White, new PointF(Width - size) / 2, (Height - size.Height) / 2);
            }
        }// Score

        // Function to clear entire panel after a score
        public static void Clear(Color[,] screen)
        {
            for(int i = 0; i < 320; i++)
            {
                for(int j = 0; j < 200; j++)
                {
                    screen[i, j] = Color.White;

                }
            }
        }// Clear

        public static void ChangeScreen(Color[,] location, Color[,] change)
        {   
            // Will return the upperbounds to neutral 
            if(location.GetUpperBound(0) != change.GetUpperBound(0) ||
                location.GetUpperBound(1) != location.GetUpperBound(1))
            {
                return;
            }

            for(int i = 0; i <location.GetUpperBound(0); i++)
            {
                for(int j = 0; j < location.GetUpperBound(1); y++)
                {
                    change[i, j] = location[i, j];
                }
            }
        }// Change

        public static bool Check(Color[,] c1, Color[,] c2)
        {
            // Will return the upperbounds to neutral 
            if (c1.GetUpperBound(0) != c2.GetUpperBound(0) ||
                c1.GetUpperBound(1) != c2.GetUpperBound(1))
            {
                return false;
            }

            for (int i = 0; i < c1.GetUpperBound(0); i++)
            {
                for (int j = 0; j < c1.GetUpperBound(1); j++)
                {
                    if(c1[i,j] != c2[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }// Check

        protected override void OnPaint(PaintEventArgs e)
        {
            InternalPaint(e.Graphics, GetRectangleToFill(true));
        }// OnPaint


  
    }
}

