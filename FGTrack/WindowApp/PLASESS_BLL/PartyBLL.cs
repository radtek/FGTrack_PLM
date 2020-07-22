using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace HTN.BITS.BLL.PLASESS
{
    public class PartyBLL : IDisposable
    {
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

        ~PartyBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        ////private TimeSpan executionTime;

        #endregion

        #region Property Member

        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}

        #endregion

        public PartyBLL()
        {
            //constructer
        }

        public List<Party> GetPartyList(string findValue)
        {
            List<Party> lstParty = null;
            Party party;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_PARTY";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPARTY_ID", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstParty = new List<Party>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        party = new Party();

                        party.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        party.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        party.PARTY_TYPE = OraDataReader.Instance.GetString("PARTY_TYPE");
                        party.ADD1 = OraDataReader.Instance.GetString("ADD1");
                        party.ADD2 = OraDataReader.Instance.GetString("ADD2");
                        party.ADD3 = OraDataReader.Instance.GetString("ADD3");
                        party.ADD4 = OraDataReader.Instance.GetString("ADD4");
                        party.TEL = OraDataReader.Instance.GetString("TEL");
                        party.FAX = OraDataReader.Instance.GetString("FAX");
                        party.EMAIL = OraDataReader.Instance.GetString("EMAIL");
                        party.PIC = OraDataReader.Instance.GetString("PIC"); 
                        party.REMARK = OraDataReader.Instance.GetString("REMARK");
                        party.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstParty.Add(party);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstParty = null;
                throw ex;
            }

            return lstParty;
        }

        public Party GetParty(string partyID)
        {
            Party party = null;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_PARTY";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strPARTY_ID", partyID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        party = new Party();

                        party.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        party.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        party.PARTY_TYPE = OraDataReader.Instance.GetString("PARTY_TYPE");
                        party.ADD1 = OraDataReader.Instance.GetString("ADD1");
                        party.ADD2 = OraDataReader.Instance.GetString("ADD2");
                        party.ADD3 = OraDataReader.Instance.GetString("ADD3");
                        party.ADD4 = OraDataReader.Instance.GetString("ADD4");
                        party.TEL = OraDataReader.Instance.GetString("TEL");
                        party.FAX = OraDataReader.Instance.GetString("FAX");
                        party.EMAIL = OraDataReader.Instance.GetString("EMAIL");
                        party.PIC = OraDataReader.Instance.GetString("PIC");
                        party.REMARK = OraDataReader.Instance.GetString("REMARK");
                        party.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                party = null;
                throw ex;
            }

            return party;
        }

        public List<Party> LovPratyList(string partyType, string findValue)
        {
            List<Party> lstPraty = null;
            Party party;

            try
            {
                ProcParam param = new ProcParam(4);

                param.ProcedureName = "LOV_PACK.GET_PARTY_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPARTY_ID", DBNull.Value);
                param.AddParamInput(3, "strPARTY_TYPE", partyType);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPraty = new List<Party>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        party = new Party();

                        party.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        party.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");

                        lstPraty.Add(party);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPraty = null;
                throw ex;
            }

            return lstPraty;
        }

        public string InsertParty(ref Party party, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(15);

                param.ProcedureName = "MASTER_PACK.M_PARTY_INS";

                param.AddParamInOutput(0, "strPARTY_ID", OracleDbType.Varchar2, 100, party.PARTY_ID);
                param.AddParamInput(1, "strPARTY_NAME", party.PARTY_NAME);
                param.AddParamInput(2, "strPARTY_TYPE", party.PARTY_TYPE);
                param.AddParamInput(3, "strADD1", party.ADD1);
                param.AddParamInput(4, "strADD2", party.ADD2);
                param.AddParamInput(5, "strADD3", party.ADD3);
                param.AddParamInput(6, "strADD4", party.ADD4);
                param.AddParamInput(7, "strTEL", party.TEL);
                param.AddParamInput(8, "strFAX", party.FAX);
                param.AddParamInput(9, "strEMAIL", party.EMAIL);
                param.AddParamInput(10, "strPIC", party.PIC);
                param.AddParamInput(11, "strREMARK", party.REMARK);
                param.AddParamInput(12, "strREC_STAT", (party.REC_STAT ? "Y" : "N"));
                param.AddParamInput(13, "strUSER_ID", userid);

                param.AddParamOutput(14, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(14);
                

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                    OracleString resultPartyID = (OracleString)param.ReturnValue(0);
                    if (!resultPartyID.IsNull)
                    {
                        party.PARTY_ID = resultPartyID.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateParty(Party party, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(15);

                param.ProcedureName = "MASTER_PACK.M_PARTY_UPD";

                param.AddParamInput(0, "strPARTY_ID", party.PARTY_ID);
                param.AddParamInput(1, "strPARTY_NAME", party.PARTY_NAME);
                param.AddParamInput(2, "strPARTY_TYPE", party.PARTY_TYPE);
                param.AddParamInput(3, "strADD1", party.ADD1);
                param.AddParamInput(4, "strADD2", party.ADD2);
                param.AddParamInput(5, "strADD3", party.ADD3);
                param.AddParamInput(6, "strADD4", party.ADD4);
                param.AddParamInput(7, "strTEL", party.TEL);
                param.AddParamInput(8, "strFAX", party.FAX);
                param.AddParamInput(9, "strEMAIL", party.EMAIL);
                param.AddParamInput(10, "strPIC", party.PIC);
                param.AddParamInput(11, "strREMARK", party.REMARK);
                param.AddParamInput(12, "strREC_STAT", (party.REC_STAT ? "Y" : "N"));
                param.AddParamInput(13, "strUSER_ID", userid);

                param.AddParamOutput(14, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(14);

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

        public string DeleteParty(string partyID, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "MASTER_PACK.M_PARTY_DEL";

                param.AddParamInput(0, "strPARTY_ID", partyID);
                param.AddParamInput(1, "strUSER_ID", userid);

                param.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(2);

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


        #region Party Product

        public List<Product> LOV_GetProductList(string partyid, string findall)
        {
            List<Product> lstProduct = null;
            Product prod;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.GET_PRODUCT_LIST_NOTIN_PARTY" };

                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strPARTY_ID", partyid);
                param.AddParamInput(2, "strFindAll", findall);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstProduct = new List<Product>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        //Console.WriteLine(reader.GetInt32(0) + ", " + myReader.GetString(1));
                        prod = new Product();

                        prod.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        prod.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prod.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");

                        lstProduct.Add(prod);

                    }

                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
                lstProduct = null;
            }

            return lstProduct;
        }

        public DataTable GetProductList(string partyid)
        {
            DataTable dtbResult;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MASTER_PACK.GET_PRATY_PRODUCT_LIST" };

                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strPARTY_ID", partyid);

                dtbResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

            }
            catch (Exception ex)
            {
                dtbResult = null;
            }

            return dtbResult;
        }

        public string UpdatePartyProduct(DataTable dtbPartyProduct, string userid)
        {
            string resultMsg = string.Empty;

            try
            {

                #region Transaction Detail Delete

                DataRow[] deleteRows = dtbPartyProduct.Select("[FLAG] = 0");
                ProcParam paramDel = null;

                if (deleteRows.Length > 0)
                {
                    paramDel = new ProcParam(4) { ProcedureName = "MASTER_PACK.PARTY_PRODUCT_DEL" };

                    //PARTY_ID
                    var arrPARTY_ID = (from DataRow row in deleteRows
                                       select row["PARTY_ID"]).ToArray();
                    paramDel.AddParamInput(0, "strPARTY_ID", arrPARTY_ID, OracleDbType.Varchar2);

                    //PROD_SEQ_NO
                    var arrPROD_SEQ_NO = (from DataRow row in deleteRows
                                          select row["PROD_SEQ_NO"]).ToArray();
                    paramDel.AddParamInput(1, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    //USER_ID
                    paramDel.AddParamInput(2, "strUSER_ID", ArrayOf<object>.Create(deleteRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramDel.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", deleteRows.Length);
                }

                #endregion

                #region Transaction Detail Insert



                DataRow[] insertRows = dtbPartyProduct.Select("[FLAG] = 2");
                ProcParam paramIns = null;

                if (insertRows.Length > 0)
                {

                    paramIns = new ProcParam(4) { ProcedureName = "MASTER_PACK.PARTY_PRODUCT_INS" };

                    //PARTY_ID
                    var arrPARTY_ID = (from DataRow row in insertRows
                                     select row["PARTY_ID"]).ToArray();
                    paramIns.AddParamInput(0, "strPARTY_ID", arrPARTY_ID, OracleDbType.Varchar2);

                    //PROD_SEQ_NO
                    var arrPROD_SEQ_NO = (from DataRow row in insertRows
                                      select row["PROD_SEQ_NO"]).ToArray();
                    paramIns.AddParamInput(1, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    //USER_ID
                    paramIns.AddParamInput(2, "strUSER_ID", ArrayOf<object>.Create(insertRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", insertRows.Length);
                }

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDel, deleteRows.Length, paramIns, insertRows.Length, null, -1);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                resultMsg = "OK";
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
