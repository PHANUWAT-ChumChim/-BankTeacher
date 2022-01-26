using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankTeacher.Class.ProtocolSharing
{
    class ConnectFileZIlla
    {
        public bool UploadFile(string strLocalFile, string strRemoteFile, string strFTPHost, string strUserName, string strPassword)
        { // Output file to remote App Data directory
            byte[] byteBuffer;
            int intBufferSize = 2048; // Buffer size.
            string strErrorMessage = ""; // Error Message upon failure.
            FtpWebRequest ftpRequest = null; // FTP Request object.
            Stream ftpStream = null; // FTP Stream
            bool bolSuccess = true; // Assume "true" for success. If errors out, then "false" for failure.
            int intWriteBytes = 0; // Bytes being written out

            string strRemotePath = strFTPHost + "/" + strRemoteFile; // Becomes essentially: ftp://00.000.000.000//App_Data/AdvertisementRates.xml
            System.IO.FileStream fsLocalStream;
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(strRemotePath); // Create an FTP Request to ftp://00.000.000.000//App_Data/AdvertisementRates.xml
                ftpRequest.Credentials = new NetworkCredential(strUserName, strPassword); // Log in to the FTP Server with the User Name and Password Provided
                ftpRequest.UseBinary = true; // When in doubt, use these options
                ftpRequest.UsePassive = false; // POTENTIAL ERROR: Was = true. But to keep active changed to = false for FileZilla Server.
                ftpRequest.KeepAlive = true;
                ftpRequest.EnableSsl = false;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile; // Specify the Type of FTP Request [ ERRORS OUT ]
                ftpStream = ftpRequest.GetRequestStream(); // Establish Return Communication with the FTP Server
                fsLocalStream = System.IO.File.OpenRead(strLocalFile); // Open a File Stream to Read the File for Upload, at
                                                                       // C:\PollofPolls\PollofPolls\App_Data\AdvertisementRates.xml
                byteBuffer = new byte[intBufferSize]; // Buffer for the Downloaded Data
                intWriteBytes = fsLocalStream.Read(byteBuffer, 0, intBufferSize); // (1) Read in first bytes of the file.
                try // Upload the File by Sending the Buffered Data Until the Transfer is Complete
                {
                    while (intWriteBytes > 0)
                    {
                        ftpStream.Write(byteBuffer, 0, intWriteBytes); // Write out bytes from the prior Read (1) or (2) if looping through the file.
                        intWriteBytes = fsLocalStream.Read(byteBuffer, 0, intBufferSize); // (2) Read in next string of bytes, if any still exist
                    }
                }
                catch (Exception ex) // Discover errors in the reading and writing to the Upload directory.
                {
                    strErrorMessage = ex.Message;
                    bolSuccess = false;
                }
                fsLocalStream.Close(); // (3) Resource Cleanup
                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception ex) // Discover Errors upon connection.
            { // If .UsePassive = false (therefore Active = true) then produced error message is this:
                bolSuccess = false; // "The remote server returned an error: 227 Entering Passive Mode (34,232,100,121,192,8)\r\n."
                strErrorMessage = ex.Message; // If .UsePassive = true (therefore Active = false) then produced error message is this:
            } // "The remote server returned an error: (550) File unavailable (e.g., file not found, no access)."
            return bolSuccess; // Return Success or failure.
        }
    }
}
