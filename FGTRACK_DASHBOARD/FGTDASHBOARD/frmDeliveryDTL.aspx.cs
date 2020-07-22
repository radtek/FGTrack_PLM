using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTN.BITS.SQLITE.BLL;
using HTN.BITS.FGTDB.BEL;
using System.Data;
using System.Globalization;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxGridView; 

public partial class frmDeliveryDTL : System.Web.UI.Page
{
    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        this.grvDelivery_Detail.DataSource = (DataTable)ctlState[1];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[2];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = this.grvDelivery_Detail.DataSource;

        return ctlState;
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        this.Page.RegisterRequiresControlState(this);
    }


    #region VARIABLE MEMBER

    private DataTable dtResult;    
    private string partyid;
    private string etd;
    private string wh;
    private string status;    

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["strPARTY_ID"] != null)
            {
                partyid = Request.QueryString["strPARTY_ID"].ToString();
            }

            if (Request.QueryString["strETD"] != null)
            {
                etd = Request.QueryString["strETD"].ToString();
            }

            if (Request.QueryString["strWH_ID"] != null)
            {
                wh = Request.QueryString["strWH_ID"].ToString();
            }

            if (Request.QueryString["strSTATUS"] != null)
            {
                status = Request.QueryString["strSTATUS"].ToString();
            }
           

            DateTime dateSel = DateTime.ParseExact(etd, "yyyyMMdd HH:mm tt", DateTimeFormatInfo.CurrentInfo);

            this.lblCustomer.Text = partyid;
            this.lblDate.Text = dateSel.ToString("dd-MM-yyyy HH:mm tt");
            this.lblStatus.Text = status.ToUpper();
            switch (status.ToLower())
            {
                case "complete":
                    this.lblStatus.ForeColor = System.Drawing.Color.SkyBlue;
                    this.grvDelivery_Detail.Columns["PICKED_QTY"].Caption = "PICKED**";
                    this.grvDelivery_Detail.Columns["LOADED_QTY"].Caption = "LOAD**";
                    break;
                case "pick-load":
                    //this.lblStatus.ForeColor = System.Drawing.Color.Green;
                    this.lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0FAB0F");
                    this.grvDelivery_Detail.Columns["PICKED_QTY"].Caption = "PICKED**";
                    this.grvDelivery_Detail.Columns["LOADED_QTY"].Caption = "LOAD**";
                    break;
                case "not compt":
                    this.lblStatus.ForeColor = System.Drawing.Color.Yellow;
                    this.grvDelivery_Detail.Columns["PICKED_QTY"].Caption = "PICKED**";
                    this.grvDelivery_Detail.Columns["LOADED_QTY"].Caption = "LOAD**";
                    break;
                case "delay":
                    this.lblStatus.ForeColor = System.Drawing.Color.Red;
                    this.grvDelivery_Detail.Columns["PICKED_QTY"].Caption = "****";
                    this.grvDelivery_Detail.Columns["LOADED_QTY"].Caption = "DELAY";                    
                    break;
                case "no plan":
                    this.lblStatus.ForeColor = System.Drawing.Color.White;
                    this.grvDelivery_Detail.Columns["PICKED_QTY"].Caption = "PICKED**";
                    this.grvDelivery_Detail.Columns["LOADED_QTY"].Caption = "LOAD**";
                    break;
                default:
                    this.lblStatus.ForeColor = System.Drawing.Color.White;                    
                    break;

            }

            this.GetDeliveryDetail(partyid, dateSel, wh, status);

            HeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? HeaderFilterMode.CheckedList : HeaderFilterMode.List;
            foreach (GridViewDataColumn column in grvDelivery_Detail.Columns)
                column.Settings.HeaderFilterMode = headerFilterMode;
        }
    }

    #region Method Member

    private void GetDeliveryDetail(string partyid, DateTime date, string wh,string status)
    {
        DataTable dtResult;         
        try
        {

            using (QuerySqliteBLL queryBll = new QuerySqliteBLL())
            {
                dtResult = queryBll.GetDeliveryDTL(partyid,date,wh,status);
            }

            this.grvDelivery_Detail.DataSource = dtResult;
            this.grvDelivery_Detail.DataBind();

        }
        catch (Exception ex)
        {


        }
    }
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmDelivery.aspx");
    }
    protected void grvDelivery_Detail_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        status = this.lblStatus.Text.ToString(); 
        if (status.ToLower() == "delay")
        {
            if (e.GetValue("LOADED_QTY") != null)
            {
                Int32 iTargetValue = Convert.ToInt32(e.GetValue("LOADED_QTY"));

                if (e.DataColumn.FieldName.Equals("LOADED_QTY"))
                {
                    if (iTargetValue > 0)
                    {
                        e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    }
                    else
                    {
                        e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                }
            }
        }      
        
    }
   
}
