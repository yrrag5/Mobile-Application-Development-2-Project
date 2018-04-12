using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTanks
{
    public partial class MainPage : Page
    {
        public MainPage()
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

                if(Tank.GreenScore >= Globals.LastScore || Tank.OrangeScore >= Globals.lastScore)
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

                if(_Clock >= 20 || _ModelPanel.GameOver)
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
            using(Form f = new Form())
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
                greenBox.BackColor = Color.FromArgb(50, 205, 50)
            }
        }
    }
}
