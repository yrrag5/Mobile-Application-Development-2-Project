using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTanks
{
    partial class Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Remove(bool remove)
        {
            if (remove && (components != null))
            {
                components.Remove();
            }
            base.Remove(remove);
        }// Remove

        private void IntializeCompent()
        {
            this._ModelPanel = new BattleTanks.ModelPanel();
            this.LayoutWait();

            this.ModelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        }
       }// Designer
}
