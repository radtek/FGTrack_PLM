using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FGTrackService.BLL;
using HTN.BITS.BEL;

/// <summary>
/// Summary description for Service_MtstVertical
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_MtstVertical : System.Web.Services.WebService
{

    public Service_MtstVertical()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    #region "User Authentication"

    [WebMethod(Description = "for check validation user scanner", EnableSession = true)]
    public string CheckValidationUser(string userid)
    {
        string result = string.Empty;
        try
        {
            using (UserBLL userBLL = new UserBLL())
            {
                result = userBLL.MtstVertical_CheckValidationUser(userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    #endregion

    #region "Delivery Order"

    [WebMethod(Description = "Get Product Card Info", EnableSession = true)]
    public DeliveryOrderInfo GetDeliveryOrderInfo(string doNo, string userid, out string resultMsg) //, out string resultMsg
    {
        resultMsg = string.Empty;
        DeliveryOrderInfo doInfo = null;
        try
        {
            using (ProductCardDoBLL pdDoBll = new ProductCardDoBLL())
            {
                doInfo = pdDoBll.GetDeliveryOrderInfo(doNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            doInfo = null;
            throw ex;
        }
        return doInfo;
    }

    #endregion

    #region "FG IN"

    [WebMethod(Description = "Get Update Product Card MTST In", EnableSession = true)]
    public ProductCard GetUpdatePC_MTST_In(string doNo, string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardDoBLL pdDoBll = new ProductCardDoBLL())
            {
                pdCard = pdDoBll.GetUpdatePC_MTSTIn(doNo, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }

        return pdCard;
    }

    #endregion

    #region "FG OUT"

    [WebMethod(Description = "Get Update Product Card MTST Out", EnableSession = true)]
    public ProductCard GetUpdatePC_MTST_Out(string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardDoBLL pdDoBll = new ProductCardDoBLL())
            {
                pdCard = pdDoBll.GetUpdatePC_MTSTOut(serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }

        return pdCard;
    }

    #endregion

    #region "Utility"

    [WebMethod(Description = "Get Product Card Status", EnableSession = true)]
    public ProductCard_Status GetProductCardStatus(string serialNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        ProductCard_Status pdCard = null;
        try
        {
            using (ProductCardStatusBLL pdBll = new ProductCardStatusBLL())
            {
                pdCard = pdBll.MtstVertical_GetProductCardStatus(serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    #endregion "Utility"

}

