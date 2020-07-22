using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.MCS.SCN.LIB;

namespace HTN.BITS.MCS.SCN.UIL.Components
{
    public partial class SplashInitForm : Form
    {
        static int barwidth = 125;
        static Bitmap backgroundBmp = null;

        public SplashInitForm()
        {
            InitializeComponent();
            ResourceManager.Instance.Culture = new System.Globalization.CultureInfo(GlobalVariable.LanguageSelect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Font font = new Font("Tahoma", 10, FontStyle.Bold);
            if (backgroundBmp == null)
                backgroundBmp = HTN.BITS.MSC.SCN.UIL.Properties.Resources.SplashInitScanner; //ResourceManager.Instance.GetBitmap("SplashInitScanner"); 
            //(Bitmap)Properties.Resources.ResourceManager.GetObject("SplashBitmap");
            e.Graphics.DrawImage(backgroundBmp, 0, 0);
            e.Graphics.DrawString("BASICS PRO. SCANNER", font, new SolidBrush(Color.Yellow), 35, 55);
            e.Graphics.DrawString("Starting...", font, new SolidBrush(Color.White), 80, 158);

            ShowProgress(0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //We don't need to do anything here
        }

        public void ShowProgress(int percentage)
        {
            Graphics gr = this.CreateGraphics();
            gr.DrawRectangle(new Pen(Color.Red), new Rectangle(57, 195, barwidth + 1, 11));
            gr.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(58, 196, (barwidth * percentage) / 100, 10));
        }

        private void SplashInitForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }
    }
}