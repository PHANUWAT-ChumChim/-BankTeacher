using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using static BankTeacher.Class.ProtocolSharing.SBM;

namespace BankTeacher.Class.ProtocolSharing
{
    class ConnectSMB
    {
        public static List<BankTeacher.Class.linkedFile> file = new List<BankTeacher.Class.linkedFile>();
        public static String StatusRetrun = "";
        public class SmbFileContainer
        {
            private readonly NetworkCredential networkCredential;
            // Path to shared folder:
            public readonly string networkPath;
            public static String PathFile;

            public SmbFileContainer(String Location)
            {
                PathFile = this.networkPath = @"\\192.168.1.3\ShareFileTestSBM\" + Location + @"\";
                var userName = "tang1811";
                var password = "123456789";
                var domain = "";
                networkCredential = new NetworkCredential(userName, password, domain);
                StatusRetrun = "";
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
            Thread ThreadOPFile;
            public String ThreadOpenFile(string ContainsName = "")
            {
                Stopwatch time = new Stopwatch();
                ThreadOPFile = new Thread(() => GetFile(ContainsName));
                ThreadOPFile.Start();
                time.Start();

                while (ThreadOPFile.ThreadState == System.Threading.ThreadState.Running)
                {
                    if (time.ElapsedMilliseconds >= 5000 && ThreadOPFile.IsAlive)
                    {
                        ThreadOPFile.Abort();
                        StatusRetrun = "หมดเวลาการเชื่อมต่อ";
                        break;
                    }
                }
                time.Stop();

                return StatusRetrun;
            }
            public void GetFile(string ContainsName = "")
            {
                using (var network = new NetworkConnection(networkPath, networkCredential))
                {
                    try
                    {
                        network.Connect();
                        List<String> GetFileName = new List<string>();
                        List<DateTime> DateaddFile = new List<DateTime>();
                        GetFileName.Clear();
                        DateaddFile.Clear();
                        file.Clear();
                        String path = PathFile;
                        System.IO.DirectoryInfo par = new System.IO.DirectoryInfo(path);
                        foreach (System.IO.FileInfo f in par.GetFiles())
                        {
                            if (f.Name.Contains(ContainsName))
                            {
                                FileInfo fi = new FileInfo(f.FullName);
                                DateTime created = Convert.ToDateTime(fi.CreationTime);
                                DateTime lastmodified = fi.LastWriteTime;
                                GetFileName.Add(f.Name);
                                DateaddFile.Add(created);
                            }
                        }
                        if (GetFileName.Count != 0 && path != "")
                        {
                            for (int x = 0; x < GetFileName.Count; x++)
                            {
                                file.Add(new BankTeacher.Class.linkedFile(GetFileName[x].ToString(), DateaddFile[x]));
                            }
                            BankTeacher.Bank.SelectFile SF = new BankTeacher.Bank.SelectFile(path);
                            SF.ShowDialog();
                            network.Dispose();
                        }
                        else
                        {
                            network.Dispose();
                            StatusRetrun = "ไม่พบไฟล์";
                            return;
                        }

                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error This :=> " + e);
                        return;
                    }
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
            Thread SendFileThread;
            public String SendFile(String LocationFile, String TargetFile)
            {
                Locationfile_TargetFile SetFile = new Locationfile_TargetFile();
                SetFile.LocationFile = LocationFile;
                SetFile.TargetFile = TargetFile;
                Stopwatch time = new Stopwatch();
                SendFileThread = new Thread(() => FileSendThread(SetFile));
                SendFileThread.Start();
                time.Start();

                while (SendFileThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    if (time.ElapsedMilliseconds >= 5000 && SendFileThread.IsAlive)
                    {
                        SendFileThread.Abort();
                        SetFile.Return = "ไม่สารถอัพโหลดได้";
                    }
                }
                time.Stop();

                return SetFile.Return;
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