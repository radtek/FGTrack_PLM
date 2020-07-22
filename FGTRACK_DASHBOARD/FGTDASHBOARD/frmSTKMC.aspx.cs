using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTN.BITS.SQLITE.BLL;
using HTN.BITS.FGTDB.BEL;
using System.Data;
using System.Threading;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPager;
using System.Configuration;
using System.Drawing;
using System.ServiceProcess;

public partial class frmSTKMC : System.Web.UI.Page
{
    private DataTable dtResult;
    private int pageIndex = 0;

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        this.grvSTK_Machine.DataSource = (DataTable)ctlState[1];
        this.pageIndex = (int)ctlState[2];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[3];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = this.grvSTK_Machine.DataSource;
        ctlState[2] = this.pageIndex;

        return ctlState;
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        this.Page.RegisterRequiresControlState(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetStockByMachine();
        }

        HeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? HeaderFilterMode.CheckedList : HeaderFilterMode.List;
        foreach (GridViewDataColumn column in grvSTK_Machine.Columns)
            column.Settings.HeaderFilterMode = headerFilterMode;
    }

    #region Method Member
    private void GetStockByMachine()
    {
        
        try
        {

            using (QuerySqliteBLL queryBll = new QuerySqliteBLL())
            {
                dtResult = queryBll.GetStockbyMachine();
            }

            this.grvSTK_Machine.DataSource = dtResult;
            this.grvSTK_Machine.DataBind();

            Session["LAST_UPDATE"] = DateTime.Now;

        }
        catch (Exception ex)
        {


        }
    }
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MainMenu.aspx"); 
    }



    protected void grvSTK_Machine_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
            if (e.DataColumn.FieldName.Equals("STATUS"))
            {                        
                if (e.CellValue.Equals("R"))            
                {
                    e.Cell.BorderColor = System.Drawing.ColorTranslator.FromHtml("#028002");
                    e.Cell.BorderWidth = 1;                    
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");
                    e.Cell.Text = ""; 
                }            
                else            
                {
                    e.Cell.BorderColor = System.Drawing.ColorTranslator.FromHtml("#7D0101");
                    e.Cell.BorderWidth = 1;
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    e.Cell.Text = "";
                }        
            }

    }

    protected void grvSTK_Machine_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        ASPxGridView editor = sender as ASPxGridView;
        try
        {
            switch (e.CallbackName)
            {
                case "APPLYFILTER":
                    editor.PageIndex = 0;
                    break;
                case "APPLYHEADERCOLUMNFILTER":
                    editor.PageIndex = 0;
                    break;
                case "CUSTOMCALLBACK":
                    this.pageIndex = editor.PageIndex + 1;
                    if (this.pageIndex >= editor.PageCount) //last page
                    {
                        this.pageIndex = 0;

                        double minute = DateTime.Now.Subtract((DateTime)Session["LAST_UPDATE"]).TotalMinutes;
                        double timediff = double.Parse(ConfigurationManager.AppSettings["TimeDiff"]);
                        if (minute >= timediff)
                        {
                            this.GetStockByMachine();
                        }
                    }

                    editor.PageIndex = this.pageIndex;
                    break;
                case "SORT":
                    editor.PageIndex = 0;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            this.btnUpdate.Enabled = false;
            this.Timer1.Interval = int.Parse(ConfigurationManager.AppSettings["McManualRefresh"]);

            ServiceController fgtdbSrv = new ServiceController("FGTDB_Service");
            
            fgtdbSrv.Refresh();

            if (fgtdbSrv.Status == ServiceControllerStatus.Running)
            {
                fgtdbSrv.ExecuteCommand(129); //129 := for Get Stock by Machine
            }

            this.Timer1.Enabled = true;

            


        }
        catch (Exception ex)
        {

        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        this.btnUpdate.Enabled = true;
        this.Timer1.Enabled = false;

        this.GetStockByMachine();

        this.pageIndex = 0;
        this.grvSTK_Machine.PageIndex = 0;
    }
}
