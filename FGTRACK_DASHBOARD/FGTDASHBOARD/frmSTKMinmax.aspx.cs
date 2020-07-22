using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTN.BITS.SQLITE.BLL;
using HTN.BITS.FGTDB.BEL;
using System.Data;
using DevExpress.Web.ASPxGridView;
using System.Configuration;
using System.ServiceProcess;  

public partial class frmSTKMinmax : System.Web.UI.Page
{
    private DataTable dtResult;
    private int pageIndex = 0;

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        this.grvSTK_Minmax.DataSource = (DataTable)ctlState[1];
        this.pageIndex = (int)ctlState[2];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[3];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = this.grvSTK_Minmax.DataSource;
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
            GetStockByMinmax();
        }

        HeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? HeaderFilterMode.CheckedList : HeaderFilterMode.List;
        foreach (GridViewDataColumn column in grvSTK_Minmax.Columns)
            column.Settings.HeaderFilterMode = headerFilterMode;
    }

    #region Method Member
    private void GetStockByMinmax()
    {
        string s1 = string.Empty;
        string s2 = string.Empty;
        string s3 = string.Empty;

        if (this.cblstatus.Items[0].Selected)
            s1 = cblstatus.Items[0].Value.ToString();
        if (this.cblstatus.Items[1].Selected)
            s2 = cblstatus.Items[1].Value.ToString();
        if (this.cblstatus.Items[2].Selected)
            s3 = cblstatus.Items[2].Value.ToString();

        //DataTable dtResult;
        try
        {
            using (QuerySqliteBLL queryBll = new QuerySqliteBLL())
            {
                dtResult = queryBll.GetStockbyMinmax(s1,s2,s3);
            }

            this.grvSTK_Minmax.DataSource = dtResult;
            this.grvSTK_Minmax.DataBind();

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

    protected void grvSTK_Minmax_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if ((e.GetValue("MIN_BOX") != null) && (e.GetValue("STOCK_BOX") != null))    
        {
            Int32 iTargetValue = Convert.ToInt32(e.GetValue("MIN_BOX"));
            Int32 iActualValue = Convert.ToInt32(e.GetValue("STOCK_BOX"));
            if (e.DataColumn.FieldName.Equals("STOCK_BOX"))        
            {            
                if (iActualValue < iTargetValue)            
                {   
                    e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9B0202");
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBCBF4");
                }            
                else            
                {
                    e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0202A0");
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBCBF4");
                }        
            }    
        }

        if ((e.GetValue("EXPECTED_DELAY") != null) && (e.GetValue("PICK_PENDING") != null))
        {
            //Int32 iTargetValue = Convert.ToInt32(e.GetValue("EXPECTED_DELAY"));
            Int32 iActualValue = Convert.ToInt32(e.GetValue("PICK_PENDING"));
            if (e.DataColumn.FieldName.Equals("PICK_PENDING"))
            {
                if (iActualValue > 0)
                {
                    e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }              
            }
        }

    }
    protected void grvSTK_Minmax_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs e)
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
                            this.GetStockByMinmax();
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

    protected void cblstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetStockByMinmax();
        this.pageIndex = 0;
        this.grvSTK_Minmax.PageIndex = 0;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            this.btnUpdate.Enabled = false;
            this.Timer1.Interval = int.Parse(ConfigurationManager.AppSettings["MmManualRefresh"]);

            ServiceController fgtdbSrv = new ServiceController("FGTDB_Service");

            fgtdbSrv.Refresh();

            if (fgtdbSrv.Status == ServiceControllerStatus.Running)
            {
                fgtdbSrv.ExecuteCommand(130); //130 := for Get Stock by Min Max
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

        this.GetStockByMinmax();

        this.pageIndex = 0;
        this.grvSTK_Minmax.PageIndex = 0;
    }
}
