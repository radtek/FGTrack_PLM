using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HTN.BITS.QRCodeLib;
using System.Data;
using System.ComponentModel;
using Oracle.DataAccess.Types;
using System.Threading;
using System.IO;
using System.Reflection;

namespace HTN.BITS.BLL.PLASESS
{
    public class UtilityBLL
    {
        public static Bitmap QRCode_Encode(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
            {
                return null;
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
                return null;
            }
            try
            {
                int version = 4;
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                return null;
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

            return qrCodeEncoder.Encode(serialNo);
        }

        public static DataTable ListToDataTable<T>(IList<T> data, string tableName)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(tableName);
            Type columnType;
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                columnType = prop.PropertyType;
                if (columnType.IsGenericType)
                {
                    columnType = columnType.GetGenericArguments()[0];
                }

                if (columnType.Namespace.Equals("System") || columnType.Namespace.Equals("System.Drawing")) //!System.Collections.Generic
                {
                    table.Columns.Add(prop.Name, columnType);
                }
            }

            if (data != null)
            {
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in props)
                    {
                        columnType = prop.PropertyType;
                        if (columnType.Namespace.Equals("System") || columnType.Namespace.Equals("System.Drawing")) //!System.Collections.Generic
                        {
                            object obj = prop.GetValue(item);
                            row[prop.Name] = (obj == null ? DBNull.Value : obj);
                        }
                    }

                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static string GetReturnRawData(OracleBinary oraValue)
        {
            string resultString = System.Text.Encoding.Default.GetString(oraValue.Value, 0, oraValue.Value.Length);
            return resultString;
        }

    }
}
