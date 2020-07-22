using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace HTN.BITS.SQLITE.DAL
{
    public class SQLiteParam
    {

        private SQLiteParameter[] param;

        public SQLiteParam(int arrSize)
        {
            param = new SQLiteParameter[arrSize];
        }

        private string commandText;
        public string CommandText
        {
            get
            {
                return this.commandText;
            }

            set
            {
                this.commandText = value;
            }
        }

        public void ParamAnsiString(int index, string paraName, string value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.AnsiString) { Value = value };
        }

        public void ParamAnsiStringFixedLength(int index, string paraName, string value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.AnsiStringFixedLength) { Value = value };
        }

        public void ParamBinary(int index, string paraName, byte[] value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Binary) { Value = value };
        }

        public void ParamBoolean(int index, string paraName, bool value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Boolean) { Value = value };
        }

        public void ParamByte(int index, string paraName, byte value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Byte) { Value = value };
        }

        public void ParamCurrency(int index, string paraName, decimal value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Currency) { Value = value };
        }

        public void ParamDate(int index, string paraName, DateTime value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Date) { Value = value };
        }

        public void ParamDateTime(int index, string paraName, DateTime value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.DateTime) { Value = value };
        }

        public void ParamDateTime2(int index, string paraName, DateTime value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.DateTime2) { Value = value };
        }

        public void ParamDateTimeOffset(int index, string paraName, DateTime value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.DateTimeOffset) { Value = value };
        }

        public void ParamDecimal(int index, string paraName, decimal value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Decimal) { Value = value };
        }

        public void ParamDouble(int index, string paraName, double value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Double) { Value = value };
        }

        public void ParamGuid(int index, string paraName, Guid value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Guid) { Value = value };
        }

        public void ParamInt16(int index, string paraName, System.Int16 value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Int16) { Value = value };
        }

        public void ParamInt32(int index, string paraName, System.Int32 value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Int32) { Value = value };
        }

        public void ParamInt64(int index, string paraName, System.Int64 value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Int64) { Value = value };
        }

        public void ParamObject(int index, string paraName, object value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Object) { Value = value };
        }

        public void ParamSByte(int index, string paraName, sbyte value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.SByte) { Value = value };
        }

        public void ParamSingle(int index, string paraName, decimal value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Single) { Value = value };
        }

        public void ParamString(int index, string paraName, string value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.String) { Value = value };
        }

        public void ParamStringFixedLength(int index, string paraName, string value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.StringFixedLength) { Value = value };
        }

        public void ParamTime(int index, string paraName, DateTime value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Time) { Value = value };
        }

        public void ParamUInt16(int index, string paraName, uint value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.UInt16) { Value = value };
        }

        public void ParamUInt32(int index, string paraName, uint value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.UInt32) { Value = value };
        }

        public void ParamUInt64(int index, string paraName, uint value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.UInt64) { Value = value };
        }

        public void ParamVarNumeric(int index, string paraName, decimal value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.VarNumeric) { Value = value };
        }

        public void ParamXml(int index, string paraName, string value)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Xml) { Value = value };
        }

        public void ParamNull(int index, string paraName)
        {
            param[index] = new SQLiteParameter(paraName, DbType.Object) { Value = DBNull.Value };
        }

        public SQLiteParameter[] Parameters
        {
            get
            {
                return this.param;
            }
        }


    }
}
