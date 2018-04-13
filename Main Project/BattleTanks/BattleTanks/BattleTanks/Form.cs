using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BattleTanks
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            _UpdateClock.Interval = 100;
            _UpdateClock.Tick += new EventHandler(_UpdateTimer_Tick);
            _UpdateClock.Start();

            NewGame(Properties.Resources.map1);
        }

        int _Clock = 0;

        void _UpdateClock_Tick(object sender, EventArgs e)
        {
            if (_RoundComplete)
            {
                _ModelPanel.Score = true;
                _RoundOver = false;

                if (Tank.GreenScore >= Globals.LastScore || Tank.OrangeScore >= Globals.lastScore)
                {
                    _ModelPanel.GameOver = true;
                }// Inner if

                else
                {
                    NewGame();
                }// Else
            }// if

            else if (_ModelPanel.ShouldDrawScore)
            {
                _Clock++;

                if (_Clock >= 20 || _ModelPanel.GameOver)
                {
                    _ModelPanel.ShouldDrawScore = false;
                    ModelPanel.Invalidate();

                    _Clock = 0;
                }
            }

        }//UpdateClock

        protected override void Load(EventArgs e)
        {
            base.OnLoad(e);

            Application.AddMessageFilter(_Keys);
        }// Load

        void menuClick(object sender, EventArgs e)
        {
            NewGame(Properties.Resources.grid);
        }

        void instructMenuClick(object sender, EventArgs e)
        {
            using (Form f = new Form())
            {
                f.FormBorderStyle = FormBoarderStyle.FixedToolWindow;
                f.AutoSize = true;
                f.AutoSizeMode = AutoSizeMode.Expand;

                f.Location = Location;

                TextBox greenBox = new TextBox();
                greenBox.Text = Properties.Resources.Text;
                greenBox.ReadOnly = true;
                greenBox.Width = 300;
                greenBox.Height = 250;
                greenBox.BackColor = Color.FromArgb(50, 205, 50);

                f.Controls.Add(greenBox);

                f.ShowDialog;
            }
        }// Menu Click

        void EndRound()
        {
            _End = true;
        }

        void NewGame(string grid)
        {
            EndRound();

            if(_Thread != null)
            {
                _Thread.Abort();
            }

            LoadGrid(grid);
            ModelPanel.CreateGrid(_Grid, _Background);
            ModelPanel.Flip(_Background, _ModelPanel);
            _ModelPanel.GameOver = false;

            Tank.GreenScore = 0;
            Tank.OrangeScore = 0;

            NextRound();
        }// NewGame

        void NextRound()
        {
            EndRound();
            ModelPanel.Clear(_ModelPanel.Screen);
            _ModelPanel.Invalidate();
            // Tank properties 
            Tank.Green = new Tank();
            Tank.Green.NotHit = true;
            Tank.Green.VGA = Properties.GT;
            Tank.Green.X = 270;
            Tank.Green.Y = 175;
            Tank.Green.Angle = 12;
            Tank.Green.Speed = 5;

            Tank.Orange = new Tank();
            Tank.Orange.NotHit = true;
            Tank.Orange.VGA = Properties.OT;
            Tank.Orange.X = 60;
            Tank.Orange.Y = 20;
            Tank.Orange.Angle = 12;
            Tank.Orange.Speed = 5;

            _Thread = new System.Threading.Thread(GameContinued);
            _Thread.IsBackground = true;
            _Thread.Start();
        }// Next round

        void LoadGrid(string grid)
        {
            try
            {
                string[] design = grid.Split(new String[] { Environment.NewLine }, 
                StringSplitOptions.RemoveEmptyEntries);

                int b = 0;

                foreach(string d in design)
                {
                    string[] pieces = d.Split(new string[] { "" }, 
                    StringSplitOptions.RemoveEmptyEntries);

                    int a = 0;

                    foreach (string p in pieces)
                    {
                        byte t = 0;
                        byte.TryParse(d, out t);

                        Cell c = new Cell();
                        c.Type = t;
                        c.X = 8 * a;
                        c.Y = 8 * b;

                        _Grid[a, b] = c;

                        a++;

                        if(a >= 40)
                        {
                            break;
                        }
                    }// Inner foreach

                    b++;
                    if(b >= 25)
                    {
                        break;
                    }

                }// foreach

            }// Try

            catch(Exception e)
            {
                // Grid will not be created
            }
        }// LoadGrid

        void GameContinued()
        {
            const int eDelay = 25;

            _RoundComplete = false;
            _Finish = false;
            int delay = Globals.gameLoop;

            while (!_Finish)
            {
                CheckKeys();
                Tank.ProjectileCheck res = Tank.Green.CheckCollisions(_Grid);
                res |= Tank.Orange.CheckCollisions(_Grid);

                if((res & Tank.ProjectileCheck.GreenScore) != 0)
                {
                    delay = eDelay;
                    Tank.Green.Collided();
                }
                if((res & Tank.ProjectileCheck.OrangeScore) != 0)
                {
                    delay = eDelay;
                    Tank.Orange.Collided();

                    ModelPanel.FlipScreen(_Background, _ModelPanel.Screen);

                    _ModelPanel.DoPaint();

                    if((Tank.Green.hasCollided || Tank.Orange.hasCollided) &&
                        Tank.Green. && Tank.Orange.)
                    {
                        if (Tank.Green.)
                        {
                            Tank.OrangeScore++;
                        }
                        else if (Tank.Orange.)
                        {
                            Tank.GreenScore++;
                        }

                        _RoundComplete = true;
                        return;
                    }//Inner if  
                }// if

                if (!_Complete)
                {
                    System.Threading.Thread.Sleep(delay);
                }
            }// while
        }// Continued

        protected override void Close(EventArgs e)
        {
          _Complete = true;
          base.Close(e);          
        }

        #region Keys

        const int Angle = 5;
        const int Go = 1;

        int _GreenAngleCounter = 0;
        int _OrangeAngleCounter = 0;
        int _GreenGoCounter = 0;
        int _OrangeGoCounter = 0;
        bool _GreenNotFiring = true;
        bool _OrangeNotFiring = true;
        void CheckKeys()
        {
            if (_Keys[Keys.Up])
            {
                _GreenAngleCounter++;

                if (_GreenAngleCounter > Angle)
                {
                    _GreenAngleCounter = 0;

                    Tank.Green.Up();
                }
            }

            if (_Keys[Keys.Down])
            {
                _GreenAngleCounter++;

                if (_GreenAngleCounter > Angle)
                {
                    _GreenAngleCounter = 0;

                    Tank.Green.Down();
                }
            }

            if (_Keys[Keys.Left])
            {
                _GreenAngleCounter++;

                if (_GreenAngleCounter > Angle)
                {
                    _GreenAngleCounter = 0;

                    Tank.Green.Left();
                }
            }

            if (_Keys[Keys.Right])
            {
                _GreenAngleCounter++;

                if (_GreenAngleCounter > Angle)
                {
                    _GreenAngleCounter = 0;

                    Tank.Green.Right();
                }
            }

            if (_Keys[Keys.OemQuotes])
            {
                _GreenGoCounter++;

                if (_GreenGoCounter > Go)
                {
                    _GreenGoCounter = 0;

                    Tank.Green.Move();
                }
            }

            Tank.Green.CheckCollisions(_Grid);

            if (!_Keys[Keys.Enter])
                _GreenNotFiring = true;
            else
            {
                if (_GreenNotFiring)
                {
                    Tank.Green.Shoot();
                }

                _GreenNotFiring = false;
            }

            if (_Keys[Keys.W])
            {
                _OrangeAngleCounter++;

                if (_OrangeAngleCounter > Angle)
                {
                    _OrangeAngleCounter = 0;

                    Tank.Orange.Up();
                }
            }

            if (_Keys[Keys.S])
            {
                _OrangeAngleCounter++;

                if (_OrangeAngleCounter > Angle)
                {
                    _OrangeAngleCounter = 0;

                    Tank.Orange.Down();
                }
            }

            if (_Keys[Keys.A])
            {
                _OrangeAngleCounter++;

                if (_OrangeAngleCounter > Angle)
                {
                    _OrangeAngleCounter = 0;

                    Tank.Orange.Left();
                }
            }

            if (_Keys[Keys.D])
            {
                _OrangeAngleCounter++;

                if (_OrangeAngleCounter > Angle)
                {
                    _OrangeAngleCounter = 0;

                    Tank.Orange.Right();
                }
            }

            if (_Keys[Keys.M])
            {
                _OrangeGoCounter++;

                if (_OrangeGoCounter > Go)
                {
                    _OrangeGoCounter = 0;

                    Tank.Orange.Move();
                }
            }

            Tank.Orange.CheckCollisions(_Grid);


            if (!_Keys[Keys.F])
                _OrangeNotFiring = true;
            else
            {
                if (_OrangeNotFiring)
                {
                    Tank.Orange.Shoot();
                }

                _OrangeNotFiring = false;
            }
        }

        #endregion


        Cell[,] _Grid = new Cell[40, 25];
        Color _Background = new Color[320, 200];
        Clock _Update = new Clock;

        TextFilter _Keys = new TextFilter();

        bool _Complete = false;
        bool _roundFinished = false;

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

