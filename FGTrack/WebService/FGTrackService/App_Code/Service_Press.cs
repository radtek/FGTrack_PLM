using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FGTrackService.BLL;
using HTN.BITS.BEL;

/// <summary>
/// Summary description for Service_Press
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_Press : System.Web.Services.WebService
{

    public Service_Press()
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
                result = userBLL.Press_CheckValidationUser(userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    #endregion

    #region "Production Finish"

    [WebMethod(Description = "Check Existing Machine", EnableSession = true)]
    public bool CheckExistMachine(string mcNo)
    {
        bool result = false;
        try
        {
            using (MachineBLL mcBll = new MachineBLL())
            {
                result = mcBll.Press_CheckExistMachine(mcNo);
            }
        }
        catch (Exception ex)
        {
            result = false;
        }

        return result;
    }

    [WebMethod(Description = "Get Product Card Info", EnableSession = true)]
    public ProductCard GetProductCardInfo(string serialNo, string processMode, string userid, out string resultMsg) //, out string resultMsg
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardBLL pdBll = new ProductCardBLL())
            {
                pdCard = pdBll.Press_GetProductCardInfo(serialNo, processMode, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    [WebMethod(Description = "Update Product Card", EnableSession = true)]
    public string UpdProductCard(string serialNo, string processMode, string mcNo, int nQty, string userid)
    {
        string result = string.Empty;
        try
        {
            using (ProductCardBLL pdBll = new ProductCardBLL())
            {
                result = pdBll.Press_UpdProductCard(serialNo, mcNo, processMode, nQty, userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }

    #endregion

    #region "Update NG Qty"

    [WebMethod(Description = "Get Job Lot List", EnableSession = true)]
    public List<JobLot> GetJobLotList(string jobNo, string userid)
    {
        List<JobLot> lstJobLot = null;

        try
        {
            using (ProductCardNGBLL prodNGBll = new ProductCardNGBLL())
            {
                lstJobLot = prodNGBll.Press_GetJobLotList(jobNo, userid);
            }
        }
        catch (Exception ex)
        {
            lstJobLot = null;
            throw ex;
        }
        return lstJobLot;
    }

    [WebMethod(Description = "Get Job Lot Info", EnableSession = true)]
    public ProductCard GetJobLotInfo(string jobNo, int lineNo, string userid)
    {
        ProductCard pdCard = null;
        try
        {
            using (ProductCardNGBLL prodNGBll = new ProductCardNGBLL())
            {
                pdCard = prodNGBll.Press_GetJobLotInfo(jobNo, lineNo, userid);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }
        return pdCard;
    }

    [WebMethod(Description = "Update NG Qty", EnableSession = true)]
    public string UpdateNGQty(string jobNo, int lineNo, int nQty, string userid)
    {
        string result = string.Empty;
        try
        {
            using (ProductCardNGBLL aNGBll = new ProductCardNGBLL())
            {
                result = aNGBll.Press_UpdateNGQty(jobNo, lineNo, nQty, userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }

    #endregion

    #region "QC Cargo"

    [WebMethod(Description = "Get Update Product Card QC Info", EnableSession = true)]
    public ProductCard GetUpdatePC_QC_Info(string serialNo, string qcStatus, string userid, out string resultMsg) //
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardQCBLL pdBll = new ProductCardQCBLL())
            {
                pdCard = pdBll.Press_GetUpdatePC_QCInfo(serialNo, qcStatus, userid, out resultMsg);
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

    #region "Replenish Confirmation"

    [WebMethod(Description = "Get Product Card NG Confirm Info", EnableSession = true)]
    public ProductCard GetPC_NGInfo(string serialNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        ProductCard pdCard = null;
        try
        {
            using (ProductCardNGConfBLL pdBll = new ProductCardNGConfBLL())
            {
                pdCard = pdBll.Press_GetPC_NGInfo(serialNo, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            pdCard = null;
            throw ex;
        }

        return pdCard;
    }

    [WebMethod(Description = "Update QC NG Confirm", EnableSession = true)]
    public string UpdateReplenishConfirm(string serialNo, string mode, int qty, string userid)
    {
        string result = string.Empty;
        try
        {
            using (ProductCardNGConfBLL aNGBll = new ProductCardNGConfBLL())
            {
                result = aNGBll.Press_UpdateReplenishConfirm(serialNo, mode, qty, userid);
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
    public ProductCard_Status GetProductCardStatus(string serialNo, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        ProductCard_Status pdCard = null;
        try
        {
            using (ProductCardStatusBLL pdBll = new ProductCardStatusBLL())
            {
                pdCard = pdBll.Press_GetProductCardStatus(serialNo, userid, out resultMsg);
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

