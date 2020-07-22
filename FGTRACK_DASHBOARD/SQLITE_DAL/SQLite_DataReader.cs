using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace HTN.BITS.SQLITE.DAL
{
    public sealed class SQLiteReader : IDisposable
    {
        //private static readonly OraDataReader instance;
        [ThreadStatic]
        static SQLiteReader instance = null;


        private SQLiteDataReader sqliteReader;

        private SQLiteReader()
        {
            IsReleased = false;
        }

        public bool IsReleased { get; private set; }

        static SQLiteReader()
        {
            instance = new SQLiteReader();
        }

        public static SQLiteReader Instance
        {
            get
            {
                if (instance == null) instance = new SQLiteReader();
                return instance;
            }
        }

        public SQLiteDataReader SLReader
        {
            get
            {
                return sqliteReader;
            }
            set
            {
                sqliteReader = value;
            }
        }

        public bool HasRows
        {
            get
            {
                return this.sqliteReader.HasRows;
            }
        }

        private int GetOrdinal(string fieldName)
        {
            return this.sqliteReader.GetOrdinal(fieldName);
        }

        public bool IsDBNull(string fieldName)
        {
            return this.sqliteReader.IsDBNull(GetOrdinal(fieldName));
        }

        public object GetValue(string fieldName)
        {
            return this.sqliteReader.GetValue(GetOrdinal(fieldName));
        }

        public DateTime GetDateTime(string fieldName)
        {
            return this.sqliteReader.GetDateTime(GetOrdinal(fieldName));
        }

        public string GetString(string fieldName)
        {
            try
            {
                int fieldIndex = GetOrdinal(fieldName);
                if (!this.sqliteReader.IsDBNull(fieldIndex))
                {
                    return this.sqliteReader.GetString(fieldIndex);
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public int GetInteger(string fieldName)
        {
            try
            {
                int fieldIndex = GetOrdinal(fieldName);
                if (!this.sqliteReader.IsDBNull(fieldIndex))
                {
                    return Convert.ToInt32(this.sqliteReader.GetValue(fieldIndex), NumberFormatInfo.InvariantInfo);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public double GetDouble(string fieldName)
        {
            try
            {
                int fieldIndex = GetOrdinal(fieldName);
                if (!this.sqliteReader.IsDBNull(fieldIndex))
                {
                    return Convert.ToDouble(this.sqliteReader.GetValue(fieldIndex), NumberFormatInfo.InvariantInfo);
                }
                else
                {
                    return 0.0D;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal GetDecimal(string fieldName)
        {
            try
            {
                int fieldIndex = GetOrdinal(fieldName);
                if (!this.sqliteReader.IsDBNull(fieldIndex))
                {
                    return Convert.ToDecimal(this.sqliteReader.GetValue(fieldIndex), NumberFormatInfo.InvariantInfo);
                }
                else
                {
                    return 0M;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Bitmap GetBitmap(string fieldName)
        {
            try
            {
                int fieldIndex = GetOrdinal(fieldName);
                if (!this.sqliteReader.IsDBNull(fieldIndex))
                {
                    Byte[] result = (Byte[])this.sqliteReader.GetValue(fieldIndex);

                    if (result != null)
                    {
                        return ByteArrayToBitmap(result);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Bitmap ByteArrayToBitmap(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Bitmap returnBitmap = (Bitmap)Bitmap.FromStream(ms);
            return returnBitmap;
        }

        public Image GetImage(string fieldName)
        {
            try
            {
                int fieldIndex = GetOrdinal(fieldName);
                if (!this.sqliteReader.IsDBNull(fieldIndex))
                {
                    Byte[] result = (Byte[])this.sqliteReader.GetValue(fieldIndex);

                    if (result != null)
                    {
                        return ByteArrayToImage(result);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public void Close()
        {
            this.sqliteReader.Close();
            Release();
        }

        private void Release()
        {
            IsReleased = true;
            this.sqliteReader = null;
            SQLiteReader.instance = null;
        }


        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion
    }
}
