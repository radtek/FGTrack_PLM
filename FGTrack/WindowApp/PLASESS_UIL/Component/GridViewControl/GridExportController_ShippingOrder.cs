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
using HTN.BITS.BEL.PLASESS;

namespace HTN.BITS.UIL.PLASESS.Component.GridViewControl
{
    public class GridExportController_ShippingOrder : IDisposable
    {
        private SaveFileDialog saveFileDialog;
        private BaseView _exportView;
        private string[] _columnsNoExp = null;
        private bool IsDisposed = false;
        //private WaitDialogForm waitDialog = null;


        private PrintingSystem printSystem = null;
        private PrintableComponentLink printCompageLink = null;
        //private PageHeaderFooter printHdrFtr = null;

        private string _TPL_FILE_NAME;
        public string TPL_FILE_NAME
        {
            get
            {
                return _TPL_FILE_NAME;
            }
            set
            {
                if (_TPL_FILE_NAME == value)
                    return;
                _TPL_FILE_NAME = value;
            }
        }

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

        private bool _UsingRowTemplate = false;
        public bool UsingRowTemplate
        {
            get
            {
                return _UsingRowTemplate;
            }
            set
            {
                if (_UsingRowTemplate == value)
                    return;
                _UsingRowTemplate = value;
            }
        }

        private bool _IsLastRowDelete = false;
        public bool IsLastRowDelete
        {
            get
            {
                return _IsLastRowDelete;
            }
            set
            {
                if (_IsLastRowDelete == value)
                    return;
                _IsLastRowDelete = value;
            }
        }

        private string _DocumentIso = string.Empty;
        public string DocumentIso
        {
            get
            {
                return _DocumentIso;
            }
            set
            {
                if (_DocumentIso == value)
                    return;
                _DocumentIso = value;
            }
        }

        private ShippingOrder _ShippingOrder_Hdr;
        public ShippingOrder ShippingOrder_Hdr
        {
            get
            {
                return _ShippingOrder_Hdr;
            }

            set
            {
                if (_ShippingOrder_Hdr == value)
                    return;
                _ShippingOrder_Hdr = value;
            }
        }

        public GridExportController_ShippingOrder(BaseView exportView)
        {
            this._exportView = exportView;
            this.saveFileDialog = new SaveFileDialog();
        }

        public GridExportController_ShippingOrder(BaseView exportView, string[] cols)
        {
            this._exportView = exportView;
            this._columnsNoExp = cols;

            this.printSystem = new PrintingSystem();
            this.printCompageLink = new PrintableComponentLink(this.printSystem) { Component = exportView.GridControl };

            this.saveFileDialog = new SaveFileDialog();
        }

