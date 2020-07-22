using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace HTN.BITS.DAL
{
    public class ProcParam
    {

        private OracleParameter[] param;

        public ProcParam(int arrSize)
        {
            param = new OracleParameter[arrSize];
        }

        private int chkIndex = -1;
        private string resultCheck;

        private string procName;
        public string ProcedureName
        {
            get
            {
                return this.procName;
            }

            set
            {
                this.procName = value;
            }
        }


        public int IndexToCheck
        {
            get
            {
                return this.chkIndex;
            }
        }

        public string MessageToCheck
        {
            get
            {
                return this.resultCheck;
            }
        }



        public void AddParamRefCursor(int index, string paraName)
        {
            param[index] = new OracleParameter(paraName, OracleDbType.RefCursor, ParameterDirection.Output);
        }

        public void AddParamInput(int index, string paraName, object value)
        {
            param[index] = new OracleParameter(paraName, value);
        }

        public void AddParamOutput(int index, string paraName, OracleDbType type, int size)
        {
            param[index] = new OracleParameter(paraName, type, size, DBNull.Value, ParameterDirection.Output);
        }

        public void AddParamOutput(int index, string paraName, OracleDbType type, int size, string messageCheck)
        {
            param[index] = new OracleParameter(paraName, type, size, DBNull.Value, ParameterDirection.Output);

            this.chkIndex = index;
            this.resultCheck = messageCheck;
        }

        public void AddParamReturn(int index, string paraName, OracleDbType type, int size)
        {
            param[index] = new OracleParameter(paraName, type, size, DBNull.Value, System.Data.ParameterDirection.ReturnValue);
        }

        public object ReturnValue(int index)
        {
            object result = param[index].Value;
            //param[0].Value = null;
            return result;
        }

        public OracleParameter[] Parameters
        {
            get
            {
                return this.param;
            }
        }


    }
}
