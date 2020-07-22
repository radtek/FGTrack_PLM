using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTN.BITS.SQLITE.BLL;
using HTN.BITS.FGTDB.BEL;
using System.Data;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxGridView;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;   

public partial class frmSTKCust : System.Web.UI.Page
{
    private DataTable dtResult;
    private int pageIndex = 0;
    //private System.Timers.Timer oTimer;
    //readonly System.Diagnostics.Stopwatch stopw = new System.Diagnostics.Stopwatch();


    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        this.grvSTK_Customer.DataSource = (DataTable)ctlState[1];
        this.pageIndex = (int)ctlState[2];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[3];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = this.grvSTK_Customer.DataSource;
        ctlState[2] = this.pageIndex;

        return ctlState;
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        this.Page.RegisterRequiresControlState(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetStockByCustomer();
        }

        HeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? HeaderFilterMode.CheckedList : HeaderFilterMode.List;
        foreach (GridViewDataColumn column in grvSTK_Customer.Columns)
            column.Settings.HeaderFilterMode = headerFilterMode;
    }

    #region Method Member

    private void GetStockByCustomer()
    {
        //DataTable dtResult;
        try
        {

            using (QuerySqliteBLL queryBll = new QuerySqliteBLL())
            {
                dtResult = queryBll.GetStockbyCustomer(); 
            }

            this.grvSTK_Customer.DataSource = dtResult;
            this.grvSTK_Customer.DataBind();

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

    protected void ASPxPager1_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxPager pager = sender as ASPxPager;
        this.grvSTK_Customer.DataSource = dtResult;
        this.grvSTK_Customer.DataBind();
        //this.grvSTK_Customer.PageIndex = this.ASPxPager1.PageIndex;
    }

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //        this.ASPxPager1.PageIndex = this.ASPxPager1.PageIndex + 1;
    //        this.grvSTK_Customer.DataSource = dtResult;
    //        this.grvSTK_Customer.DataBind();
    //        this.grvSTK_Customer.PageIndex = this.ASPxPager1.PageIndex;
    //}
    //protected void grvSTK_Customer_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    //{
        
    //    //this.pageIndex = this.grvSTK_Customer.PageIndex + 1;
    //    //if (this.pageIndex >= this.grvSTK_Customer.PageCount)
    //    //{
    //    //    this.pageIndex = 0;
    //    //}

    //    //this.grvSTK_Customer.PageIndex = this.pageIndex;
    //}
    protected void grvSTK_Customer_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs e)
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

                    if (this.pageIndex >= editor.PageCount)
                    {
                        this.pageIndex = 0;

                        double minute = DateTime.Now.Subtract((DateTime)Session["LAST_UPDATE"]).TotalMinutes;
                        double timediff = double.Parse(ConfigurationManager.AppSettings["TimeDiff"]);
                        if (minute >= timediff)
                        {
                            this.GetStockByCustomer();
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
    protected void grvSTK_Customer_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName.Equals("BOX_QTY"))
        {
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#2C43AB");
        }
        if (e.DataColumn.FieldName.Equals("QTY"))
        {
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#2C43AB");
        }
        if (e.DataColumn.FieldName.Equals("NO_OF_BOX"))
        {
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#2C43AB");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            this.btnUpdate.Enabled = false;
            this.Timer1.Interval = int.Parse(ConfigurationManager.AppSettings["CtManualRefresh"]);

            ServiceController fgtdbSrv = new ServiceController("FGTDB_Service");

            fgtdbSrv.Refresh();

            if (fgtdbSrv.Status == ServiceControllerStatus.Running)
            {
                fgtdbSrv.ExecuteCommand(128); //128 := for Get Stock by Customer
            }

            this.Timer1.Enabled = true;


        }
        catch (Exception ex)
        {
            this.btnUpdate.Enabled = true;
        }
        
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        this.btnUpdate.Enabled = true;
        this.Timer1.Enabled = false;

        this.GetStockByCustomer();

        this.pageIndex = 0;
        this.grvSTK_Customer.PageIndex = 0;
    }
}
