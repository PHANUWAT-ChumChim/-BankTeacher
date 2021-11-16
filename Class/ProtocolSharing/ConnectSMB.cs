using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static BankTeacher.Class.ProtocolSharing.SBM;

namespace BankTeacher.Class.ProtocolSharing
{
    class ConnectSMB
    {
        public class SmbFileContainer
        {
            private readonly NetworkCredential networkCredential;
            // Path to shared folder:
            public readonly string networkPath;
            public static String PathFile;

            public SmbFileContainer(String Location)
            {
                PathFile = this.networkPath = @"\\166.166.4.189\Newfolder\" + Location + @"\";
                var userName = "tang1811";
                var password = "123456789";
                var domain = "";
                networkCredential = new NetworkCredential(userName, password, domain);
                //NetworkCredential a = new NetworkCredential();
            }

            public bool IsValidConnection()
            {
                using (var network = new NetworkConnection($"{networkPath}", networkCredential))
                {
                    var result = network.Connect();

                    return result != 0;
                }
            }

            public void CreateFile(string targetFile, string content)
            {
                using (var network = new NetworkConnection(networkPath, networkCredential))
                {
                    network.Connect();
                    var file = Path.Combine(this.networkPath, targetFile);
                    if (!File.Exists(file))
                    {
                        using (File.Create(file)) { };
                        using (StreamWriter sw = File.CreateText(file))
                        {
                            sw.WriteLine(content);
                        }
                    }
                }
            }
            public String SendFile(String LocationFile, String TargetFile)
            {
                //String[] d = (CBMonth.SelectedItem as String[]);
                //String[] File = new string[] { LocationFile, TargetFile };
                Locationfile_TargetFile SetFile = new Locationfile_TargetFile();
                SetFile.LocationFile = LocationFile;
                SetFile.TargetFile = TargetFile;

                Thread SendFileThread = new Thread(() => FileSendThread(SetFile));
                SendFileThread.Start();

                if (SendFileThread.IsAlive)
                {

                }
                //try
                //{
                //    using (NetworkConnection network = new NetworkConnection(networkPath, networkCredential))
                //    {
                //        network.Connect();
                //        var path = Path.Combine(networkPath, TargetFile);
                //        if (!File.Exists(path))
                //        {
                //            File.Copy(LocationFile, path, true);
                //        }
                //        else
                //        {
                //            for (int x = 0; x < x + 1; x++)
                //            {
                //                if (!File.Exists(path.Replace(".pdf", "_" + (x + 1) + ".pdf")))
                //                {
                //                    File.Copy(LocationFile, path
                //                        .Replace(".pdf", "_" + (x + 1) + ".pdf"), true);
                //                    break;
                //                }
                //            }

                //        }
                //        return "Upload File Complete";
                //    }

                //}
                //catch
                //{
                //    return "UploadFail { Error อะ ก็ตามนั้น }";
                //}
            }

            public void FileSendThread(Locationfile_TargetFile SetFile)
            {
                
                try
                {
                    using (NetworkConnection network = new NetworkConnection(networkPath, networkCredential))
                    {
                        network.Connect();
                        var path = Path.Combine(networkPath, SetFile.TargetFile);
                        if (!File.Exists(path))
                        {
                            File.Copy(SetFile.LocationFile, path, true);
                        }
                        else
                        {
                            for (int x = 0; x < x + 1; x++)
                            {
                                if (!File.Exists(path.Replace(".pdf", "_" + (x + 1) + ".pdf")))
                                {
                                    File.Copy(SetFile.LocationFile, path
                                        .Replace(".pdf", "_" + (x + 1) + ".pdf"), true);
                                    break;
                                }
                            }

                        }
                    }
                    SetFile.Return = "อัพโหลดสำเร็จ";
                }
                catch
                {
                    SetFile.Return = "ไม่สามารถอัพโหลดได้";
                }
            }
        }
        public class Locationfile_TargetFile
        {
            public String LocationFile
            {
                get;
                set;
            }
            public String TargetFile
            {
                get;
                set;
            }
            public String Return
            {
                get;
                set;
            }
        }
    }
}