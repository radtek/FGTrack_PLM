using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Xml;
using System.Net;

/// <summary>
/// Summary description for Service_Center
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service_Center : System.Web.Services.WebService
{

    public Service_Center()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Check Version Application")]
    public string LatestVersionScanner(string currentV)
    {
        string lastestV = null;

        XmlTextReader reader = null;
        try
        {
            string xmlURL = Server.MapPath(".") + "\\UpdateFile\\app_version.xml";

            if (File.Exists(xmlURL))
            {
                reader = new XmlTextReader(xmlURL);

                reader.MoveToContent();
                string elementName = "";

                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ourfancyapp"))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else
                        {
                            if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                            {
                                switch (elementName)
                                {
                                    case "scanner_version":
                                        lastestV = reader.Value;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                lastestV = currentV;
            }

        }
        catch (Exception ex)
        {
            lastestV = currentV;
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }

        return lastestV;
    }

    private TransferFile transferFile = new TransferFile();

    [WebMethod(Description = "Web service provides mothed,return the array of byte")]
    public byte[] DownloadFile(string fileName)
    {
        return transferFile.ReadBinaryFile(Server.MapPath(".") + "\\", fileName);
    }


    [WebMethod(Description = "Web service provides mothed，if upload file successfully。")]
    public string UploadFile(byte[] fs, string fileName)
    {
        return transferFile.WriteBinarFile(fs, Server.MapPath(".") + "\\", fileName);
    }

    class TransferFile
    {
        public TransferFile() { }

        public string WriteBinarFile(byte[] fs, string path, string fileName)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(fs);
                FileStream fileStream = new FileStream(path + fileName, FileMode.Create);
                memoryStream.WriteTo(fileStream);
                memoryStream.Close();
                fileStream.Close();
                fileStream = null;
                memoryStream = null;
                return "File has already uploaded successfully。";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// getBinaryFile：Return array of byte which you specified。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public byte[] ReadBinaryFile(string path, string fileName)
        {
            if (File.Exists(path + fileName))
            {
                try
                {
                    ///Open and read a file。
                    FileStream fileStream = File.OpenRead(path + fileName);
                    return ConvertStreamToByteBuffer(fileStream);
                }
                catch (Exception ex)
                {
                    return new byte[0];
                }
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// ConvertStreamToByteBuffer：Convert Stream To ByteBuffer。
        /// </summary>
        /// <param name="theStream"></param>
        /// <returns></returns>
        public byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
        {
            int b1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            while ((b1 = theStream.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream.ToArray();
        }
    }

}

