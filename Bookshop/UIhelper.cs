using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace Bookshop.Helpers
{
    class UIhelper
    {
        public static void LoadUC(Panel panel, UserControl uc)
        {
            panel.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel.Controls.Add(uc);
        }

        public static void HighlightButton(
            Button newBtn,
            ref Button currentBtn)
        {
            if (currentBtn != null)
                currentBtn.BackColor = Color.FromArgb(0,33,71);

            currentBtn = newBtn;
            currentBtn.BackColor = Color.RoyalBlue;
        }
    }
}

