using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Resources;
using DevExpress.XtraEditors.DXErrorProvider;
using System.IO;
using DevExpress.XtraGrid;
using System.Collections;
using System.Text.RegularExpressions;
using DevExpress.Utils;
using HTN.BITS.LIB;
using HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component.GridViewControl;
using DevExpress.XtraGrid.Views.Base;
using HTN.BITS.BEL.PLASESS;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component
{
    public enum GridExportType
    {
        HTML,
        PDF,
        RTF,
        TEXT,
        XLS,
        XLSX,
    }

    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        private LanguageRes language;
        private WaitDialogForm processDialog = null;
        private List<GridControl> lstGridControl = null;


        public BaseForm()
        {
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
         ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();

            GC.ReRegisterForFinalize(this);

            this.language = new LanguageRes(UiUtility.LanguageUse);
        }

        ~BaseForm()
        {
            MemoryCleaner mc = new MemoryCleaner();
            mc.Start();

            this.Controls.Clear();
            GC.SuppressFinalize(this);

            mc.Stop();

            GC.Collect(GC.MaxGeneration);
        }

        public LanguageRes Language
        {
            get
            {
                return this.language;
            }
        }

        protected void LoadFormLayout()
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);
            string formFileName = string.Format("{0}\\{1}.xml", stateLayoutPath, this.Name);

            if (File.Exists(formFileName))
            {
                FormConfigState formState = UiUtility.DeserializeObject<FormConfigState>(formFileName);
                if (formState != null)
                {
                    if (!formState.FormSize.IsEmpty)
                        this.Size = formState.FormSize;
                    if (!formState.FormLocation.IsEmpty)
                        this.Location = formState.FormLocation;
                }
            }
        }

        protected void LoadGridLayout(GridControl grd)
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);
            string formFileName = string.Format("{0}\\{1}.xml", stateLayoutPath, this.Name);

            if (File.Exists(formFileName))
            {
                FormConfigState formState = UiUtility.DeserializeObject<FormConfigState>(formFileName);
                if (formState != null)
                {
                    string[] grdArr = Regex.Split(formState.GridControl, @",");
                    int index = Array.IndexOf(grdArr, grd.Name);

                    if (index >= 0)
                    {
                        string[] lastViewArr = Regex.Split(formState.LastDefaultView, @",");
                        this.LoadGridLayoutMultipleView(this.Name, grd, lastViewArr[index]);
                    }
                }
            }

            //this.LoadGridLayoutMultipleView(this.Name, grd);
            //all to list;
            if (this.lstGridControl == null)
            {
                this.lstGridControl = new List<GridControl>();
            }

            this.lstGridControl.Add(grd);
        }

        protected void LoadGridLayoutMultipleView(string formName, GridControl grd, string lastViewText)
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);

            string formFileName = string.Format("{0}\\{1}.xml", stateLayoutPath, formName);
            string filename = string.Empty;
            BaseView currentView = null;
            //if (File.Exists(formFileName))
            //{
            //FormConfigState formState = UiUtility.DeserializeObject<FormConfigState>(formFileName);
            //if (formState != null)
            //{
            //int i = grd.ViewCollection.Count;
            BaseView tView;
            for (int i = 0; i < grd.ViewCollection.Count; i++)
            {
                tView = grd.ViewCollection[i];
                filename = string.Format("{0}\\{1}_{2}.xml", stateLayoutPath, formName, tView.Name);
                if (File.Exists(filename))
                {
                    tView.RestoreLayoutFromXml(filename, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                }

                if (tView.GetType().Name.Equals(lastViewText))
                {
                    currentView = tView;
                }
            }

            if (currentView != null)
            {
                grd.MainView = currentView;
            }
            //}
            //}


        }

        protected void ViewExportToExcel(BaseView view, GridExportType exportType, string filename, string[] colNoExports)
        {
            try
            {
                using (GridExportController gridview = new GridExportController(view, colNoExports))
                {
                    switch (exportType)
                    {
                        case GridExportType.HTML:
                            gridview.ExportToHTML(filename, "HTML Document", "HTML Files|*.html");
                            break;
                        case GridExportType.PDF:
                            gridview.ExportToPDF(filename, "PDF Document", "PDF Files|*.pdf");
                            break;
                        case GridExportType.RTF:
                            gridview.ExportToRTF(filename, "RTF Document", "RTF Files|*.rtf");
                            break;
                        case GridExportType.TEXT:
                            gridview.ExportToTEXT(filename, "Text Document", "Text Files|*.txt");
                            break;
                        case GridExportType.XLS:
                            gridview.ExportToXLS(filename, "Microsoft Excel Document", "Microsoft Excel|*.xls");
                            break;
                        case GridExportType.XLSX:
                            gridview.ExportToXLSX(filename, "Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        #region Show Processing

        protected void BeginProcessing(string caption, string title)
        {
            if (this.processDialog == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.processDialog = new WaitDialogForm(caption, title);
                //test by jack
                //this.processDialog.AllowFormSkin = true;
            }
        }

        protected void ProcessCaption(string caption)
        {
            if (this.processDialog != null)
                this.processDialog.SetCaption(caption);
        }

        protected void FinishedProcessing()
        {
            Cursor.Current = Cursors.Default;
            if (this.processDialog != null)
            {
                this.processDialog.Close();
                this.processDialog.Dispose();
                this.processDialog = null;

            }
        }

        #endregion

        protected override void OnClosed(EventArgs e)
        {
            #region Create XML Layout

            string formFileName = string.Empty;
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);
            List<string> gridControlArr = new List<string>();
            List<string> lastViewArr = new List<string>();


            if (this.lstGridControl != null)
            {
                foreach (GridControl grd in this.lstGridControl)
                {
                    gridControlArr.Add(grd.Name);
                    lastViewArr.Add(grd.MainView.GetType().Name);

                    this.SaveGridLayoutMultipleView(this.Name, grd);
                }

                this.lstGridControl.Clear();
                this.lstGridControl = null;
            }

            //Check Exist Directory
            if (!Directory.Exists(stateLayoutPath))
            {
                Directory.CreateDirectory(stateLayoutPath);
            }

            FormConfigState formState = new FormConfigState { FormName = this.Name, GridControl = string.Join(",", gridControlArr.ToArray()), LastDefaultView = string.Join(",", lastViewArr.ToArray()), FormSize = this.Size, FormLocation = this.Location };

            formFileName = string.Format("{0}\\{1}.xml", stateLayoutPath, this.Name);
            UiUtility.SerializeObject(formState, formFileName);

            #endregion

            #region Application Idle Dispose

            //if (this.applicationIdle1.IsRunning)
            //{
            //    this.applicationIdle1.Stop();
            //}

            //this.applicationIdle1.Dispose();

            #endregion

            if (this.processDialog != null)
                this.processDialog = null;

            this.Controls.Clear();

            base.OnClosed(e);
        }

        protected void SaveGridLayoutMultipleView(string formName, GridControl grd)
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);

            string formFileName = string.Empty;
            string filename = string.Empty;
            string mainview = string.Empty;

            try
            {

                filename = string.Format("{0}\\{1}_{2}.xml", stateLayoutPath, formName, grd.MainView.Name);
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    grd.MainView.SaveLayoutToStream(stream, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

    }
}