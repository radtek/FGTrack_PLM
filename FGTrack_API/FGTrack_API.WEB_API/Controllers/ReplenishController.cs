using HTN.BITS.FGTRACK.BEL;
using HTN.BITS.FGTRACK.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FGTrack_API.WEB_API.Controllers
{
    public class ReplenishController : ApiController
    {
        ResponseResult response;

        [Route("CheckJobOrder")]
        [HttpGet]
        public ResponseResult CheckJobOrder(string jobno)
        {
            response = new ResponseResult();

            using (ReplenishBLL repBll = new ReplenishBLL())
            {
                response = repBll.CheckJobOrder(jobno);
            }

            return response;
        }

        [Route("CheckMachine")]
        [HttpGet]
        public ResponseResult CheckMachine(string mcno)
        {
            response = new ResponseResult();

            using (ReplenishBLL repBll = new ReplenishBLL())
            {
                response = repBll.CheckMachine(mcno);
            }

            return response;
        }

        [Route("StartReplenish")]
        [HttpGet]
        public ResponseResult StartReplenish(string jobno, string mcno, int noOfBag, string userid)
        {
            response = new ResponseResult();

            using (ReplenishBLL repBll = new ReplenishBLL())
            {
                response = repBll.StartReplenish(jobno, mcno, noOfBag, userid);
            }

            return response;
        }

        [Route("ScanRepLabel")]
        [HttpGet]
        public ResponseResult ScanRepLabel(string serialno, string repno, string jobno, string mcno, string userid)
        {
            response = new ResponseResult();

            using (ReplenishBLL repBll = new ReplenishBLL())
            {
                response = repBll.ScanRepLabel(serialno, repno, jobno, mcno, userid);
            }

            return response;
        }
    }
}