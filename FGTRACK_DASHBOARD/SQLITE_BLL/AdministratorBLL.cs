using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.SQLITE.DAL;
using System.Data;
using HTN.BITS.FGTDB.BEL;

namespace HTN.BITS.SQLITE.BLL
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
        private int userid;

        #endregion

        #region Property Member

        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}

        public int UserID
        {
            get
            {
                return this.userid;
            }
        }

        #endregion

        public bool UserAuthentication(string userlogin, string password, out int userid, out string empid)
        {
            bool result = false;
            userid = 0;
            empid = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(2) { CommandText = StoreProcedure.Instance.GetScript("CheckValidationUser") };

                param.ParamString(0, "@USER_LOGIN", userlogin);
                param.ParamString(1, "@USER_PASS", password);

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        userid = SQLiteReader.Instance.GetInteger("USER_ID");
                        empid = SQLiteReader.Instance.GetString("EMPLOYEE_ID");
                    }

                    result = true;
                }
                else
                    result = false;

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAllUsers()
        {
            try
            {
                string sql = "SELECT USER_ID, EMPLOYEE_ID, EMPLOYEE_NAME, USER_REMARK, IS_ONLINE, IS_ACTIVE FROM M_USER ORDER BY USER_ID;";

                DataTable dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(sql);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UserInsert(string empid, string empname, string userlogin, string password, string remark)
        {
            try
            {
                string sql = "INSERT INTO M_USER (EMPLOYEE_ID, EMPLOYEE_NAME, USER_LOGIN, USER_PASS, USER_REMARK) " +
                             "VALUES (@EMPLOYEE_ID, @EMPLOYEE_NAME, @USER_LOGIN, @USER_PASS, @USER_REMARK);";

                SQLiteParam param = new SQLiteParam(5) { CommandText = sql };

                param.ParamStringFixedLength(0, "@EMPLOYEE_ID", empid);
                param.ParamStringFixedLength(1, "@EMPLOYEE_NAME", empname);
                param.ParamStringFixedLength(2, "@USER_LOGIN", userlogin);
                param.ParamStringFixedLength(3, "@USER_PASS", password);
                param.ParamStringFixedLength(4, "@USER_REMARK", remark);

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                return rowAfect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUserIsOnline(int userid, bool isOnline)
        {
            try
            {
                string sql = string.Format("UPDATE M_USER SET IS_ONLINE = '{0}' WHERE USER_ID = @USER_ID;",
                    (isOnline ? "Y" : "N"));

                SQLiteParam param = new SQLiteParam(1) { CommandText = sql };

                param.ParamInt32(0, "@USER_ID", userid);

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MenuAuthentication> GetMenuAuthentication(int userId)
        {
            List<MenuAuthentication> menuAuthenList = null;
            MenuAuthentication menuAuthen;
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("GetMenuAuthentication") };

                param.ParamInt32(0, "@USER_ID", userId);

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    menuAuthenList = new List<MenuAuthentication>();

                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        menuAuthen = new MenuAuthentication();

                        menuAuthen.PROG_TYPE_ID = SQLiteReader.Instance.GetInteger("PROG_TYPE_ID");
                        menuAuthen.PROG_TYPE_NAME = SQLiteReader.Instance.GetString("PROG_TYPE_NAME");
                        menuAuthen.PG_ICON = SQLiteReader.Instance.GetString("PG_ICON");
                        menuAuthen.PROG_TYPE_RESOURCE = SQLiteReader.Instance.GetString("PROG_TYPE_RESOURCE");
                        menuAuthen.PROG_ID = SQLiteReader.Instance.GetInteger("PROG_ID");
                        menuAuthen.PROG_NAME = SQLiteReader.Instance.GetString("PROG_NAME");
                        menuAuthen.ICON = SQLiteReader.Instance.GetString("ICON");
                        menuAuthen.PROG_KEY = SQLiteReader.Instance.GetString("PROG_KEY");
                        menuAuthen.PROG_RESOURCE = SQLiteReader.Instance.GetString("PROG_RESOURCE");

                        menuAuthenList.Add(menuAuthen);
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();
            }
            catch (Exception ex)
            {
                menuAuthenList = null;
            }

            return menuAuthenList;
        }

        #region "User"

        public List<User> GetUserList()
        {
            List<User> lstUser = null;
            User user;

            try
            {
                string sql = StoreProcedure.Instance.GetScript("GetUserList");

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(sql);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                if (SQLiteReader.Instance.HasRows)
                {
                    lstUser = new List<User>();

                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        user = new User();

                        user.USER_ID = SQLiteReader.Instance.GetInteger("USER_ID");
                        user.EMPLOYEE_ID = SQLiteReader.Instance.GetString("EMPLOYEE_ID");
                        user.EMPLOYEE_NAME = SQLiteReader.Instance.GetString("EMPLOYEE_NAME");
                        user.USER_LOGIN = SQLiteReader.Instance.GetString("USER_LOGIN");
                        user.USER_PASS = SQLiteReader.Instance.GetString("USER_PASS");
                        user.USER_REMARK = SQLiteReader.Instance.GetString("USER_REMARK");
                        user.IS_ONLINE = SQLiteReader.Instance.GetString("IS_ONLINE").ToUpper().Equals("Y");
                        user.IS_ACTIVE = SQLiteReader.Instance.GetString("IS_ACTIVE").ToUpper().Equals("Y");

                        lstUser.Add(user);
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();

            }
            catch (Exception ex)
            {
                lstUser = null;
            }

            return lstUser;
        }

        public User GetUserByUserId(int userId)
        {
            User user = null;

            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("GetUserById") };

                param.ParamInt32(0, "@USER_ID", userId);

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        user = new User();

                        user.USER_ID = SQLiteReader.Instance.GetInteger("USER_ID");
                        user.EMPLOYEE_ID = SQLiteReader.Instance.GetString("EMPLOYEE_ID");
                        user.EMPLOYEE_NAME = SQLiteReader.Instance.GetString("EMPLOYEE_NAME");
                        user.USER_LOGIN = SQLiteReader.Instance.GetString("USER_LOGIN");
                        user.USER_PASS = SQLiteReader.Instance.GetString("USER_PASS");
                        user.USER_REMARK = SQLiteReader.Instance.GetString("USER_REMARK");
                        user.IS_ACTIVE = SQLiteReader.Instance.GetString("IS_ACTIVE").ToUpper().Equals("Y");
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();
            }
            catch (Exception ex)
            {
                user = null;
            }

            return user;
        }

        public List<UserProgram> GetUserProgramList(int userId)
        {
            List<UserProgram> lstUserProg = null;
            UserProgram userProg;

            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("GetUserProgramList") };

                param.ParamInt32(0, "@USER_ID", userId);

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    lstUserProg = new List<UserProgram>();

                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        userProg = new UserProgram();

                        userProg.USER_PROG_ID = SQLiteReader.Instance.GetInteger("USER_PROG_ID");
                        userProg.PG_ICON = SQLiteReader.Instance.GetString("PG_ICON");
                        userProg.PROG_TYPE_NAME = SQLiteReader.Instance.GetString("PROG_TYPE_NAME");
                        userProg.ICON = SQLiteReader.Instance.GetString("ICON");
                        userProg.PROG_ID = SQLiteReader.Instance.GetInteger("PROG_ID");
                        userProg.PROG_NAME = SQLiteReader.Instance.GetString("PROG_NAME");
                        userProg.IS_ACCESS = SQLiteReader.Instance.GetString("IS_ACCESS").ToUpper().Equals("Y");
                        userProg.IS_INSERT = SQLiteReader.Instance.GetString("IS_INSERT").ToUpper().Equals("Y");
                        userProg.IS_UPDATE = SQLiteReader.Instance.GetString("IS_UPDATE").ToUpper().Equals("Y");
                        userProg.IS_DELETE = SQLiteReader.Instance.GetString("IS_DELETE").ToUpper().Equals("Y");
                        userProg.IS_PRINT = SQLiteReader.Instance.GetString("IS_PRINT").ToUpper().Equals("Y");
                        userProg.CUSTOM1 = SQLiteReader.Instance.GetString("CUSTOM1");
                        userProg.IS_CUSTOM1 = SQLiteReader.Instance.GetString("IS_CUSTOM1").ToUpper().Equals("Y");
                        userProg.CUSTOM2 = SQLiteReader.Instance.GetString("CUSTOM2");
                        userProg.IS_CUSTOM2 = SQLiteReader.Instance.GetString("IS_CUSTOM2").ToUpper().Equals("Y");
                        userProg.CUSTOM3 = SQLiteReader.Instance.GetString("CUSTOM3");
                        userProg.IS_CUSTOM3 = SQLiteReader.Instance.GetString("IS_CUSTOM3").ToUpper().Equals("Y");
                        userProg.USER_ID = SQLiteReader.Instance.GetString("USER_ID");
                        userProg.FLAG = SQLiteReader.Instance.GetInteger("FLAG");

                        lstUserProg.Add(userProg);
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();

            }
            catch (Exception ex)
            {
                lstUserProg = null;
            }

            return lstUserProg;
            //return UtilityBLL.ListToDataTable(lstUserProg, "M_USER_PROGRAM");
        }

        public void InsertUserProgram(List<UserProgram> lstUserProg, int userid)
        {
            try
            {
                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (UserProgram userProg in lstUserProg)
                {
                    param = new SQLiteParam(13) { CommandText = StoreProcedure.Instance.GetScript("UserProgramInsert") };

                    param.ParamInt32(0, "@USER_ID", userid);
                    param.ParamInt32(1, "@PROG_ID", userProg.PROG_ID);
                    param.ParamStringFixedLength(2, "@IS_ACCESS", (userProg.IS_ACCESS ? "Y" : "N"));
                    param.ParamStringFixedLength(3, "@IS_INSERT", (userProg.IS_INSERT ? "Y" : "N"));
                    param.ParamStringFixedLength(4, "@IS_UPDATE", (userProg.IS_UPDATE ? "Y" : "N"));
                    param.ParamStringFixedLength(5, "@IS_DELETE", (userProg.IS_DELETE ? "Y" : "N"));
                    param.ParamStringFixedLength(6, "@IS_PRINT", (userProg.IS_PRINT ? "Y" : "N"));
                    param.ParamStringFixedLength(7, "@CUSTOM1", userProg.CUSTOM1);
                    param.ParamStringFixedLength(8, "@IS_CUSTOM1", (userProg.IS_CUSTOM1 ? "Y" : "N"));
                    param.ParamStringFixedLength(9, "@CUSTOM2", userProg.CUSTOM2);
                    param.ParamStringFixedLength(10, "@IS_CUSTOM2", (userProg.IS_CUSTOM2 ? "Y" : "N"));
                    param.ParamStringFixedLength(11, "@CUSTOM3", userProg.CUSTOM3);
                    param.ParamStringFixedLength(12, "@IS_CUSTOM3", (userProg.IS_CUSTOM3 ? "Y" : "N"));

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUserProgram(List<UserProgram> lstUserProg, int userid)
        {
            try
            {
                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (UserProgram userProg in lstUserProg)
                {
                    param = new SQLiteParam(12) { CommandText = StoreProcedure.Instance.GetScript("UserProgramUpdate") };

                    param.ParamStringFixedLength(0, "@IS_ACCESS", (userProg.IS_ACCESS ? "Y" : "N"));
                    param.ParamStringFixedLength(1, "@IS_INSERT", (userProg.IS_INSERT ? "Y" : "N"));
                    param.ParamStringFixedLength(2, "@IS_UPDATE", (userProg.IS_UPDATE ? "Y" : "N"));
                    param.ParamStringFixedLength(3, "@IS_DELETE", (userProg.IS_DELETE ? "Y" : "N"));
                    param.ParamStringFixedLength(4, "@IS_PRINT", (userProg.IS_PRINT ? "Y" : "N"));
                    param.ParamStringFixedLength(5, "@CUSTOM1", userProg.CUSTOM1);
                    param.ParamStringFixedLength(6, "@IS_CUSTOM1", (userProg.IS_CUSTOM1 ? "Y" : "N"));
                    param.ParamStringFixedLength(7, "@CUSTOM2", userProg.CUSTOM2);
                    param.ParamStringFixedLength(8, "@IS_CUSTOM2", (userProg.IS_CUSTOM2 ? "Y" : "N"));
                    param.ParamStringFixedLength(9, "@CUSTOM3", userProg.CUSTOM3);
                    param.ParamStringFixedLength(10, "@IS_CUSTOM3", (userProg.IS_CUSTOM3 ? "Y" : "N"));
                    param.ParamInt32(11, "@USER_PROG_ID", userProg.USER_PROG_ID);

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertUser(ref User user)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(6) { CommandText = StoreProcedure.Instance.GetScript("UserInsert") };

                param.ParamStringFixedLength(0, "@EMPLOYEE_ID", user.EMPLOYEE_ID);
                param.ParamStringFixedLength(1, "@EMPLOYEE_NAME", user.EMPLOYEE_NAME);
                param.ParamStringFixedLength(2, "@USER_LOGIN", user.USER_LOGIN);
                param.ParamStringFixedLength(3, "@USER_PASS", user.USER_PASS);
                param.ParamStringFixedLength(4, "@USER_REMARK", user.USER_REMARK);
                param.ParamStringFixedLength(5, "@IS_ACTIVE", (user.IS_ACTIVE ? "Y" : "N"));

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.DataAc.LastException != null)
                    throw GlobalSqliteDB.Instance.DataAc.LastException;

                object resultid = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT SEQ FROM sqlite_sequence WHERE NAME = 'M_USER'");

                if (resultid != null)
                    user.USER_ID = Convert.ToInt32(resultid);

                result = "OK";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public string UpdateUser(User user)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(7) { CommandText = StoreProcedure.Instance.GetScript("UserUpdate") };

                param.ParamStringFixedLength(0, "@EMPLOYEE_ID", user.EMPLOYEE_ID);
                param.ParamStringFixedLength(1, "@EMPLOYEE_NAME", user.EMPLOYEE_NAME);
                param.ParamStringFixedLength(2, "@USER_LOGIN", user.USER_LOGIN);
                param.ParamStringFixedLength(3, "@USER_PASS", user.USER_PASS);
                param.ParamStringFixedLength(4, "@USER_REMARK", user.USER_REMARK);
                param.ParamStringFixedLength(5, "@IS_ACTIVE", (user.IS_ACTIVE ? "Y" : "N"));
                param.ParamInt32(6, "@USER_ID", user.USER_ID);

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                result = "OK";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public string UserChangePassword(string newPass, int userid)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(2) { CommandText = StoreProcedure.Instance.GetScript("UserChangePassword") };

                param.ParamStringFixedLength(0, "@USER_PASS", newPass);
                param.ParamInt32(1, "@USER_ID", userid);

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                result = "OK";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        #endregion "User"

        #region "Program Group"

        public List<ProgramGroup> GetProgramGroupList()
        {
            List<ProgramGroup> lstProcGroup = null;
            ProgramGroup procGroup;

            try
            {
                string sql = StoreProcedure.Instance.GetScript("GetProgramGroupList");

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(sql);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                if (SQLiteReader.Instance.HasRows)
                {
                    lstProcGroup = new List<ProgramGroup>();

                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        procGroup = new ProgramGroup();

                        procGroup.PROG_TYPE_ID = SQLiteReader.Instance.GetInteger("PROG_TYPE_ID");
                        procGroup.PROG_TYPE_NAME = SQLiteReader.Instance.GetString("PROG_TYPE_NAME");
                        procGroup.ORDER_BY = SQLiteReader.Instance.GetInteger("ORDER_BY");
                        procGroup.ICON = SQLiteReader.Instance.GetString("ICON");
                        procGroup.DESCRIPTION = SQLiteReader.Instance.GetString("DESCRIPTION");
                        procGroup.PROG_TYPE_RESOURCE = SQLiteReader.Instance.GetString("PROG_TYPE_RESOURCE");
                        procGroup.IS_ACTIVE = (SQLiteReader.Instance.GetString("IS_ACTIVE").ToUpper() == "Y");


                        lstProcGroup.Add(procGroup);
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProcGroup = null;
            }

            return lstProcGroup;
        }

        public ProgramGroup GetProgramGroupById(int progGroupId)
        {
            ProgramGroup procGroup = null;
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("GetProgramGroupById") };

                param.ParamInt32(0, "@PROG_TYPE_ID", progGroupId);

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        procGroup = new ProgramGroup();

                        procGroup.PROG_TYPE_ID = SQLiteReader.Instance.GetInteger("PROG_TYPE_ID");
                        procGroup.PROG_TYPE_NAME = SQLiteReader.Instance.GetString("PROG_TYPE_NAME");
                        procGroup.ORDER_BY = SQLiteReader.Instance.GetInteger("ORDER_BY");
                        procGroup.ICON = SQLiteReader.Instance.GetString("ICON");
                        procGroup.DESCRIPTION = SQLiteReader.Instance.GetString("DESCRIPTION");
                        procGroup.PROG_TYPE_RESOURCE = SQLiteReader.Instance.GetString("PROG_TYPE_RESOURCE");
                        procGroup.IS_ACTIVE = (SQLiteReader.Instance.GetString("IS_ACTIVE").ToUpper() == "Y");
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();
            }
            catch (Exception ex)
            {
                procGroup = null;
            }

            return procGroup;
        }

        public string InsertProgramGroup(ref ProgramGroup procGroup)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(6) { CommandText = StoreProcedure.Instance.GetScript("ProgramGroupInsert") };

                param.ParamStringFixedLength(0, "@PROG_TYPE_NAME", procGroup.PROG_TYPE_NAME);
                param.ParamInt32(1, "@ORDER_BY", procGroup.ORDER_BY);
                param.ParamStringFixedLength(2, "@ICON", procGroup.ICON);
                param.ParamStringFixedLength(3, "@DESCRIPTION", procGroup.DESCRIPTION);
                param.ParamStringFixedLength(4, "@PROG_TYPE_RESOURCE", procGroup.PROG_TYPE_RESOURCE);
                param.ParamStringFixedLength(5, "@IS_ACTIVE", (procGroup.IS_ACTIVE ? "Y" : "N"));

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                object resultid = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT SEQ FROM sqlite_sequence WHERE NAME = 'M_PROGRAM_TYPE'");

                if (resultid != null)
                    procGroup.PROG_TYPE_ID = Convert.ToInt32(resultid);

                result = "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string UpdateProgramGroup(ProgramGroup procGroup, int userid)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(7) { CommandText = StoreProcedure.Instance.GetScript("ProgramGroupUpdate") };

                param.ParamStringFixedLength(0, "@PROG_TYPE_NAME", procGroup.PROG_TYPE_NAME);
                param.ParamInt32(1, "@ORDER_BY", procGroup.ORDER_BY);
                param.ParamStringFixedLength(2, "@ICON", procGroup.ICON);
                param.ParamStringFixedLength(3, "@DESCRIPTION", procGroup.DESCRIPTION);
                param.ParamStringFixedLength(4, "@PROG_TYPE_RESOURCE", procGroup.PROG_TYPE_RESOURCE);
                param.ParamStringFixedLength(5, "@IS_ACTIVE", (procGroup.IS_ACTIVE ? "Y" : "N"));
                param.ParamInt32(6, "@PROG_TYPE_ID", procGroup.PROG_TYPE_ID);

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                result = "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion "Program Type"

        #region "Program"

        public List<M_Program> GetProgramList(int? progId)
        {
            List<M_Program> lstProgram = null;
            M_Program mProgram;

            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("GetProgramList") };

                if (progId.HasValue)
                    param.ParamInt32(0, "@PROG_TYPE_ID", progId.Value);
                else
                    param.ParamNull(0, "@PROG_TYPE_ID");

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    lstProgram = new List<M_Program>();

                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        mProgram = new M_Program();

                        mProgram.PROG_ID = SQLiteReader.Instance.GetInteger("PROG_ID");
                        mProgram.PROG_NAME = SQLiteReader.Instance.GetString("PROG_NAME");
                        mProgram.PROG_KEY = SQLiteReader.Instance.GetString("PROG_KEY");
                        mProgram.PROG_GROUP.PROG_TYPE_ID = SQLiteReader.Instance.GetInteger("PROG_TYPE_ID");
                        mProgram.PROG_GROUP.PROG_TYPE_NAME = SQLiteReader.Instance.GetString("PROG_TYPE_NAME");
                        mProgram.PROG_TYPE_ICON = SQLiteReader.Instance.GetString("PROG_TYPE_ICON");

                        if (!SQLiteReader.Instance.IsDBNull("ORDER_BY"))
                        {
                            mProgram.ORDER_BY = SQLiteReader.Instance.GetInteger("ORDER_BY");
                        }
                        mProgram.ICON = SQLiteReader.Instance.GetString("ICON");
                        mProgram.IS_ACTIVE = (SQLiteReader.Instance.GetString("IS_ACTIVE").ToUpper() == "Y");
                        mProgram.DESCRIPTION = SQLiteReader.Instance.GetString("DESCRIPTION");
                        mProgram.PROG_RESOURCE = SQLiteReader.Instance.GetString("PROG_RESOURCE");

                        lstProgram.Add(mProgram);
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();
            }
            catch (Exception ex)
            {
                string re = ex.Message;

                lstProgram = null;
            }

            return lstProgram;
        }

        public M_Program GetProgramByID(int progId)
        {
            M_Program mProgram = null;

            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("GetProgramById") };

                param.ParamInt32(0, "@PROG_ID", progId);

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(param);

                if (SQLiteReader.Instance.HasRows)
                {
                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        mProgram = new M_Program();

                        mProgram.PROG_ID = SQLiteReader.Instance.GetInteger("PROG_ID");
                        mProgram.PROG_NAME = SQLiteReader.Instance.GetString("PROG_NAME");
                        mProgram.PROG_KEY = SQLiteReader.Instance.GetString("PROG_KEY");
                        mProgram.PROG_GROUP.PROG_TYPE_ID = SQLiteReader.Instance.GetInteger("PROG_TYPE_ID");
                        mProgram.PROG_GROUP.PROG_TYPE_NAME = SQLiteReader.Instance.GetString("PROG_TYPE_NAME");
                        mProgram.PROG_TYPE_ICON = SQLiteReader.Instance.GetString("PROG_TYPE_ICON");

                        if (!SQLiteReader.Instance.IsDBNull("ORDER_BY"))
                        {
                            mProgram.ORDER_BY = SQLiteReader.Instance.GetInteger("ORDER_BY");
                        }

                        mProgram.ICON = SQLiteReader.Instance.GetString("ICON");
                        mProgram.DESCRIPTION = SQLiteReader.Instance.GetString("DESCRIPTION");
                        mProgram.PROG_RESOURCE = SQLiteReader.Instance.GetString("PROG_RESOURCE");
                        mProgram.IS_ACTIVE = (SQLiteReader.Instance.GetString("IS_ACTIVE").ToUpper() == "Y");
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();
            }
            catch (Exception ex)
            {
                mProgram = null;
            }

            return mProgram;
        }

        public string InsertProgram(ref M_Program proc)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(8) { CommandText = StoreProcedure.Instance.GetScript("ProgramInsert") };

                param.ParamStringFixedLength(0, "@PROG_NAME", proc.PROG_NAME);
                param.ParamStringFixedLength(1, "@PROG_KEY", proc.PROG_KEY);
                param.ParamInt32(2, "@PROG_TYPE_ID", proc.PROG_GROUP.PROG_TYPE_ID);

                if (proc.ORDER_BY.HasValue)
                    param.ParamInt32(3, "@ORDER_BY", proc.ORDER_BY.Value);
                else
                    param.ParamNull(3, "@ORDER_BY");

                param.ParamStringFixedLength(4, "@ICON", proc.ICON);
                param.ParamStringFixedLength(5, "@DESCRIPTION", proc.DESCRIPTION);
                param.ParamStringFixedLength(6, "@PROG_RESOURCE", proc.PROG_RESOURCE);
                param.ParamStringFixedLength(7, "@IS_ACTIVE", (proc.IS_ACTIVE ? "Y" : "N"));

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                object resultid = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT SEQ FROM sqlite_sequence WHERE NAME = 'M_PROGRAM'");

                if (resultid != null)
                    proc.PROG_ID = Convert.ToInt32(resultid);

                result = "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string UpdateProgram(M_Program proc)
        {
            string result = string.Empty;

            try
            {
                SQLiteParam param = new SQLiteParam(9) { CommandText = StoreProcedure.Instance.GetScript("ProgramUpdate") };

                param.ParamStringFixedLength(0, "@PROG_NAME", proc.PROG_NAME);
                param.ParamStringFixedLength(1, "@PROG_KEY", proc.PROG_KEY);
                param.ParamInt32(2, "@PROG_TYPE_ID", proc.PROG_GROUP.PROG_TYPE_ID);

                if (proc.ORDER_BY.HasValue)
                    param.ParamInt32(3, "@ORDER_BY", proc.ORDER_BY.Value);
                else
                    param.ParamNull(3, "@ORDER_BY");

                param.ParamStringFixedLength(4, "@ICON", proc.ICON);
                param.ParamStringFixedLength(5, "@DESCRIPTION", proc.DESCRIPTION);
                param.ParamStringFixedLength(6, "@PROG_RESOURCE", proc.PROG_RESOURCE);
                param.ParamStringFixedLength(7, "@IS_ACTIVE", (proc.IS_ACTIVE ? "Y" : "N"));
                param.ParamInt32(8, "@PROG_ID", proc.PROG_ID);

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                result = "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion "Program"
    }
}
