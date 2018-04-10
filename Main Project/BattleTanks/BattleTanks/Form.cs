using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTanks
{
    public partial class Form : Form
    {
        //IntializeComponent();

        _Clock.Interval = 50;
        _Clock.Tick += new EventHandler(_Clock_Tick);

        _Clock.Start();
    }//Form

    void _Clock_Tick(object sender, EventArgs e)
    {
        _ModelPanel.Clear();
        //_ModelPanel.Draw

        r++;

        r = r % 15;
    }

    Clock _Timer = new Timer();

    int r = 0;
}
