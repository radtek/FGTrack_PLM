using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraPrinting;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component.GridViewControl
{
    public class GridExportController2 : IDisposable
    {
        private SaveFileDialog saveFileDialog;
        private BaseView _exportView;
        private string[] _columnsNoExp = null;
        private bool IsDisposed = false;
        //private WaitDialogForm waitDialog = null;


        private PrintingSystem printSystem = null;
        private PrintableComponentLink printCompageLink = null;
        //private PageHeaderFooter printHdrFtr = null;

        private string _sheetName = "";

        public string SheetName
        {
            get
            {
                return _sheetName;
            }
            set
            {
                if (_sheetName == value)
                    return;
                _sheetName = value;
            }
        }

        public GridExportController2(BaseView exportView)
        {
            this._exportView = exportView;
            this.saveFileDialog = new SaveFileDialog();
        }

        public GridExportController2(BaseView exportView, string[] cols)
        {
            this._exportView = exportView;
            this._columnsNoExp = cols;

            this.printSystem = new PrintingSystem();
            this.printCompageLink = new PrintableComponentLink(this.printSystem) { Component = exportView.GridControl };

            this.saveFileDialog = new SaveFileDialog();
        }

        public void ExportToXLS(string defaultFilename, string title, string filter)
        {
            this.saveFileDialog.CheckFileExists = false;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;


            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                //if (this.waitDialog == null)
                //    this.waitDialog = new WaitDialogForm("Starting Export...", "Please waiting to Export Data");

                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    if (!this.IsFileLocked(filename))
                    {
                        this.ExportToEx(filename, "XLS");
                        this.OpenFile(filename);
                    }
                    else
                    {
                        XtraMessageBox.Show(string.Format("File '{0}' is Opened\nPlease Close and Export again!!", filename), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
            }

        }

        public void ExportToXLSX(string defaultFilename, string title, string filter)
        {
            this.saveFileDialog.CheckFileExists = false;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;


            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    if (!this.IsFileLocked(filename))
                    {
                        this.ExportToEx(filename, "XLSX");
                        this.OpenFile(filename);
                    }
                    else
                    {
                        XtraMessageBox.Show(string.Format("File '{0}' is Opened\nPlease Close and Export again!!", filename), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }

        private void ExportToEx(string filename, string ext)
        {
            try
            {
                this.VisibleColumnNoExport(false);

                this.printSystem = new PrintingSystem();
                this.printCompageLink = new PrintableComponentLink(this.printSystem) { Component = this._exportView.GridControl };
                this.printCompageLink.CreateDocument();

                //if (this.waitDialog != null)
                //    this.waitDialog.SetCaption("Exporting Data ...");

                switch (ext.ToUpper())
                {
                    case "XLS":
                        var optXls = new XlsExportOptions() { SheetName = (string.IsNullOrEmpty(_sheetName) ? "Sheet1" : _sheetName) };
                        this.printCompageLink.PrintingSystem.ExportToXls(filename, optXls);

                        break;
                    case "XLSX":
                        var optXlsx = new XlsxExportOptions() { SheetName = (string.IsNullOrEmpty(_sheetName) ? "Sheet1" : _sheetName) };
                        this.printCompageLink.PrintingSystem.ExportToXlsx(filename, optXlsx);
                        break;
                    default:
                        break;
                }

                this.VisibleColumnNoExport(true);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void VisibleColumnNoExport(bool status)
        {
            if (this._columnsNoExp == null) return;
            if (this._exportView == null) return;

            ColumnView colView = this._exportView as ColumnView;

            foreach (string col in this._columnsNoExp)
            {

                if (colView.Columns.ColumnByName(col) != null)
                    colView.Columns.ColumnByName(col).Visible = status;
            }
        }


        private void OpenFile(string filename)
        {
            //if (this.waitDialog != null)
            //{
            //    this.waitDialog.SetCaption("Check Opening File...");
            //    this.waitDialog.Close();
            //}
            //DialogResult message = MessageBox.Show("Do you want to open?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            DialogResult message = XtraMessageBox.Show("Do you want to open?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (message == DialogResult.Yes)
                {
                    if (File.Exists(filename))
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.FileName = filename;
                            myProcess.Start();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Could not find a part of the file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool IsFileLocked(string path)
        {
            try
            {
                FileAccess access = FileAccess.Write;
                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open, access, FileShare.ReadWrite))
                    { }
                }
            }
            catch (IOException e)
            {
                return (System.Runtime.InteropServices.Marshal.GetHRForException(e) & 0xFFFF) == 32;
            }

            return false;
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
                    //if (this.waitDialog != null)
                    //{
                    //    this.waitDialog.Dispose();
                    //    this.waitDialog = null;
                    //}

                    this.saveFileDialog.Dispose();
                    this.saveFileDialog = null;

                    this.printCompageLink.Dispose();
                    this.printCompageLink = null;

                    this.printSystem.Dispose();
                    this.printSystem = null;

                    //this._exportView.Dispose();
                    //this._exportView = null;

                }
            }

            IsDisposed = true;
        }

        ~GridExportController2()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
