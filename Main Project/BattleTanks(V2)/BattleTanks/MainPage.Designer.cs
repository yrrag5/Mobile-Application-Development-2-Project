using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTanks
{
    partial class MainPage
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
            this.menu = new System.Windows.Forms.MenuStrip;
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ModelPanel = new tanks.Mode; hPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(952, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
        }// Intialise

        #endregion

        private ModelPanel _ModelPanel;
        private System.Windows.Forms.Menu menu;

    }// Main
}
