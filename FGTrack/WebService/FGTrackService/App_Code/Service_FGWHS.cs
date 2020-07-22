using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using FGTrackService.BLL;
using HTN.BITS.BEL;

/// <summary>
/// Summary description for Service_FGWHS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_FGWHS : System.Web.Services.WebService
{

    public Service_FGWHS()
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
                result = userBLL.FGWHS_CheckValidationUser(userid);
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
                pdCard = prdFGInBll.GetUpdatePC_FGIn(serialNo, userid, out resultMsg);
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
                pickQty = pdBll.GetPickInfo(pickNo, userid, out resultMsg);
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
    public ProductCard GetUpdatePCPicking(string pickNo, string serialNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardPICKBLL pdBll = new ProductCardPICKBLL())
            {
                pdCard = pdBll.GetUpdatePCPicking(pickNo, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    
    //Re-Pack
    [WebMethod(Description = "Get Pallet List", EnableSession = true)]
    public PalletList GetPalletList(string pickno, out string resultMsg) //
    {
        resultMsg = string.Empty;
        PalletList lstPallet = null;

        try
        {
            using (ProductCardRePackBLL prodRePack = new ProductCardRePackBLL())
            {
                lstPallet = prodRePack.GetPalletList(pickno, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            resultMsg = ex.Message;
        }

        return lstPallet;
    }

    [WebMethod(Description = "Get Pallet Info", EnableSession = true)]
    public Pallet GetPalletInfo(string palletno)
    {
        Pallet pallet = null;
        try
        {
            using (ProductCardRePackBLL prodRePack = new ProductCardRePackBLL())
            {
                pallet = prodRePack.GetPalletDetail(palletno);
            }
        }
        catch (Exception ex)
        {
            pallet = null;
            throw ex;
        }
        return pallet;
    }

    [WebMethod(Description = "Get Update Box on Pallet", EnableSession = true)]
    public ProductCard GetUpdatePCPallet(string palletno, string serialNo, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardRePackBLL prodRePack = new ProductCardRePackBLL())
            {
                pdCard = prodRePack.GetUpdatePCRepack(palletno, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    [WebMethod(Description = "Update Loading Seal", EnableSession = true)]
    public string UpdatePalletFinish(string palletno, string userid)
    {
        string result = string.Empty;
        try
        {
            using (ProductCardRePackBLL prodRePack = new ProductCardRePackBLL())
            {
                result = prodRePack.UpdatePalletFinish(palletno, userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
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
                loadQty = prdLoadingBll.GetLoadInfo(loadNo, userid, out resultMsg);
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
                pdCard = prdLoadingBll.GetUpdatePCLoading(loadNo, serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    //New Loading
    [WebMethod(Description = "Get Update Finish Good Loading Product Card", EnableSession = true)]
    public Pallet GetUpdatePalletLoading(string loadNo, string palletno, string userid, out string resultMsg) //, out string resultMsg
    {
        resultMsg = string.Empty;
        Pallet pallet = null;
        try
        {
            using (ProductCardLOADBLL prdLoadingBll = new ProductCardLOADBLL())
            {
                pallet = prdLoadingBll.GetUpdatePalletLoading(loadNo, palletno, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pallet = null;
            throw ex;
        }
        return pallet;
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
                result = prdLoadingBll.UpdateLoadingSeal(loadNo, userid);
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
                pdCard = pdBll.GetProductCardStatusFG(serialNo, userid, out resultMsg);
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

