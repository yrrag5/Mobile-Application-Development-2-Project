using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTanks
{
    public partial class ModelPanel1 : Component
    {
        public ModelPanel1()
        {
            InitializeComponent();
        }

        public ModelPanel1(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
