using HTN.BITS.FGTRACK.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FGTrack_API.WEB_API.Controllers
{
    public class AutoUpdateController : ApiController
    {
        [Route("GetLatestVersion")]
        [HttpGet]
        public string GetLatestVersion(string curVersion)
        {
            Version resultVersion;
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    resultVersion = adminBll.GetLastestVersion(curVersion);
                }
            }
            catch (Exception ex)
            {
                return curVersion;
            }

            return resultVersion.ToString();
        }

        [Route("GetLatestUpdated")]
        [HttpGet]
        public HttpResponseMessage GetLatestUpdated(string filename)
        {
            try
            {
                string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/LatestUpdated/");

                var fullname = string.Format("{0}/{1}", root, filename);

                if (File.Exists(fullname))
                {
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                    var dataStream = OpenFileStream(fullname);

                    result.Content = new StreamContent(dataStream);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = filename
                    };

                    return result;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "File not found");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private FileStream OpenFileStream(string filename)
        {
            var stream = new FileStream(filename, FileMode.Open);

            return stream;
        }

        [Route("PostLatestUpdated")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostLatestUpdated()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/LatestUpdated/");

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                var filename = provider.Contents.First().Headers.ContentDisposition.FileName;
                var dataStream = await provider.Contents.First().ReadAsStreamAsync();

                CopyStream(dataStream, root + filename);

                var response = Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new StringContent("Successful upload", Encoding.UTF8, "text/plain");
                response.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(@"text/html");

                return response;


            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private void CopyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}