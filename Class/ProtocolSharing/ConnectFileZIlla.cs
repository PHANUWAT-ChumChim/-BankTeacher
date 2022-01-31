using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using WinSCP;
namespace BankTeacher.Class.ProtocolSharing
{
    class FileZilla
    {
        public static List<BankTeacher.Class.linkedFile> file = new List<BankTeacher.Class.linkedFile>();
        public static bool StatusReturn = false;
        protected static bool StatusRunning = false;
        public class FileZillaConnection
        {

            // Path to shared folder:
            public readonly string networkPath;
            public readonly String PathFile;
            public readonly String HostplusPathFile;

            protected static String Folder { get; } = "ShareFile";
            protected static String Location { get; private set; } = "";

            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                FtpSecure = FtpSecure.Explicit,
                HostName = "192.168.1.3",
                UserName = "test",
                Password = "test",
                PortNumber = 21,
                TlsHostCertificateFingerprint = "7c:9e:ef:e8:d7:0a:93:1d:56:53:2e:26:4e:35:d3:b6:41:4c:65:76:16:5c:ff:4c:70:85:dc:06:3d:75:c2:9f"
            };

            public FileZillaConnection(String location)
            {
                StatusReturn = false;
                PathFile = this.networkPath = $"/{Folder}/{location}/";
                this.networkPath = $@"ftp://{sessionOptions.HostName}/{Folder}/{location}/";
                HostplusPathFile = $@"//{sessionOptions.HostName}{PathFile}";
                Location = location;
            }

            Thread ThreadConnected;
            public void FTPSendFile(String pathfile, String ChangeFilename)
            {
                Stopwatch time = new Stopwatch();
                ThreadConnected = new Thread(() => ThreadSendFile(pathfile,ChangeFilename));
                ThreadConnected.Start();
                time.Start();
                StatusRunning = true;
                while (true)
                {
                    if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.IsAlive && StatusRunning)
                    {
                        ThreadConnected.Abort();
                        MessageBox.Show("หมดเวลาการเชื่อมต่อโปรดเชื่อมต่อเครือข่าย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        break;
                    }
                }
                time.Stop();
            }

            public void FTPRemoveFile(String filename)
            {
                Stopwatch time = new Stopwatch();
                ThreadConnected = new Thread(() => ThreadDeleteFile(filename));
                ThreadConnected.Start();
                time.Start();
                StatusRunning = true;
                while (true)
                {
                    if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.IsAlive && StatusRunning)
                    {
                        ThreadConnected.Abort();
                        MessageBox.Show("หมดเวลาการเชื่อมต่อโปรดเชื่อมต่อเครือข่าย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        break;
                    }
                }
                time.Stop();
            }

            public void FTPOpenFile(String filename)
            {
                Stopwatch time = new Stopwatch();
                ThreadConnected = new Thread(() => ThreadOpenFile(filename));
                ThreadConnected.Start();
                time.Start();
                StatusRunning = true;
                while (true)
                {
                    if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.IsAlive && StatusRunning)
                    {
                        ThreadConnected.Abort();
                        MessageBox.Show("หมดเวลาการเชื่อมต่อโปรดเชื่อมต่อเครือข่าย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        break;
                    }
                }
                time.Stop();
            }

            public void FTPMoveFile(String filename, String Foldertarget)
            {
                Stopwatch time = new Stopwatch();
                ThreadConnected = new Thread(() => ThreadMoveLocationFIle(filename, Foldertarget));
                ThreadConnected.Start();
                time.Start();
                StatusRunning = true;
                while (true)
                {
                    if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.IsAlive && StatusRunning)
                    {
                        ThreadConnected.Abort();
                        MessageBox.Show("หมดเวลาการเชื่อมต่อโปรดเชื่อมต่อเครือข่าย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (time.ElapsedMilliseconds >= 5000 && ThreadConnected.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        break;
                    }
                }
                time.Stop();
            }

