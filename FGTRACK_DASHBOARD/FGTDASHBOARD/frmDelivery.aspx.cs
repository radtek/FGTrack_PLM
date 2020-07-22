using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using HTN.BITS.SQLITE.BLL;
using System.Data;
using System.Globalization;
using System.ServiceProcess;

public partial class frmDelivery : System.Web.UI.Page
{
    private DataTable dtResult;
    private int pageIndex = 0;

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        this.ASPxGridView1.DataSource = (DataTable)ctlState[1];
        this.pageIndex = (int)ctlState[2];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[3];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = this.ASPxGridView1.DataSource;
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
            BindingBandColumns();
            BindingDeliveryBoard();
        }
 
    }

    protected void hyperLink_Init(object sender, EventArgs e)
    {
        System.Drawing.Color showcolor = Color.Transparent;
        bool isEnableLink = false;

        ASPxHyperLink link = (ASPxHyperLink)sender;

        GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)link.NamingContainer;

        int rowVisibleIndex = templateContainer.VisibleIndex;

        string status = templateContainer.Grid.GetRowValues(rowVisibleIndex, templateContainer.Column.FieldName).ToString();
        string partyid = templateContainer.Grid.GetRowValues(rowVisibleIndex, "PARTY_ID").ToString();
       
        string fieldSelect = templateContainer.Column.FieldName.Substring(0, 2);

        string etd_date = templateContainer.Grid.GetRowValues(rowVisibleIndex, fieldSelect + "_ETD_DATE").ToString();
        string etd_time = templateContainer.Grid.GetRowValues(rowVisibleIndex, fieldSelect + "_ETD_TIME").ToString();
            

        string etd = etd_date + " " + etd_time; 

        string wh = templateContainer.Grid.GetRowValues(rowVisibleIndex, "WH_ID").ToString();

        switch (status.ToString().ToLower())
        {
            case "complete":
                showcolor = System.Drawing.Color.SkyBlue;
                isEnableLink = true;
                break;
            case "pick-load":
                //showcolor = System.Drawing.Color.Green;
                showcolor = System.Drawing.ColorTranslator.FromHtml("#0FAB0F");
                isEnableLink = true;
                break;
            case "not compt":
                showcolor = System.Drawing.Color.Yellow;
                isEnableLink = true;
                break;
            case "delay":
                showcolor = System.Drawing.Color.Red;
                isEnableLink = true;
                break;
            case "no plan":
                showcolor = System.Drawing.Color.White;
                isEnableLink = false;
                break;
            default:
                showcolor = System.Drawing.Color.White;
                isEnableLink = false;
                break;

        }


        //link.NavigateUrl = "javascript:void(0);";


        string detailUrl = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") +
            string.Format("frmDeliveryDTL.aspx?strPARTY_ID={0}&strETD={1}&strWH_ID={2}&strSTATUS={3}", partyid, etd, wh, status);
        //string url = string.Format("~//frmDeliveryDTL.aspx?strPARTY_ID={0}&strETD={1}&strWH_ID={2}&strSTATUS={3}", partyid, etd, wh, status);        
        link.NavigateUrl = detailUrl;//"~/frmDeliveryDTL.aspx";
        link.Text = status;
        link.ForeColor = showcolor;
        link.Font.Size = 13;
        link.Font.Bold = true;
        link.Enabled = isEnableLink;
        
        //link.ClientSideEvents.Click = string.Format("function(s, e) {{ ShowDetailPopup('{0}', '{1}', '{2} {3}'); }}", status, partyid, etd_date, etd_time);
        
    }

    protected void ASPxGridView1_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        //int stockbox = Convert.ToInt32(ASPxGridView1.GetRowValues(e.VisibleRowIndex, "STOCK_BOX"));
        //if (stockbox == 0)
        //{
        //    e.Column.PropertiesEdit.DisplayFormatString = "#,##0";
        //}
        //else
        //{
        //    e.Column.PropertiesEdit.DisplayFormatString = "#,##0";
        //}

        //int stockpcs = Convert.ToInt32(ASPxGridView1.GetRowValues(e.VisibleRowIndex, "STOCK_PCS"));
        //if (stockpcs == 0)
        //{
        //    e.Column.PropertiesEdit.DisplayFormatString = "#,##0";
        //}
        //else
        //{
        //    e.Column.PropertiesEdit.DisplayFormatString = "#,##0";
        //}

        //int qtybox = Convert.ToInt32(ASPxGridView1.GetRowValues(e.VisibleRowIndex, "STD_BOX_QTY"));
        //if (qtybox == 0)
        //{
        //    e.Column.PropertiesEdit.DisplayFormatString = "#,###";
        //}
        //else
        //{
        //    e.Column.PropertiesEdit.DisplayFormatString = "#,##0";
        //}
        //int freebox = 0;
        //int minbox = Convert.ToInt32(ASPxGridView1.GetRowValues(e.VisibleRowIndex, "MIN_BOX"));
        //int maxbox = Convert.ToInt32(ASPxGridView1.GetRowValues(e.VisibleRowIndex, "MAX_BOX"));

        //if (e.Column.FieldName == "FREE_BOX")
        //{
        //    freebox = Convert.ToInt32(e.Value);

        //    if (freebox < minbox)
        //    {
        //        e.Column.CellStyle.ForeColor = System.Drawing.Color.Red;

        //    }
        //    else if(freebox > minbox && freebox < maxbox)

        //    {
        //        e.Column.CellStyle.ForeColor = System.Drawing.Color.SkyBlue;
        //    }
        //    else
        //    {
        //        e.Column.CellStyle.ForeColor = System.Drawing.Color.Black;
        //    }
        //}

    }

    protected void ASPxGridView1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Preview || e.RowType == GridViewRowType.Data)

        //if (e.RowType == GridViewRowType.Data)
        //{
        //    ASPxGridView gridView = sender as ASPxGridView;
        //    //e.Row.BackColor = e.VisibleIndex % 2 == 0 ? gridView.Styles.Row.BackColor : gridView.Styles.AlternatingRow.BackColor;

        //    if (e.VisibleIndex % 2 == 0)
        //    {

        //        //gridView.Styles.Row.BackColor = System.Drawing.Color.DarkBlue;
        //        gridView.Styles.Row.BackColor = System.Drawing.Color.FromName(Convert.ToString(ConfigurationManager.AppSettings["BackColorRow"]));


        //    }

        //    e.Row.Height = Unit.Pixel(Convert.ToInt32(ConfigurationManager.AppSettings["GridRowHeight"]));

        //}
    }

    protected void ASPxGridView1_ClientLayout(object sender, DevExpress.Web.ASPxClasses.ASPxClientLayoutArgs e)
    {

    }

    private void BindingBandColumns()
    {
        int i = 1;
        List<DateTime> listDt;
        DevExpress.Web.ASPxGridView.GridViewBandColumn banShowDate;

        using (QuerySqliteBLL custBll = new QuerySqliteBLL())
        {
            listDt = custBll.Get3DayWithoutSun(DateTime.Now);
        }

        if (listDt != null)
        {
            foreach (DateTime dt in listDt)
            {
                banShowDate = (DevExpress.Web.ASPxGridView.GridViewBandColumn)this.ASPxGridView1.Columns[i++];
                banShowDate.Caption = dt.ToString("dd/MM/yyyy");
            }
        }
    }

    private void BindingDeliveryBoard()
    {
        //DataTable dtResult;
        try
        {

            using (QuerySqliteBLL queryBll = new QuerySqliteBLL())
            {
                dtResult = queryBll.GetDeliveryBoardControl();
            }

            this.ASPxGridView1.DataSource = dtResult;
            this.ASPxGridView1.DataBind();

            Session["LAST_UPDATE"] = DateTime.Now;
        }
        catch (Exception ex)
        {


        }
       
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MainMenu.aspx");  
    }
    protected void ASPxGridView1_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
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
                            this.BindingDeliveryBoard();
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
            this.Timer1.Interval = int.Parse(ConfigurationManager.AppSettings["DlManualRefresh"]);

            ServiceController fgtdbSrv = new ServiceController("FGTDB_Service");

            fgtdbSrv.Refresh();

            if (fgtdbSrv.Status == ServiceControllerStatus.Running)
            {
                fgtdbSrv.ExecuteCommand(131); //131 := for Get Stock by Delivery
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

        this.BindingBandColumns();
        this.BindingDeliveryBoard();

        this.pageIndex = 0;
        this.ASPxGridView1.PageIndex = 0;


    }
}
