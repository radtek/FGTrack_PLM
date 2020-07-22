using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FGTrackService.BLL;
using HTN.BITS.BEL;

/// <summary>
/// Summary description for Service_FGPRESS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_FGPRESS : System.Web.Services.WebService
{

    public Service_FGPRESS()
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
                result = userBLL.FGPress_CheckValidationUser(userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    #endregion

    #region "FG IN"

    [WebMethod(Description = "Get Update Product Card FG In", EnableSession = true)]
    public ProductCard GetUpdatePC_FG_In(string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardFGINBLL prdFGInBll = new ProductCardFGINBLL())
            {
                pdCard = prdFGInBll.FGPress_GetUpdatePC_FGIn(serialNo, userid, out resultMsg);
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

    //picking
    [WebMethod(Description = "Get Picking Info", EnableSession = true)]
    public PickQty GetPickInfo(string pickNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        PickQty pickQty = null;
        try
        {
            using (ProductCardPICKBLL pdBll = new ProductCardPICKBLL())
            {
                pickQty = pdBll.FGPress_GetPickInfo(pickNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pickQty = null;
            throw ex;
        }
        return pickQty;
    }

    //pingking
    [WebMethod(Description = "Get Update Finish Good Picking Product Card", EnableSession = true)]
    public ProductCard GetUpdatePCPicking(string pickNo, string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardPICKBLL pdBll = new ProductCardPICKBLL())
            {
                pdCard = pdBll.FGPress_GetUpdatePCPicking(pickNo, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }


    //loading
    [WebMethod(Description = "Get Loading Info", EnableSession = true)]
    public LoadQty GetLoadInfo(string loadNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        LoadQty loadQty = null;
        try
        {
            using (ProductCardLOADBLL prdLoadingBll = new ProductCardLOADBLL())
            {
                loadQty = prdLoadingBll.FGPress_GetLoadInfo(loadNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            loadQty = null;
            throw ex;
        }
        return loadQty;
    }

    //Loading
    [WebMethod(Description = "Get Update Finish Good Loading Product Card", EnableSession = true)]
    public ProductCard GetUpdatePCLoading(string loadNo, string serialNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardLOADBLL prdLoadingBll = new ProductCardLOADBLL())
            {
                pdCard = prdLoadingBll.FGPress_GetUpdatePCLoading(loadNo, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    //Loading
    [WebMethod(Description = "Update Loading Seal", EnableSession = true)]
    public string UpdateLoadingSeal(string loadNo, string userid)
    {
        string result = string.Empty;
        try
        {
            using (ProductCardLOADBLL prdLoadingBll = new ProductCardLOADBLL())
            {
                result = prdLoadingBll.FGPress_UpdateLoadingSeal(loadNo, userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }


    #endregion

    #region "Utility"

    [WebMethod(Description = "Get Product Card Status", EnableSession = true)]
    public ProductCardStatusFG GetProductCardStatus(string serialNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        ProductCardStatusFG pdCard = null;
        try
        {
            using (ProductCardStatusBLL pdBll = new ProductCardStatusBLL())
            {
                pdCard = pdBll.FGPress_GetProductCardStatusFG(serialNo, userid, out resultMsg);
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

