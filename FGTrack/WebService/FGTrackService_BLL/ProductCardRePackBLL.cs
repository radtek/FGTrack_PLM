using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace FGTrackService.BLL
{
    public class ProductCardRePackBLL : IDisposable
    {
        public ProductCardRePackBLL()
        {
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                {
                    //this.CloseConnection();
                }
            }

            IsDisposed = true;
        }

        ~ProductCardRePackBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        private TimeSpan executionTime;

        #endregion

        #region Property Member

        public TimeSpan ExecutionTime
        {
            get
            {
                return this.executionTime;
            }
        }

        #endregion

        #region Method Member

        public PalletList GetPalletList(string pickno, out string resultMessage) //
        {
            resultMessage = string.Empty;

            PalletList lstPallet = null;
            Pallet pallet = null;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_FG.GET_PALLET_LIST" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strPICK_NO", pickno);
                procPara.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);
                

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPallet = new PalletList();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 100;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pallet = new Pallet();

                        pallet.PALLET_NO = OraDataReader.Instance.GetString("PALLET_NO");
                        pallet.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        pallet.PALLET_SEQ = OraDataReader.Instance.GetInteger("PALLET_SEQ");
                        pallet.PALLET_TOTAL = OraDataReader.Instance.GetInteger("PALLET_TOTAL");
                        pallet.PALLET_STATUS = OraDataReader.Instance.GetString("PALLET_STATUS");
                        pallet.PALLET_BOX = OraDataReader.Instance.GetInteger("PALLET_BOX");
                        pallet.PALLET_PCS = OraDataReader.Instance.GetInteger("PALLET_PCS");

                        lstPallet.Add(pallet);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                OracleString resultDB = (OracleString)procPara.ReturnValue(2);
                if (!resultDB.IsNull)
                {
                    resultMessage = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                lstPallet = null;
                throw ex;
            }

            return lstPallet;
        }

        public Pallet GetPalletDetail(string palletno)
        {
            Pallet pallet = null;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_FG.GET_PALLET_DETAIL" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strPALLET_NO", palletno);


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 100;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pallet = new Pallet();

                        pallet.PALLET_NO = OraDataReader.Instance.GetString("PALLET_NO");
                        pallet.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        pallet.PALLET_SEQ = OraDataReader.Instance.GetInteger("PALLET_SEQ");
                        pallet.PALLET_TOTAL = OraDataReader.Instance.GetInteger("PALLET_TOTAL");
                        pallet.PALLET_STATUS = OraDataReader.Instance.GetString("PALLET_STATUS");
                        pallet.PALLET_BOX = OraDataReader.Instance.GetInteger("PALLET_BOX");
                        pallet.PALLET_PCS = OraDataReader.Instance.GetInteger("PALLET_PCS");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                pallet = null;
                throw ex;
            }

            return pallet;
        }

        public ProductCard GetUpdatePCRepack(string palletno, string serialNo, string userid, out string resultMessage) //
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_FG.GET_UPD_PC_PALLET" };
                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strPALLET_NO", palletno);
                procPara.AddParamInput(2, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }
                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(3);
                    if (!result.IsNull)
                    {
                        resultMessage = result.Value;
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pcCard;
        }

        public string UpdatePalletFinish(string palletno, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_FG.UPD_PALLET_FINISHED" };
                procPara.AddParamInput(0, "strPALLET_NO", palletno);
                procPara.AddParamOutput(1, "RESULTMSG", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(2, "strUSER_ID", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(1);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        #endregion
    }
}
