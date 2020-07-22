using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.XtraGrid.Views.Base;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors;
using System.IO;
using System.Collections;
using DevExpress.Utils;

namespace HTN.BITS.UIL.PLASESS.Component.GridViewControl
{
    public class GridExportController : IDisposable
    {
        private SaveFileDialog saveFileDialog;
        private BaseView _exportView;
        private string[] _columnsNoExp = null;
        private bool IsDisposed = false;
        private WaitDialogForm waitDialog = null;

        public GridExportController(BaseView exportView)
        {
            UiUtility.ClearSelection(exportView);
            this._exportView = exportView;
            this.saveFileDialog = new SaveFileDialog();
        }

        public GridExportController(BaseView exportView, string[] cols)
        {
            UiUtility.ClearSelection(exportView);
            this._exportView = exportView;
            this._columnsNoExp = cols;
            this.saveFileDialog = new SaveFileDialog();
        }

        public void ExportToXLS(string defaultFilename, string title, string filter)
        {
            UiUtility.ClearSelection(this._exportView);
            //this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;

            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                if(this.waitDialog == null)
                    this.waitDialog = new WaitDialogForm("Starting Export...", "Please waiting to Export Data");

                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    this.ExportToEx(filename, "XLS");

                    if (this.waitDialog != null)
                        this.waitDialog.SetCaption("Export Finished");
                    this.OpenFile(filename);
                }
            }
            
        }

        public void ExportToXLSX(string defaultFilename, string title, string filter)
        {
            UiUtility.ClearSelection(this._exportView);
            //this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;

            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    this.ExportToEx(filename, "XLSX");
                    this.OpenFile(filename);
                }
            }
        }

        public void ExportToPDF(string defaultFilename, string title, string filter)
        {
            UiUtility.ClearSelection(this._exportView);
            //this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;

            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    this.ExportToEx(filename, "PDF");
                    this.OpenFile(filename);
                }
            }
        }

        public void ExportToRTF(string defaultFilename, string title, string filter)
        {
            UiUtility.ClearSelection(this._exportView);
            //this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;

            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    this.ExportToEx(filename, "RTF");
                    this.OpenFile(filename);
                }
            }
        }

        public void ExportToTEXT(string defaultFilename, string title, string filter)
        {
            UiUtility.ClearSelection(this._exportView);
            //this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;

            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    this.ExportToEx(filename, "TXT");
                    this.OpenFile(filename);
                }
            }
        }

        public void ExportToHTML(string defaultFilename, string title, string filter)
        {
            UiUtility.ClearSelection(this._exportView);
            //this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.FileName = defaultFilename;
            this.saveFileDialog.Title = title;
            this.saveFileDialog.Filter = filter;

            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                string filename = this.saveFileDialog.FileName;

                if (filename != "")
                {
                    this.ExportToEx(filename, "HTML");
                    this.OpenFile(filename);
                }
            }
        }

        private void ExportToEx(string filename, string ext)
        {
            try
            {
                if (this.waitDialog != null)
                    this.waitDialog.SetCaption("Exporting Data ...");
                this.VisibleColumnNoExport(false);
                switch (ext.ToUpper())
                {
                    case "XLS":
                        this._exportView.ExportToXls(filename);
                        break;
                    case "XLSX":
                        this._exportView.ExportToXlsx(filename);
                        break;
                    case "PDF":
                        this._exportView.ExportToPdf(filename);
                        break;
                    case "RTF":
                        this._exportView.ExportToRtf(filename);
                        break;
                    case "TXT":
                        this._exportView.ExportToText(filename);
                        break;
                    case "HTML":
                        this._exportView.ExportToHtml(filename);
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
                if (colView.Columns[col] != null)
                    colView.Columns[col].Visible = status;
            }
        }


        private void OpenFile(string filename)
        {
            if (this.waitDialog != null)
            {
                this.waitDialog.SetCaption("Check Opening File...");
                this.waitDialog.Close();
            }
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
                    if (this.waitDialog != null)
                    {
                        this.waitDialog.Dispose();
                        this.waitDialog = null;
                    }

                    this.saveFileDialog.Dispose();
                    this.saveFileDialog = null;

                    //this._exportView.Dispose();
                    //this._exportView = null;

                }
            }

            IsDisposed = true;
        }

        ~GridExportController()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
