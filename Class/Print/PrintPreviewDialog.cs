using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace BankTeacher.Class.Print
{
    class PrintPreviewDialog
    {
        // ======================= ข้อมูลเเบบปริ้น ====================
        //ข้อมูลส่วนตัว
        public static string info_name;
        public static string info_id;
        // ข้อมูลผู้รายการ
        public static string info_TeacherAdd;
        // สถานะ
        public static string info_Payment;
        // หุ้นสะสม
        public static string info_Billpay;
        public static string info_Savingtotel;
        public static string info_datepayShare;
        // กู้
        //public static string info_LoanNo;
        public static string info_Lona_AmountRemain;
        // จ่ายกู้
        public static string info_PayLoanBill;
        public static string info_PayLoandate;
        // ถอนหุ้นสะสม
        public static string info_Loanstatus;
        public static string info_ShareNo;
        // กล่งอเอกสาาร
        public static string info_BillAmounoff;
        public static string info_datepayAmounoff;
        // ยอดที่ถอน
        public static string info_Amounoff;
        public static string info_Amounoffinsystem;
        public static string info_canbeAmounoff;
        // รายงาน
        public static string info_Cash;
        public static string info_Tranfer;
        public static string info_Cradit;
        public static string info_SUMAmount;
        // ReportDividend
        public static string info_RemainInterest;
        public static string info_SavingAmount;
        public static string info_DividendPerShare;
        public static string info_InterestAmount;
        public static string info_InterestNextYear;
        public static string info_DividendAmount;
        // ================= ข้อมูล ===============
        public static int details = 0;
        public static int start_and_stop = 0;
        // ================================================================= info SQL =======================================================================
        // ข้อมูลโรงเรียน
        private static  DataTable DT = Class.SQLConnection.InputSQLMSSQL("SELECT CAST(SchoolNameThai as nvarchar(255)), SchoolNameEng, SchoolNameEngShort,CAST(NameManager as nvarchar),CAST(NameTeacherBig as nvarchar), CAST(AddressLetter + ' ' + CAST(Tel as nvarchar(10)) + ' ' + WebSite as nvarchar (255)),CAST(AddressLetter as nvarchar(255))  \r\n" +
        "FROM BaseData.dbo.tblSchoolInfo;");
        // ข้อมูลรายการ
        private static DataTable DT_ListType = Class.SQLConnection.InputSQLMSSQL("SELECT  TypeNo,TypeName FROM EmployeeBank.dbo.tblBillDetailType");
        static Font THsarabun30 = new Font("TH Sarabun New", 30, FontStyle.Bold);
        static Font THsarabun18 = new Font("TH Sarabun New", 18, FontStyle.Bold);
        static Font THsarabun16 = new Font("TH Sarabun New", 16, FontStyle.Bold);
        static Font THsarabun14 = new Font("TH Sarabun New", 14, FontStyle.Bold);
        static Brush BrushBlack = Brushes.Black;
        static String ThaiSarabun = "TH Sarabun New";
        // ปากกา//
        static Pen PenBlack = new Pen(Color.Black);
        static Pen PenRed = new Pen(Color.Red);
        static Pen PenGreen = new Pen(Color.Green);
        
        // สำปริ้น 2 อย่าง ต้นฉบับกัลสำเนา
        static int Print_two = 0;
        // หน้าตอนนี้
        static int pageNow = 0;
        // ปริ้น รอบ เดียว
        static int onetimestartColumns = 0;
        // ขนาดของหน้ากระดาษ
        static int page_length = 0;
        // รับ เเถวที่ปริ้นอยู่
        static int position = 0, Currentposition_Row = 0;
        // จุดกึ่งกลางเส้น
        static float center = 0;
        static List<float> Center = new List<float>();
        // เส้นปิดข้าง
        static List<float> cutline = new List<float>();
        // เเบบ ปริ้น หน้า สมัคร
        public static void PrintMember(System.Drawing.Printing.PrintPageEventArgs e, String SQLCode, String Day, String Month, String Year, String TeacherNo, String Amount,bool confirmation_scrip,bool confirmation_copy)
        {
            e.HasMorePages = true;
            int PageX = (e.PageBounds.Width - 50);
            int PageY = (e.PageBounds.Height);
            int XP = 0;
            int XD = 0;
            int X = 50;
            int Y = 50;
            int SpacePerRow = 35;
            int CurrentRows = 0;
            SizeF SizeText;
          
            if (Amount == "")
            {
                Amount = BankTeacher.Bank.Menu.startAmountMin.ToString();
            }
            
            int confirmation = 0;
            if (confirmation_scrip == true && confirmation_copy == true)
            {
                confirmation = 1;
            }
            else if (confirmation_scrip == false && confirmation_copy == false)
            {
                confirmation = 0;
            }
            else
            {
                if (confirmation_scrip == true) { confirmation = 2; }
                else { confirmation = 3; }
            }
            if (confirmation != 0 && pageNow == 0)
            {
                if (confirmation == 1)
                {
                    Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack, Print_two, PageX, 0);
                }
                else
                {
                    Print_two++;
                    if (confirmation == 2)
                    {
                        Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack, 0, PageX, 0);
                    }
                    else if (confirmation == 3)
                    {
                        Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack, 1, PageX, 0);
                    }
                }
            }
            // X 850 = 22 cm เเนะนำ 800 /
            // A4 = 21 cm  {Width = 356.70163 Height = 136.230438} {Width = 356.70163 Height = 102.954086} // 
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode.Replace("{TeacherNo}", TeacherNo));
              
            //------------------------

            // ส่วนหัว

            Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "ใบสมัครสมาชิกสหกรณ์ครู", THsarabun30, BrushBlack);
            Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), $"{DT.Rows[0][0].ToString()}", THsarabun30, BrushBlack);
            // วันที่
            string MemberID = "สมาชิกเลขที่ " + dt.Rows[0][0].ToString();
            string School = $"{DT.Rows[0][0].ToString()}";

            //ข้อมูลส่วนตัว
            string Name = "ข้าพเจ้า " + dt.Rows[0][1].ToString();
            string IdCardNum = "เลขประจำตัวประชาชน " + dt.Rows[0][4].ToString();
            string HouseNum = "อยู่บ้านเลขที่ " + dt.Rows[0][5].ToString();
            string Village = "หมู่ " + dt.Rows[0][6].ToString();
            string SubDistrict = "ตำบล " + dt.Rows[0][7].ToString();
            string District = "อำเภอ " + dt.Rows[0][8].ToString();
            string Province = "จังหวัด " + dt.Rows[0][9].ToString();
            string TelNo = "เบอร์โทร " + dt.Rows[0][10].ToString();

            //รายละเอียด
            string amountbauy = "ข้อ 2 ข้าพเจ้าขอถือหุ้นของกิจกรรมสหกรณ์ครู ซึ่งมีค่าหุ้นล่ะ 500 บาท";
            string buy = $"2.1 ข้อซื้อหุ้นจำนวน " + dt.Rows[0][11].ToString() + " บาท";
            string share = "2.2 รับโอนหุ้นจาก -";
            string Who = "สมาชิกเลขที่ ";
            string quantity = "จำนวน 1 หุ้น (ถ้ามี)";
            string price = "เเละชำระค่าหุ้น " + dt.Rows[0][11].ToString() + " บาท ทันทีที่ได้รับเเจ้งให้เข้าเป็นสมาชิก";

            //CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{MemberID}\r\n" +
            //                                      $"{School}\r\n" +
            //                                      $"วันที่ {Day} เดือน {Month} พ.ศ. {Year}\r\n",
            //          THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 10, 400f, 200, false);
            SizeText = e.Graphics.MeasureString($"{MemberID}", Font(18, ThaiSarabun, FontStyle.Regular));
            e.Graphics.DrawString($"{MemberID}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, PageX - SizeText.Width, Y + (SpacePerRow * CurrentRows++));
            SizeText = e.Graphics.MeasureString($"{School}", Font(18, ThaiSarabun, FontStyle.Regular));
            e.Graphics.DrawString($"{School}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, PageX - SizeText.Width, Y + (SpacePerRow * CurrentRows++));
            SizeText = e.Graphics.MeasureString($"วันที่ {Day} เดือน {Month} พ.ศ. {Year}", Font(18, ThaiSarabun, FontStyle.Regular));
            e.Graphics.DrawString($"วันที่ {Day} เดือน {Month} พ.ศ. {Year}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, PageX - SizeText.Width, Y + (SpacePerRow * CurrentRows++));


            Class.Print.SetPrintMedtods.CenterLeft(e, $"ถึงคณะกรรมการดำเนินการกิจกรรมสหกรณ์ครู{DT.Rows[0][0].ToString()}", THsarabun18, BrushBlack, X + XD, Y + (SpacePerRow * CurrentRows++), XP, XD);


            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{Name} {IdCardNum}\r\n" +
                                        $"{HouseNum} {Village}\r\n" +
                                        $"{SubDistrict} {District}\r\n" +
                                        $"{Province} {TelNo}\r\n", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);


            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"ได้ทราบข้อบังคับของกิจกรรมสหกรณ์ครู{DT.Rows[0][0].ToString()} ขอสมัครเป็นสมาชิกของสหกรณ์ครู  เเละขอให้คำเป็นหลักฐานดังต่อไปนี้", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows), 750, 200, false);

            string status = "ข้อที่ 1 ข้าพเจ้าเป็นผู้มีคุณสมบัติถูกต้องตามข้อบังคับทุกประการ";
            string teacher = "1.เป็นครู - อาจารย์";
            string officer = "2.เป็นเจ้าหน้าที่ - ภารโรง";

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{status}\r\n" +
                                        $"  {teacher}\r\n" +
                                        $"  {officer}\r\n", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{amountbauy}\r\n" +
                                        $"  {buy}\r\n" +
                                        $"  {share} {Who}\r\n" +
                                        $"{quantity} {price}\r\n", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ข้อที่ 3 เมื่อ ข้าพเจ้าเป็นสมาชิกจะปฎิบัติตามข้อบังคับทุกประการ เเละจะพยายามส่งเสริมให้กิจกรรมสหกณ์ครูให้เจริญก้าวหน้ายิี่งขึ้นไป", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750, 700);
            // ตกลง
            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ลงชื่อ......................................................." +
                                        "       (..............................................................)", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 100, 400, 700);

            Class.Print.SetPrintMedtods.CenterRight(e, "ผู้สมัคร", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 100, XP, XD + 230);

            if (Print_two >= 1 || confirmation == 0)
            {
                Print_two++;
                if (Print_two >= 2 || confirmation == 0)
                {
                    Print_two = 0;
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                }
            }
            else
            {
                Print_two++;
                e.HasMorePages = true;
            }

            //int c = 0;
            //if (confirmation_scrip != 0)
            //{
            //    if (confirmation_scrip == 1)
            //        c = Print_two;
            //    else if (confirmation_scrip == 4)
            //        c = 2;
            //    if (confirmation_scrip != 2)
            //        Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack,c, PageX, 0);
            //}
            //if (Print_two == 1 || confirmation_scrip == 3 || confirmation_scrip == 4 || confirmation_scrip == 2 || confirmation_scrip == 0)
            //{
            //    Print_two = 0;
            //    e.HasMorePages = false;
            //}
            //else
            //{
            //    Print_two++;
            //    e.HasMorePages = true;
            //}

        }
        // เเบบ ปริ้น หน้า กู้
        private static int dub = 1;
        public static void PrintLoan(System.Drawing.Printing.PrintPageEventArgs e, String SQLCode, String Day, String Month, String Year, String TeacherNo, String LoanNo,int Rowscount,bool confirmation_scrip,bool confirmation_copy)
        {
            if (TeacherNo != "")
            {
                int PageX = (e.PageBounds.Width);
                int PageY = (e.PageBounds.Height);
                int XP = 0;
                int XD = 0;
                int X = 50;
                int Y = 50;
                int SpacePerRow = 35;
                int CurrentRows = 0;
                e.HasMorePages = true;
                int confirmation = 0;
                if (confirmation_scrip == true && confirmation_copy == true)
                {
                    confirmation = 1;
                }
                else if (confirmation_scrip == false && confirmation_copy == false)
                {
                    Print_two++;
                    confirmation = 0;
                }
                else
                {
                    Print_two++;
                    if (confirmation_scrip == true) { confirmation = 2; }
                    else { confirmation = 3; }
                }
                if (confirmation != 0 && pageNow == 0)
                {
                    if (confirmation == 1)
                    {
                        Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack,Print_two, 200, 0);
                    }
                    else
                    {
                        if (confirmation == 2)
                        {
                            Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack,0, 200, 0);
                        }
                        else if (confirmation == 3)
                        {
                            Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack,1, 200, 0);
                        }
                    }
                }
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode);
                //string IDBorrower = dt.Rows[0][0].ToString();
                string School = $"{DT.Rows[0][0].ToString()}";
                String Lender = $"{DT.Rows[0][4].ToString()}";
                String Borrower = dt.Rows[0][1].ToString();
                String AmountLoan = dt.Rows[0][2].ToString();
                string Borroweraddress = dt.Rows[0][3].ToString();
                String PayMin = $"{Bank.Menu.startAmountMin}";
                String LimitMonthPay = "";
                if (dt.Rows[0][6].ToString() != "0")
                {
                    int PayNo = Convert.ToInt32(dt.Rows[0][6].ToString());
                    int Yearpay = Convert.ToInt32(dt.Rows[0][5].ToString());
                    PayNo = PayNo + Convert.ToInt32(dt.Rows[0][4].ToString());
                    while (PayNo > 12)
                    {
                        Yearpay++;
                        PayNo = PayNo - 12;
                    }
                    if (PayNo == 12)
                        PayNo--;
                    LimitMonthPay = BankTeacher.Bank.Menu.Month[PayNo].ToString() + " พ.ศ. " + Convert.ToInt32(dt.Rows[0][5].ToString());
                    //LimitMonthPay = example.Bank.Menu.Monthname.ToString() + " พ.ศ. " + (Yearpay + 543).ToString();
                }
                //----------------------

                string Text_number;
                if (dub <= 2) { Text_number = dub.ToString(); dub++; } else { Text_number = "1"; dub = 2; }
                e.Graphics.DrawString($"หน้า {Text_number}/2", Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack,735,20);
                if (pageNow == 0)
                {
                    //------------------------
                    Class.Print.SetPrintMedtods.CenterRight(e, "สมาชิกเลขที่ " + TeacherNo, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "สัญญากู้ยืมเงินสวัสดิการพนักงาน", THsarabun30, BrushBlack);
                    Class.Print.SetPrintMedtods.CenterRight(e, "เขียนที่ " + School, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 10, XP, XD);
                    Class.Print.SetPrintMedtods.CenterRight(e, $"วันที่ {dt.Rows[0][8].ToString()} ", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "       สัญญาฉบับนี้ทำขึ้นมาระหว่างผู้เเทน" + School +
                    $" ( {Lender} ) {DT.Rows[0][6].ToString()} ซึ่งต่อไปในสัญญานี้เรียกว่า '" + School + "' ฝ่ายหนึ่งกับ " + Borrower, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    Class.Print.SetPrintMedtods.CenterLeft(e, Borroweraddress, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    //  เงื่อนไข 1
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ซึ่งต่อไปในสัญญานี้เรียกว่า 'พนักงานครู / อาจารย์'อีกฝ่าย \r\n" +
                        "   ทั้งสองฝ่ายตกลงทำสัญญาดังมีข้อความดังต่อไปนี้ \r\n" +
                        "   ข้อ 1. พนักงานได้กู้ยืมเงินจากสหกรณ์ครู" + School + " ไปเป็นจำนวน \r\n" +
                        "" + AmountLoan + " บาท ( " + NumToBath(AmountLoan) + " ) เเละพนักงานครูได้รับเงินกู้ \r\n" +
                        "จำนวนดังกล่าวจาก" + School + "ไปถูกต้องครบถ้วนเเล้วในขณะทำสัญญากู้ยืมเงินฉบังนี้", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    // เงื่อนไข 2
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "   ข้อ 2. หนักงานครู จะชำระหนี้เงินกู้ยืมตาม ข้อ 1. ของสัญญานี้คืนให้เเก่วิทยาลัยสหกรณ์ครู \r\n" +
                        $"{School} โดยผ่อนชำระเป็นรายเดือนในจำนวนไม่ต่ำกว่าเดือนละ {PayMin} บาท ({NumToBath(PayMin)}) ในวันที่พนักงานครูรับเงินค่าจ้างจาก  {School}" +
                        "เเละส่งให้สหกรณ์ก่อนวันที่ 3 ของเดือน ติดต่อกันจนกว่าจะชำระหนี้เงินกู้ยืมครบถ้วน เเละจะต้องชำระคืน" +
                        "ให้เสร็จสิ้นภายในเดือน " + LimitMonthPay, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    // เงื่อนไข 3
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "   ข้อ 3. พนักงานยืนยอมให้" + School + "หักเงินค่าตอบเเทนที่หนักงานครูมีสิทธิได้รับ" +
                        "อันได้เเก่ ค่าจ้าง ค่าล่วงเวลา ค่าทำงานในวันหยุด เเละค่าจ้างล่วงเวลาในวันหยุด เพื่อเป็นการใช้คืนเงินกู้ตาม" +
                        "สัญญานี้ หากจำนวนเงินที่" + School + "หักไว้จากค่าตอบเเทนดังกล่าวในวรรคเเรก มีจำนวน" +
                        "เกินกว่าหนึ่งในห้าของเงินค่าตอบเเทนที่พนักงานครูมีสิทธิได้รับไม่ว่าจะเป็นการหักเพื่อเหตุใดเหตุหนึ่ง หรือ" +
                        "หลายเหตุรวมกันก็ตาม พนักงานครูยืนยอมให้" + School + " สามารถหักเงินได้ตามจำนวน" +
                        "ดังกล่าวนจนครบถ้วน หากผู้กู้ยืมไม่ชำระเงินตามกำหนด จะต้องชำระอัตราดอกเบี้ยเพิ่มอีก 1 เท่า เเละผู้ค้ำ\r\n" +
                        "ประกันจะต้องรับผิดชอบชำระเเทนผู้กู้ยืมโดยไม่มีข้อทักท้วงใดๆ", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 300, false);
                    // เงื่อนไข 4
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "   ข้อที่ 4. หากพนักงานครูพ้นสภาพจากการเป็นลูกจ้าง" + School + "ไม่ว่าจะด้วยสาเหตุ" +
                        "ใดๆ ก็ตามพนักงานครูจะต้องชำระหนี้เงินกู้ในส่วนที่ยังค้างชำระอยู่ทั้งหมดคืนให้กับ" + School + "ในทันที", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 20, 750f, 300, false);
                }
                else
                {
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "สัญญานี้ทำขึ้น 2 ฉบับ มีข้อความตรงกัน คู่สัญญาทั้งสองฝ่ายได้อ่าน เเละเข้าใจข้อความในสัญญา\r\n" +
                        "ฉบับนี้โดยตลอดเเล้ว เห็นว่าถูกต้องเเละตรงตามความประสงค์เเล้ว จึงได้ลงลายมือชื่อไว้เป็นหลักฐานต่อหน้า" +
                        "พยาน เเละเก็บสัญญาไว้ฝ่ายละฉบับ", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 300, false);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ลงชื่อ ................................................ นายจ้าง\r\n" +
                                          "     (" + Lender + ")", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 350f, 70, false);
                    string NameLoan = $"ลงชื่อ ................................................ ผู้กู้ยืม\r\n" +
                                      "    (" + Borrower + ")";
                    // เเก้ใหม่ตามจำนวนคนที่ค้ำ
                    for (int around = 0; around < Rowscount; around++)
                    {
                        if(around == 0)
                        {
                            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, NameLoan, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 350f, 70, false);
                        }
                        else
                        {
                            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"ลงชื่อ ................................................ ผู้ค้ำประกัน {around} ({dt.Rows[around][1].ToString()})\r\n" +
                                            "      ( " + dt.Rows[around][1].ToString() + " )", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 350f, 70, false);
                        }
                    }
                    Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "ลงชื่อรับเงิน ................................................................................................ วันที่ " + Day + " " + Month + " " + Year, THsarabun18, BrushBlack);


                }
                if(pageNow == 1)
                {
                    Print_two++;
                    if (Print_two >= 2)
                    {
                        Print_two = 0;
                        pageNow = 0;
                        e.HasMorePages = false;
                    }
                    else
                    {
                        dub = 1;
                        pageNow = 0;
                        e.HasMorePages = true;
                    }
                }
                else
                {
                    pageNow++;
                    e.HasMorePages = true;
                }
            }


        }
        // =============================== ข้อมูลสำหรับMetodนี้ ====================================
        static int pagepaper = 1;
        // รวมเป็นเงิน
        static int Amountotel_SUM = 0;
        static int Amountotel_Pay = 0;
        static int Amountotel_Loan = 0;
        // สรุป ยอดเงินที่ได้มาเเยกกัน 
        static List<int> Pay = new List<int>(); 
        static List<int> Loan = new List<int>();
        // SUM จำนวนเลขท้ายสุด เพื่อเเสดงผลยอดรวม
        static List<int> SUM = new List<int>();
        /// <summary>
        /// <para> (System.Drawing.Printing.PrintPageEventArgs e) INDAX : e</para>
        /// <para> (DataGridView G) INDAX : DataGridView</para>
        /// <para> (string header) INDAX : Name_header</para>
        /// <para> (string TextForm) INDAX : Name_From</para>
        /// <para> (int confirmation) INDAX : Scrip_true = ues // copy_false = not ues</para>
        /// <para> (string Sizepaper) INDAX : A4 / A5</para>
        /// <para> (int summarize) INDAX : 0 ues / > 0 not</para>
        /// </summary>
        public static void PrintReportGrid(System.Drawing.Printing.PrintPageEventArgs e, DataGridView G, string header, string TextForm,bool confirmation_scrip,bool confirmation_copy, string Sizepaper,int summarize)
        {
            page_length = e.PageBounds.Height-50;
            PenBlack.Width = 1;
            PenRed.Width = 1;
            PenGreen.Width = 1;
            //================= Pen =====================
            // สำหรับภาw Draw
            float imageX = 80, imageY = 80;
            //================= Image =====================
            // เพิ่มระยะสักหน่อยเพื่อความสวยงาม
            float lower = 20;
            //================= Lower =====================
            // จุดกำหนดการวาดของเเกนทั้งหมด
            float X = 50, Y = 50;
            X = 50; Y = 50 + imageY + lower;
            // ================ ถูกลบไปเเล้วเเย่จัง
            //if (onetimestartColumns == 0)
            //{
            //    X = 50;  Y = 50 + imageY + lower;
            //}
            //else
            //{
            //    X = 50; Y = 50;
            //}
            //================= Position =====================
            // เส้น //
            float Line1_x = X, Line2_x = e.PageBounds.Width - 50; // เส้นสิ้นสุดในเเกน x
            float Line1_y = Y, y2 = Y; // สิ้นสุดการปริ้น
            //================= Line =====================
            // สำหรับ Columns And Rows สำหรับเริ่มปริ้น ตาราง
            float startTableY = Y, startTableX = X;
            float Getwidth = 0;
            //================= PositionC&R =====================
            // สำหรับข้อความ
            float TextX = X, TextY = Y - imageY - lower;
            //================= Position T =====================
            // สำหรับ Rectanglef กำหนด สี่เหลี่ยมผืนผ้า
            float Rectangle_X, Rectangle_Y;
            //================= Rectangle =====================
            // เส้นปิด
            List<float> ColseLine = new List<float>();
            //================= CloseLise =====================
            // ขนาดการตัด // seting nomal 23
            int setcut = 25;
            //================= Cut =====================
            // ตัววัดขนาด ข้อความที่ได้รับมา สำหรับเรียกใช้เเล้วทิ้งเพราะงั้นเรียกใช้ได้เบย
            SizeF Size = e.Graphics.MeasureString("", THsarabun16);
            SizeF Size1 = e.Graphics.MeasureString("", THsarabun16);
            //================= Sizetext =====================
            // ส่งค่าที่ได้กลับมาไปคำนวณหาตำเเหน่งที่ถูกต้อง
            float result = 0;
            // ============================= Retun ============================
            // ตัวกำหนดRows ที่วาดได้
            int SizeRows = 10;
            if(Sizepaper == "A4")
            {
                SizeRows = 18;
            }
            else
            {
                SizeRows = 4;
            }
            // ตัวกำหนดการวาด ต้นฉบับ กับ สำเนา 
            //  0 = ไม่ใช่ // 1 = ใช่ทั้งหมด // 2 = เเค่ต้นฉบับ // 3 = เเค่สำเนา
            int confirmation = 0;
            if (confirmation_scrip == true && confirmation_copy == true)
            {
                confirmation = 1;
            }
            else if(confirmation_scrip == false && confirmation_copy == false)
            {
                confirmation = 0;
            }
            else
            {
                Print_two++;
                if (confirmation_scrip == true) { confirmation = 2; }
                else { confirmation = 3; }
            }
            // ========================== กล่อง =========================
            float Box_SizeX = 200;
            float Box_SizeY = 30;
            float location_Box = 60;
            if (G.ColumnCount != 0)
            {
                // ================================================================== รูปภาพ เเละ ชื่อวิทยาลัย =================================================
                System.Drawing.Image img = global::BankTeacher.Properties.Resources._64x64_TLC;
                // วาดภาw (โลโก้)
                e.Graphics.DrawImage(img, 50, 50, imageX, imageY);
                // ข้อความทั้งหมดที่ใช้พิมพ์
                string TLC = $"{DT.Rows[0][0].ToString()}", tlc = $"{DT.Rows[0][1].ToString()}";
                string address = $"{DT.Rows[0][5].ToString()}";
                // ================================================================ ไม่ได้ใช้งาน ===================================================================
                //Size = e.Graphics.MeasureString($"วันที่ออกใบ {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}", FonT(16, ThaiSarabun, FontStyle.Bold));
                //Size = e.Graphics.MeasureString($"วันที่ออกใบ {Bank.Pay.pay.info_datepay}", FonT(16, ThaiSarabun, FontStyle.Bold));
                //e.Graphics.DrawString($"วันที่ออกใบ {Bank.Pay.pay.info_datepay}", FonT(16, ThaiSarabun, FontStyle.Bold), BrushBlack, Line2_x - Size.Width, 50);
                //e.Graphics.DrawString($"วันที่ออกใบ {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}", FonT(16, ThaiSarabun, FontStyle.Bold), BrushBlack, Line2_x - Size.Width, 50);
                // ================================================================ เวลาที่เอกสารถูกปริ้น ===================================================================
               
                // เลขหน้า
                int all_paper, number;
                if (details == 1) 
                { 
                    number = 18; 
                } else 
                { 
                    number = 4; 
                }
                double b = (double)G.RowCount / number;
                if (G.RowCount / number == 0)
                { 
                    all_paper = 1; 
                }
                else if (b.ToString().Length > 2) 
                { 
                    all_paper = (G.RowCount / number) + 1;
                }
                else 
                { 
                    all_paper = (int)b; 
                }
                if (Bank.Loan.InfoLoan.how_many_laps >= 3)
                {
                    all_paper += 1;
                    details = 0;
                }
                else { details = 0; }
                Size = e.Graphics.MeasureString($"หน้า {pagepaper}/{all_paper.ToString()} ", Font(16, ThaiSarabun, FontStyle.Bold));
                e.Graphics.DrawString($"หน้า {pagepaper}/{all_paper.ToString()} ", Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack, Line2_x - Size.Width, 30);
                
                // เเบบพิมพ์ชื่อ วิทยาลัยเทคโนโลยีเเหลมฉบัง Thai
                TextX += imageX;
                e.Graphics.DrawString(TLC, Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack, new RectangleF(TextX+10, TextY, 700, 100));
                Size = e.Graphics.MeasureString(TLC, Font(16, ThaiSarabun, FontStyle.Bold));
                TextY += Size.Height/2;
                // เเบพิมพ์ชื่อ วิทยาลัยเทคโนโลยีเเหลมฉบัง English
                e.Graphics.DrawString(tlc, Font(14, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(TextX+10, TextY, 700, 100));
                Size = e.Graphics.MeasureString(tlc, Font(14, ThaiSarabun, FontStyle.Regular));
                TextY += Size.Height/2;
                // สถานที่ตั้ง
                TextY += calculate_distance(e, address, Font(12, ThaiSarabun, FontStyle.Regular), BrushBlack, TextX + 10, TextY + 10,300,200, Line2_x / 2, 100);
                Size = e.Graphics.MeasureString(address, Font(12, ThaiSarabun, FontStyle.Regular));
                TextY += Size.Height;
                // =============================================================== หัวข้อรายการ ================================================
                if (header != "")
                {
                    Size = e.Graphics.MeasureString(header, Font(18, ThaiSarabun, FontStyle.Bold));
                    e.Graphics.DrawString(header, Font(18, ThaiSarabun, FontStyle.Bold), BrushBlack, new RectangleF(((Line2_x + 50) / 2) - Size.Width / 2, TextY += 50, 500, 100));
                    TextY += Size.Height;
                }
                // ================================================= ปริ้นข้อความต้นฉบับ ==================================
                if(confirmation != 0)
                {
                    if(confirmation == 1)
                    {
                        Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack, Print_two, Line2_x, 260);
                    }
                    else 
                    {
                        if(confirmation == 2)
                        {
                            Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack,0, Line2_x, 260);
                        }
                        else if(confirmation == 3)
                        {
                            Class.Print.SetPrintMedtods.confirmation(e, PenBlack, BrushBlack,1, Line2_x, 260);
                        }
                    }
                }
                // =========================================================== ตารางบอก หลายการข้อมูล (กล่อง) ============================================
                if (TextForm == "InfoLoan")
                {
                    Class.Print.SetPrintMedtods.Box(e, "เริ่ม", "สิ้นสุด", Bank.Loan.InfoLoan.info_startdate, Bank.Loan.InfoLoan.info_duedate, Box_SizeX, Box_SizeY, location_Box, Line2_x);
                }
                else if (TextForm == "pay" && details != 1)
                {
                    Class.Print.SetPrintMedtods.Box(e, "เลขบิลล์", "จ่ายวันที่",info_Billpay,info_datepayShare, Box_SizeX, Box_SizeY, location_Box, Line2_x);
                }
                else if(TextForm == "AmountOff" && details != 1)
                {
                    Class.Print.SetPrintMedtods.Box(e, "เลขบิลล์", "จ่ายวันที่",info_BillAmounoff,info_datepayAmounoff, Box_SizeX, Box_SizeY, location_Box, Line2_x);       
                }
                else if(TextForm == "PayLoan")
                {
                    Class.Print.SetPrintMedtods.Box(e, "เลขบิลล์", "จ่ายวันที่",info_PayLoanBill,info_PayLoandate, Box_SizeX, Box_SizeY, location_Box, Line2_x);
                }
                // ======================== เปิดการวาด Columns รอบเดียว ======================
                if (onetimestartColumns == 0)
                {
                    //========================================================= Check Form for Print ตวรจสอบข้อความที่จะปริ้นในหน้านั้นๆ
                    if (TextForm == "InfoLoan" && details != 1)
                    {
                        string infomember = $"ชื่อ-นามสกุล : {Bank.Loan.InfoLoan.info_name}            รหัสประจำตัว : {Bank.Loan.InfoLoan.info_id}            เลขที่สัญญากู้ : {Bank.Loan.InfoLoan.info_Loanid}\r\n" +
                                           $"ยอดเงินค้ำ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Convert.ToDouble(Bank.Loan.InfoLoan.Amount[0])))} บาท                      เปอร์เซ็นต์ค้ำ : {Bank.Loan.InfoLoan.Percent[0]}%\r\n" +
                                           $"ยอดที่กู้ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Convert.ToDouble(Bank.Loan.InfoLoan.info_Sum)))} บาท                   ยอดคงเหลือ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Convert.ToDouble(Bank.Loan.InfoLoan.info_totelLoan)))} บาท                   ยอดที่ชำระ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Convert.ToDouble(Bank.Loan.InfoLoan.info_Loanpayall)))}";
                        // กรอบ อร่อยต้อง Rectangle พูดอีกอย่างคือ ขนาดข้อความ
                        Size = e.Graphics.MeasureString(infomember, Font(18, ThaiSarabun, FontStyle.Regular));
                        // กรอบ
                        e.Graphics.DrawRectangle(PenBlack, 50, TextY, Line2_x - 50, Size.Height - 20 + 5);

                        Size = e.Graphics.MeasureString("infomeber", Font(16, ThaiSarabun, FontStyle.Regular));
                        result = calculate_distance(e, infomember, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, Line2_x, Size.Height + 5);
                        TextY += (Size.Height * result);

                        // เว้นระยะตัวหนังสือกัวกรอบ
                        TextY += 10;

                        if (Bank.Loan.InfoLoan.how_many_laps >= 1)
                        {
                            e.Graphics.DrawString($"ผู้ค้ำ {Bank.Loan.InfoLoan.how_many_laps} คน", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY);
                            Size = e.Graphics.MeasureString("ผู้ค้ำ", Font(18, ThaiSarabun, FontStyle.Bold));
                            TextY += Size.Height;
                        }

                        float start = TextY, end = 0;

                        for (int Grt = 0; Grt < Bank.Loan.InfoLoan.how_many_laps; Grt++)
                        {
                            string infoGuarantor = $"ชื่อ-นามสกุล : {Bank.Loan.InfoLoan.info_GuarantrN[Grt]}            ยอดค้ำ : {Bank.Loan.InfoLoan.info_GuarantRemains[Grt]}\r\n" +
                                                   $"ยอดเงินค้ำ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Bank.Loan.InfoLoan.info_GuarantrAmount[Grt]))} บาท                      เปอร์เซ็นต์ค้ำ : {Bank.Loan.InfoLoan.info_GuarantrPercent[Grt]}%";
                            Size = e.Graphics.MeasureString(infoGuarantor, Font(16, ThaiSarabun, FontStyle.Regular));
                            Size1 = e.Graphics.MeasureString("infoGuarantor", Font(16, ThaiSarabun, FontStyle.Regular));
                            result = calculate_distance(e, infoGuarantor, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY + 5, 700, 300, Line2_x, Size1.Height);
                            if (Grt > 0)
                            {
                                e.Graphics.DrawLine(PenBlack, 50, TextY, Line2_x, TextY);
                            }
                            TextY += (Size1.Height * result);
                            end += Size.Height;
                            if (Grt == Bank.Loan.InfoLoan.how_many_laps - 1)
                            {
                                // เว้นระยะตัวหนังสือกัวกรอบ
                                TextY += 10;
                            }
                            page_length -= 50;
                        }
                        if (Bank.Loan.InfoLoan.how_many_laps > 0)
                        {
                            // กรอบ
                            e.Graphics.DrawRectangle(PenBlack, 50, start, Line2_x - 50, end + 5);
                        }
                    }
                    else if (TextForm == "pay" && details != 1)
                    {
                        string Remain;
                        if (info_Lona_AmountRemain != "")
                        {
                            Remain = $"ยอดกู้คงเหลือ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Lona_AmountRemain))}";
                        }
                        else
                        {
                            Remain = "";
                        }
                        string infopay = $"ชื่อ-นามสกุล : {info_name}            รหัสประจำตัว : {info_id}           \r\n" +
                                         $"หุ้นสะสมทั้งหมดก่อนเเละหลัง : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Savingtotel))}            {Remain}        ";
                        // กรอบ อร่อยต้อง Rectangle
                        Size = e.Graphics.MeasureString(infopay, Font(18, ThaiSarabun, FontStyle.Regular));
                        //// กรอบ
                        //e.Graphics.DrawRectangle(PenBlack, 50, TextY, x2 - 50, Size.Height - 20 + 5);

                        Size = e.Graphics.MeasureString("infopay", Font(16, ThaiSarabun, FontStyle.Regular));
                        result = calculate_distance(e, infopay, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, Line2_x, Size.Height + 5);
                        TextY += (Size.Height * result);
                    }
                    else if (TextForm == "Home")
                    {
                        string Remain;
                        if (info_Lona_AmountRemain != "0")
                        {
                            Remain = $"ยอดกู้คงเหลือ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Lona_AmountRemain))}";
                        }
                        else
                        {
                            Remain = "";
                        }
                        string infoHome = $"ชื่อ-นามสกุล : {info_name}            รหัสประจำตัว : {info_id}           \r\n" +
                                         $"หุ้นสะสมทั้งหมด : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Savingtotel))}            {Remain}        ";

                        Size = e.Graphics.MeasureString(infoHome, Font(18, ThaiSarabun, FontStyle.Regular));
                        //// กรอบ
                        //e.Graphics.DrawRectangle(PenBlack, 50, TextY, x2 - 50, Size.Height - 20 + 5);

                        Size = e.Graphics.MeasureString("infopay", Font(16, ThaiSarabun, FontStyle.Regular));
                        result = calculate_distance(e, infoHome, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, Line2_x, Size.Height + 5);
                        TextY += (Size.Height * result);
                    }
                    else if (TextForm == "AmountOff" && details != 1)
                    {
                        string infoAmountoff = $"ชื่อ-นามสกุล : {info_name}            รหัสประจำตัว : {info_id}            เลขที่หุ้นสะสม : {info_ShareNo}\r\n" +
                                           $"ยอดเงินสะสมทั้งหมด : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Savingtotel))} บาท          ยอดเงินที่ถอนออกได้ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_canbeAmounoff))} บาท           สถานะ : {info_Loanstatus}\r\n" +
                                           $"ยอดที่ถอนออก : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Amounoff))} บาท                      ยอดเงินค้ำในระบบ : {Class.Print.SetPrintMedtods.comma(Convert.ToInt32(info_Amounoffinsystem))}";
                        Size = e.Graphics.MeasureString(infoAmountoff, Font(18, ThaiSarabun, FontStyle.Regular));
                        //// กรอบ
                        //e.Graphics.DrawRectangle(PenBlack, 50, TextY, x2 - 50, Size.Height - 20 + 5);

                        Size = e.Graphics.MeasureString("infoAmountoff", Font(16, ThaiSarabun, FontStyle.Regular));
                        result = calculate_distance(e, infoAmountoff, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, Line2_x, Size.Height + 5);
                        TextY += (Size.Height * result);
                    }
                    else if(TextForm == "PayLoan")
                    {
                        string info_Loanpay = $"ชื่อ-นามสกุล : {info_name}            รหัสประจำตัว : {info_id}";

                        Size = e.Graphics.MeasureString(info_Loanpay, Font(18, ThaiSarabun, FontStyle.Regular));
                        Size = e.Graphics.MeasureString("info_Loanpay", Font(16, ThaiSarabun, FontStyle.Regular));
                        result = calculate_distance(e, info_Loanpay, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, Line2_x, Size.Height + 5);
                        TextY += (Size.Height * result);
                    }
                    else if(TextForm == "ReportDividend")
                    {
                        string info_Dividend = $"ดอกเบี้ยคงเหลือจากปีที่ผ่านมา : {info_RemainInterest}     |    เงินหุ้นสะสม : {info_SavingAmount}   |     ปันผลเฉลี่ยต่อหุ้น : {info_DividendPerShare} \r\n" +
                                               $"จำนวนดอกเบี้ย : {info_InterestAmount}    |    ดอกเบี้ยไว้จ่ายปีหน้า : {info_InterestNextYear}     |     จำนวนเงินปันผล : {info_DividendAmount}";
                        Size = e.Graphics.MeasureString("info_Dividend", Font(16, ThaiSarabun, FontStyle.Regular));
                        result = calculate_distance(e, info_Dividend, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, Line2_x, Size.Height + 5);
                        TextY += (Size.Height * result);
                    }
                }
                // บวกขนาดตารางเเละเส้น เพื่อทราบตำเเหน่ง ได้เเก่   startTableY ตาราง / y1&y2 เส้น
                float IandT = 0;
                if (imageY > TextY)
                    IandT = imageY / 4;
                else
                    IandT = TextY;
                startTableY = IandT;
                Line1_y = IandT;
                y2 = IandT;
                // ====================== ลูป ตาราง =============================
                int CheckList = 0;
                for (int Columns = 0; Columns < G.ColumnCount; Columns++)
                {
                    if (Columns != 0)
                    {
                        if(G.Columns[Columns].HeaderText == "รายการ" || CheckList == 1)
                        {
                            if(CheckList == 1)
                            {
                                if(TextForm == "pay")
                                {
                                    Getwidth += 100 + Class.Print.SetPrintMedtods.Chcekspan(e, G, startTableX, startTableY, G.Columns[Columns - 1].HeaderText
                                    , setcut, Columns, Font(18, ThaiSarabun, FontStyle.Regular), 0, 0);
                                }
                                else
                                {
                                    Getwidth +=Class.Print.SetPrintMedtods.Chcekspan(e, G, startTableX, startTableY, G.Columns[Columns - 1].HeaderText
                                  , setcut, Columns, Font(18, ThaiSarabun, FontStyle.Regular), 0, 0);
                                }
                                if (onetimestartColumns == 0)
                                    cutline.Add(Getwidth);
                            }
                            else
                            {
                                Getwidth += Class.Print.SetPrintMedtods.Chcekspan(e, G, startTableX, startTableY, G.Columns[Columns - 1].HeaderText
                             , setcut, Columns, Font(18, ThaiSarabun, FontStyle.Regular), 0, 0);
                                if (onetimestartColumns == 0)
                                    cutline.Add(Getwidth);
                            }
                            CheckList++;
                        }
                        else
                        {
                            Getwidth += Class.Print.SetPrintMedtods.Chcekspan(e, G, startTableX, startTableY, G.Columns[Columns - 1].HeaderText
                              , setcut, Columns, Font(18, ThaiSarabun, FontStyle.Regular), 0, 0);
                            if (onetimestartColumns == 0)
                                cutline.Add(Getwidth);
                        }
                        // โรงงานบอกระยะของการวาด เก็บค่า Wihte ไว้
                      
                    }
                    else
                    {
                        Getwidth = 50;
                    }
                    // โรงงานบอกขนาด ของ สี่เหลี่ยม ได้เเก่ พื้นที่/ความกว้าง (ค่าที่เอาออกมาใช้คือ Rectangle)
                    Class.Print.SetPrintMedtods.CutingCharAndString
                    (e, G.Columns[Columns].HeaderText, setcut, 50, startTableY, out Rectangle_X, out Rectangle_Y, 0);
                    // เก็บขนาดของข้อความในเเถว
                    Size = e.Graphics.MeasureString(G.Columns[Columns].HeaderText, Font(18, ThaiSarabun, FontStyle.Regular));
                    // ระยะการวาดในรอบถัดไป สูตร(ขนาดของกรอบ / 2 + ความยาว - กรอบ / 2)
                    if (Columns != 0)
                        center = (Rectangle_X / 2 + Getwidth - Rectangle_X / 2);
                    else
                    {
                        Size = e.Graphics.MeasureString(G.Columns[Columns].HeaderText, Font(18, ThaiSarabun, FontStyle.Regular));
                        center = ((50 + Size.Width / 2)-10);
                    }

                    float CT = center;
                    if (Columns == G.ColumnCount - 1)
                    {
                        SizeF f = e.Graphics.MeasureString(G.Rows[0].Cells[G.Rows[0].Cells.Count-1].Value.ToString(), Font(18, ThaiSarabun, FontStyle.Regular));
                        center = (Line2_x - f.Width);
                    }
                    if(onetimestartColumns == 0)
                    Center.Add(center);

                    if (Columns == G.ColumnCount - 1)  // ถ้าเเถวสุดท้ายให้ ขยายพื้นที่ให้สุด
                    {
                        center = ((Line2_x - CT) / 2 + CT - (Size.Width / 2));
                        //e.Graphics.DrawLine(PenRed, center, 50, center, 200); ตัวTest เส้นกลาง
                    }
                    // ========================= วาดข้อความ Columns
                    if(Columns != 0)
                    {
                        e.Graphics.DrawString(G.Columns[Columns].HeaderText, Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(center, startTableY, Rectangle_X - 50, Rectangle_Y - 50));
                    }
                    else // ลำดับที่
                        e.Graphics.DrawString(G.Columns[Columns].HeaderText, Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(center-Size.Width/2+10, startTableY, Rectangle_X - 50, Rectangle_Y - 50));


                    //================================ เส้นปิด =======================================
                    ColseLine.Add(Rectangle_Y);
                    //LineEndC.Add(Y + Sizetext.Width);
                    if (Columns == G.ColumnCount - 1)
                    {
                        startTableY = Class.Print.SetPrintMedtods.MaxValues(startTableY, ColseLine);
                        e.Graphics.DrawLine(PenBlack, Line1_x, startTableY, Line2_x, startTableY);
                        ColseLine.Clear();
                    }
                }
                
                // เส้นเปิด
                e.Graphics.DrawLine(PenBlack, Line1_x, Line1_y, Line2_x, y2);
                // ปิด Columns
                onetimestartColumns++;
            }

            if (G.RowCount != 0)
            {
                for (int Rows = 0; Rows < G.RowCount; Rows++)  // เเถวทั้งหมด
                {
                    if (Rows >= Currentposition_Row)
                    {
                        if(Rows < Currentposition_Row+SizeRows)
                        if (startTableY < page_length) 
                        {
                            position++;
                            for (int Cells = 0; Cells < G.Rows[Rows].Cells.Count; Cells++) // ช่องในเเถว
                            {
                                if (Cells == G.Rows[Rows].Cells.Count - 1) // ถ้าช่องสิ้นสุดเเล้ว ให้ทำการหาค่า ตัวเเปรเลข มาเก็บรวมไว้ใน SUM
                                {
                                        if (TextForm == "pay")
                                        {
                                            bool L = false;
                                            for (int List = 0; List < G.Rows[Rows].Cells.Count; List++) // เช็ครายการที่มียอด
                                            {
                                               L = G.Rows[Rows].Cells[List].Value.ToString().Contains("รายการกู้");
                                                if (G.Rows[Rows].Cells[List].Value.ToString() == DT_ListType.Rows[0][1].ToString() || G.Rows[Rows].Cells[List].Value.ToString() == DT_ListType.Rows[2][1].ToString()) // ราย การหู้นสะสม
                                                {
                                                    for (int returcells = G.Rows[Rows].Cells.Count - 1; returcells > 0; returcells--) // ลูปจากหลังไปหน้า
                                                    {
                                                        Pay.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value));
                                                        SUM.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value)); // ยอดรวม
                                                        break;
                                                    }
                                                }
                                                else if (G.Rows[Rows].Cells[List].Value.ToString() == DT_ListType.Rows[1][1].ToString() || L == true)
                                                {
                                                    for (int returcells = G.Rows[Rows].Cells.Count - 1; returcells > 0; returcells--)
                                                    {
                                                        Loan.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value));
                                                        SUM.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value));
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else if (TextForm == "ReportIncome" || TextForm == "ReportEpenses")
                                        {
                                            bool B = false;
                                            for (int List = 0; List < G.Rows[Rows].Cells.Count; List++) // เช็ครายการที่มียอด
                                            {
                                                B = G.Rows[Rows].Cells[List].Value.ToString().Contains("สรุป");
                                                if (G.Rows[Rows].Cells[List].Value.ToString() == "สรุปยอดบิลล์" || B == true) // ราย การหู้นสะสม
                                                {
                                                    for (int returcells = G.Rows[Rows].Cells.Count - 1; returcells > 0; returcells--) // ลูปจากหลังไปหน้า
                                                    {
                                                        try
                                                        {
                                                            SUM.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value)); // ยอดรวม
                                                            break;
                                                        }
                                                        catch { }
                                                    }
                                                }

                                            }
                                        }
                                        else
                                        {
                                            var a = G.Rows[Rows].Cells[Cells].Value.ToString();
                                            int b = 0;
                                            int.TryParse(a, out b);
                                            if(b != 0)
                                            for (int returcells = G.Rows[Rows].Cells.Count - 1; returcells > 0; returcells--)
                                            {
                                                SUM.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value));
                                                break;
                                            }
                                        }
                                }
                                    var number_Or_String = G.Rows[Rows].Cells[Cells].Value.ToString();
                                    int number = 0 ;
                                    int NB;
                                    string NameG = "";
                                    int.TryParse(number_Or_String, out number);
                                    // ================== เพิ่ม ลูกน้ำตามความสวยงาม
                                    if (number != 0)
                                    {
                                        for (int returcells = G.Rows[Rows].Cells.Count - 1; returcells > 0; returcells--) // ลูปจากหลังไปหน้า
                                        {
                                            number_Or_String = G.Rows[Rows].Cells[returcells].Value.ToString();
                                            int.TryParse(number_Or_String, out NB);
                                            if (NB != 0)
                                            {
                                                List<string> Sort = new List<string>();
                                                int GetNumber_FromG = Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value);
                                                string Number = "";
                                                int up = 1;
                                                string getRemove = GetNumber_FromG.ToString();
                                                string getNum = "";
                                                if (number.ToString().Length > 3 && number == GetNumber_FromG)
                                                {
                                                    for (int loop = 0; loop < GetNumber_FromG.ToString().Length; loop++)
                                                    {
                                                        getNum = getRemove.Remove(0, 1);
                                                        getRemove = getRemove.Remove(1, getRemove.Length - 1);
                                                        Sort.Add(getRemove);
                                                        getRemove = getNum;
                                                    }
                                                    for (int loop = 0; loop < Sort.Count; loop++)
                                                    {
                                                        if (loop == 3 * up )
                                                        {
                                                            Number += ",";
                                                            loop--;
                                                            up++;
                                                        }
                                                        else
                                                            Number += Sort[Sort.Count() - (loop+1)];
                                                    }
                                                    char[] charArray = Number.ToCharArray();
                                                    Array.Reverse(charArray);
                                                    for (int loop = 0; loop < charArray.Count(); loop++)
                                                    {
                                                        NameG += charArray[loop];
                                                    }
                                                    break;
                                                }
                                                else { NameG = G.Rows[Rows].Cells[Cells].Value.ToString(); break; }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        NameG = G.Rows[Rows].Cells[Cells].Value.ToString();
                                    }
                                    // เรียกใช้ โรงงาน การตัด  เเละ การวัด ขนาดสี่เหลี่ยมพื้นผ้า
                                    Class.Print.SetPrintMedtods.CutingCharAndString
                                (e, NameG, setcut, 50, startTableY, out Rectangle_X, out Rectangle_Y, 0);
                                    
                                    if (Cells == G.Rows[Rows].Cells.Count - 1)
                                    {
                                        Size = e.Graphics.MeasureString(NameG, Font(18, ThaiSarabun, FontStyle.Regular));
                                        // วาดRows ลำดับสุดท้าย
                                        e.Graphics.DrawString(NameG, Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Line2_x - Size.Width, startTableY, Rectangle_X - 50, Rectangle_Y - 50));
                                    }
                                    else
                                    {
                                        // วาดRows
                                        e.Graphics.DrawString(NameG, Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center[Cells], startTableY, Rectangle_X - 50, Rectangle_Y - 50));
                                    }
                              
                                // =================================================== เช็คว่าขนาดข้อความใหญ่เกินกำหนดหรือไม่ เพื่อความปลอดภัย ในการทับเส้น
                                if (G.Rows[Rows].Cells[Cells].Value.ToString().Length >= setcut + 5)
                                {
                                    Rectangle_Y = Rectangle_Y + 36.40136f;
                                }
                                // ============= เก็บขนาดที่ได้รับมาไปหาค่ามากที่สุด
                                ColseLine.Add(Rectangle_Y);
                                // ============================================ หาขนาดที่มากที่สุด เช็คว่า Colum หรือ Rows ที่มีขนาดความกว้างที่สุดจึงเอาไปวาดเส้น =========================================
                                if (Cells == G.Rows[Rows].Cells.Count - 1)
                                {
                                    startTableY = Class.Print.SetPrintMedtods.MaxValues(startTableY, ColseLine);
                                    e.Graphics.DrawLine(PenBlack, Line1_x, startTableY, Line2_x, startTableY);
                                    ColseLine.Clear();
                                }
                            }
                        }
                    }
                    if (startTableY >= page_length)
                        break;
                }
            }
           
            for (int l = 0; l < cutline.Count; l++)
            {
                e.Graphics.DrawLine(PenBlack, cutline[l], Line1_y, cutline[l], startTableY);
            }
            // ========================== เส้นปิดข้าง =========================
            e.Graphics.DrawLine(PenBlack, Line1_x, Line1_y, Line1_x, startTableY);
            e.Graphics.DrawLine(PenBlack, Line2_x, Line1_y, Line2_x, startTableY);
            // ========================= เส้นปิด ============================ 
            e.Graphics.DrawLine(PenBlack, Line1_x, startTableY, Line2_x, startTableY);
            // ==================================== เช็คขนาดตำเเหน่งที่วาดปัจุบัน ==========================
            Currentposition_Row += position; // ทำการเก็บค่าตำเเหน่ง Rows ที่นับได้ไว้ใน Currenposition
            // =========================================== สรุปยอด ========================================
            if(TextForm != "Home" && TextForm != "ReportDividend")
            Class.Print.SetPrintMedtods.Tabletotal(e, PenBlack, SUM, BrushBlack, 18, X, startTableY, Line2_x);
            // ====================================== ตัวเเปร ====================================
            SizeF TextSize = e.Graphics.MeasureString("", THsarabun16);
            SizeF TextSize1 = e.Graphics.MeasureString("", THsarabun16);
            TextSize1 = e.Graphics.MeasureString($"ได้เวลาสนุกเเล้วสิ", Font(16, ThaiSarabun, FontStyle.Regular));
            startTableY += TextSize1.Height + 10;
            if(summarize == 0)
            {
                Amountotel_SUM += Convert.ToInt32(SUM.Sum());
                if (Currentposition_Row == G.Rows.Count) // ตำเเหน่งปัจุบันเกินขนาด ความยาวที่กำหนด หรือ ไม่
                {
                    e.Graphics.DrawString("รูปเเบบการจ่าย ", Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack, 50, startTableY + 10);
                    TextSize = e.Graphics.MeasureString("รูปเเบบการจ่าย ", Font(16, ThaiSarabun, FontStyle.Bold));

                    e.Graphics.DrawString($" : {info_Payment}", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50 + TextSize.Width, startTableY + 10);

                    e.Graphics.DrawString("ยอดรวมทั้งหมด ", Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack, 50, startTableY + 40);
                    TextSize = e.Graphics.MeasureString("ยอดรวมทั้งหมด ", Font(16, ThaiSarabun, FontStyle.Bold));

                    e.Graphics.DrawString($" : {Amountotel_SUM}", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50 + TextSize.Width, startTableY + 40);

                    e.Graphics.DrawString("หมายเหตุ", Font(18, ThaiSarabun, FontStyle.Bold), BrushBlack, 50, startTableY + 70);
                    TextSize = e.Graphics.MeasureString("หมายเหตุ", Font(18, ThaiSarabun, FontStyle.Regular));

                    e.Graphics.DrawString("ใบเสร็จรับเงินฉบับนี้จะสมบูรณ์เมื่อผู้รับเงินลงลายมือชื่อเป็นอันเสร็จสิ้น \r\n" +
                                        "ชำระเเล้วไม่สามารถรับคืนหรือเปลี่ยนตัวไม่ว่ากรณีใดๆ", Font(12, ThaiSarabun, FontStyle.Bold), BrushBlack, 80 + TextSize.Width, startTableY + 70);
                    if (TextForm == "pay" || TextForm == "AmountOff" || TextForm == "PayLoan" || TextForm == "InfoLoan")
                    {
                        Amountotel_Pay += Convert.ToInt32(Pay.Sum());
                        Amountotel_Loan += Convert.ToInt32(Loan.Sum());
                        if (Amountotel_Pay != 0 && Amountotel_Loan != 0)
                        {
                            //TextSize1 = e.Graphics.MeasureString($"ได้เวลาสนุกเเล้วสิ", FonT(16, ThaiSarabun, FontStyle.Regular));
                            //startTableY += TextSize1.Height+10;
                            TextSize = e.Graphics.MeasureString("บาท", Font(16, ThaiSarabun, FontStyle.Regular));
                            TextSize1 = e.Graphics.MeasureString($"{Amountotel_Pay.ToString("D")}", Font(16, ThaiSarabun, FontStyle.Regular));
                            for (int Bath = 0; Bath < 2; Bath++)
                            {
                                e.Graphics.DrawString("บาท", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - TextSize.Width, startTableY);
                                if (Bath == 0)
                                {
                                    e.Graphics.DrawString($"{Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Amountotel_Pay.ToString("D")))}", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - (TextSize1.Width + TextSize.Width + 100), startTableY);
                                    e.Graphics.DrawString($"หุ้นสะสม", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - (TextSize1.Width + TextSize.Width + 200), startTableY);
                                }
                                else
                                {
                                    e.Graphics.DrawString($"{Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Amountotel_Loan.ToString("D")))}", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - (TextSize1.Width + TextSize.Width + 100), startTableY);
                                    e.Graphics.DrawString($"กู้", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - (TextSize1.Width + TextSize.Width + 200), startTableY);
                                }
                                // เน้นช๊อกโก็เเลต บวก บัพเฟอร์ ที่ เเสนอร่่อย เนื้อ ครีมเน้นๆ ต้อง  DrawRectangle
                                e.Graphics.DrawRectangle(PenBlack, Line2_x - (TextSize1.Width + TextSize.Width + 200), startTableY, Line2_x - (Line2_x - (TextSize1.Width + TextSize.Width + 200)), TextSize1.Height);
                                startTableY += TextSize.Height;
                            }
                            TextSize = e.Graphics.MeasureString("บาท", Font(16, ThaiSarabun, FontStyle.Regular));
                            e.Graphics.DrawString("บาท", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - TextSize.Width, startTableY);
                            TextSize1 = e.Graphics.MeasureString($"{Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Amountotel_SUM.ToString("D")))}", Font(16, ThaiSarabun, FontStyle.Regular));
                            e.Graphics.DrawString($"{Class.Print.SetPrintMedtods.comma(Convert.ToInt32(Amountotel_SUM.ToString("D")))}", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - (TextSize1.Width + TextSize.Width + 100), startTableY);
                            e.Graphics.DrawString($"จำนวน", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - (TextSize1.Width + TextSize.Width + 200), startTableY);
                            // กรอบๆ
                            e.Graphics.DrawRectangle(PenBlack, Line2_x - (TextSize1.Width + TextSize.Width + 196), startTableY, Line2_x - (Line2_x - (TextSize1.Width + TextSize.Width + 196)), TextSize1.Height);
                        }
                        TextSize = e.Graphics.MeasureString("_____________________________", Font(13, ThaiSarabun, FontStyle.Regular));
                        e.Graphics.DrawString("_____________________________", Font(13, ThaiSarabun, FontStyle.Regular), BrushBlack, Line2_x - TextSize.Width, startTableY + 50);
                        TextSize1 = e.Graphics.MeasureString("ลงนาม", Font(18, ThaiSarabun, FontStyle.Bold));
                        e.Graphics.DrawString("ลงนาม", Font(18, ThaiSarabun, FontStyle.Bold), BrushBlack, Line2_x - (TextSize.Width + TextSize1.Width), startTableY + 30);
                        // คนทำรายการ
                        TextSize1 = e.Graphics.MeasureString(info_TeacherAdd, Font(18, ThaiSarabun, FontStyle.Regular));
                        e.Graphics.DrawString(info_TeacherAdd, Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, (Line2_x - TextSize1.Width) - ((TextSize.Width - TextSize1.Width) / 2), startTableY + 80);
                    }
                }
                else
                {
                    if (TextForm == "pay" || TextForm == "AmountOff" || TextForm == "PayLoan")
                    {
                        e.Graphics.DrawString("หมายเหตุ", Font(18, ThaiSarabun, FontStyle.Bold), BrushBlack, 50, startTableY + 20);
                        TextSize = e.Graphics.MeasureString("หมายเหตุ", Font(18, ThaiSarabun, FontStyle.Regular));

                        e.Graphics.DrawString("ใบเสร็จรับเงินฉบับนี้จะสมบูรณ์เมื่อผู้รับเงินลงลายมือชื่อเป็นอันเสร็จสิ้น \r\n" +
                                            "ชำระเเล้วไม่สามารถรับคืนหรือเปลี่ยนตัวไม่ว่ากรณีใดๆ", Font(12, ThaiSarabun, FontStyle.Bold), BrushBlack, 50 + TextSize.Width, startTableY + 20);
                    }
                    startTableY = 2000; // ทำให้ความยาวหน้ากระดาษ เกิน 
                }
            }
            pagepaper++;
            if (Currentposition_Row >= G.RowCount) // เเถวต้องเท่ากับเเถวทั้งหมดเเล้ว
            {
                // ========== Clear ===========
                SUM.Clear();
                Pay.Clear();
                Loan.Clear();
                cutline.Clear();
                Center.Clear();
                // ยอด
                Amountotel_SUM = 0;
                Amountotel_Pay = 0;
                Amountotel_Loan = 0;
                // เปิดหัวตารางเเค่ครั้งเดียว
                onetimestartColumns = 0;
                // ตำเเหน่งที่อยู่
                Currentposition_Row = 0;
                position = 0;
                // เลขหน้า
                pagepaper = 1;
                // ========== Clear ===========
                Print_two++;
                //start_and_stop++;
                if (confirmation == 1)
                {
                    if (Print_two >= 2)
                    {
                        e.HasMorePages = false;  // ปิด
                        Print_two = 0;
                    }
                    else
                    {
                        e.HasMorePages = true; // เปิด
                    }
                }
                else
                {
                    Print_two = 0;
                    e.HasMorePages = false;  // ปิด
                }
            }
            else
            {
                // ========== Clear ===========
                SUM.Clear();
                position = 0;
                // ========== Clear ===========
                // เปิดต่อ
                e.HasMorePages = true;
            }
        }
        // =============================== ข้อมูลสำหรับMetodนี้ ====================================
        // List  Material 
        static List<string[]> Rows = new List<string[]>(); 
        static List<bool[]> List_AloneOrNot_cells = new List<bool[]>(); // เก็บ ค่า ข้อความที่่มี Rows เดียว ไว้ ทั้งหมด
        static List<int> List_similar3 = new List<int>(); // ค่าที่ใช้สำหรับเช็ค // ไม่โดน
        static List<int> List_round = new List<int>(); // ค่าที่นับในเเต่ละรอบ นับ โดยนับจากเเต่ละบิลล์ มีกี่บิลล์ไม่รวมกับRows ทำให้รู้ว่ามีข้อมูลกี่อย่าง
        static List<int> List_similar_page = new List<int>(); // ค่าที่คัดเเยกเรียบร้อย
        static List<float[]> List_Text_Width = new List<float[]>(); // เก็บขนาดRows ความยาวที่มากที่สุดเเต่ละหน้ามา
        // เช็ค ตำเเหน่งข้อความ
        static string @string_Unicode = ""; // ข้อความที่ใช้สำหรับเช็ค 
        static int location_Unicode_Rows = 0, location_Unicode_Cells = 0; // ตำเเหน่งที่อยู่
        // เลขหน้า
        static string getder = "";
        public static void Detailspayment(System.Drawing.Printing.PrintPageEventArgs e, DataGridView G,string Header,string TextForm)
        {
            // sizepage
            float page_width = e.PageBounds.Width - 50, page_height = e.PageBounds.Height;
            // SET PoSITION
            float page_x = 50,page_y = 50;
            // Sizepage
            float Sizepage_x = e.PageBounds.Width, Sizepage_y = e.PageBounds.Height;
            // Sizestring
            SizeF Size = new SizeF();
            SizeF Size_2 = new SizeF();
            SizeF Size_3 = new SizeF();
            // Text
            // ข้อมูลวิทยาลัย
            string college_thai = DT.Rows[0][0].ToString(), 
            college_english = DT.Rows[0][1].ToString(),
            college_address = DT.Rows[0][5].ToString();
            // ข้อมูลในตาราง
            int all_paper, Rows = 18,Pagenum = 0;
            double b = (double)G.RowCount / Rows;
            if (G.RowCount / Rows == 0)
            {
                all_paper = 1;
            }
            else if (b.ToString().Length > 2)
            {
                all_paper = (G.RowCount / Rows) + 1;
            }
            else
            {
                all_paper = (int)b;
            }
            Size = e.Graphics.MeasureString($"หน้า {pagepaper}/{all_paper.ToString()} ", Font(16, ThaiSarabun, FontStyle.Bold));
           // e.Graphics.DrawString($"หน้า {List_round.Count}/{all_paper.ToString()} ", Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack, page_width - Size.Width, 30);
            Size = e.Graphics.MeasureString($"ประจำวันที่ : {Bank.Menu.Date_Time_SQL_Now.Rows[0][0].ToString()}", Font(16, ThaiSarabun, FontStyle.Regular));
            e.Graphics.DrawString($"ประจำวันที่ : {Bank.Menu.Date_Time_SQL_Now.Rows[0][0].ToString()}", Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, page_width - Size.Width,50);
            // ===============================================================
            // =========================== รูปภาพ เเละ ชื่อวิทยาลัย =================
            //================================================================
            System.Drawing.Image img = global::BankTeacher.Properties.Resources._64x64_TLC;
            // วาดภาw (โลโก้)
            // set
            int x = 50,y = 50,width_img = 100,height_img = 100;
            e.Graphics.DrawImage(img,x,y,width_img,height_img);
            page_x += width_img+10; 
            // ชื่อวิทยาลัย
            e.Graphics.DrawString(college_thai, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
            Size = e.Graphics.MeasureString(college_thai, Font(16, ThaiSarabun, FontStyle.Regular));
            page_y += Size.Height/2+5;
            // ชื่อวิทยาลัยภาษาอังกฤษ
            e.Graphics.DrawString(college_english, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
            Size = e.Graphics.MeasureString(college_english, Font(16, ThaiSarabun, FontStyle.Regular));
            page_y += Size.Height/2+5;
            // ที่ตั้ง
            e.Graphics.DrawString(college_address, Font(16, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
            Size = e.Graphics.MeasureString(college_address, Font(16, ThaiSarabun, FontStyle.Regular));
            page_y += Size.Height/2+5;
            // หัวข้อ
            page_y += 50;
            Size = e.Graphics.MeasureString(Header, Font(18, ThaiSarabun, FontStyle.Regular));
            e.Graphics.DrawString(Header, Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack,(Sizepage_x/2)-(Size.Width/2), page_y);
            // Reset page
            page_x = 50;
            page_y += 50;
            // ===============================================================
            // =========================== เริ่มเก็บข้อมูลใส่ในList =================
            //================================================================
            // ลูปหัวข้อตาราง เเล้วเช็คขนาดที่กว้างที่สุด
            float Text_width = 0; // ขนาดความยาวข้อความ
            // ================================= loop 1 =========================== 
            List<float> List_Text_width = new List<float>(); // เก็บ ความยาวทั้งหมดไว้     // material  width
            // หาค่าที่ซ้ำกัน
            int similar = 0; // เก็บค่าที่ ซ้ำกันในรายการ
            List<int> List_similar = new List<int>(); // เก็บค่าทีซ้ำกันไว้สำหรับคัดเเยก  // material similar
            List<int> List_similar2 = new List<int>(); // ค่าที่ใช้สำหรับเช็ค// โดนคัดเเยก  // material similar
            var Unicode = "  "; // U+0020 //(ห้ามเเก้ไขมันไม่ใช้ strng ว่างเปล่าเเต่เป็น Unicode) 
            List<string> cells = new List<string>(); // material cells
            // ================================= loop 2 =========================== 
            //List<float> List_similar_width = new List<float>();
            List<bool> List_aloneOrnot = new List<bool>(); // เก็บค่า ข้อความที่่มี Rows เดียว ไว้ ใน Rows เเต่ละรอล // // material Check location
            float Line_x = page_x, Line_y = page_y;
            int Check = 0; // ตรวจสอบการซ้ำ
            int Up = 0; // บอกตำเเหน่งที่อยู่ ของ Rows ปัจจุบัน
            // ================================= loop 3 =========================== 
            // เพิ่มRowsเเละเก็บค่า
            int up = 0, get = 0; // ขนาดข้อความหัวข้อ
            int setcut = 20; // ขนาดการตัดข้อความ
            if (PrintPreviewDialog.Rows.Count == 0) // เช็คข้อมูลในList ว่าพบข้อมูลหรือไม่ถ้า ไม่ให้ทำการเก็บ material all
            {
                // วาดข้อมูลการสมัคร
                if (TextForm.Contains("ReportIncome"))
                {
                    e.Graphics.DrawString($"จำนวนเงินสด : {info_Cash}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    Size = e.Graphics.MeasureString($"จำนวนเงินสด : {info_Cash}", Font(18, ThaiSarabun, FontStyle.Regular));
                    page_x += Size.Width;
                    e.Graphics.DrawString($"จำนวนเงินโอน : {info_Tranfer}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    Size = e.Graphics.MeasureString($"จำนวนเงินโอน : {info_Tranfer}", Font(18, ThaiSarabun, FontStyle.Regular));
                    page_x += Size.Width;
                    e.Graphics.DrawString($"จำนวนเงินบัตรเครดิต : {info_Cradit}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    Size = e.Graphics.MeasureString($"จำนวนเงินบัตรเครดิต : {info_Cradit}", Font(18, ThaiSarabun, FontStyle.Regular));
                    page_x += Size.Width;
                    e.Graphics.DrawString($"จำนวนเงินทั้งหมด : {info_SUMAmount}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    page_y += Size.Height;
                    page_x = 50;
                }
                else if (TextForm.Contains("ReportEpenses"))
                {
                    e.Graphics.DrawString($"จำนวนเงินทั้งหมด : {info_SUMAmount}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    Size = e.Graphics.MeasureString($"จำนวนเงินทั้งหมด : {info_SUMAmount}", Font(18, ThaiSarabun, FontStyle.Regular));
                    page_x += Size.Width;
                    e.Graphics.DrawString($"เงินกู้ทั้งหมด : {info_Cash}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    Size = e.Graphics.MeasureString($"เงินกู้ทั้งหมด : {info_Cash}", Font(18, ThaiSarabun, FontStyle.Regular));
                    page_x += Size.Width;
                    e.Graphics.DrawString($"จำนวนเงินถอนหุ้นทั้งหมด : {info_Cradit}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, page_x, page_y);
                    page_y += Size.Height;
                    page_x = 50;
                }
                // Reset page
                for (int C = 0; C < G.Columns.Count; C++) // ลูป ข้อมูลตาราง
                {
                    for (int R = 0; R < G.Rows.Count; R++) // ลูป ข้อมูลเเถว
                    {
                        for (int c = 0; c < G.Rows[R].Cells.Count; c++)
                        {
                            cells.Add(G.Rows[R].Cells[c].Value.ToString()); // เก็บ cells 
                        }
                        if (C == 0)
                            PrintPreviewDialog.Rows.Add(cells.ToArray()); // เก็บ cells ใน Rows  ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                        cells.Clear();
                        // ========= ส่วนการทำงานหารายการที่ซ้ำกัน ========
                        if (C == 1) // ทำงานเเค่คครั้งเดียว
                        {
                            // ตรวจเช็ค ข้อความที่มี Unicode ถ้าไม่มียอดสรุปผลไม่สามารถเช็คเอกสารได้ (Unicode ใช้สำหรับเช็คข้อความในตาราง จะมี Unicode ซ่อนอยู่ จึงจำเป็นต้อง บอกตำเเหน่ง Unicode ที่อยู่ด้วย)
                            //if (@string_Unicode == "") 
                            for (int r = 0; r < G.Rows.Count; r++)
                            {
                                for (int c = 0; c < G.Rows[r].Cells.Count; c++)
                                {
                                    if (G.Rows[r].Cells[c].Value.ToString().Contains(Unicode)) // ถ้าเจอข้อความที่กำหนดใน cells เเล้วให้ทำการเก็บ ค่าทั้งหมด
                                    {
                                        string_Unicode += G.Rows[r].Cells[c].Value.ToString(); // ข้อความ // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                        location_Unicode_Rows = r; // Rows // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                        location_Unicode_Cells = c; // Cells // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                        break;
                                    }
                                }
                                if (string_Unicode != "") { break; }
                            }
                            if (string_Unicode == "") { string_Unicode = G.Rows[-1].Cells[-1].Value.ToString(); } // เเจ้งเตือน Error ไม่มี ยอดสรุป เเบบปริ้นนี้จำเป็นต้องอ้างอิงยอดสรุป

    

                            if (!G.Rows[R].Cells[location_Unicode_Cells].Value.ToString().Contains(Unicode)) // ซ้ำกัน  // (เเบบปริ้นนี้จำเป็นต้องอ้างอิงยอดสรุป) ถูกกำหนดไว้ ถ้าข้อมูลในตารางเปลี่ยน ข้อมูลอาจเสียหาย สามารถอ้างอิงโดยใช้ Unicode
                            {
                                similar++; // บวกขนาดที่ไม่มี ยอดสรุป
                            }
                            else  // ไม่ซ้ำ
                            {
                                similar++; // บวกขนาดที่มี ยอดสรุป
                                List_similar.Add(similar); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                List_similar2.Add(similar); // เเทนค่าก่อนถูกเเยก ค่านี้ใช้สำหรับเช็ค Rows ที่จะวาดที่ตำเเหน่งใด } // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                List_similar3.Add(similar); // เหมือนกันเเค่ไม่คัดออก // ข้อมูลหลักที่ใช้ในการเช็ค (จำเป็น)
                                similar = 0;
                            }
                            // ========= ส่วนการทำงานเเยกรายการที่ซ้ำกันเเล้วตรวจสอบข้อมูลที่ควรจะปริ้นในเเต่ละหน้า ========
                            if (R == G.RowCount - 1) // ทำงานครั้งเดียว
                            {
                                int sum = 0; // รวม Rows ที่ ปริ้นได้ในรอบหน้ากระดาษ
                                bool Is = false,not = false; // เช็คข้อมูลว่าครบหรือยัง
                                int Number_round = 0; // นับ รอบที่ ปริ้นในหน้า โดยนับเป็นรายการ
                                // ตวรจสอบข้อมูลที่ควรปริ้นใน เเต่ละ โดยเเยกออกมาเก็บไว้ใน List
                                do
                                {
                                    sum += List_similar[0]; // เก็บค่า
                                    Number_round++;
                                    if(List_similar.Count() == 0) { not = true; }
                                    if (sum >= Rows) // ตรวจสอบว่าเกินขนาดหรือไม่ 18 คือขนาดทีRows ที่ไม่ควรเกินหน้ากระดาษ
                                    {
                                        if (sum > Rows) // ถ้าขนาดไม่ พอ ดี ตามกำหนด ให้ตัด ขนาดที่เกินออกไป
                                        {
                                            sum -= List_similar[0]; // ย้อนค่าคืน
                                            Number_round--;
                                            not = true;  // เปิดการลบค่า
                                        }
                                        if(sum != 0)
                                        {
                                            List_similar_page.Add(sum); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                            List_round.Add(Number_round); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                        }
                                        else
                                        {
                                            sum += List_similar[0];
                                            Number_round++;
                                            List_similar_page.Add(sum); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                            List_round.Add(Number_round); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                            not = false;
                                        }
                                        Number_round = 0;
                                        sum = 0;
                                    }
                                    if(!not) // ค่าการลบต้องปิด
                                        List_similar.RemoveAt(0);
                                    if (List_similar.Count == 0) // เช็คข้อมูลที่ตกหล่น
                                    {
                                        if (sum > 0) // เช็คว่าข้อมูลยังเหลืออยู่หรือไม่
                                        {
                                            List_similar_page.Add(sum); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                            List_round.Add(Number_round); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                                            Number_round = 0;
                                            sum = 0;
                                        }
                                        Is = true;
                                    }
                                    not = false;
                                } while (!Is); // ถ้าข้อมูลหมดเเล้ว จะได้เป็น เธอ
                                Pagenum = List_round.Count;
                            }
                        }
                    }
                }
                // ===============================================================
                // =========================== เช็คRowsที่มี 1 Rows ==================
                //================================================================
                for (int next = 0; next < List_round.Count(); next++) // ลูปข้อมูลของเเต่ละหน้า
                {
                    for (int r = 0; r < List_round[next]; r++) // หน้านี้มีกี่ Rows
                    {
                        for (int c = 0; c < G.Rows[r].Cells.Count; c++)   // Cells ที่มีทั้งหมด
                        {
                            for (int loop = 0; loop < List_similar2[r]; loop++) // loop ข้อความในเเต่ละ Rows เพื่อตรวจสอบว่า ข้อความในรอบ ได้มีเพียงเเค่ข้อความเดียวหรือไม่
                            {
                                if (G.Rows[Up + loop].Cells[c].Value.ToString() == "" && G.Rows[Up + loop].Cells[location_Unicode_Cells].Value.ToString().Contains(Unicode)) // ข้อความต้องไม่ใช้ ข้ออความที่กำหนด ถ้าใช้เเสดงว่า ขนาดที่ซ้ำกันน้อยกว่า 1 จึง ไม่ซ้ำกัน   if (G.Rows[Up + loop].Cells[c].Value.ToString() == "" && G.Rows[Up + loop].Cells[location_Unicode_Cells].Value.ToString() == string_Unicode)
                                {
                                    if (Check < 2) // ขนาดที่ซ้ำน้อยเกินกว่าจะหาค่ากึ่งกลางได้
                                    {
                                        Check = 0;
                                    }
                                }
                                else if (G.Rows[Up + loop].Cells[c].Value.ToString() == "" || loop == 0) // ครั้งเเรกนับ เเละ ครั้ง ต่อไป ที่ว่างเปล่า
                                {
                                    Check++; // บวกจำนวนที่ซ้ำ
                                }
                            }
                            if (Check >= 2) // ข้อความมี อย่างเดียว
                            {
                                List_aloneOrnot.Add(true); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                            }
                            else // ข้อมีขนาดที่ซ้ำกัน
                            {
                                List_aloneOrnot.Add(false); // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                            }
                            if (c == G.Rows[r].Cells.Count - 1) // บอกตำเเหน่งปัจับัน
                            {
                                //if (r < List_round[next] - 1)
                                    Up += List_similar2[r];
                            }
                            Check = 0;
                        }
                        List_AloneOrNot_cells.Add(List_aloneOrnot.ToArray()); // เก็บค่าในเเต่ละ cells // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                        List_aloneOrnot.Clear();
                    }
                    for (int l = 0; l < List_round[next]; l++)
                    {
                        List_similar2.RemoveAt(0);
                    }
                }
                // ===============================================================
                // =========================== เช็คขนาดข้อความ ======================
                //================================================================
                // ลูปรายการที่มีในเเต่ละหน้า รายเเก่ได้เเก่ ขนาดที่ปริ้นในเเต่ละหน้า
                for (int list = 0; list < List_similar_page.Count(); list++)
                {
                    for (int C = 0; C < G.ColumnCount; C++)  // หัวตาราง
                    {
                        Size.Width = Class.Print.SetPrintMedtods.CutingCharAndString(e, G.Columns[C].HeaderText.ToString(), setcut, 0, 0, out Class.Print.SetPrintMedtods.nu, out Class.Print.SetPrintMedtods.nu, 1);
                        //Size = e.Graphics.MeasureString(G.Columns[C].HeaderText.ToString(), Font(18, ThaiSarabun, FontStyle.Regular)); // ขนาดหัวตารางของเเต่ละอัน
                        for (int loop = 0; loop < List_similar_page[list]; loop++)
                        {
                            // ========= ส่วนการทำงานหาขนาด ========
                            if (list == 0) // ครั้งเเรกให้เก็บค่าใน loop
                            {
                                up = loop;
                            }
                            else // ถ้ารายการมีมากกว่า 2 หมายถึงมีมากกว่า 1 หน้า  ให้ทำการ เก็บ ค่า จาก get ซึ่งรับของมูลมาจาก List_similar2[list]
                            {
                                up = get + loop;
                            }
                            // ขนาดข้อความในเเถว
                            // ใช้สำหรับหาขนาดที่ถูกตัด ข้อดีคือหาขนาดที่เเน่นอนได้ หลังการตัด
                            Size_2.Width = Class.Print.SetPrintMedtods.CutingCharAndString(e, G.Rows[up].Cells[C].Value.ToString(), setcut, 0, 0, out Class.Print.SetPrintMedtods.nu, out Class.Print.SetPrintMedtods.nu, 1);
                            // ข้อเสียไม่สามรถตรวจสอบการเว้นบรรดทัดได้ จึงเสียพื้นที่ได้การวาดได้
                            //Size_3 = e.Graphics.MeasureString(G.Rows[up].Cells[C].Value.ToString(), Font(18, ThaiSarabun, FontStyle.Regular));
                            if (Size_2.Width > Size_3.Width) // ขนาดในเเถว เเต่ละรอบ เเล้วหาขาดที่มากที่สุด
                            {
                                Text_width = Size_2.Width;
                            }
                            if (Size_2.Width != 0 && Size_2.Width > Size_3.Width)
                            {
                                Size_3.Width = Size_2.Width;
                            }
                        }
                        Size_3.Width = 0;
                        // หาขนาดที่ยาวที่สุดเพื่อวาดตารางของหัวข้อนั้นๆ
                        if (Size.Width > Text_width)
                        {
                            Text_width = Size.Width;
                        }
                        // เก็บขนาดที่มากที่สุดของเเเต่ละ เเถว
                        List_Text_width.Add(Text_width); 
                        up = 0;
                    }
                    List_Text_Width.Add(List_Text_width.ToArray()); // เก็บค่าของเเต่ละหน้า // ข้อมูลรองที่ใช้ในการเช็ค (จำเป็น)
                    List_Text_width.Clear(); // ลบ
                    get += List_similar_page[list]; // เเทนค่าให้สำหรับเช็คตำเเหน่งใน Rows
                }
            }
            // วาดเส้นเปิด
            e.Graphics.DrawLine(PenBlack, page_width, page_y, page_x, page_y);
            Line_x = page_x;
            Line_y = page_y;
            // ===============================================================
            // =========================== Columns ===========================
            //================================================================
            float page_notovers = 0; // หน้ากระดาษเหลือ
            float Rectanglef_width,Rectanglef_height; // กำหนดกรอบการวาด
            bool center = true; // กึ่งกลาง = true
            float Center_ ,Center = 50; // Ceter_ ค่าที่คำนวณ / Ceter ค่าที่ใช้
            List<float> ListOver_height = new List<float>(); // ขนาดความยาว สำหรับหาค่่า Y เพื่อปริ้นในรอบถัดไป
            List<float> locationline = new List<float>();  // ตำเเหน่งเส้น 
            float location_y = page_y, location_y2 = 0,location_INDAX;
            bool chcel_line = true;
            if (List_Text_Width[0].Sum() + 50 <= page_width) // ถ้าขนาดที่วาดเหลือให้เอาไปบวกเพิ่ม
            {
                page_notovers = (page_width - (List_Text_Width[0].Sum() + 50)) / G.ColumnCount; // ขนาดพื้นที่ที่เหลือ
            }
            for (int C = 0; C < G.ColumnCount; C++) // หัวตาราง
            {
                Size = e.Graphics.MeasureString(G.Columns[C].HeaderText, Font(18, ThaiSarabun, FontStyle.Regular)); // เก็บค่าหัวตาราง
                if (page_x + Size.Width > page_width) // ถ้า ระยะการวาด เกิน ขอบที่กำหนดให้ตัดเส้น
                {
                    Rectanglef_width =  page_x + Size.Width - page_width;
                    Rectanglef_height = Size.Height;
                }
                else 
                { 
                    Rectanglef_width = List_Text_Width[0][C];
                    Rectanglef_height = Size.Height;
                    if (C == G.ColumnCount - 1 && Center < page_width)
                    {
                        Rectanglef_width = page_width - Center;
                    }
                }
                if (center)  // เป็นจริงให้ ตำเเหน่งกลาง
                {
                    Size = e.Graphics.MeasureString(G.Columns[C].HeaderText, Font(18, ThaiSarabun, FontStyle.Regular));
                    Center_ = ((List_Text_Width[0][C]/2) + (page_notovers/2));
                    //Center += Center_ - (Size.Width / 2);
                    Center += Center_ - (Size.Width / 2);
                    if (Center > page_width) // ถ้าขนาดหน้าเกินกำหนดให้ใช้ค่าที่ไม่เกินขนาด
                    {
                        Center = page_x; 
                    }
                }
                else // ไม่เป็นจริงให้ ชิดซ้าย
                {
                    Center = page_x;
                }
                Size_2 = e.Graphics.MeasureString($"{G.Columns[C].HeaderText}", Font(18, ThaiSarabun, FontStyle.Regular)); // เก็บขนาด
                if(Size_2.Width > Rectanglef_width) // ถ้าขนาด ความยาวเกินกรอบให้ทำการเพิ่มระยะคววามยาว
                {
                    var @decimal = Size_2.Width / Rectanglef_width;
                    var Ceiling = Math.Ceiling(@decimal);
                    var INDAX_int = Size.Height * Ceiling;
                    Rectanglef_height = (float)INDAX_int;
                }
                ListOver_height.Add(Rectanglef_height); // เก็บค่าความยาว
                // ตรวจเช็คขนาดที่ควรจะวาดลง เหมือนมีข้อมูลที่ซ้ำกัน
                
                e.Graphics.DrawString($"{G.Columns[C].HeaderText}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center, page_y, Rectanglef_width, Rectanglef_height)); // วาด หัวข้อความ
                // Test
                //e.Graphics.DrawRectangle(PenBlack, page_x, page_y, Rectanglef_width, Rectanglef_height); // Test กรอบ
                //e.Graphics.DrawString($"|{Center}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center, page_y + 50, 200, 200)); // Test สำหรับผู้เเเก้ไข กลาง
                //e.Graphics.DrawString($"|{page_x}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(page_x, page_y + 100, 200, 200)); // Test สำหรับผู้เเเก้ไข ชิดซ้าย
                locationline.Add(page_x);
                page_x += List_Text_Width[0][C] + page_notovers; // บวกขนาดในรอบถัดไป
                Center = page_x;
            }
            page_x = 50; // Test ถ้าใช้งานจริงจะไม่ loop เเต่เป็นการบอกรายการใน Array เองว่าเป็นรายการอะไร
            Center = 50;
            page_y += Class.Print.SetPrintMedtods.MaxValues(0, ListOver_height);
            location_y2 = page_y;
            for (int line = 0; line < G.ColumnCount;line++)
            {
                // เส้นปิดข้าง
                e.Graphics.DrawLine(PenBlack, locationline[line], location_y, locationline[line], location_y2);
            }
            locationline.Clear();
            location_y = 0;
            location_y2 = 0;
            ListOver_height.Clear();
            // วาดเส้นเปิด
            e.Graphics.DrawLine(PenBlack, page_width, page_y, page_x, page_y);
            //e.Graphics.DrawString("|-850", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(page_width, 170, 50, 50));
            //e.Graphics.DrawString("|-50", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(50, 170, 50, 50));
            // ============================================================
            // =========================== Rows ===========================
            //=============================================================
            //วาด Rows กำลังทดสอบ
            // ตำเเหน่งที่วาด Rows เดียว
            int UP = 0;
            float page_Yreturn = page_y;
            bool CHECK = false;
            for (int R = 0; R < List_similar_page[0]; R++)
            {
                if(chcel_line)
                location_y = page_y;
                for (int c = 0; c < G.Rows[R].Cells.Count; c++)
                {
                    Size = e.Graphics.MeasureString($"{PrintPreviewDialog.Rows[R][c]}", Font(18, ThaiSarabun, FontStyle.Regular));
                    //Size = e.Graphics.MeasureString($"{G.Rows[R].Cells[c].Value}", Font(18, ThaiSarabun, FontStyle.Regular)); // เก็บค่าหัวตาราง
                    if (page_x + Size.Width > page_width && c == G.Rows[R].Cells.Count - 1) // ถ้า ระยะการวาด เกิน ขอบที่กำหนดให้ตัดเส้น
                    {
                        Rectanglef_width = page_x + Size.Width - page_width;
                        Rectanglef_height = Size.Height;
                    }
                    else
                    {
                        Rectanglef_width = List_Text_Width[0][c];
                        Rectanglef_height = Size.Height;
                        if (R == G.Rows[R].Cells.Count - 1 && Center < page_width)
                        {
                            Rectanglef_width = page_width - Center;
                        }
                    }
                    if (center)  // เป็นจริงให้ ตำเเหน่งกลาง
                    {
                        Size = e.Graphics.MeasureString($"{PrintPreviewDialog.Rows[R][c]}", Font(18, ThaiSarabun, FontStyle.Regular));
                        //Size = e.Graphics.MeasureString($"{G.Rows[R].Cells[c].Value}", Font(18, ThaiSarabun, FontStyle.Regular));
                        Center_ = ((List_Text_Width[0][c] / 2) + (page_notovers / 2));
                        //Center += Center_ - (Size.Width / 2);
                        Center += Math.Abs(Center_ - (Size.Width / 2));
                        if (Center > page_width) // ถ้าขนาดหน้าเกินกำหนดให้ใช้ค่าที่ไม่เกินขนาด
                        {
                            Center = page_x;
                        }
                    }
                    else // ไม่เป็นจริงให้ ชิดซ้าย
                    {
                        Center = page_x;
                    }
                    if (Rectanglef_width > 250f) // เช็คขนาดที่เกิน เเต่อาจจะได้เช็คเเค่ cells สุดท้าย ยังไม่เเน่ใจวาสควรมีมั้ย
                    {
                        Rectanglef_width = List_Text_Width[0][c];
                        Size_2 = e.Graphics.MeasureString($"{PrintPreviewDialog.Rows[R][c]}", Font(18, ThaiSarabun, FontStyle.Regular));
                        //Size_2 = e.Graphics.MeasureString($"{G.Rows[R].Cells[c].Value}", Font(18, ThaiSarabun, FontStyle.Regular),(int)Rectanglef_width); // เก็บขนาด
                    }
                    else // ใช้ปกติ
                    {
                        Size_2 = e.Graphics.MeasureString($"{PrintPreviewDialog.Rows[R][c]}", Font(18, ThaiSarabun, FontStyle.Regular));
                        //Size_2 = e.Graphics.MeasureString($"{G.Rows[R].Cells[c].Value}", Font(18, ThaiSarabun, FontStyle.Regular)); // เก็บขนาด
                    }
                    if (Size_2.Width > Rectanglef_width) // ถ้าขนาด ความยาวเกินกรอบให้ทำการเพิ่มระยะคววามยาว
                    {
                        var @decimal = Size_2.Width / Rectanglef_width;
                        var Ceiling = Math.Ceiling(@decimal);
                        var INDAX_int = Size.Height * Ceiling;
                        Rectanglef_height = (float)INDAX_int;
                    }
                    if (Size_2.Width < Rectanglef_width)
                    {
                        ListOver_height.Add(Rectanglef_height); // เก็บค่าความยาว
                    }
                    else
                    {
                        if (!List_AloneOrNot_cells[UP][c])
                        {
                            ListOver_height.Add(Rectanglef_height); // เก็บค่าความยาว
                        }
                    }    
                    if (PrintPreviewDialog.Rows[R][c].ToString() == "") { CHECK = true; }
                    if (!CHECK) 
                    {
                        if (List_AloneOrNot_cells[UP][c]) // เช็คว่าตำเเหน่งเป็นจริงหรือไม่
                        {
                            if (page_Yreturn == page_y) // ขนาด กระดาษ ต้อง เหมือนเดิม
                            { page_Yreturn = page_y; } // เก็บค่าใหม่
                            else { page_y = page_Yreturn; } // ถ้าขนาดไม่เหมือนเดิมให้เปลี่ยนกลับ
                            Size_2 = e.Graphics.MeasureString($"{PrintPreviewDialog.Rows[R][c]}", Font(18, ThaiSarabun, FontStyle.Regular)); // เก็บขนาด
                            Size_3 = e.Graphics.MeasureString($"{PrintPreviewDialog.Rows[R][location_Unicode_Cells]}", Font(18, ThaiSarabun, FontStyle.Regular)); // เก็บขนาด
                            //var height = Size_3.Height * List_similar2[UP];
                            //var @as = height / 2;
                            //var tast = @as - (Size_2.Height / 2);
                            //var tt = page_y + tast;
                            var height = page_y + ((Size_3.Height * List_similar3[UP]) / 2) - Size_2.Height;
                            page_y = height;
                            //Test การวาดระยะ e.Graphics.DrawString($"|{a}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(page_x, a, 200, 200)); // Test สำหรับผู้เเเก้ไข ชิดซ้าย
                        }
                        else {  page_y = page_Yreturn; } // เก็บค่าขนาดเดิม
                        if (c == G.Rows[R].Cells.Count - 1) // ถ้าระยะสุดท้าย ให้บวกเพิ่ม ตำเเหน่ง Rows
                        {
                            if (UP < List_AloneOrNot_cells.Count()-1)
                            {
                                UP++;
                            }
                        }
                    }
                    e.Graphics.DrawString($"{PrintPreviewDialog.Rows[R][c]}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center, page_y, Rectanglef_width, Rectanglef_height)); // วาด หัวข้อความ
                    //e.Graphics.DrawString($"{G.Rows[R].Cells[c].Value}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center, page_y, Rectanglef_width, Rectanglef_height)); // วาด หัวข้อความ
                    // Test เช็คตำเเหน่งที่จะวาด
                    //e.Graphics.DrawRectangle(PenBlack, page_x, page_y, Rectanglef_width, Rectanglef_height); // Test กรอบ
                    //e.Graphics.DrawString($"|{Center}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center, page_y + 50, 200, 200)); // Test สำหรับผู้เเเก้ไข กลาง
                    //e.Graphics.DrawString($"|{page_x}", Font(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(page_x, page_y + 100, 200, 200)); // Test สำหรับผู้เเเก้ไข ชิดซ้าย
                    if (locationline.Count() < G.ColumnCount)
                    locationline.Add(page_x);
                    page_x += List_Text_Width[0][c] + page_notovers; // บวกขนาดในรอบถัดไป
                    Center = page_x;
                    chcel_line = false;
                    if (PrintPreviewDialog.Rows[R][location_Unicode_Cells].Contains(Unicode)) 
                    {
                        location_y2 = page_y;
                        // เส้นปิด Rows
                        e.Graphics.DrawLine(PenBlack, page_width, page_y, Line_x, page_y);
                    }
                    else if (TextForm == "ReportEpenses" && !PrintPreviewDialog.Rows[R][location_Unicode_Cells].Contains(Unicode))
                    {
                        location_y2 = page_y;
                        e.Graphics.DrawLine(PenBlack, page_width, page_y, Line_x, page_y);
                    }
                }
                page_x = 50; // Test ถ้าใช้งานจริงจะไม่ loop เเต่เป็นการบอกรายการใน Array เองว่าเป็นรายการอะไร
                Center = 50;
                page_y = page_Yreturn; // New คืนค่า
                page_y += Class.Print.SetPrintMedtods.MaxValues(0, ListOver_height);
                ListOver_height.Clear();
                location_INDAX = page_y;
                page_Yreturn = page_y;
                CHECK = false;
                // เส้นปิด Rows
                if (PrintPreviewDialog.Rows[R][location_Unicode_Cells].Contains(Unicode)) //   if (Rows[R][location_Unicode_Cells] == string_Unicode)
                {
                    for(int line = 0; line < G.ColumnCount; line++)
                    {
                        if(line == G.ColumnCount - 1) { location_y2 = location_INDAX; }
                        // เส้นปิดข้าง
                        e.Graphics.DrawLine(PenBlack, locationline[line], location_y, locationline[line], location_y2);
                    }
                    // เส้นปิด Rows
                    e.Graphics.DrawLine(PenBlack, page_width, page_y, Line_x, page_y); // สำหรับปิดยอดเงิน
                    chcel_line = true;
                }
            }
            // เส้นปิด
            e.Graphics.DrawLine(PenBlack, page_width, page_y, Line_x, page_y);
            // เส้นปิดข้าง 
            e.Graphics.DrawLine(PenBlack, page_x,Line_y, page_x, page_y); // ขวา
            e.Graphics.DrawLine(PenBlack, page_width, Line_y, page_width, page_y); // ซ้าย
            // เช็คหน้าที่ยังไม่ได้ทำการปริ้นเอกสาร
            // OpenPrint new page
            // ============================================================
            // ============ ลบ ข้อมูลใน List รายอย่างมากที่จำเป็น =================
            //=============================================================
            for (int loop8 = 0; loop8 < List_round[0]; loop8++)  // สำหรับข้อมูลที่ มีรายการเเค่ 8 
            {
                List_AloneOrNot_cells.RemoveAt(0);
                List_similar3.RemoveAt(0);
                if (loop8 == List_round[0] - 1) // สำหรับข้อมูลที่ มีรายการเดียว  
                {
                    List_Text_Width.RemoveAt(0);
                    List_round.RemoveAt(0);
                    break;
                }
                //List_aloneOrnot_cells.RemoveAll(item => true);
            }
            for (int loop18 = 0; loop18 < List_similar_page[0]; loop18++) // สำหรับข้อมูลที่ มีรายการเเค่ 16 
            {
                PrintPreviewDialog.Rows.RemoveAt(0);
                if(loop18 == List_similar_page[0] - 1) // สำหรับข้อมูลที่ มีรายการเดียว  
                {
                    List_similar_page.RemoveAt(0);
                    break;
                }
            }
            // ย้ายการวาดเส้น
            if(Pagenum != 0)
            {
                getder = Pagenum.ToString();
            }
            Size = e.Graphics.MeasureString($"หน้า {pagepaper}/{getder} ", Font(16, ThaiSarabun, FontStyle.Bold));
            e.Graphics.DrawString($"หน้า {pagepaper}/{getder} ", Font(16, ThaiSarabun, FontStyle.Bold), BrushBlack, page_width - Size.Width, 30);
            if (PrintPreviewDialog.Rows.Count() != 0)
            {
                pagepaper++;
                e.HasMorePages = true; // เปิดการวาดหน้าต่อไป เพราะ Rows ยังคงเหลืออยู่
            }
            else
            {
                string_Unicode = "";
                location_Unicode_Cells = 0;
                location_Unicode_Rows = 0;
                pagepaper = 1;
                e.HasMorePages = false; // ปิดการวาดหน้าต่อไป ถ้า Rows ไม่เหลือเเล้ว
            }
        }
        // เลือก Font ตามใจฉัน
        public static Font Font(float SizeFont, String Font, FontStyle Fontstyle)
        {
            Font F = new Font(Font, SizeFont, Fontstyle);
            return F;
        }

       
        public static int calculate_distance(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float TextX, float TextY, float Rectangle_width, float Rectangle_height, float Size_X, float lengthY)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText, (int)Rectangle_width);
            float startingpoint = Size_X - Rectangle_width;
            int ExtraRow;
            if (SizeString.Height < Rectangle_height)
            {
                ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(lengthY) + 1);
            }
            else
            {
                SizeString.Height = Rectangle_height;
                ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(lengthY) + 1);
            }
            e.Graphics.DrawString(Text, fontText, brush, new RectangleF(TextX, TextY, Rectangle_width, Rectangle_height));
            return ExtraRow;
        }

        public static string NumToBath(string InpuNum, bool IsTrillion = false)
        {
            string BahtText = "";
            string Trillion = "";
            string[] strThaiNumber = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] strThaiPos = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };

            decimal ReturnToBath = 0;
            decimal.TryParse(InpuNum, out ReturnToBath);

            if (ReturnToBath == 0)
            {
                return "ศูนย์บาท";
            }

            InpuNum = ReturnToBath.ToString("0.00");
            string strInteger = InpuNum.Split('.')[0];
            string strSatang = InpuNum.Split('.')[1];

            if (strInteger.Length > 13)
                throw new Exception("รองรับตัวเลขได้เพียง ล้านล้าน เท่านั้น!");

            bool _IsTrillion = strInteger.Length > 7;
            if (_IsTrillion)
            {
                Trillion = strInteger.Substring(0, strInteger.Length - 6);
                BahtText = NumToBath(Trillion, _IsTrillion);
                strInteger = strInteger.Substring(Trillion.Length);
            }

            int strLength = strInteger.Length;
            for (int i = 0; i < strInteger.Length; i++)
            {
                string number = strInteger.Substring(i, 1);
                if (number != "0")
                {
                    if (i == strLength - 1 && number == "1" && strLength != 1)
                    {
                        BahtText += "เอ็ด";
                    }
                    else if (i == strLength - 2 && number == "2" && strLength != 1)
                    {
                        BahtText += "ยี่";
                    }
                    else if (i != strLength - 2 || number != "1")
                    {
                        BahtText += strThaiNumber[int.Parse(number)];
                    }

                    BahtText += strThaiPos[(strLength - i) - 1];
                }
            }

            if (IsTrillion)
            {
                return BahtText + "ล้าน";
            }

            if (strInteger != "0")
            {
                BahtText += "บาท";
            }

            if (strSatang == "00")
            {
                BahtText += "ถ้วน";
            }
            else
            {
                strLength = strSatang.Length;
                for (int i = 0; i < strSatang.Length; i++)
                {
                    string number = strSatang.Substring(i, 1);
                    if (number != "0")
                    {
                        if (i == strLength - 1 && number == "1" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "เอ็ด";
                        }
                        else if (i == strLength - 2 && number == "2" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "ยี่";
                        }
                        else if (i != strLength - 2 || number != "1")
                        {
                            BahtText += strThaiNumber[int.Parse(number)];
                        }

                        BahtText += strThaiPos[(strLength - i) - 1];
                    }
                }

                BahtText += "สตางค์";
            }

            return BahtText;
        }

        // ตัด หน้าจอ
        public static void Printdatagridview(System.Drawing.Printing.PrintPageEventArgs e, DataGridView G, Bitmap bmp)
        {
            G.ClearSelection();
            int height = G.Height;
            G.Height = G.RowTemplate.Height * G.RowCount + 39; /*G.Height = G.RowCount * G.RowTemplate.Height * 2;*/
            bmp = new Bitmap(G.Width, G.Height);
            G.DrawToBitmap(bmp, new Rectangle(0, 0, G.Width, G.Height));
            G.Height = height;
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        // เเบบฝึกสอนการ ปริ้น หน้าเพิ่ม
        static int printedLines = 0;
        public static void ExamplePrint(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = true;
            Graphics graphic = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.Black);

            Font font = new Font("Courier New", 12);

            e.PageSettings.PaperSize = new PaperSize("A4", 850, 1100);

            float pageWidth = e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PrintableArea.Height;

            float fontHeight = font.GetHeight();
            int startX = 40;
            int startY = 30;
            int offsetY = 0;
            int SpacePerRow = 35;
            int CurrentRows = 0;

            if (pageNow == 0)
            {
                while (printedLines < page_length)
                {
                    graphic.DrawString("Line: " + printedLines, font, brush, startX, startY + offsetY + (SpacePerRow * CurrentRows++));
                    //offsetY += (int)fontHeight;

                    ++printedLines;
                }
            }
            else
            {
                while (printedLines < page_length)
                {
                    graphic.DrawString("Line: " + printedLines, font, brush, startX, startY + offsetY + (SpacePerRow * CurrentRows++));
                    //offsetY += (int)fontHeight;

                    ++printedLines;
                }
            }



            page_length = page_length * 2;
            pageNow++;

            if (pageNow == 2)
            {
                //graphic.DrawString("Line:OOP", font, brush, startX, startY + offsetY);
                e.HasMorePages = false;
                pageNow = 0;
                page_length = 50;
                printedLines = 0;
            }
        }

    }
}
