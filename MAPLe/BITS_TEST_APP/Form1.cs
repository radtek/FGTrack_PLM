using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.DAL;
using System.Data.Common;
using Oracle.DataAccess.Client;
using HTN.BITS.QRCodeLib;
using System.IO;

using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BITS_TEST_APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt2 = OraDB.Instance.DataAc.GetDataTable("SELECT * FROM M_USER");
            

            MessageBox.Show(dt2.Rows.Count.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt2 = OraDB.Instance.DataAc.GetDataTable("SELECT * FROM M_MATERIAL");
            

            MessageBox.Show(dt2.Rows.Count.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt2 = OraDB.Instance.DataAc.GetDataTable("select * from V_UCT_STOCK_AS_ON_TR where MTL_TYPE = 'M'");
            

            this.dataGridView1.DataSource = dt2;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OraDB.Instance.Disconenct();
            OraDB.Instance.Release();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OraDB.Instance.Init();
            OraDB.Instance.Connect();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int result = OraDB.Instance.DataAc.ExecuteNonQuery("UPDATE M_USER SET EMAIL = 'rungsan@nittsu.co.th' where USER_ID in ('JACK','TEST3')");
            if (OraDB.Instance.DataAc.LastException != null)
            {
                MessageBox.Show(OraDB.Instance.DataAc.LastException.Message);
            }
            else
            {
                MessageBox.Show(result.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcParam procPara = new ProcParam(3);

            procPara.ProcedureName = "DAL_PACK.USER_LOGON";

            procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 100);
            procPara.AddParamInput(1, "strVal", "JACK");
            procPara.AddParamInput(2, "strPwd", "pa$$w1");

            OraDB.Instance.DataAc.ExecuteNonQuery(procPara);

            if(OraDB.Instance.DataAc.LastException != null)
            {
                MessageBox.Show(OraDB.Instance.DataAc.LastException.Message);
            }
            else
            {
                object objResult = (string.IsNullOrEmpty(procPara.Parameters[0].Value.ToString().Trim()) ? null : procPara.Parameters[0].Value);

                if (objResult == null)
                {
                    MessageBox.Show("is null");
                }
                else
                {
                    MessageBox.Show(objResult.ToString());
                }
            }

        }

        private void QRCode_Encode()
        {
            if (txtEncodeData.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Data must not be empty.");
                return;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = 4;
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid size!");
                return;
            }
            try
            {
                int version = 4;
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid version !");
            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            Image image;
            String data = txtEncodeData.Text;
            image = qrCodeEncoder.Encode(data);
            picEncode.Image = image;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //this.QRCode_Encode();
            string result = Convertor(this.txtEncodeData.Text);

            //string result2 = DecodeEncodedNonAsciiCharacters(result);
            MessageBox.Show(result);
        }

        private string EncodeNonAsciiCharacters(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }

        private string DecodeString(string orig_string)
        {
            Encoding cod = Encoding.GetEncoding(28595);
            Encoding unicode = Encoding.Unicode;

            byte[] unicodeBytes = unicode.GetBytes(orig_string);

            byte[] codBytes = Encoding.Convert(unicode, cod, unicodeBytes);

            char[] codChars = new char[cod.GetCharCount(codBytes, 0, codBytes.Length)];
            cod.GetChars(codBytes, 0, codBytes.Length, codChars, 0);
            string codString = new string(codChars);
            return codString;

        }

        private string Convertor(string asciiString)
        {
            string test = System.Text.Encoding.Default.GetString(
                System.Text.Encoding.Default.GetBytes("52756E6773616E204C657873696E67746F"));

            return test;

            //byte[] asciiBytes = ascii.GetBytes(asciiString);
            //byte[] utf8Bytes = System.Text.Encoding.Convert(ascii, utf8,
            //asciiBytes);

            //char[] utf8Chars = new char[utf8.GetCharCount(utf8Bytes, 0,
            //utf8Bytes.Length)];
            //utf8.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0);
            //string utf8String = new string(utf8Chars);

            //return utf8String;


            //byte[] a = new byte[asciiString.Length];
            //char[] s = new char[asciiString.Length];
    
            //int i;

            //s = asciiString.ToCharArray();

            //for (i = 0; i <= asciiString.Length; i++)
            //{
            //        a[i] = (byte)s[i];
            //}
 
            //UTF8Encoding.Convert(Encoding.ASCII, Encoding.UTF8, a);
            //return a.ToString();
        }

        private string ConvertDecode(string inputString)
        {
            //Encoding cod = Encoding.GetEncoding(500);
            //string asciiString = inputString;
            //// Create two different encodings.
            //Encoding ascii = Encoding.ASCII;
            //byte[] ASCIIBytes = ascii.GetBytes(inputString);
            //// Perform the conversion from one encoding to the other.			 
            //byte[] EBCDIC = Encoding.Convert(ascii, cod, ASCIIBytes);
            //return ascii.GetString(EBCDIC);

            //ASCIIEncoding ascii = new ASCIIEncoding();
            //byte[] byteArray = Encoding.UTF7.GetBytes(inputString);
            //byte[] asciiArray = Encoding.Convert(Encoding.UTF7, Encoding.ASCII, byteArray);
            //return ascii.GetString(asciiArray);

            UnicodeEncoding uni = new UnicodeEncoding();
            byte[] byteArray = Encoding.ASCII.GetBytes(inputString);
            byte[] uniArray = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, byteArray);
            return uni.GetString(uniArray);

            //ASCIIEncoding ascii = new ASCIIEncoding();
            //byte[] byteArray = Encoding.Unicode.GetBytes(inputString);
            //byte[] asciiArray = Encoding.Convert(Encoding.ASCII, Encoding.Unicode, byteArray);
            //return ascii.GetString(asciiArray);


        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string[] dirs;
            using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
            {
                if (unc.NetUseWithCredentials(tbUNCPath.Text,
                                              tbUserName.Text,
                                              tbDomain.Text,
                                              tbPassword.Text))
                {
                    dirs = Directory.GetDirectories(tbUNCPath.Text);
                    //foreach (string d in dirs)
                    //{
                    //    tbDirList.Text += d + "\r\n";
                    //}
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Failed to connect to " + tbUNCPath.Text + "\r\nLastError = " + unc.LastError.ToString(),
                                    "Failed to connect",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                

            }
            this.Cursor = Cursors.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowsIdentity identi = WindowsIdentity.GetCurrent();
            this.tbUserName.Text = identi.Name;
        }

    }
}
