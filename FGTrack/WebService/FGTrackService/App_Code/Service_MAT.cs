using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FGTrackService.BLL;
using HTN.BITS.BEL;


/// <summary>
/// Summary description for Service_MAT
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_MAT : System.Web.Services.WebService
{

    public Service_MAT()
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
                result = userBLL.Material_CheckValidationUser(userid);
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    #endregion

    #region Material

    [WebMethod(Description = "Check Existing Location", EnableSession = true)]
    public bool CheckExistLocation(string location)
    {
        bool result = false;
        try
        {
            using (MaterialBLL materialBll = new MaterialBLL())
            {
                result = materialBll.CheckExistLocation(location);
            }
        }
        catch (Exception ex)
        {
            result = false;
        }

        return result;
    }

    [WebMethod(Description = "Scan Material In Complete", EnableSession = true)]
    public MaterialCard ScanMatIn_Complete(string serialno, string location, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        MaterialCard mtl = null;

        //remark userid value := userid & serial #
        string serial = string.Empty;

        string[] user_v = userid.Split('|');

        if (user_v.Length.Equals(2))
        {
            userid = user_v[0];
            serial = user_v[1];
        }

        try
        {
            using (MaterialBLL materialBll = new MaterialBLL())
            {
                mtl = materialBll.ScanMatIn_Complete(serialno, location, userid, out resultMsg);
            }

           // UserOnline.ProcessUserOnline(serial, userid, "Scan Material In.");
        }
        catch (Exception ex)
        {
            mtl = null;
            throw ex;
        }

        return mtl;
    }

    [WebMethod(Description = "Scan Material Out Complete", EnableSession = true)]
    public MaterialCard ScanMatOut_Complete(string serialno, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        MaterialCard mtl = null;

        //remark userid value := userid & serial #
        string serial = string.Empty;

        string[] user_v = userid.Split('|');

        if (user_v.Length.Equals(2))
        {
            userid = user_v[0];
            serial = user_v[1];
        }

        try
        {
            using (MaterialBLL materialBll = new MaterialBLL())
            {
                mtl = materialBll.ScanMatOut_Complete(serialno, userid, out resultMsg);
            }

           // UserOnline.ProcessUserOnline(serial, userid, "Scan Material Out.");
        }
        catch (Exception ex)
        {
            mtl = null;
            throw ex;
        }

        return mtl;
    }

    [WebMethod(Description = "Scan Check Material Status", EnableSession = true)]
    public MaterialCard ScanMat_Status(string serialno, out string resultMsg)
    {
        resultMsg = string.Empty;
        MaterialCard mtl = null;

        try
        {
            using (MaterialBLL materialBll = new MaterialBLL())
            {
                mtl = materialBll.ScanMat_Status(serialno, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            mtl = null;
            throw ex;
        }

        return mtl;
    }

    [WebMethod(Description = "Scan Check Material Stock", EnableSession = true)]
    public MaterialCard ScanMat_Stock(string serialno, out string resultMsg)
    {
        resultMsg = string.Empty;
        MaterialCard mtl = null;
        try
        {
            using (MaterialBLL materialBll = new MaterialBLL())
            {
                mtl = materialBll.ScanMat_Stock(serialno, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            mtl = null;
            throw ex;
        }

        return mtl;
    }

    [WebMethod(Description = "Scan Material Change Location", EnableSession = true)]
    public MaterialCard ScanMat_ChangeLocation(string serialno, string location, string userid, out string resultMsg)
    {
        resultMsg = string.Empty;
        MaterialCard mtl = null;

        //remark userid value := userid & serial #
        string serial = string.Empty;

        string[] user_v = userid.Split('|');

        if (user_v.Length.Equals(2))
        {
            userid = user_v[0];
            serial = user_v[1];
        }

        try
        {
            using (MaterialBLL materialBll = new MaterialBLL())
            {
                mtl = materialBll.ScanMat_ChangeLocation(serialno, location, userid, out resultMsg);
            }
        }
        catch (Exception ex)
        {
            mtl = null;
            throw ex;
        }

        return mtl;
    }

    #endregion

}

