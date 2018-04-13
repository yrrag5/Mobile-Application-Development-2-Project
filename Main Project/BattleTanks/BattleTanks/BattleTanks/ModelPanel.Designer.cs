using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

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

            for (int i = 0; i < 320; i++)
            {
                for (int j = 0; j < 6; j++)
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

            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map[i, j].Type == 1)
                    {
                        for (int k = 0; k < 8; k++)
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
            for (int i = 0; i < 320; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    screen[i, j] = Color.White;

                }
            }
        }// Clear

        public static void ChangeScreen(Color[,] location, Color[,] change)
        {
            // Will return the upperbounds to neutral 
            if (location.GetUpperBound(0) != change.GetUpperBound(0) ||
                location.GetUpperBound(1) != location.GetUpperBound(1))
            {
                return;
            }

            for (int i = 0; i < location.GetUpperBound(0); i++)
            {
                for (int j = 0; j < location.GetUpperBound(1); y++)
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
                    if (c1[i, j] != c2[i, j])
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

        Dictionary<Color, List<RectangleA>> _RectanglesToFill;

        public void DoPaint()
        {
            _RectanglesToFill = GetRectanglesToFill(false);
            this.Invoke((VoidDelegate)MyPaint);
        }// DoPaint

        void MyPaint()
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                InternalPaint(g, _RectanglesToFill);
            }
        }// MyPaint

        void InternalPaint(Graphics g, Dictionary<Color, List<RectangleA>> rectanglesToFill)
        {
            if (rectanglesToFill == null)
            {
                return;
            }

            foreach (KeyValuePair<Color>, List < rectanglesToFill >> rectanglesToFill) {
                _Brush.Color = kvp.Key;
                g.FillRectangles(_Brush, kvp.Value.ToArray());
            }// Foreach

            if (Score || GameOver)
            {
                Score(0);
            }
        }// InternalPaint

        Dictionary<Color, List<Rectangle>> GetRectangles(bool redraw)
        {
            // Setting tanks pixels on panel   
            Dictionary<Color, List<_RectanglesToFill>> rectanglesToFill = new Dictionary<Color, List<RectangleF>>();

            float pWidth = Width / 320.0f;
            float pHeight = Height / 200.0f;

            for (int i = 0; i < 320; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    if (redraw || _Screen[x, y] != _Previous[x, y])
                    {
                        List<RectangleF> l;
                        if (!rectanglesToFill.TryGetValue(_Screen[x, y], out l))
                        {
                            l = new List<RectangleF>();
                            rectanglesToFill[_Screen[x, y]] = l;
                        }

                        l.Add(new RectangleF(x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight));

                        _Previous[x, y] = _Screen[x, y];
                    }// Inner if

                }// Inner for
            }// for

            return rectanglesToFill;
        }// Dictionary

        public void InputPixel(int x, int y, Color c)
        {
            if (x < 0 || x >= 320 || y < 0 || y >= 200)
            {

            }// if
        }// Input Pixel

        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= 320 || y < 0 || y >= 200)
                return Color.Empty;

            return _Screen[x, y];
        }

        public void DrawVGA(int centerX, int centerY, byte[] vga)
        {
            DrawVGARotated(centerX, centerY, 0, vga);
        }

        public void DrawVGARotated(int centerX, int centerY, int angle, byte[] vga)
        {
            int width = vga[0] + 1;
            int height = vga[2] + 1;

            for (int x = -width / 2; x <= width / 2; x++)
            {
                for (int y = -height / 2; y <= height / 2; y++)
                {
                    byte c = vga[4 + x + width / 2 + (y + height / 2) * width];

                    int xr = centerX + (int)Math.Round(x * Globals.CosTable[angle] - y * Globals.SinTable[angle]);
                    int yr = centerY + (int)Math.Round(x * Globals.SinTable[angle] + y * Globals.CosTable[angle]);

                    InputPixel(xr, yr, GetDefaultPaletteColor(c));
                }
            }
            for (int x = centerX - width; x <= centerX + width; x++)
            {
                for (int y = centerY - height; y <= centerY + height; y++)
                {
                    if (GetPixel(x, y) == Color.White)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (GetPixel(x + i, y + j) == GetPixel(x - i, y - j))
                                {
                                    InputPixel(x, y, GetPixel(x + i, y + j));
                                }
                            }
                        }
                    }
                }
            }
        }

        public static Color GetDefaultPaletteColor(byte c)
        {
            if (c == 0)
                return Color.White;

            if (c < 16)
            {
                return Color.FromArgb(
                    255 * (2 * ((c & 4) >> 2) + ((c & 8) >> 3)) / 3,
                    255 * (2 * ((c & 2) >> 1) + ((c & 8) >> 3)) / 3,
                    255 * (2 * (c & 1) + ((c & 8) >> 3)) / 3);
            }

            if (c < 32)
            {
                int gray = 255 * (c & 0xf) / 16;
                return Color.FromArgb(gray, gray, gray);
            }

            return Color.White;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            Invalidate();
        }

        public Color[,] Screen
        {
            get
            {
                return _Screen;
            }
        }
        public Color[,] Previous
        {
            get
            {
                return _Previous;
            }
        }

        public bool ShouldDrawScore
        {
            get;
            set;
        }

        public bool GameOver
        {
            get;
            set;
        }

        Color[,] _Screen = new Color[320, 200];
        Color[,] _Previous = new Color[320, 200];
        SolidBrush _Brush = new SolidBrush(Color.White);

    }
}