        public void ExportToXLS(string defaultFilename, string title, string filter)
        {
            this.TPL_FILE_NAME = defaultFilename.Substring(0, defaultFilename.IndexOf("_"));
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

                        if (_UsingRowTemplate)
                            this.ExportExcelWithInsertTemplate(filename, "xlt");

                        if (_IsLastRowDelete)
                            DeleteLastRow(filename);

                        if (!string.IsNullOrEmpty(_DocumentIso))
                            this.AddDocumentNoInLastRow(filename, _DocumentIso);

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
            this.TPL_FILE_NAME = defaultFilename.Substring(0, defaultFilename.IndexOf("_"));

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

                        if (_UsingRowTemplate)
                            this.ExportExcelWithInsertTemplate(filename, "xltx");

                        if (_IsLastRowDelete)
                            DeleteLastRow(filename);

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

                //this.printCompageLink.RtfReportFooter = "1234";

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

        private void ExportExcelWithInsertTemplate(string filename, string ext)
        {
            string templateFilename = string.Format("{0}\\{1}\\{2}.{3}", Application.StartupPath, UiUtility.ExcelTemplatePath, this.TPL_FILE_NAME, ext);

            try
            {
                //declare for using Ms.Excel Object
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();

                ObjExcel.DisplayAlerts = false;

                //for template excel
                Microsoft.Office.Interop.Excel.Workbooks objTempbooks = ObjExcel.Workbooks;
                //for current excel
                Microsoft.Office.Interop.Excel.Workbooks objWorkbooks = ObjExcel.Workbooks;
                
                //-----------------------------------------------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Workbook book = objWorkbooks.Open(filename, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Microsoft.Office.Interop.Excel.Sheets sheets = book.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[this.SheetName];

                workSheet.Select(Type.Missing);

                //Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)workSheet.UsedRange;
                Microsoft.Office.Interop.Excel.Range insertRange = workSheet.get_Range(UiUtility.ExcelHeaderRange, Type.Missing);
                insertRange.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);

                //insert last row
                //Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)workSheet.UsedRange;
                //int lastRow = range.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, System.Type.Missing).Row;

                //string strRange = string.Format("A{0}", lastRow + 1);
                //Microsoft.Office.Interop.Excel.Range insertLastRange = workSheet.get_Range(strRange, Type.Missing);
                //insertLastRange.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromRightOrBelow);
                //-----------------------------------------------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Workbook tmpBook = objTempbooks.Open(templateFilename, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Microsoft.Office.Interop.Excel.Sheets tmpSheets = tmpBook.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet tmpWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)tmpSheets[this.SheetName];

                tmpWorkSheet.Select(Type.Missing);

                Microsoft.Office.Interop.Excel.Range copyRange = tmpWorkSheet.get_Range(UiUtility.ExcelHeaderRange, Type.Missing);
                copyRange.EntireRow.Copy(Type.Missing);

                Microsoft.Office.Interop.Excel.Range pasteRange = workSheet.get_Range(UiUtility.ExcelHeaderRange, Type.Missing);
                pasteRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteAll, Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                //-----------------------------------------------------------------------------------------------------------------------------
                //Microsoft.Office.Interop.Excel.Range copyLastRange = tmpWorkSheet.get_Range("A11", Type.Missing);
                //copyLastRange.EntireRow.Copy(Type.Missing);

                //Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)workSheet.UsedRange;
                //int lastRow = range.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, System.Type.Missing).Row;

                //string strRange = string.Format("A{0}", lastRow + 1);

                //Microsoft.Office.Interop.Excel.Range insertLastRange = workSheet.get_Range(strRange, Type.Missing);
                //insertLastRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteAllUsingSourceTheme, Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                //-------------------------------------------------------------------------------------------------------------------------------------


                book.Application.CutCopyMode = Microsoft.Office.Interop.Excel.XlCutCopyMode.xlCopy;
                //book.CheckCompatibility = false;

                tmpBook.Close(false, Type.Missing, Type.Missing);

                if (this._ShippingOrder_Hdr != null)
                {
                    //Set Shipping Order Information.

                    //SO_NO
                    Microsoft.Office.Interop.Excel.Range sonoRange = workSheet.get_Range("K2", Type.Missing);
                    sonoRange.set_Value(System.Type.Missing, this._ShippingOrder_Hdr.SO_NO);

                    //SO_DATE
                    Microsoft.Office.Interop.Excel.Range sodateRange = workSheet.get_Range("K3", Type.Missing);
                    if (this._ShippingOrder_Hdr.SO_DATE.HasValue)
                    {
                        sodateRange.set_Value(System.Type.Missing, string.Format("{0:dd-MM-yyyy}", this._ShippingOrder_Hdr.SO_DATE.Value));
                    }
                    else
                    {
                        sodateRange.set_Value(System.Type.Missing, string.Empty);
                    }

                    //Customer
                    Microsoft.Office.Interop.Excel.Range custRange = workSheet.get_Range("B4", Type.Missing);
                    custRange.set_Value(System.Type.Missing, string.Format("{0}  -  {1}", this._ShippingOrder_Hdr.PARTY_ID, this._ShippingOrder_Hdr.PARTY_NAME));

                    //PO_REF_NO
                    Microsoft.Office.Interop.Excel.Range porefRange = workSheet.get_Range("B5", Type.Missing);
                    porefRange.set_Value(System.Type.Missing, this._ShippingOrder_Hdr.REF_NO);


                    //PO_REF_DATE
                    Microsoft.Office.Interop.Excel.Range podateRange = workSheet.get_Range("G5", Type.Missing);
                    if (this._ShippingOrder_Hdr.REF_DATE.HasValue)
                    {
                        podateRange.set_Value(System.Type.Missing, string.Format("{0:dd-MM-yyyy}", this._ShippingOrder_Hdr.REF_DATE.Value));
                    }
                    else
                    {
                        podateRange.set_Value(System.Type.Missing, string.Empty);
                    }

                    //ETD
                    Microsoft.Office.Interop.Excel.Range etdRange = workSheet.get_Range("K5", Type.Missing);
                    if (this._ShippingOrder_Hdr.ETA.HasValue)
                    {
                        etdRange.set_Value(System.Type.Missing, string.Format("{0:dd-MM-yyyy HH:mm}", this._ShippingOrder_Hdr.ETA.Value));
                    }
                    else
                    {
                        etdRange.set_Value(System.Type.Missing, string.Empty);
                    }

                    //REMARK
                    Microsoft.Office.Interop.Excel.Range remarkRange = workSheet.get_Range("B6", Type.Missing);
                    remarkRange.set_Value(System.Type.Missing, this._ShippingOrder_Hdr.REMARK);

                    var focusRange = workSheet.get_Range("A1", "A1").Select();
                    
                    book.Save();
                    book.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlSaveChanges, Type.Missing, Type.Missing);

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(focusRange);

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sonoRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sodateRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(custRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(porefRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(podateRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(etdRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(remarkRange);



                }
                else
                {
                    var focusRange = workSheet.get_Range("A1", "A1").Select();

                    book.Save();
                    book.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlSaveChanges, Type.Missing, Type.Missing);

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(focusRange);
                }





                
                //-------------------------------------------------------------------------------------


                
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(insertLastRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(copyRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tmpSheets);


                System.Runtime.InteropServices.Marshal.ReleaseComObject(tmpBook);


                System.Runtime.InteropServices.Marshal.ReleaseComObject(insertRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);



                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);

                ObjExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
            }
            catch (Exception ex)
            {
                //
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

        private void DeleteLastRow(string filename)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks objWorkbooks = ObjExcel.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook book = objWorkbooks.Open(filename, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Microsoft.Office.Interop.Excel.Sheets sheets = book.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

                workSheet.Select(Type.Missing);

                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)workSheet.UsedRange;

                int lastRow = range.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, System.Type.Missing).Row;

                string strRange = string.Format("A{0}", lastRow);
                Microsoft.Office.Interop.Excel.Range deleteRange = workSheet.get_Range(strRange, Type.Missing);

                deleteRange.EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                //lastCell.EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                //range.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(deleteRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);

                book.Save();
                book.Close(false, Type.Missing, Type.Missing);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);

                ObjExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
            }
            catch (Exception ex)
            {
                //
            }

        }

        private void AddDocumentNoInLastRow(string filename, string docno)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks objWorkbooks = ObjExcel.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook book = objWorkbooks.Open(filename, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Microsoft.Office.Interop.Excel.Sheets sheets = book.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

                workSheet.Select(Type.Missing);

                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)workSheet.UsedRange;

                int lastRow = range.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, System.Type.Missing).Row;

                string strRange = string.Format("B{0}", lastRow + 2);

                Microsoft.Office.Interop.Excel.Range docIsoRange = workSheet.get_Range(strRange, Type.Missing);


                //docIsoRange.EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                docIsoRange.set_Value(System.Type.Missing, docno);
                //lastCell.EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                //range.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(docIsoRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);

                book.Save();
                book.Close(false, Type.Missing, Type.Missing);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);

                ObjExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
            }
            catch (Exception ex)
            {
                //
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

        ~GridExportController_ShippingOrder()
        {
            this.Dispose(false);
        }

        #endregion
    }


}
