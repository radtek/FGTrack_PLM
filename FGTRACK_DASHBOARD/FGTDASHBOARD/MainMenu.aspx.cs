using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    protected void btnCustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmSTKCust.aspx");  
    }
    protected void btnMinmax_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmSTKMinmax.aspx");  
    }
    protected void btnMachine_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmSTKMC.aspx");  
    }
    protected void btnDelivery_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmDelivery.aspx");  
    }
    //protected void btnLogout_Click(object sender, EventArgs e)
    //{
    //    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    //}
}
