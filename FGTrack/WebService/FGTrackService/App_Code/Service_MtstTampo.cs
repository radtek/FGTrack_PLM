using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FGTrackService.BLL;
using HTN.BITS.BEL;

/// <summary>
/// Summary description for Service_MtstTampo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_MtstTampo : System.Web.Services.WebService
{

    public Service_MtstTampo()
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
                result = userBLL.MtstTampo_CheckValidationUser(userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    #endregion

    //#region "PC Info"

    //[WebMethod(Description = "Get Product Card Info", EnableSession = true)]
    //public DeliveryOrderInfo GetPCInfo(string doNo, string userid, out string resultMsg) //, out string resultMsg
    //{
    //    resultMsg = string.Empty;
    //    DeliveryOrderInfo doInfo = null;
    //    try
    //    {
    //        using (ProductCardDoBLL pdDoBll = new ProductCardDoBLL())
    //        {
    //            doInfo = pdDoBll.GetDeliveryOrderInfo(doNo, userid, out resultMsg);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        doInfo = null;
    //        throw ex;
    //    }
    //    return doInfo;
    //}

    //#endregion

    #region "FG IN"

    [WebMethod(Description = "Get Update Product Card MTST In Tampo", EnableSession = true)]
    public ProductCard GetUpdatePC_MTST_In_Tampo( string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardDoBLL pdDoBll = new ProductCardDoBLL())
            {
                pdCard = pdDoBll.GetUpdatePC_MTSTIn_Tampo(serialNo, userid, out resultMsg);
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

    [WebMethod(Description = "Get Update Product Card MTST Out Tampo", EnableSession = true)]
    public ProductCard GetUpdatePC_MTST_Out_Tampo(string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardDoBLL pdDoBll = new ProductCardDoBLL())
            {
                pdCard = pdDoBll.GetUpdatePC_MTSTOut_Tampo(serialNo, userid, out resultMsg);
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

    [WebMethod(Description = "Get QC Return Order Info", EnableSession = true)]
    public QCReturn GetQCReturnOrderInfo(string qcReturnNo, string userid, out string resultMsg) //, 
    {
        resultMsg = string.Empty;
        QCReturn qcReturn = null;
        try
        {
            using (QCReturnBLL qcREturnBll = new QCReturnBLL())
            {
                qcReturn = qcREturnBll.GetQcReturnInfo(qcReturnNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            qcReturn = null;
            throw ex;
        }
        return qcReturn;
    }

    [WebMethod(Description = "Get Return Product Card Info", EnableSession = true)]
    public ProductCard GetReturnProductCardInfo(string qcReturnNo, string serialNo, string userid, out string resultMsg) //, out string resultMsg
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (QCReturnBLL qcREturnBll = new QCReturnBLL())
            {
                pdCard = qcREturnBll.GetProductCardInfo(qcReturnNo, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    [WebMethod(Description = "Update Return Product Card", EnableSession = true)]
    public string UpdateReturnProductCard(string qcReturnNo, string serialNo, string userid, out int totalBox)
    {
        totalBox = -1;
        string result = string.Empty;
        try
        {
            using (QCReturnBLL qcREturnBll = new QCReturnBLL())
            {
                result = qcREturnBll.UpdateReturnProductCard(qcReturnNo, serialNo, userid, out totalBox);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    #endregion "Utility"

}

