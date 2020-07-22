using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.LIB;
using System.Resources;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using DevExpress.XtraGrid;
using HTN.BITS.BEL.PLASESS;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Collections;
using System.Text.RegularExpressions;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using DevExpress.Utils;

namespace HTN.BITS.UIL.PLASESS.Component
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
        //For event LoadCompleted
        public delegate void LoadCompletedEventHandler();
        public event LoadCompletedEventHandler LoadCompleted;


        private LanguageRes language;
        ResXResourceWriter RwX = null;
        private List<GridControl> lstGridControl = null;

        //add new process dialog by jack 21-06-2011
        private WaitDialogForm processDialog = null;
        //private ExecutionStopwatch executionSW = null;
        private DateTime startTime, stopTime;

        public BaseForm()
        {
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
         ControlStyles.AllPaintingInWmPaint, true);

            //For event LoadCompleted
            this.Shown += new EventHandler(BaseForm_Shown);

            InitializeComponent();
            GC.ReRegisterForFinalize(this);

            //this.executionSW = new ExecutionStopwatch();

            //this.AllowFormSkin = true;
            //this.defaultLookAndFeel1.LookAndFeel.UseDefaultLookAndFeel = false;

            //this.CustomInitializeComponent();
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

        //For event LoadCompleted
        void BaseForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (LoadCompleted != null)
                LoadCompleted();
        }

        //private void CustomInitializeComponent()
        //{
        //    if (HTN.BITS.UIL.PLASESS.Properties.Settings.Default.IsRuntime)
        //    {
        //        this.defaultLookAndFeel1.LookAndFeel.SkinName = UiUtility.ApplicationStyle;
        //        //this.applicationIdle1.IdleTime = System.TimeSpan.Parse(UiUtility.ApplicationIdleTime);
        //    }
        //}

        public void UpdatePerformLayout()
        {
            //this.CustomInitializeComponent();
            base.PerformLayout();
        }


        public LanguageRes Language
        {
            get
            {
                return this.language;
            }
        }

        //public Winforms.Components.ApplicationIdle AppIdle
        //{
        //    get
        //    {
        //        return this.applicationIdle1;
        //    }

        //}

        protected void UpdateControls(Control control, string formName)
        {
            if (RwX == null)
            {
                RwX = new ResXResourceWriter("D:\\Temps\\ResxFile\\" + formName + ".resx");
            }
            // update the controls that we can do automatically
            if (control is DevExpress.XtraEditors.LabelControl || //Label
                control is DevExpress.XtraTab.XtraTabPage ||  //TabPage
                control is DevExpress.XtraEditors.PanelControl || //Panel
                control is DevExpress.XtraEditors.SimpleButton || //Button
                control is DevExpress.XtraEditors.CheckEdit || //Checkbox
                control is DevExpress.XtraEditors.XtraForm || //Form
                control is DevExpress.XtraGrid.GridControl) //GridColumn
            {
                try
                {
                    //control.Text = ResourceManager.Instance.GetString(control);


                    string contrlName = string.Empty;
                    int i = 0;
                    if (control is DevExpress.XtraEditors.XtraForm)
                    {
                        RwX.AddResource(control.Name, control.Text);
                    }
                    else if (control is DevExpress.XtraGrid.GridControl)
                    {

                        DevExpress.XtraGrid.GridControl grd = (control as DevExpress.XtraGrid.GridControl);
                        DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)grd.Views[0];
                        foreach (DevExpress.XtraGrid.Columns.GridColumn gColumn in view.Columns)
                        {
                            contrlName = string.Format("{0}_{1}", formName, gColumn.Name);
                            RwX.AddResource(contrlName, gColumn.Caption);
                        }
                    }
                    else
                    {
                        contrlName = string.Format("{0}_{1}", formName, control.Name);
                        RwX.AddResource(contrlName, control.Text);
                    }
                }
                catch
                {
                    // log the error if desired
                }
            }

            foreach (Control c in control.Controls)
            {
                // get any nested controls
                UpdateControls(c, formName);
            }
        }

        protected void UpdateLanguageControls(Control control, string formName)
        {
            // update the controls that we can do automatically
            if (control is DevExpress.XtraEditors.LabelControl || //Label
                control is DevExpress.XtraTab.XtraTabPage ||  //TabPage
                control is DevExpress.XtraEditors.PanelControl || //Panel
                control is DevExpress.XtraEditors.SimpleButton || //Button
                control is DevExpress.XtraEditors.CheckEdit || //Checkbox
                control is DevExpress.XtraEditors.XtraForm || //Form
                control is DevExpress.XtraGrid.GridControl) //GridColumn
            {
                try
                {
                    //control.Text = ResourceManager.Instance.GetString(control);


                    string contrlName = string.Empty;
                    int i = 0;
                    if (control is DevExpress.XtraEditors.XtraForm)
                    {
                        //RwX.AddResource(control.Name, control.Text);
                        control.Text = language.GetValue(control.Name);
                    }
                    else if (control is DevExpress.XtraGrid.GridControl)
                    {

                        DevExpress.XtraGrid.GridControl grd = (control as DevExpress.XtraGrid.GridControl);
                        DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)grd.Views[0];
                        foreach (DevExpress.XtraGrid.Columns.GridColumn gColumn in view.Columns)
                        {
                            contrlName = string.Format("{0}_{1}", formName, gColumn.Name);
                            //RwX.AddResource(contrlName, gColumn.Caption);
                            gColumn.Caption = language.GetValue(contrlName);
                        }
                    }
                    else
                    {
                        contrlName = string.Format("{0}_{1}", formName, control.Name);
                        //RwX.AddResource(contrlName, control.Text);
                        control.Text = language.GetValue(contrlName);
                    }
                }
                catch
                {
                    // log the error if desired
                }
            }

            foreach (Control c in control.Controls)
            {
                // get any nested controls
                UpdateLanguageControls(c, formName);
            }
        }

        protected void GenerateResx()
        {
            RwX.Generate();
            RwX.Close();
        }

        protected void ClearValidControls(Control control, ref DXErrorProvider errCtl)
        {
            this.SetValidControls(control, errCtl);
        }

        private void SetValidControls(Control control, DXErrorProvider errCtl)
        {
            if (control is TextEdit ||
                control is ButtonEdit ||
                control is ImageComboBoxEdit ||
                control is LookUpEdit)
            {
                errCtl.SetError(control, string.Empty);
            }

            foreach (Control c in control.Controls)
            {
                // get any nested controls
                SetValidControls(c, errCtl);
            }
        }



        protected void SaveGridLayoutMultipleView(string formName, GridControl grd)
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);

            string formFileName = string.Empty;
            string filename = string.Empty;
            string mainview = string.Empty;

            //Check Exist Directory
            //if (!Directory.Exists("GridLayout"))
            //{
            //    Directory.CreateDirectory("GridLayout");
            //}

            //FormConfigState formState = new FormConfigState();
            //formState.FormName = formName;
            //formState.GridControl = grd.Name;
            //formState.LastDefaultView = grd.MainView.GetType().Name;
            //formState.FormSize = this.Size;
            //formState.FormLocation = this.Location;

            //formFileName = string.Format("{0}\\{1}.xml", stateLayoutPath, formName);
            //UiUtility.SerializeObject(formState, formFileName);
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
            try
            {
                BaseView tView;
                for (int i = 0; i < grd.ViewCollection.Count; i++)
                {
                    tView = grd.ViewCollection[i];
                    filename = string.Format("{0}\\{1}_{2}.xml", stateLayoutPath, formName, tView.Name);
                    if (File.Exists(filename))
                    {
                        if (UiUtility.IsValidXML(filename)) 
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error LoadGridLayoutMultipleView", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
        }

        //protected void SaveGridLayout(string formName, BaseView view)
        //{
        //    string stateLayoutPath = UiUtility.StateConfigPath;
        //    string formFileName = string.Empty;
        //    string filename = string.Format("{0}\\{1}_{2}.xml", stateLayoutPath, formName, view.Name);

        //    //Check Exist Directory
        //    if (!Directory.Exists("GridLayout"))
        //    {
        //        Directory.CreateDirectory("GridLayout");
        //    }

        //    using (FileStream stream = new FileStream(filename, FileMode.Create))
        //    {
        //        view.SaveLayoutToStream(stream, DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //        stream.Close();
        //    }
        //}

        //protected void LoadGridLayout(string formName, BaseView view)
        //{
        //    string stateLayoutPath = UiUtility.StateConfigPath;
        //    string filename = string.Format("{0}\\{1}_{2}.xml", stateLayoutPath, formName, view.Name);
        //    if (File.Exists(filename))
        //    {
        //        view.RestoreLayoutFromXml(filename, DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //    }
        //}

        //test
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

        public void SaveGridControlBeforeClose()
        {
            if (this.lstGridControl != null)
            {
                foreach (GridControl grd in this.lstGridControl)
                {
                    this.SaveGridLayoutMultipleView(this.Name, grd);
                }

                this.lstGridControl.Clear();
                this.lstGridControl = null;
            }
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

        protected void ViewExportToExcel(BaseView view, GridExportType exportType, string filename, string[] colNoExports, string sheetname, bool usingTemplate, ShippingOrder shippingOrd)
        {
            try
            {
                using (GridExportController_ShippingOrder gridview = new GridExportController_ShippingOrder(view, colNoExports) { SheetName = sheetname, UsingRowTemplate = usingTemplate, IsLastRowDelete = false, ShippingOrder_Hdr = shippingOrd})
                {
                    switch (exportType)
                    {
                        case GridExportType.XLS:
                            gridview.ExportToXLS(filename, "Microsoft Excel Document", "Microsoft Excel|*.xls");
                            break;
                        case GridExportType.XLSX:
                            gridview.ExportToXLSX(filename, "Microsoft Excel 2007-2010 Document", "Microsoft Excel|*.xlsx");
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

        protected void BeginProcessing(string caption, string title, Form form)
        {
            if (this.processDialog == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.processDialog = new WaitDialogForm(caption, title);
                this.processDialog.Owner = form;
            }
        }

        protected void ProcessCaption(string caption)
        {
            if (this.processDialog != null)
            {
                this.processDialog.SetCaption(caption);
            }
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

        #region ExcutionStopwatch

        protected void ExecutionStart()
        {
            this.startTime = DateTime.Now;
        }

        protected void ExecutionStop()
        {
            this.stopTime = DateTime.Now;
        }

        protected string ExecuteTime
        {
            get 
            {
                return "Execute Time: " + UiUtility.TimeSpanInWords(this.stopTime - this.startTime);
            }
        }

        #endregion

    }
}