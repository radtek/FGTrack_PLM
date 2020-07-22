using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace HTN.BITS.BLL.PLASESS
{
    public sealed class PrintingBuilder : IDisposable
    {
        [ThreadStatic]
        static PrintingBuilder instance = null;

        private PrintingBuilder()
        {
            IsReleased = false;
        }

        public bool IsReleased { get; private set; }

        static PrintingBuilder()
        {
            instance = new PrintingBuilder();
        }

        public static PrintingBuilder Instance
        {
            get
            {
                if (instance == null) instance = new PrintingBuilder();
                return instance;
            }
        }

        public int GeneratePrintSEQ()
        {
            int printSeq = 0;

            try
            {
                ProcParam procSEQ = new ProcParam(1);
                procSEQ.ProcedureName = "GEN_NO.PRINT_SEQ";
                procSEQ.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 100);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procSEQ);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal result = (OracleDecimal)procSEQ.ReturnValue(0);
                if (!result.IsNull)
                {
                    printSeq = result.ToInt32();
                }
            }
            catch (Exception ex)
            {
                printSeq = 0;
            }

            return printSeq;
        }

        public void RemovePrintSEQ(int printSeq)
        {
            try
            {
                ProcParam procSEQ = new ProcParam(1);
                procSEQ.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_DEL";
                procSEQ.AddParamInput(0, "strSEQ_NO", printSeq);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procSEQ);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

            }
            catch (Exception ex)
            {
                printSeq = 0;
            }
            finally
            {
                Release();
            }
        }

        public void InsertTransactionPrint(ProcParam transPrint)
        {
            try
            {
                GlobalDB.Instance.DataAc.ExecuteNonQuery(transPrint);
                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                //nothing
            }
        }

        public void InsertTransactionPrint(ProcParam transPrint, int arrBindCount)
        {
            try
            {
                GlobalDB.Instance.DataAc.ExecuteNonQuery(transPrint, arrBindCount);
                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                //nothing
            }
        }

        public void InsertTransactionPrint(List<ProcParam> lstTransPrint)
        {
            try
            {
                GlobalDB.Instance.DataAc.ExecuteNonQuery(lstTransPrint);
                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                //nothing
            }
        }

        public DataTable PrintTableResult(string procedureName, int printSeq, string tableName)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = procedureName;
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", printSeq);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, tableName, 1000);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public DataTable PrintTableResult(string procedureName, string printNo, string tableName)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = procedureName;
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strNo", printNo);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, tableName, 1000);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public DataTable PrintTableResult(ProcParam param, string tableName)
        {
            DataTable dtResult = null;
            try
            {
                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, tableName, 1000);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        private void Release()
        {
            IsReleased = true;
            PrintingBuilder.instance = null;
        }

        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion
        ////private TimeSpan executionTime;
        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}
    }
}
