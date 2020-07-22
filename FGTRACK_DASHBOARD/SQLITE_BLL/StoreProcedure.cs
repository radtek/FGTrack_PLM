using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Web;

namespace HTN.BITS.SQLITE.BLL
{
    public class StoreProcedure : IDisposable
    {
        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion

        public void Release()
        {
            IsReleased = true; ;
            StoreProcedure.instance = null;
        }

        [ThreadStatic]
        static StoreProcedure instance = null;

        public bool IsReleased { get; private set; }

        private StoreProcedure()
        {
            IsReleased = false;
        }

        static StoreProcedure()
        {
            instance = new StoreProcedure();
            procPath = string.Format("{0}\\{1}", Application.StartupPath, ConfigurationManager.AppSettings["ProcPath"]);
        }

        private static string procPath;

        public static StoreProcedure Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreProcedure();
                    if (ConfigurationManager.AppSettings["IsService"] == "True")
                    {
                        procPath = string.Format("{0}\\{1}", Application.StartupPath, ConfigurationManager.AppSettings["ProcPath"]);
                    }
                    else
                    {
                        string appPath = HttpContext.Current.Request.ApplicationPath;
                        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                        procPath = string.Format("{0}\\{1}", physicalPath, ConfigurationManager.AppSettings["ProcPath"]);
                    }
                }

                return instance;
            }
        }

        public string GetScript(string procname)
        {
            string sql = string.Empty;
            string filename = string.Empty; 

            if (ConfigurationManager.AppSettings["IsService"] == "True")
            {
                procPath = string.Format("{0}\\{1}", Application.StartupPath, ConfigurationManager.AppSettings["ProcPath"]);
                filename = string.Format("{0}\\{1}.sql", procPath, procname);
            }
            else
            {
                string appPath = HttpContext.Current.Request.ApplicationPath;
                string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                procPath = string.Format("{0}\\{1}", physicalPath, ConfigurationManager.AppSettings["ProcPath"]);
                filename = string.Format("{0}\\{1}.sql", procPath, procname);
            }
            //string filename = string.Format("{0}\\{1}.sql", procPath, procname);

            try
            {
                if (File.Exists(filename))
                {
                    using (StreamReader sr = File.OpenText(filename))
                    {
                        sql = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                sql = string.Empty;
            }

            return sql;
        }
    }
}