            public void FTPMoveFileandRename(String filename, String Foldertarget, String Rename)
            {
                Stopwatch time = new Stopwatch();
                ThreadConnected = new Thread(() => ThreadMoveLocationFIleRename(filename, Foldertarget , Rename));
                ThreadConnected.Start();
                time.Start();
                StatusRunning = true;
                while (true)
                {
                    //ให่้เวลานานกว่าอันอื่นหน่อยเพราะ มันทำงานไปหลายจุด
                    if (time.ElapsedMilliseconds >= 7000 && ThreadConnected.IsAlive && StatusRunning)
                    {
                        ThreadConnected.Abort();
                        MessageBox.Show("หมดเวลาการเชื่อมต่อโปรดเชื่อมต่อเครือข่าย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (time.ElapsedMilliseconds >= 7000 && ThreadConnected.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        break;
                    }
                }
                time.Stop();
            }

            private void ThreadSendFile(String pathfile, String Changefilename)
            {
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    if (!session.FileExists(PathFile+Changefilename))
                    {
                        try
                        {
                            // Upload files
                            TransferOptions transferOptions = new TransferOptions();
                            transferOptions.TransferMode = TransferMode.Binary;

                            TransferOperationResult transferResult;
                            transferResult =
                                session.PutFiles(pathfile, PathFile + Changefilename, false, transferOptions);

                            // Throw on any error
                            transferResult.Check();

                            // Print results
                            foreach (TransferEventArgs transfer in transferResult.Transfers)
                            {
                                Console.WriteLine($"Upload of {transfer.FileName} Succeeded");
                            }
                            StatusReturn = true;
                            StatusRunning = false;
                            MessageBox.Show("อัพโหลดไฟล์สำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            if (ThreadConnected.ThreadState == System.Threading.ThreadState.Running)
                            {
                                Console.WriteLine($"Error -> {ex}");
                                StatusRunning = false;
                                MessageBox.Show("อัพโหลดไฟล์ไม่สำเร็จกรุณาลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            StatusReturn = false;
                        }
                    }
                    else
                    {
                        StatusReturn = false;
                        StatusRunning = false;
                        MessageBox.Show("มีเอกสารอยู่ในระบบแล้วไม่สามารถอัพโหลดซ้ำได้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }
            }

            private void ThreadDeleteFile(String filename)
            {
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    if (!session.FileExists(PathFile + filename))
                    {
                        MessageBox.Show("ไม่พบไฟล์","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        StatusReturn = false;
                        StatusRunning = false;
                    }
                    else
                    {
                        try
                        {
                            session.RemoveFile(PathFile + filename);
                            StatusReturn = true;
                            StatusRunning = false;
                            MessageBox.Show("ลบไฟล์เสร็จเรียบร้อย","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        catch
                        {
                            StatusReturn = false;
                            StatusRunning = false;
                            MessageBox.Show("เกิดข้อผิดพลาดโปรดลองใหม่อีกครั้ง","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    }
                    return;
                }
            }

            private void ThreadOpenFile(String filename)
            {
                String ThisPcPath = DowloadFile(filename);
                if(ThisPcPath != null)
                {
                    System.Diagnostics.Process.Start(ThisPcPath);
                    StatusRunning = false;
                    StatusReturn = true;
                }
                else
                {
                    StatusReturn = false;
                    StatusRunning = false;
                }
                return;
            }

            private String DowloadFile(String filename)
            {
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    if (!session.FileExists(PathFile + filename))
                    {
                        MessageBox.Show("ไม่พบไฟล์", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        StatusReturn = false;
                        StatusRunning = false;
                        return null;
                    }
                    else
                    {
                        String ThisPcFilePath = "";
                        if (Location.Contains("RegMember"))
                            ThisPcFilePath = @"C:\BankTeacher\RegMember\" + filename;
                        else if (Location.Contains("Loan"))
                            ThisPcFilePath = @"C:\BankTeacher\Loan\" + filename;
                        else if (Location.Contains("CancelMember"))
                            ThisPcFilePath = @"C:\BankTeacher\CancelMember\" + filename;
                        try
                        {
                            session.GetFileToDirectory(PathFile + filename, ThisPcFilePath);
                            return ThisPcFilePath;
                        }
                        catch
                        {
                            StatusReturn = false;
                            StatusRunning = false;
                            MessageBox.Show("เกิดข้อผิดพลาดโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                    }
                }
            }

            private void ThreadMoveLocationFIle(String filename , String Foldertarget)
            {
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);
                    String targetPath = PathFile.Replace(Location, Foldertarget) + filename;
                    if (!session.FileExists(PathFile + filename))
                    {
                        MessageBox.Show("ไม่พบไฟล์", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        StatusReturn = false;
                        StatusRunning = false;
                        return;
                    }
                    else
                    {
                        try
                        {
                            session.MoveFile(PathFile + filename, targetPath);
                            StatusReturn = true;
                            StatusRunning = false;
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดโปรดลองใหม่อีกครั้ง","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            StatusReturn = false;
                            StatusRunning = false;
                            return;
                        }
                    }
                }
            }

            private void ThreadMoveLocationFIleRename(String filename, String Foldertarget , String Rename)
            {
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);
                    String targetPath = PathFile.Replace(Location, Foldertarget) + filename;
                    if (!session.FileExists(PathFile + filename))
                    {
                        MessageBox.Show("ไม่พบไฟล์", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        StatusReturn = false;
                        StatusRunning = false;
                        return;
                    }
                    else
                    {
                        try
                        {
                            String ThisPcFilePath = "";
                            if (Foldertarget.Contains("RegMember"))
                                ThisPcFilePath = @"C:\BankTeacher\RegMember\" + filename;
                            else if (Foldertarget.Contains("Loan"))
                                ThisPcFilePath = @"C:\BankTeacher\Loan\" + filename;
                            else if (Foldertarget.Contains("CancelMember"))
                                ThisPcFilePath = @"C:\BankTeacher\CancelMember\" + filename;

                            TransferOptions transferOptions = new TransferOptions();
                            transferOptions.TransferMode = TransferMode.Binary;
                            TransferOperationResult transferResult;

                            //DowloadFile
                            session.GetFileToDirectory(PathFile + filename, ThisPcFilePath);
                            //UploadFile
                            transferResult = session.PutFiles(ThisPcFilePath, PathFile.Replace(Location,Foldertarget) + Rename, false, transferOptions);
                            //CheckFile
                            transferResult.Check();
                            StatusReturn = true;
                            StatusRunning = false;
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            StatusReturn = false;
                            StatusRunning = false;
                            return;
                        }
                    }
                }
            }
        }
    }
}
