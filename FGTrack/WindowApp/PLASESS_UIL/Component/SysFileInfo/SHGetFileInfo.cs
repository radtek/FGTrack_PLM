using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using HTN.BITS.BEL.PLASESS;
using System.IO;
using System.Security.Principal;

namespace HTN.BITS.UIL.PLASESS.Component.SysFileInfo
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    public class Win32
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
    }

    public class CSVFileHistoryFileInfo : IDisposable
    {

        public CSVFileHistoryFileInfo()
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

        ~CSVFileHistoryFileInfo()
        {
            this.Dispose(false);
        }

        #endregion

        #region "Private Member"

        private bool IsDisposed = false;

        #endregion

        public string ExistCSVFileInHistoryPath(string filename)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(UiUtility.HistoryCSVPath);

                FileInfo[] rgFiles = di.GetFiles(filename);

                if (rgFiles.Length > 0)
                    return rgFiles[0].FullName;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<eFileInfo> GetPoOnlinePendingFile()
        //{
        //    List<eFileInfo> lstFileInfo = null;
        //    eFileInfo fInfo = null;

        //    //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal); //PrincipalPolicy.WindowsPrincipal
        //    //WindowsIdentity identity = new WindowsIdentity("HTN\\Administrator", "ashishisadmin");
        //    //WindowsImpersonationContext context = identity.Impersonate();

        //    try
        //    {
        //        DirectoryInfo di = new DirectoryInfo(UiUtility.PoOnlinePendingPath);

        //        FileInfo[] rgFiles = di.GetFiles("*.TXT");
        //        if (rgFiles.Length != 0)
        //        {
        //            lstFileInfo = new List<eFileInfo>();

        //            foreach (FileInfo fi in rgFiles)
        //            {
        //                fInfo = new eFileInfo();
        //                fInfo.Name = fi.Name;
        //                fInfo.FullName = fi.FullName;
        //                fInfo.CreationTime = fi.CreationTime;
        //                fInfo.Length = fi.Length;
        //                fInfo.DisplaySize = UiUtility.GetFileSize(fi.Length);

        //                SHFILEINFO shinfo = new SHFILEINFO();
        //                IntPtr hImgSmall = Win32.SHGetFileInfo(fi.FullName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);

        //                fInfo.DisplayName = fi.Name.Replace(fi.Extension, "");
        //                fInfo.TypeName = UiUtility.GetFileType(fi.Extension);
        //                fInfo.SmallIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
        //                TimeSpan takeTime = DateTime.Now.Subtract(fi.CreationTime);
        //                fInfo.TakeTimeMin = takeTime.TotalMinutes;
        //                fInfo.TakeTimeString = UiUtility.TimeSpanInWords(takeTime);

        //                lstFileInfo.Add(fInfo);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //context.Undo();
        //        throw ex;
        //    }

        //    return lstFileInfo;
        //}

        
    }

    public class XLSFileHistoryFileInfo : IDisposable
    {

        public XLSFileHistoryFileInfo()
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

        ~XLSFileHistoryFileInfo()
        {
            this.Dispose(false);
        }

        #endregion

        #region "Private Member"

        private bool IsDisposed = false;

        #endregion

        public string ExistXLSFileInHistoryPath(string filename)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(UiUtility.HistoryXLSPath);

                FileInfo[] rgFiles = di.GetFiles(filename);

                if (rgFiles.Length > 0)
                    return rgFiles[0].FullName;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<eFileInfo> GetPoOnlinePendingFile()
        //{
        //    List<eFileInfo> lstFileInfo = null;
        //    eFileInfo fInfo = null;

        //    //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal); //PrincipalPolicy.WindowsPrincipal
        //    //WindowsIdentity identity = new WindowsIdentity("HTN\\Administrator", "ashishisadmin");
        //    //WindowsImpersonationContext context = identity.Impersonate();

        //    try
        //    {
        //        DirectoryInfo di = new DirectoryInfo(UiUtility.PoOnlinePendingPath);

        //        FileInfo[] rgFiles = di.GetFiles("*.TXT");
        //        if (rgFiles.Length != 0)
        //        {
        //            lstFileInfo = new List<eFileInfo>();

        //            foreach (FileInfo fi in rgFiles)
        //            {
        //                fInfo = new eFileInfo();
        //                fInfo.Name = fi.Name;
        //                fInfo.FullName = fi.FullName;
        //                fInfo.CreationTime = fi.CreationTime;
        //                fInfo.Length = fi.Length;
        //                fInfo.DisplaySize = UiUtility.GetFileSize(fi.Length);

        //                SHFILEINFO shinfo = new SHFILEINFO();
        //                IntPtr hImgSmall = Win32.SHGetFileInfo(fi.FullName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);

        //                fInfo.DisplayName = fi.Name.Replace(fi.Extension, "");
        //                fInfo.TypeName = UiUtility.GetFileType(fi.Extension);
        //                fInfo.SmallIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
        //                TimeSpan takeTime = DateTime.Now.Subtract(fi.CreationTime);
        //                fInfo.TakeTimeMin = takeTime.TotalMinutes;
        //                fInfo.TakeTimeString = UiUtility.TimeSpanInWords(takeTime);

        //                lstFileInfo.Add(fInfo);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //context.Undo();
        //        throw ex;
        //    }

        //    return lstFileInfo;
        //}


    }

}
