using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using HTN.BITS.BEL.PLASESS;
using Oracle.DataAccess.Types;
using System.Data;

namespace HTN.BITS.BLL.PLASESS
{
    public class AdministratorBLL : IDisposable
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

        ~AdministratorBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        ////private TimeSpan executionTime;
        private string userid;

        #endregion

        #region Property Member

        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}

        public string UserID
        {
            get
            {
                return this.userid;
            }
        }

        #endregion

        public bool UserAuthentication(string userName, string password)
        {
            this.userid = string.Empty;
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(3);

                procPara.ProcedureName = "ADMINISTRATOR_PACK.USER_LOGON";

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strVal", userName);
                procPara.AddParamInput(2, "strPwd", password);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.Parameters[0].Value;
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                //byte[] resultByte = this.GetCurrectByteArray(objResult);
                //result = System.Text.Encoding.Default.GetString(resultByte, 0, resultByte.Length);

                if (!objResult.IsNull)
                {
                    result = Encoding.Default.GetString(objResult.Value, 0, objResult.Length);
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (result != "")
            {
                //Authentication pass
                this.userid = result;
                return true;
            }
            else
            {
                //Authentication fail
                return false;
            }
            
        }

        public bool ManagerAuthenRollback(string userName, string password, string remark)
        {
            this.userid = string.Empty;
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "ADMINISTRATOR_PACK.MANAGER_AUTHEN" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUid", userName);
                procPara.AddParamInput(2, "strPwd", password);
                procPara.AddParamInput(3, "strRemark", remark);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                //byte[] resultByte = this.GetCurrectByteArray(objResult);
                //result = System.Text.Encoding.Default.GetString(resultByte, 0, resultByte.Length);
                if (!objResult.IsNull)
                {
                    result = Encoding.Default.GetString(objResult.Value, 0, objResult.Length);
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (result != "")
            {
                //Authentication pass
                this.userid = result;
                return true;
            }
            else
            {
                //Authentication fail
                return false;
            }

        }

        public string UserChangePassword(string password, string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "ADMINISTRATOR_PACK.USER_CHANGE_PASSWORD";

                param.AddParamInput(0, "strUSER_ID", userid);
                param.AddParamInput(1, "strNewPwd", password);
                param.AddParamOutput(2, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(2);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private byte[] GetCurrectByteArray(OracleBinary oraValue)
        {
            string test = string.Empty;
            int accLength = oraValue.Value.Length / 2;
            byte[] result = new byte[accLength];
            int j = 0;
            for(int i = 0;i<oraValue.Value.Length;i++)
            {
                if (!oraValue.Value[i].Equals(0))
                {
                    result[j] = oraValue.Value[i];
                    j++;
                }
            }

            return result;
        }

        public List<MenuAuthentication> GetMenuAuthentication(string userId)
        {
            List<MenuAuthentication> menuAuthenList = null;
            MenuAuthentication menuAuthen;
            try
            {
                ProcParam procPara = new ProcParam(2);

                procPara.ProcedureName = "ADMINISTRATOR_PACK.GETMENUAUTHENTICATION";
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strUserId", userId);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    menuAuthenList = new List<MenuAuthentication>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        menuAuthen = new MenuAuthentication();

                        menuAuthen.ProgramType = OraDataReader.Instance.GetString("PROG_TYPE");
                        menuAuthen.ProgramTypeName = OraDataReader.Instance.GetString("PROG_TYPE_NAME");
                        menuAuthen.ProgramTypeImage = OraDataReader.Instance.GetString("PROG_TYPE_ICON");
                        menuAuthen.ProgramID = OraDataReader.Instance.GetString("PROG_ID");
                        menuAuthen.ProgramKey = OraDataReader.Instance.GetString("PROG_KEY");
                        menuAuthen.ProgramName = OraDataReader.Instance.GetString("PROG_NAME");
                        menuAuthen.IconImage = OraDataReader.Instance.GetString("ICON");

                        menuAuthenList.Add(menuAuthen);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                
            }
            catch (Exception ex)
            {
                menuAuthenList = null;
            }

            return menuAuthenList;
        }

        #region "Program Type"

        public List<ProgramType> GetProgramTypeList(string procTypeID)
        {
            List<ProgramType> lstProcType = null;
            ProgramType procType;
            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_PROGRAM_TYPE_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPROG_TYPE", procTypeID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstProcType = new List<ProgramType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        //Console.WriteLine(reader.GetInt32(0) + ", " + myReader.GetString(1));
                        procType = new ProgramType();

                        procType.PROG_TYPE = OraDataReader.Instance.GetString("PROG_TYPE");
                        procType.PROG_TYPE_NAME = OraDataReader.Instance.GetString("PROG_TYPE_NAME");
                        procType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        procType.ORDER_BY = OraDataReader.Instance.GetInteger("ORDER_BY");
                        procType.ICON = OraDataReader.Instance.GetString("ICON");

                        lstProcType.Add(procType);

                    }
                
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProcType = null;
            }

            return lstProcType;
        }

        public DataTable GetProgramType(string procTypeID)
        {
            List<ProgramType> lstProcType = null;
            ProgramType procType;
            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_PROGRAM_TYPE_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPROG_TYPE", procTypeID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

               // //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstProcType = new List<ProgramType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        //Console.WriteLine(reader.GetInt32(0) + ", " + myReader.GetString(1));
                        procType = new ProgramType();

                        procType.PROG_TYPE = OraDataReader.Instance.GetString("PROG_TYPE");
                        procType.PROG_TYPE_NAME = OraDataReader.Instance.GetString("PROG_TYPE_NAME");
                        procType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        procType.ORDER_BY = OraDataReader.Instance.GetInteger("ORDER_BY");
                        procType.ICON = OraDataReader.Instance.GetString("ICON");

                        lstProcType.Add(procType);

                    }
                    
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProcType = null;
            }

            //return lstProcType;
            return UtilityBLL.ListToDataTable(lstProcType, "M_PROGRAM_TYPE");
        }

        public string InsertProgramType(ProgramType procType, string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(7);

                param.ProcedureName = "ADMINISTRATOR_PACK.PROGRAM_TYPE_INS";

                param.AddParamInput(0, "strPROG_TYPE", procType.PROG_TYPE);
                param.AddParamInput(1, "strPROG_TYPE_NAME", procType.PROG_TYPE_NAME);
                param.AddParamInput(2, "strREC_STAT", (procType.REC_STAT ? "Y" : "N"));
                param.AddParamInput(3, "strORDER_BY", procType.ORDER_BY);
                param.AddParamInput(4, "strICON", procType.ICON);
                param.AddParamInput(5, "strUSER_ID", userid);

                param.AddParamOutput(6, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(6);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string UpdateProgramType(ProgramType procType, string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(7);

                param.ProcedureName = "ADMINISTRATOR_PACK.PROGRAM_TYPE_UPD";

                param.AddParamInput(0, "strPROG_TYPE", procType.PROG_TYPE);
                param.AddParamInput(1, "strPROG_TYPE_NAME", procType.PROG_TYPE_NAME);
                param.AddParamInput(2, "strREC_STAT", (procType.REC_STAT ? "Y" : "N"));
                param.AddParamInput(3, "strORDER_BY", procType.ORDER_BY);
                param.AddParamInput(4, "strICON", procType.ICON);
                param.AddParamInput(5, "strUSER_ID", userid);

                param.AddParamOutput(6, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(6);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion "Program Type"

        #region "Program"

        public List<M_Program> GetProgramList(string procType, string procID)
        {
            List<M_Program> lstProgram = null;
            M_Program mProgram;

            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_PROGRAM_LIST";

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPROG_TYPE", procType);
                param.AddParamInput(2, "strPROG_ID", procID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstProgram = new List<M_Program>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mProgram = new M_Program();

                        mProgram.PROG_ID = OraDataReader.Instance.GetString("PROG_ID");
                        mProgram.PROG_NAME = OraDataReader.Instance.GetString("PROG_NAME");
                        mProgram.PROG_TYPE = OraDataReader.Instance.GetString("PROG_TYPE");
                        if (!OraDataReader.Instance.IsDBNull("ORDER_BY"))
                        {
                            mProgram.ORDER_BY = OraDataReader.Instance.GetInteger("ORDER_BY");
                        }
                        mProgram.ICON = OraDataReader.Instance.GetString("ICON");
                        mProgram.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        mProgram.DESCRIPTION = OraDataReader.Instance.GetString("DESCRIPTION");
                        mProgram.PROG_KEY = OraDataReader.Instance.GetString("PROG_KEY");

                        lstProgram.Add(mProgram);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
                lstProgram = null;
            }

            return lstProgram;
        }

        public M_Program GetProgramByID(string procID)
        {
            M_Program mProgram = null;

            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_PROGRAM_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPROG_TYPE", string.Empty);
                param.AddParamInput(2, "strPROG_ID", procID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mProgram = new M_Program();

                        mProgram.PROG_ID = OraDataReader.Instance.GetString("PROG_ID");
                        mProgram.PROG_NAME = OraDataReader.Instance.GetString("PROG_NAME");
                        mProgram.PROG_TYPE = OraDataReader.Instance.GetString("PROG_TYPE");
                        if (!OraDataReader.Instance.IsDBNull("ORDER_BY"))
                        {
                            mProgram.ORDER_BY = OraDataReader.Instance.GetInteger("ORDER_BY");
                        }
                        mProgram.ICON = OraDataReader.Instance.GetString("ICON");
                        mProgram.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        mProgram.DESCRIPTION = OraDataReader.Instance.GetString("DESCRIPTION");
                        mProgram.PROG_KEY = OraDataReader.Instance.GetString("PROG_KEY");
                    }

                    
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                mProgram = null;
            }

            return mProgram;
        }

        public string InsertProgram(M_Program proc, string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(10);

                param.ProcedureName = "ADMINISTRATOR_PACK.PROGRAM_INS";

                param.AddParamInput(0, "strPROG_ID", proc.PROG_ID);
                param.AddParamInput(1, "strPROG_NAME", proc.PROG_NAME);
                param.AddParamInput(2, "strPROG_TYPE", proc.PROG_TYPE);
                param.AddParamInput(3, "strORDER_BY", proc.ORDER_BY);
                param.AddParamInput(4, "strICON", proc.ICON);
                param.AddParamInput(5, "strREC_STAT", (proc.REC_STAT ? "Y" : "N"));
                param.AddParamInput(6, "strDESCRIPTION", proc.DESCRIPTION);
                param.AddParamInput(7, "strPROG_KEY", proc.PROG_KEY);
                param.AddParamInput(8, "strUSER_ID", userid);

                param.AddParamOutput(9, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(9);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string UpdateProgram(M_Program proc, string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(10);

                param.ProcedureName = "ADMINISTRATOR_PACK.PROGRAM_UPD";

                param.AddParamInput(0, "strPROG_ID", proc.PROG_ID);
                param.AddParamInput(1, "strPROG_NAME", proc.PROG_NAME);
                param.AddParamInput(2, "strPROG_TYPE", proc.PROG_TYPE);
                param.AddParamInput(3, "strORDER_BY", proc.ORDER_BY);
                param.AddParamInput(4, "strICON", proc.ICON);
                param.AddParamInput(5, "strREC_STAT", (proc.REC_STAT ? "Y" : "N"));
                param.AddParamInput(6, "strDESCRIPTION", proc.DESCRIPTION);
                param.AddParamInput(7, "strPROG_KEY", proc.PROG_KEY);
                param.AddParamInput(8, "strUSER_ID", userid);

                param.AddParamOutput(9, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(9);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion "Program"

        #region "Roles"

        public List<Role> GetRoleList()
        {
            List<Role> lstRole = null;
            Role role;
            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_ROLE_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strROLE_ID", string.Empty);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstRole = new List<Role>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        //Console.WriteLine(reader.GetInt32(0) + ", " + myReader.GetString(1));
                        role = new Role();

                        role.ROLE_ID = OraDataReader.Instance.GetString("ROLE_ID");
                        role.ROLE_NAME = OraDataReader.Instance.GetString("ROLE_NAME");
                        role.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstRole.Add(role);

                    }
                    
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
                lstRole = null;
            }

            return lstRole;
        }

        public DataTable GetRoles(string roleID)
        {
            List<Role> lstRole = null;
            Role role;
            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_ROLE_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strROLE_ID", roleID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstRole = new List<Role>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        role = new Role();

                        role.ROLE_ID = OraDataReader.Instance.GetString("ROLE_ID");
                        role.ROLE_NAME = OraDataReader.Instance.GetString("ROLE_NAME");
                        role.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstRole.Add(role);

                    }
                    
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
                lstRole = null;
            }

            return UtilityBLL.ListToDataTable(lstRole, "M_ROLE");
        }

        public DataTable GetRoleProgramByRole(string roleID)
        {
            List<RoleProgram> lstRoleProg = null;

            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_ROLE_PROGRAM_LIST";

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strROLE_ID", roleID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstRoleProg = new List<RoleProgram>();
                    RoleProgram roleProg;

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        roleProg = new RoleProgram();

                        roleProg.ROLE_ID = OraDataReader.Instance.GetString("ROLE_ID");
                        roleProg.PROG_ID = OraDataReader.Instance.GetString("PROG_ID");
                        roleProg.PROG_NAME = OraDataReader.Instance.GetString("PROG_NAME");
                        roleProg.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        roleProg.FLAG = OraDataReader.Instance.GetInteger("FLAG");

                        lstRoleProg.Add(roleProg);

                    }
                    // always call Close when done reading.
                    OraDataReader.Instance.Close();
                }
            }
            catch (Exception ex)
            {
                lstRoleProg = null;
            }

            return UtilityBLL.ListToDataTable(lstRoleProg, "M_ROLE_PROGRAM");
        }

        public string InsertRole(Role role, string userid)
        {
            string result = string.Empty;
            try
            {
                ProcParam param = new ProcParam(5);

                param.ProcedureName = "ADMINISTRATOR_PACK.ROLE_INS";

                param.AddParamInput(0, "strROLE_ID", role.ROLE_ID);
                param.AddParamInput(1, "strROLE_NAME", role.ROLE_NAME);
                param.AddParamInput(2, "strREC_STAT", (role.REC_STAT ? "Y" : "N"));
                param.AddParamInput(3, "strUSER_ID", userid);

                param.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(4);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string UpdateRole(Role role, string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(5);

                param.ProcedureName = "ADMINISTRATOR_PACK.ROLE_UPD";

                param.AddParamInput(0, "strROLE_ID", role.ROLE_ID);
                param.AddParamInput(1, "strROLE_NAME", role.ROLE_NAME);
                param.AddParamInput(2, "strREC_STAT", (role.REC_STAT ? "Y" : "N"));
                param.AddParamInput(3, "strUSER_ID", userid);

                param.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(4);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public void UpdateRoleProgram(List<RoleProgram> lstRoleProgUpd, string userid)
        {
            try
            {
                List<ProcParam> paraList = new List<ProcParam>();
                ProcParam procDetail = null;

                foreach (RoleProgram roleProg in lstRoleProgUpd)
                {
                    procDetail = new ProcParam(6);
                    procDetail.ProcedureName = "ADMINISTRATOR_PACK.ROLE_PROGRAM_UPD";

                    procDetail.AddParamInput(0, "strROLE_ID", roleProg.ROLE_ID);
                    procDetail.AddParamInput(1, "strPROG_ID", roleProg.PROG_ID);
                    procDetail.AddParamInput(2, "strREP_ID", "001");  //DEFAULT FOR REPORT ID '001'
                    procDetail.AddParamInput(3, "strREC_STAT ", (roleProg.REC_STAT ? "Y" : "N"));
                    procDetail.AddParamInput(4, "strUSER_ID", userid);
                    procDetail.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255, "OK");

                    paraList.Add(procDetail);
                }

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paraList);
                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "Roles"

        #region "User"

        public List<User> GetUserList(string userID)
        {
            List<User> lstUser = null;
            User user;

            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_USER_LIST";

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strUSER_ID", userID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstUser = new List<User>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        //Console.WriteLine(reader.GetInt32(0) + ", " + myReader.GetString(1));
                        user = new User();

                        user.USER_ID = OraDataReader.Instance.GetString("USER_ID");
                        user.USER_NAME = OraDataReader.Instance.GetString("USER_NAME");
                        user.WARE_ID = OraDataReader.Instance.GetString("WARE_ID");
                        user.LOGIN = OraDataReader.Instance.GetString("LOGIN");
                        user.PWD = OraDataReader.Instance.GetString("PWD");
                        user.EMPLOYEE_ID = OraDataReader.Instance.GetString("EMPLOYEE_ID");
                        user.ROLE_ID = OraDataReader.Instance.GetString("ROLE_ID");
                        user.REMARK = OraDataReader.Instance.GetString("REMARK");
                        user.REC_STAT = OraDataReader.Instance.GetString("REC_STAT").ToUpper().Equals("Y");
                        user.EMAIL = OraDataReader.Instance.GetString("EMAIL");
                        user.COMP_ID = OraDataReader.Instance.GetString("COMP_ID");

                        lstUser.Add(user);

                    }
                    
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstUser = null;
            }

            return lstUser;
        }

        public User GetUserByUserID(string userID)
        {
            User user = null;

            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_USER_LIST";

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strUSER_ID", userID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);
                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        //Console.WriteLine(reader.GetInt32(0) + ", " + myReader.GetString(1));
                        user = new User();

                        user.USER_ID = OraDataReader.Instance.GetString("USER_ID");
                        user.USER_NAME = OraDataReader.Instance.GetString("USER_NAME");
                        user.WARE_ID = OraDataReader.Instance.GetString("WARE_ID");
                        user.LOGIN = OraDataReader.Instance.GetString("LOGIN");
                        user.PWD = OraDataReader.Instance.GetString("PWD");
                        user.EMPLOYEE_ID = OraDataReader.Instance.GetString("EMPLOYEE_ID");
                        user.ROLE_ID = OraDataReader.Instance.GetString("ROLE_ID");
                        user.REMARK = OraDataReader.Instance.GetString("REMARK");
                        user.REC_STAT = OraDataReader.Instance.GetString("REC_STAT").ToUpper().Equals("Y");
                        user.EMAIL = OraDataReader.Instance.GetString("EMAIL");
                        user.COMP_ID = OraDataReader.Instance.GetString("COMP_ID");

                    }
                    
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
                user = null;
            }

            return user;
        }

        public DataTable GetUserRoleByUserID(string userID)
        {
            List<UserRole> lstUserRole = null;

            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.GET_USER_ROLE_LIST";

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strUSER_ID", userID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstUserRole = new List<UserRole>();
                    UserRole userRole;
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        userRole = new UserRole();

                        userRole.ROLE_ID = OraDataReader.Instance.GetString("ROLE_ID");
                        userRole.ROLE_NAME = OraDataReader.Instance.GetString("ROLE_NAME");
                        userRole.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        userRole.FLAG = OraDataReader.Instance.GetInteger("FLAG");

                        lstUserRole.Add(userRole);

                    }
                    
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstUserRole = null;
            }

            return UtilityBLL.ListToDataTable(lstUserRole, "M_ROLE_PROGRAM");
        }

        public string InsertUser(User user, string userid)
        {
            string result = string.Empty;
            try
            {
                ProcParam param = new ProcParam(13);

                param.ProcedureName = "ADMINISTRATOR_PACK.USER_INS";

                param.AddParamInput(0, "strUSER_ID", user.USER_ID);
                param.AddParamInput(1, "strUSER_NAME", user.USER_NAME);
                param.AddParamInput(2, "strWARE_ID", user.WARE_ID);
                param.AddParamInput(3, "strLOGIN", user.LOGIN);
                param.AddParamInput(4, "strPWD", user.PWD);
                param.AddParamInput(5, "strEMPLOYEE_ID", user.EMPLOYEE_ID);
                param.AddParamInput(6, "strROLE_ID", user.ROLE_ID);
                param.AddParamInput(7, "strREMARK", user.REMARK);
                param.AddParamInput(8, "strREC_STAT", (user.REC_STAT ? "Y" : "N"));
                param.AddParamInput(9, "strEMAIL", user.EMAIL);
                param.AddParamInput(10, "strCOMP_ID", user.COMP_ID);
                param.AddParamInput(11, "strUSER_ID", userid);

                param.AddParamOutput(12, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(12);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public string UpdateUser(User user, string userid)
        {
            string result = string.Empty;
            try
            {
                ProcParam param = new ProcParam(13);

                param.ProcedureName = "ADMINISTRATOR_PACK.USER_UPD";

                param.AddParamInput(0, "strUSER_ID", user.USER_ID);
                param.AddParamInput(1, "strUSER_NAME", user.USER_NAME);
                param.AddParamInput(2, "strWARE_ID", user.WARE_ID);
                param.AddParamInput(3, "strLOGIN", user.LOGIN);
                param.AddParamInput(4, "strPWD", user.PWD);
                param.AddParamInput(5, "strEMPLOYEE_ID", user.EMPLOYEE_ID);
                param.AddParamInput(6, "strROLE_ID", user.ROLE_ID);
                param.AddParamInput(7, "strREMARK", user.REMARK);
                param.AddParamInput(8, "strREC_STAT", (user.REC_STAT ? "Y" : "N"));
                param.AddParamInput(9, "strEMAIL", user.EMAIL);
                param.AddParamInput(10, "strCOMP_ID", user.COMP_ID);
                param.AddParamInput(11, "strUSER_ID", userid);

                param.AddParamOutput(12, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(12);

                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
                else
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public string UpdateUserRole(string userid, List<Role> lstUserRoles, string userAction)
        {
            string result = string.Empty;
            List<ProcParam> paraList = new List<ProcParam>();
            ProcParam procDetail = null;

            try
            {
                foreach (Role role in lstUserRoles)
                {
                    switch (role.FLAG)
                    {
                        case 0: //delete
                            procDetail = new ProcParam(4);
                            procDetail.ProcedureName = "ADMINISTRATOR_PACK.USER_ROLE_DEL";

                            procDetail.AddParamInput(0, "strUSER_ID", userid);
                            procDetail.AddParamInput(1, "strROLE_ID", role.ROLE_ID);
                            procDetail.AddParamInput(2, "strUSER_ID_AC", userAction);
                            procDetail.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            paraList.Add(procDetail);

                            break;
                        case 1: //no change
                            break;
                        case 2: //add new 

                            procDetail = new ProcParam(5);
                            procDetail.ProcedureName = "ADMINISTRATOR_PACK.USER_ROLE_INS";

                            procDetail.AddParamInput(0, "strUSER_ID", userid);
                            procDetail.AddParamInput(1, "strROLE_ID", role.ROLE_ID);
                            procDetail.AddParamInput(2, "strREC_STAT ", (role.REC_STAT ? "Y" : "N"));
                            procDetail.AddParamInput(3, "strUSER_ID_AC", userAction);


                            procDetail.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            paraList.Add(procDetail);

                            break;
                        case 3: // edit

                            procDetail = new ProcParam(5);
                            procDetail.ProcedureName = "ADMINISTRATOR_PACK.USER_ROLE_UPD";

                            procDetail.AddParamInput(0, "strUSER_ID", userid);
                            procDetail.AddParamInput(1, "strROLE_ID", role.ROLE_ID);
                            procDetail.AddParamInput(2, "strREC_STAT ", (role.REC_STAT ? "Y" : "N"));
                            procDetail.AddParamInput(3, "strUSER_ID_AC", userAction);


                            procDetail.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            paraList.Add(procDetail);

                            break;

                    }


                }

                if (paraList.Count != 0)
                {
                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paraList);
                    ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                }

                result = "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        #endregion "User"


        public string TEST_PROC_EXCEPTION(string val)
        {
            string result = string.Empty;

            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "ADMINISTRATOR_PACK.TEST_EXCEPTION";

                param.AddParamInput(0, "strNewPwd", val);
                param.AddParamOutput(1, "RESULTMSG", OracleDbType.NVarchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
