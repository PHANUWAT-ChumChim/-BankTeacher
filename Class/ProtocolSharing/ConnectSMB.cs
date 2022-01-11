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
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] INSert UploadFile log INPUT: {TeacherNo} {FileTypeNo} {PathFile} {TeacherAddBy} {LoanID} </para> 
        /// </summary> 
        private static String[] SQLDefault = new String[]
         { 
           //[0] INSert UploadFile log INPUT: {TeacherNo} {FileTypeNo} {PathFile} {TeacherAddBy}  {LoanID}
           "INSERT INTO EmployeeBank.dbo.tblFile (TeacherNo,FiletypeNo,pathFile,TeacherAddBy, LoanID ,DateAddFile) \r\n " +
          "VALUES ('{TeacherNo}','{FileTypeNo}','{PathFile}','{TeacherAddBy}',{LoanID},CURRENT_TIMESTAMP)"
           ,

         };
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
                // IP พี่ตังค์ \\LAPTOP-A1H4E5P4\ShareFileTestSBM
                // IP  PathFile = this.networkPath = @"\\192.168.1.3\ShareFileTestSBM\" + Location + @"\";
                PathFile = this.networkPath = @"\\LAPTOP-A1H4E5P4\ShareFileTestSBM\" + Location + @"\";
-
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
                if(Bank.Add_Member.infoMeber.OroD != "ลบ")
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
            String ID = "";
            String LocalReplace = "";

            private void CancelToken()
            {
                var source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                Task.Factory.StartNew(() => CheckFile(ID,LocalReplace), token);
                source.CancelAfter(5000);
            }

            Thread ThreadCheckFile;
            public String ThreadCheckFiles(string id = "" , String localreplace = "")
            {
                ID = id;
                LocalReplace = localreplace;
                
                Stopwatch time = new Stopwatch();
                ThreadCheckFile = new Thread(() => CheckFile(id , localreplace));
                ThreadCheckFile.Start();
                time.Start();

                while (ThreadCheckFile.ThreadState == System.Threading.ThreadState.Running)
                {
                    if (time.ElapsedMilliseconds >= 5000 && ThreadCheckFile.IsAlive)
                    {
                        StatusRetrun = "หมดเวลาการเชื่อมต่อ";
                        //ThreadCheckFile.Abort();
                        break;
                    }
                }
                time.Stop();

                return StatusRetrun;
            }

            public void CheckFile(string ID = "", String LocalReplace = "")
            {
                try
                {
                    using (var network = new NetworkConnection(networkPath, networkCredential))
                    {
                        network.Connect();
                        String path = PathFile;
                        int Count = 0;
                        System.IO.DirectoryInfo par = new System.IO.DirectoryInfo(path);
                        foreach (System.IO.FileInfo f in par.GetFiles())
                        {
                            if (f.Name.Contains(ID))
                            {
                                Count++;
                                break;
                            }
                        }
                        foreach(System.IO.FileInfo f in par.GetFiles())
                        {
                            if (f.Name.Contains(ID))
                            {
                                if(Count > 1)
                                    for(int x = 0; x < Count; x++)
                                    {
                                        if(f.Name
                                            .Replace(LocalReplace+"_","")
                                            .Replace(".pdf","")
                                            .Replace("_"+x,"") == ID)
                                        {
                                            Count++;
                                            break;
                                        }
                                    }
                            }
                            else if (f.Name
                                .Replace(LocalReplace + "_", "")
                                .Replace(".pdf", "") == ID)
                            {
                                Count++;
                                break;
                            }
                        }
                        if (Count != 0 && path != "")
                        {
                            network.Dispose();
                            StatusRetrun = "มีเอกสารแล้ว";
                            return;
                        }
                        else
                        {
                            network.Dispose();
                            StatusRetrun = "ไม่พบเอกสารโปรดอัพโหลด เอกสารก่อนทำรายการ";
                            return;
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
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
            public String SendFile(String LocationFile, String TargetFile , String TeacherNo , int FileTypeNo , String TeacherAddBy , String LoanID = "NULL")
            {
                Locationfile_TargetFile SetFile = new Locationfile_TargetFile();
                SetFile.LocationFile = LocationFile;
                SetFile.TargetFile = TargetFile;
                Stopwatch time = new Stopwatch();
                SendFileThread = new Thread(() => FileSendThread(SetFile , TeacherNo , FileTypeNo , TeacherAddBy , LoanID));
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
            public void FileSendThread(Locationfile_TargetFile SetFile, String TeacherNo, int FileTypeNo, String TeacherAddBy, String LoanID = "NULL")
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
                                    path = path.Replace(".pdf", "_" + (x + 1) + ".pdf");
                                    File.Copy(SetFile.LocationFile, path);
                                    break;
                                }
                            }

                        }
                        BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                            .Replace("{TeacherNo}",TeacherNo)
                            .Replace("{FileTypeNo}",FileTypeNo.ToString())
                            .Replace("{PathFile}", path)
                            .Replace("{TeacherAddBy}",TeacherAddBy)
                            .Replace("{LoanID}",LoanID));
                    }
                    SetFile.Return = "อัพโหลดเอกสารสำเร็จ";
                    return;
                }
                catch
                {
                    SetFile.Return = "ไม่สามารถอัพโหลดเอกสารได้";
                    return;
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