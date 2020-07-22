using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for TransferFile
/// </summary>
public class TransferFile
{
    public TransferFile()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string WriteBinarFile(byte[] fs, string path, string fileName)
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
