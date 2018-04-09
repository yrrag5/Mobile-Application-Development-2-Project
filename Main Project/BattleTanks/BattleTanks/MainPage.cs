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
            }
        }
    }
}
