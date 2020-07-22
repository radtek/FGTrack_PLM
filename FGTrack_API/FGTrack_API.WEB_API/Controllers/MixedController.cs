using HTN.BITS.FGTRACK.BEL;
using HTN.BITS.FGTRACK.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace FGTrack_API.WEB_API.Controllers
{
    public class MixedController : ApiController
    {
        ResponseResult response;

        [Route("StartMixing")]
        [HttpGet]
        public ResponseResult StartMixing(decimal percen, int noOfBag, string userid)
        {
            response = new ResponseResult();

            using (MixedBLL mixedBll = new MixedBLL())
            {
                response = mixedBll.StartMixing(percen, noOfBag, userid);
            }

            return response;
        }

        [Route("ScanMixingLabel")]
        [HttpGet]
        public ResponseResult ScanMixingLabel(string serialno, string mixedno, string mtlCode, string userid)
        {
            response = new ResponseResult();

            if (!string.IsNullOrEmpty(mtlCode))
            {
                var replacements = new Dictionary<string, string>()
                {
                   {"(plus)","+"},
                   {"(percen)","%"}
                };

                var regex = new Regex(String.Join("|", replacements.Keys.Select(k => Regex.Escape(k))));
                mtlCode = regex.Replace(mtlCode, m => replacements[m.Value]);
            }

            using (MixedBLL mixedBll = new MixedBLL())
            {
                response = mixedBll.ScanMixingLabel(serialno, mixedno, mtlCode, userid);
            }

            return response;
        }

        [Route("UpdateMixedQty")]
        [HttpGet]
        public ResponseResult UpdateMixedQty(string serialno, string mixedno, decimal mixQty)
        {
            response = new ResponseResult();

            using (MixedBLL mixedBll = new MixedBLL())
            {
                response = mixedBll.UpdateMixedQty(serialno, mixedno, mixQty);
            }

            return response;
        }


    }
}