using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        }// Map
    }
}
