using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BarReader = HTN.BITS.MCS.SCN.LIB.Scanner.clsBarcodeReader;

namespace HTN.BITS.MCS.SCN.LIB
{
    public class SplasherLoading
    {
        private static Panel objPnl = null;
        public static void Loading(Form objF)
        {
            try
            {
                BarReader.Instance.BarReader.ScannerEnable = false;

                objPnl = new Panel();
                objPnl.BackColor = System.Drawing.Color.Black;
                objPnl.Size = new System.Drawing.Size(240, 340);
                objPnl.Dock = DockStyle.Fill;

                //test by jack
                TextBox objText = new TextBox();
                objText.Width = 0;
                objText.BorderStyle = BorderStyle.None;
                objText.BackColor = System.Drawing.Color.Black;
                objText.Location = new System.Drawing.Point(0,0);

                

                Label objLabel = new Label();
                objLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Bold);
                objLabel.Size = new System.Drawing.Size(140, 20);
                objLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                objLabel.Text = ResourceManager.Instance.GetString("MSG_PLEASE_WAIT");
                objLabel.ForeColor = System.Drawing.Color.White;
                objLabel.BackColor = System.Drawing.Color.Black;
                //objLabel.Location = new System.Drawing.Point((objPnl.Size.Width / 2) + 8, (objPnl.Size.Height / 2) + 50);
                objLabel.Location = new System.Drawing.Point((objPnl.Size.Width / 2) - (objLabel.Size.Width / 2), ((objPnl.Size.Height / 2) - (objLabel.Size.Height / 2)) - 50);


                objPnl.Controls.Add(objText);
                objPnl.Controls.Add(objLabel);

                objLabel.Visible = true;
                objText.Visible = true;

                objF.Controls.Add(objPnl);

                objPnl.Show();
                objPnl.BringToFront();
                objText.Focus();


                Application.DoEvents();
                //Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void Unload(Form objF)
        {
            try
            {
                if (objPnl != null)
                {
                    objPnl.Controls.Clear();
                    objPnl.Visible = false;

                    objF.Controls.Remove(objPnl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
