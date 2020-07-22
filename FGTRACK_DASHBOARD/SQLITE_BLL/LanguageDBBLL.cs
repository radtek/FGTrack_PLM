using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SQLite;
using HTN.BITS.SQLITE.DAL;

namespace HTN.BITS.SQLITE.BLL
{
    public class LanguageDBBLL : IDisposable
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

        ~LanguageDBBLL()
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

        public Hashtable GetMessageDB(string language)
        {
            Hashtable htLanguage = null;
            string sql = string.Empty;

            switch (language)
            {
                case "en":
                    sql = StoreProcedure.Instance.GetScript("GetMessageEN");
                    break;
                case "th":
                    sql = StoreProcedure.Instance.GetScript("GetMessageTH");
                    break;
                default:
                    break;
            }

            try
            {

                SQLiteReader.Instance.SLReader = GlobalSqliteDB.Instance.DataAc.ExecuteDataReader(sql);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                if (SQLiteReader.Instance.HasRows)
                {
                    htLanguage = new Hashtable();

                    while (SQLiteReader.Instance.SLReader.Read())
                    {
                        if (!string.IsNullOrEmpty(SQLiteReader.Instance.GetString("MESSAGE_RESULT")))
                        {
                            htLanguage.Add(SQLiteReader.Instance.GetString("MESSAGE_KEY"), SQLiteReader.Instance.GetString("MESSAGE_RESULT"));
                        }
                    }
                }

                //Always call Close when done reading.
                SQLiteReader.Instance.Close();


            }
            catch (Exception ex)
            {
                htLanguage = null;
            }

            return htLanguage;
        }

    }
}
