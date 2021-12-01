using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Class.Print
{

    class PrintPreviewDialog
    {
        public static Font THsarabun30 = new Font("TH Sarabun New", 30, FontStyle.Bold);
        public static Font THsarabun18 = new Font("TH Sarabun New", 18, FontStyle.Bold);
        public static Font THsarabun16 = new Font("TH Sarabun New", 16, FontStyle.Bold);
        public static Font THsarabun14 = new Font("TH Sarabun New", 14, FontStyle.Bold);
        public static Brush BrushBlack = Brushes.Black;
        public static String ThaiSarabun = "TH Sarabun New";

        static int pageNow = 0;
        static int printedLines = 0;
        static int onetimestartColumns = 0;
        public static int linesToPrint = 0;
        public static int position = 0, Currentposition = 0;
        // จุดกึ่งกลางเส้น
        static float center = 0;
        static List<float> Center = new List<float>();
        // เส้นปิดข้าง
        static List<float> cutline = new List<float>();
        // เเบบ ปริ้น หน้า สมัคร
        public static void PrintMember(System.Drawing.Printing.PrintPageEventArgs e, String SQLCode, String Day, String Month, String Year, String TeacherNo, String Amount)
        {
            if (TeacherNo != "")
            {
                if (Amount == "")
                {
                    Amount = BankTeacher.Bank.Menu.startAmountMin.ToString();
                }
                // X 850 = 22 cm เเนะนำ 800 //
                // A4 = 21 cm  {Width = 356.70163 Height = 136.230438} {Width = 356.70163 Height = 102.954086} // 
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode.Replace("{TeacherNo}", TeacherNo));
                int PageX = (e.PageBounds.Width);
                int PageY = (e.PageBounds.Height);
                int XP = 0;
                int XD = 0;
                int X = 50;
                int Y = 50;
                int SpacePerRow = 35;
                int CurrentRows = 0;
                //------------------------

                // ส่วนหัว

                Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "ใบสมัครสมาชิกสหกรณ์ครู", THsarabun30, BrushBlack);
                Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "วิทยาลัยเทคโนโลยี อีอีซีเอ็นจิเนีย แหลมฉบัง", THsarabun30, BrushBlack);
                // วันที่
                string MemberID = "สมาชิกเลขที่ " + dt.Rows[0][0].ToString();
                string School = "เขียนที่ วิทยาลัยเทคโนโลยี อีอีซีเอ็นจิเนีย แหลมฉบัง";

                //ข้อมูลส่วนตัว
                string Name = "ข้าพเจ้า " + dt.Rows[0][1].ToString();
                string IdCardNum = "เลขประจำตัวประชาชน " + dt.Rows[0][2].ToString();
                string HouseNum = "อยู่บ้านเลขที่ " + dt.Rows[0][3].ToString();
                string Village = "หมู่ " + dt.Rows[0][4].ToString();
                string SubDistrict = "ตำบล " + dt.Rows[0][5].ToString();
                string District = "อำเภอ " + dt.Rows[0][6].ToString();
                string Province = "จังหวัด " + dt.Rows[0][7].ToString();
                string TelNo = "เบอร์โทร " + dt.Rows[0][8].ToString();

                //รายละเอียด
                string amountbauy = "ข้อ 2 ข้าพเจ้าขอถือหุ้นของกิจกรรมสหกรณ์ครู ซึ่งมีค่าหุ้นล่ะ 500 บาท";
                string buy = $"2.1 ข้อซื้อหุ้นจำนวน " + Amount + " บาท";
                string share = "2.2 รับโอนหุ้นจาก -";
                string Who = "สมาชิกเลขที่ " + TeacherNo;
                string quantity = "จำนวน 1 หุ้น (ถ้ามี)";
                string price = "เเละชำระค่าหุ้น " + Amount + " บาท ทันทีที่ได้รับเเจ้งให้เข้าเป็นสมาชิก";

                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{MemberID}\r\n" +
                                                      $"{School}\r\n" +
                                                      $"วันที่ {Day} เดือน {Month} พ.ศ. {Year}\r\n",
                          THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++)+10, 400f, 200, false);

                Class.Print.SetPrintMedtods.CenterLeft(e, "ถึงคณะกรรมการดำเนืนการกิจกรรมสหกรณ์ครูวิทยาลัยเทคโนโลยีอีอีซี เอ็นจิเนีย เเหลมฉบัง", THsarabun18, BrushBlack, X + XD, Y + (SpacePerRow * CurrentRows++), XP, XD);


                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{Name} {IdCardNum}\r\n" +
                                           $"{HouseNum} {Village}\r\n" +
                                           $"{SubDistrict} {District}\r\n" +
                                           $"{Province} {TelNo}\r\n", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);


                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ได้ทราบข้อบังคับของกิจกรรมสหกรณ์ครูวิทยาลัยเทคโนโลยีอีอีซี เอ็นจิเนีย เเหลมฉบัง ขอสมัครเป็นสมาชิกของสหกรณ์ครู  เเละขอให้คำเป็นหลักฐานดังต่อไปนี้", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows), 750, 200, false);

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
                                            "       (..............................................................)",FonT(18,ThaiSarabun,FontStyle.Regular),BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 50, 400, 700);

                Class.Print.SetPrintMedtods.CenterRight(e, "ผู้สมัคร",FonT(18, ThaiSarabun, FontStyle.Regular), BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 50, XP, XD+230);
            }

        }
        // เเบบ ปริ้น หน้า กู้
        public static void PrintLoan(System.Drawing.Printing.PrintPageEventArgs e, String SQLCode, String Day, String Month, String Year, String TeacherNo, String LoanNo,int Rowscount)
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
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode);
                //string IDBorrower = dt.Rows[0][0].ToString();
                string School = "วิทยาลัยเทคโนโลยี อีอีซีเอ็นจิเนีย แหลมฉบัง";
                String Lender = "นางสาวภาตะวัน บูญจี๊ด";
                String Borrower = dt.Rows[0][1].ToString();
                String AmountLoan = dt.Rows[0][2].ToString();
                string Borroweraddress = dt.Rows[0][3].ToString();
                String PayMin = "700";
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
                    LimitMonthPay = BankTeacher.Bank.Menu.Month[PayNo].ToString() + " พ.ศ. " + (Yearpay + 543).ToString();
                    //LimitMonthPay = example.Bank.Menu.Monthname.ToString() + " พ.ศ. " + (Yearpay + 543).ToString();
                }
                //----------------------

                if (pageNow == 0)
                {
                    //------------------------


                    Class.Print.SetPrintMedtods.CenterRight(e, "สมาชิกเลขที่" + LoanNo, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "สัญญากู้ยืมเงินสวัสดิการพนักงาน", THsarabun30, BrushBlack);
                    Class.Print.SetPrintMedtods.CenterRight(e, "เขียนที่ " + School, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 10, XP, XD);
                    Class.Print.SetPrintMedtods.CenterRight(e, "วันที่ " + Day + " " + Month + " " + Year, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "       สัญญาฉบับนี้ทำขึ้นมาระหว่างผู้เเทน" + School +
                    " (" + Lender + ") อยู่ ณ เลขที่ 75/2 หมู่  10 ต.ทุ่งสุขลา อ. ศรีราชา จ. ชลบุรี ซึ่งต่อไปในสัญญานี้เรียกว่า '" + School + "' ฝ่ายหนึ่งกับ " + Borrower, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    Class.Print.SetPrintMedtods.CenterLeft(e, Borroweraddress, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    //  เงื่อนไข 1
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ซึ่งต่อไปในสัญญานี้เรียกว่า 'พนักงานครู / อาจารย์'อีกฝ่าย \r\n" +
                        "   ทั้งสองฝ่ายตกลงทำสัญญาดังมีข้อความดังต่อไปนี้ \r\n" +
                        "   ข้อ 1. พนักงานได้กู้ยืมเงินจากสหกรณ์ครู" + School + " ไปเป็นจำนวน \r\n" +
                        "" + AmountLoan + " บาท ( " + NumToBath(AmountLoan) + " ) เเละพนักงานครูได้รับเงินกู้ \r\n" +
                        "จำนวนดังกล่าวจาก" + School + "ไปถูกต้องครบถ้วนเเล้วในขณะทำสัญญากู้ยืมเงินฉบังนี้", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    // เงื่อนไข 2
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "   ข้อ 2. หนักงานครู จะชำระหนี้เงินกู้ยืมตาม ข้อ 1. ของสัญญานี้คืนให้เเก่วิทยาลัยสหกรณ์ครู \r\n" +
                        "เทคโนโลยีเเหบมฉบัง โดยผ่อนชำระเป็นรายเดือนในจำนวนไม่ต่ำกว่าเดือนละ " + PayMin + " บาท\r\n" +
                        "( " + NumToBath(PayMin) + " )ในวันที่พนักงานครูรับเงินค่าจ้างจาก" + School + "\r\n" +
                        "เเละส่งให้สหกรณ์ก่อนวันที่ 3 ของเดือน ติดต่อกันจนกว่าจะชำระหนี้เงินกู้ยืมครบถ้วน เเละจะต้องชำระคืน\r\n" +
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
                    int round = 0;
                    for (int around = 0; around < Rowscount; around++)
                    {
                        if(around == 0)
                        {
                            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, NameLoan, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 350f, 70, false);
                        }
                        else
                        {
                            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e,$"ลงชื่อ ................................................ ผู้ค้ำประกัน {round+1}\r\n" +
                                            "      ( " + dt.Rows[round++][1].ToString() + " )", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 350f, 70, false);
                        }
                    }
                    Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "ลงชื่อรับเงิน ................................................................................................ วันที่ " + Day + " " + Month + " " + Year, THsarabun18, BrushBlack);


                }

                pageNow++;
                if (pageNow == 2)
                {
                    e.HasMorePages = false;
                    pageNow = 0;
                }
            }


        }
        public static int Centerset(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float TextX, float TextY, float Rectangle_width, float Rectangle_height, float Size_X, float lengthY)
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
        // SUM จำนวนเลขท้ายสุด เพื่อเเสดงผลยอดรวม
        public static List<float> SUM = new List<float>();
        // เเบบ ปริ้น รายงาน สำหรับ ที่มีปุ่มปริ้น
        public static void PrintReportGrid(System.Drawing.Printing.PrintPageEventArgs e, DataGridView G, string header, string TextForm,int Aroundscript)
        {
            linesToPrint = e.PageBounds.Height-50;
            // ปากกา//
            Pen PenBlack = new Pen(Color.Black);
            PenBlack.Width = 1;
            Pen PenRed = new Pen(Color.Red);
            PenRed.Width = 1;
            Pen PenGreen = new Pen(Color.Green);
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
            if (onetimestartColumns == 0)
            {
                X = 50;  Y = 50 + imageY + lower;
            }
            else
            {
                X = 50; Y = 50;
            }
            //================= Position =====================
            // เส้น //
            float x1 = X, x2 = e.PageBounds.Width - 50; // จุดเริ่มการปริ้น
            float y1 = Y, y2 = Y; // สิ้นสุดการปริ้น
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
            // ขนาดการตัด
            int setcut = 21;
            //================= Cut =====================
            // ตัววัดขนาด ข้อความที่ได้รับมา สำหรับเรียกใช้เเล้วทิ้งเพราะงั้นเรียกใช้ได้เบย
            SizeF Size = e.Graphics.MeasureString("", THsarabun16);
            SizeF Size1 = e.Graphics.MeasureString("", THsarabun16);
            //================= Sizetext =====================
            // ยังไม่เเน่ใจว่าจะใช้มั้ย
            //int p = position, c = Currentposition;
            if (G.ColumnCount != 0)
            {
                // เปิดการวาด Columns รอบเดียว
                if (onetimestartColumns == 0)
                {
                    // รูปภาพ
                    System.Drawing.Image img = global::BankTeacher.Properties.Resources._64x64_TLC;
                    // วาดภาw (โลโก้)
                    e.Graphics.DrawImage(img, 50, 50, imageX, imageY);
                    // ข้อความทั้งหมดที่ใช้พิมพ์
                    string TLC = "วิทยาลัยเทคโนโลยีอีอีซี เอ็นจิเนีย เเหลมฉบัง", tlc = "EEC ENGINEERING TECHNOLOGICAL COLLEGE";
                    string address = "75/2 หมู่ที่ : 10 ถนน : สุขาภิบาล ตำบล : ทุ่งสุขลา อำเภอ : ศรีราชา จังหวัด : ชลบุรี 20230 โทร : 088-888-888 WWW.EEC.AC";
                    Size = e.Graphics.MeasureString($"วันที่ออกใบ {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}", FonT(16, ThaiSarabun, FontStyle.Bold));
                    e.Graphics.DrawString($"วันที่ออกใบ {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}", FonT(16, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - Size.Width, 50);
                    //TextY += Size.Height;
                    // เเบบพิมพ์ชื่อ วิทยาลัยเทคโนโลยีเเหลมฉบัง Thai
                    TextX += imageX;
                    e.Graphics.DrawString(TLC, FonT(16, ThaiSarabun, FontStyle.Bold), BrushBlack, new RectangleF(TextX + 10, TextY + 10, 600, 100));
                    Size = e.Graphics.MeasureString(TLC, FonT(16, ThaiSarabun, FontStyle.Bold));
                    TextY += Size.Height / 2;
                    // เเบพิมพ์ชื่อ วิทยาลัยเทคโนโลยีเเหลมฉบัง English
                    e.Graphics.DrawString(tlc, FonT(14, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(TextX + 10, TextY + 10, 700, 100));
                    Size = e.Graphics.MeasureString(tlc, FonT(14, ThaiSarabun, FontStyle.Regular));
                    TextY += Size.Height/2;
                    // สถานที่ตั้ง
                    e.Graphics.DrawString(address, FonT(12, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(TextX + 10, TextY + 10, x2/2, 100));
                    Size = e.Graphics.MeasureString(address, FonT(12, ThaiSarabun, FontStyle.Regular));
                    TextY += Size.Height/2;
                   
                 
               
                    if (header != "")
                    // หัวข้อรายการ
                    {
                        Size = e.Graphics.MeasureString(header, FonT(18, ThaiSarabun, FontStyle.Bold));
                        e.Graphics.DrawString(header, FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, new RectangleF(((x2 + 50) / 2) - Size.Width / 2, TextY += 50, 500, 100));
                        TextY += Size.Height;
                    }
                    // Check Form for Print ตวรจสอบข้อความที่จะปริ้นในหน้านั้นๆ
                    float result = 0;
                    if (TextForm == "InfoLoan")
                    {
                        string infomeber = $"ชื่อ-นามสกุล : {Bank.Loan.InfoLoan.info_name}            รหัสประจำตัว : {Bank.Loan.InfoLoan.info_id}            เลขที่สัญญากู้ : {Bank.Loan.InfoLoan.info_Loanid}\r\n" +
                                           $"ยอดเงินค้ำ : {Bank.Loan.InfoLoan.Amount[0]} บาท                      เปอร์เซ็นต์ค้ำ : {Bank.Loan.InfoLoan.Percent[0]}%\r\n" +
                                           $"ยอดที่กู้ : {Bank.Loan.InfoLoan.info_Sum} บาท                   ยอดคงเหลือ : {Bank.Loan.InfoLoan.info_totelLoan} บาท                   ยอดที่ชำระ : {Bank.Loan.InfoLoan.info_Loanpayall}";
                        // กรอบ อร่อยต้อง Rectangle
                        Size = e.Graphics.MeasureString(infomeber, FonT(18, ThaiSarabun, FontStyle.Regular));
                        // กรอบ
                        e.Graphics.DrawRectangle(PenBlack, 50, TextY, x2 - 50, Size.Height - 20 + 5);

                        Size = e.Graphics.MeasureString("infomeber", FonT(16, ThaiSarabun, FontStyle.Regular));
                        result = Centerset(e, infomeber, FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, x2, Size.Height + 5 );
                        TextY += (Size.Height * result);

                        // เว้นระยะตัวหนังสือกัวกรอบ
                        TextY += 10;

                        if (Bank.Loan.InfoLoan.how_many_laps >= 1)
                        {
                            e.Graphics.DrawString($"ผู้ค้ำ {Bank.Loan.InfoLoan.how_many_laps} คน", FonT(18, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY);
                            Size = e.Graphics.MeasureString("ผู้ค้ำ", FonT(18, ThaiSarabun, FontStyle.Bold));
                            TextY += Size.Height;
                        }

                        float start = TextY, end = 0;
                       
                        for (int Grt = 0; Grt < Bank.Loan.InfoLoan.how_many_laps; Grt++)
                        {
                            string infoGuarantor = $"ชื่อ-นามสกุล : {Bank.Loan.InfoLoan.info_GuarantrN[Grt]}            ยอดค้ำ : {Bank.Loan.InfoLoan.info_GuarantRemains[Grt]}\r\n" +
                                                   $"ยอดเงินค้ำ : {Bank.Loan.InfoLoan.info_GuarantrAmount[Grt]} บาท                      เปอร์เซ็นต์ค้ำ : {Bank.Loan.InfoLoan.info_GuarantrPercent[Grt]}%";
                            Size = e.Graphics.MeasureString(infoGuarantor, FonT(16, ThaiSarabun, FontStyle.Regular));
                            Size1 = e.Graphics.MeasureString("infoGuarantor", FonT(16, ThaiSarabun, FontStyle.Regular));
                            result = Centerset(e, infoGuarantor, FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY+5, 700, 300, x2, Size1.Height);
                            if(Grt > 0)
                            {
                                e.Graphics.DrawLine(PenBlack, 50, TextY, x2, TextY);
                            }
                            TextY += (Size1.Height * result);
                            end += Size.Height;
                            if(Grt == Bank.Loan.InfoLoan.how_many_laps-1)
                            {
                                // เว้นระยะตัวหนังสือกัวกรอบ
                                TextY += 10;
                            } 
                        }
                        if (Bank.Loan.InfoLoan.how_many_laps > 0)
                        {
                            // กรอบ
                            e.Graphics.DrawRectangle(PenBlack, 50, start, x2 - 50, end + 5);
                        }


                        // กล่อง
                        float Box_SizeX = 200;
                        float Box_SizeY = 30;
                        float location_Box = 80;
                        Size = e.Graphics.MeasureString(Bank.Loan.InfoLoan.info_startdate, FonT(18, ThaiSarabun, FontStyle.Bold));
                        Size1 = e.Graphics.MeasureString("เริ่ม", FonT(16, ThaiSarabun, FontStyle.Regular));
                        e.Graphics.DrawString("เริ่ม", FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, (x2 - Box_SizeX) - Size1.Width, location_Box + 5);
                        // ข้อความ
                        e.Graphics.DrawString(Bank.Loan.InfoLoan.info_startdate, FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - (Box_SizeX / 2) - Size.Width / 2, location_Box);
                        // กล่อง
                        e.Graphics.DrawRectangle(PenBlack, x2 - Box_SizeX, location_Box, Box_SizeX, Box_SizeY);
                        location_Box += Box_SizeY;

                        Size = e.Graphics.MeasureString(Bank.Loan.InfoLoan.info_duedate, FonT(18, ThaiSarabun, FontStyle.Bold));
                        Size1 = e.Graphics.MeasureString("สิ้นสุด", FonT(16, ThaiSarabun, FontStyle.Regular));
                        e.Graphics.DrawString("สิ้นสุด", FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, (x2 - Box_SizeX) - Size1.Width, location_Box + 5);
                        // ข้อความ
                        e.Graphics.DrawString(Bank.Loan.InfoLoan.info_duedate, FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - (Box_SizeX / 2) - Size.Width / 2, location_Box);
                        // กล่อง
                        e.Graphics.DrawRectangle(PenBlack, x2 - Box_SizeX, location_Box, Box_SizeX, Box_SizeY);
                    }
                    else if (TextForm == "pay")
                    {
                        string Remain;
                        if (Bank.Pay.pay.info_Lona_AmountRemain != "")
                        {
                             Remain = $"ยอดกู้คงเหลือ : {Bank.Pay.pay.info_Lona_AmountRemain}";
                        }
                        else
                        {
                             Remain = "";
                        }
                        string infopay = $"ชื่อ-นามสกุล : {Bank.Pay.pay.info_name}            รหัสประจำตัว : {Bank.Pay.pay.info_id}           \r\n" +
                                         $"หุ้นสะสมทั้งหมด : {Bank.Pay.pay.info_totelAmountpay}            {Remain}        ";
                        // กรอบ อร่อยต้อง Rectangle
                        Size = e.Graphics.MeasureString(infopay, FonT(18, ThaiSarabun, FontStyle.Regular));
                        //// กรอบ
                        //e.Graphics.DrawRectangle(PenBlack, 50, TextY, x2 - 50, Size.Height - 20 + 5);

                        Size = e.Graphics.MeasureString("infopay", FonT(16, ThaiSarabun, FontStyle.Regular));
                        result = Centerset(e, infopay, FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, 50, TextY, 700, 300, x2, Size.Height + 5);
                        TextY += (Size.Height * result);

                        //// เว้นระยะตัวหนังสือกัวกรอบ
                        //TextY += 10;
                        float Box_SizeX = 200;
                        float Box_SizeY = 30;
                        float location_Box = 80;
                        Size = e.Graphics.MeasureString(Bank.Pay.pay.info_Billpay, FonT(18, ThaiSarabun, FontStyle.Bold));
                        Size1 = e.Graphics.MeasureString("เลขบิลล์", FonT(16, ThaiSarabun, FontStyle.Regular));
                        e.Graphics.DrawString("เลขบิลล์", FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, (x2 - Box_SizeX) - Size1.Width, location_Box + 5);
                        // ข้อความ
                        e.Graphics.DrawString(Bank.Pay.pay.info_Billpay, FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - (Box_SizeX / 2) - Size.Width / 2, location_Box);
                        // กล่อง
                        e.Graphics.DrawRectangle(PenBlack, x2 - Box_SizeX, location_Box, Box_SizeX, Box_SizeY);

                        location_Box += Box_SizeY;
                        Size = e.Graphics.MeasureString(Bank.Pay.pay.info_datepay, FonT(18, ThaiSarabun, FontStyle.Bold));
                        Size1 = e.Graphics.MeasureString("จ่ายวันที่", FonT(16, ThaiSarabun, FontStyle.Regular));
                        e.Graphics.DrawString("จ่ายวันที่", FonT(16, ThaiSarabun, FontStyle.Regular), BrushBlack, (x2 - Box_SizeX) - Size1.Width, location_Box + 5);
                        // ข้อความ
                        e.Graphics.DrawString(Bank.Pay.pay.info_datepay, FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - (Box_SizeX / 2) - Size.Width / 2, location_Box);
                        // กล่อง
                        e.Graphics.DrawRectangle(PenBlack, x2 - Box_SizeX, location_Box, Box_SizeX, Box_SizeY);
                    }
                    else if (TextForm == "")
                    {

                    }
                    // ปริ้นข้อความต้นฉบับ
                    if (Aroundscript == 1)
                    {
                        // ต้นฉบับ
                        Size = e.Graphics.MeasureString("ต้นฉบับ", FonT(30, ThaiSarabun, FontStyle.Bold));
                        // พื้นหลัง
                        e.Graphics.FillRectangle(Brushes.White, x2 - Size.Width - 250, 50, Size.Width, Size.Height);
                        // ข้อความ
                        e.Graphics.DrawString("ต้นฉบับ", FonT(30, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - Size.Width - 250, 50);
                        // กล่อง
                        e.Graphics.DrawRectangle(PenBlack, x2 - Size.Width - 250, 50, Size.Width, Size.Height);

                    }
                    // บวกขนาดตารางเเละเส้น เพื่อทราบตำเเหน่ง ได้เเก่   startTableY ตาราง / y1&y2 เส้น
                    float IandT = 0;
                    if (imageY > TextY)
                        IandT = imageY / 4;
                    else
                        IandT = TextY;
                    startTableY = IandT;
                    y1 = IandT;
                    y2 = IandT;
                    for (int Columns = 0; Columns < G.ColumnCount; Columns++)
                    {
                        if (Columns != 0)
                        {
                            // โรงงานบอกระยะของการวาด เก็บค่า Wihte ไว้
                            Getwidth += Class.Print.SetPrintMedtods.Chcekspan(e, G, startTableX, startTableY, G.Columns[Columns - 1].HeaderText
                                    , setcut, Columns, FonT(18, ThaiSarabun, FontStyle.Regular), 0, 0, out position, out Currentposition);
                            cutline.Add(Getwidth);
                        }
                        else
                        {
                            Getwidth = 50;
                        }
                        // โรงงานบอกขนาด ของ สี่เหลี่ยม ได้เเก่ พื้นที่/ความกว้าง
                        Class.Print.SetPrintMedtods.CutingCharAndString
                        (e, G.Columns[Columns].HeaderText, setcut, 50, startTableY, out Rectangle_X, out Rectangle_Y, 0);

                        Size = e.Graphics.MeasureString(G.Columns[Columns].HeaderText, FonT(18, ThaiSarabun, FontStyle.Regular));
                        // ระยะการวาดในรอบถัดไป
                        center = (Rectangle_X / 2 + Getwidth - Rectangle_X / 2);
                        float CT = center;
                        if (Columns == G.ColumnCount - 1)
                        {
                            SizeF f = e.Graphics.MeasureString(G.Rows[0].Cells[G.Rows[0].Cells.Count-1].Value.ToString(), FonT(18, ThaiSarabun, FontStyle.Regular));
                            center = (x2 - f.Width);
                        }
                        Center.Add(center);

                        if (Columns == G.ColumnCount - 1)
                        {
                            center = ((x2 - CT) / 2 + CT - (Size.Width / 2));
                            //e.Graphics.DrawLine(PenRed, center, 50, center, 200); ตัวTest เส้นกลาง
                        }
                        //วาดข้อความ Columns
                        e.Graphics.DrawString(G.Columns[Columns].HeaderText, FonT(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(center, startTableY, Rectangle_X - 50, Rectangle_Y - 50));

                        //เส้นปิด
                        ColseLine.Add(Rectangle_Y);
                        //LineEndC.Add(Y + Sizetext.Width);
                        if (Columns == G.ColumnCount - 1)
                        {
                            startTableY = Class.Print.SetPrintMedtods.MaxValues(startTableY, ColseLine);
                            e.Graphics.DrawLine(PenBlack, x1, startTableY, x2, startTableY);
                            ColseLine.Clear();
                        }
                    }
                }
                // เส้นเปิด
                e.Graphics.DrawLine(PenBlack, x1, y1, x2, y2);
                // ปิด Columns
                onetimestartColumns++;
            }
            if (G.RowCount != 0)
            {
                for (int Rows = 0; Rows < G.RowCount; Rows++)
                {
                    if (Rows >= Currentposition && startTableY < linesToPrint)
                    {
                        position++;
                        for (int Cells = 0; Cells < G.Rows[Rows].Cells.Count; Cells++)
                        {
                            if (Cells == G.Rows[Rows].Cells.Count - 1)
                            {
                                for(int returcells = G.Rows[Rows].Cells.Count-1; returcells > 0; returcells--)
                                {
                                    try
                                    {
                                        //float Checkstring = Convert.ToSingle(G.Rows[Rows].Cells[returcells].Value);
                                        SUM.Add(Convert.ToInt32(G.Rows[Rows].Cells[returcells].Value));
                                        break;
                                    }
                                    catch
                                    {
                                        //if (Checkstring == typeof(float))
                                        //SUM.Add(Convert.ToInt32(G.Rows[Rows].Cells[Cells].Value.ToString()));
                                        //if(G.Rows[Rows].Cells[returcells].Value == )
                                    }
                                }
                            }
                            // เรียกใช้ โรงงาน การตัด  เเละ การวัด ขนาดสี่เหลี่ยมพื้นผ้า
                            Class.Print.SetPrintMedtods.CutingCharAndString
                            (e, G.Rows[Rows].Cells[Cells].Value.ToString(), setcut, 50, startTableY, out Rectangle_X, out Rectangle_Y, 0);
                            // ???????????
                            //Size = e.Graphics.MeasureString(G.Rows[Rows].Cells[Cells].Value.ToString(), FonT(18, ThaiSarabun, FontStyle.Regular));
                            // วาดRows
                            e.Graphics.DrawString(G.Rows[Rows].Cells[Cells].Value.ToString(), FonT(18, ThaiSarabun, FontStyle.Regular), BrushBlack, new RectangleF(Center[Cells], startTableY, Rectangle_X - 50, Rectangle_Y - 50));
                            // เช็คว่าขนาดข้อความใหญ่เกินกำหนดหรือไม่ เพื่อความปลอดภัย ในการทับเส้น
                            if (G.Rows[Rows].Cells[Cells].Value.ToString().Length >= setcut + 5)
                            {
                                Rectangle_Y = Rectangle_Y + 36.40136f;
                            }
                            // เก็บขนาดที่ได้รับมาไปหาค่ามากที่สุด
                            ColseLine.Add(Rectangle_Y);
                            // หาขนาดที่มากที่สุด
                            if (Cells == G.Rows[Rows].Cells.Count - 1)
                            {
                                startTableY = Class.Print.SetPrintMedtods.MaxValues(startTableY, ColseLine);
                                e.Graphics.DrawLine(PenBlack, x1, startTableY, x2, startTableY);
                                ColseLine.Clear();
                            }
                        }
                    }
                }
            }
           
            for (int l = 0; l < cutline.Count; l++)
            {
                e.Graphics.DrawLine(PenBlack, cutline[l], y1, cutline[l], startTableY);
            }
            // เส้นปิดข้าง
            e.Graphics.DrawLine(PenBlack, x1, y1, x1, startTableY);
            e.Graphics.DrawLine(PenBlack, x2, y1, x2, startTableY);
            // เส้นปิด 
            e.Graphics.DrawLine(PenBlack, x1, startTableY, x2, startTableY);

            if (startTableY < linesToPrint - 100)
            {
                // วาด ตารางรวม
                if (TextForm != "Home")
                {
                    Class.Print.SetPrintMedtods.Tabletotal(e, PenBlack, SUM, BrushBlack, 18, X, startTableY, x2);
                }
                    

                if(TextForm == "pay")
                {
                    SizeF TextSize = e.Graphics.MeasureString("", THsarabun16);
                    SizeF TextSize1 = e.Graphics.MeasureString("", THsarabun16);

                    e.Graphics.DrawString("หมายเหตุ", FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, 50, startTableY + 80);
                    TextSize = e.Graphics.MeasureString("หมายเหตุ", FonT(18, ThaiSarabun, FontStyle.Regular));

                    e.Graphics.DrawString("ใบเสร็จรับเงินฉบับนี้จะสมบูรณ์เมื่อผู้รับเงินลงลายมือชื่อเป็นอันเสร็จสิ้น \r\n" +
                                        "ชำระเเล้วไม่สามารถรับคืนหรือเปลี่ยนตัวไม่ว่ากรณีใดๆ", FonT(12, ThaiSarabun, FontStyle.Bold), BrushBlack, 50 + TextSize.Width, startTableY + 80);
                    TextSize = e.Graphics.MeasureString("_____________________________", FonT(13, ThaiSarabun, FontStyle.Regular));
                    e.Graphics.DrawString("_____________________________", FonT(13, ThaiSarabun, FontStyle.Regular), BrushBlack, x2 - TextSize.Width, startTableY + 100);
                    TextSize1 = e.Graphics.MeasureString("ลงนาม", FonT(18, ThaiSarabun, FontStyle.Bold));
                    e.Graphics.DrawString("ลงนาม", FonT(18, ThaiSarabun, FontStyle.Bold), BrushBlack, x2 - (TextSize.Width + TextSize1.Width), startTableY + 90);
                    // คนทำรายการ
                    TextSize1 = e.Graphics.MeasureString("ผู้ทำรายการ", FonT(18, ThaiSarabun, FontStyle.Regular));
                    e.Graphics.DrawString("ผู้ทำรายการ", FonT(18, ThaiSarabun, FontStyle.Regular), BrushBlack, (x2 - TextSize1.Width) - ((TextSize.Width - TextSize1.Width) / 2), startTableY + 120);
                }
            }
            else
            {
                startTableY = 2000;
                Currentposition += - 1;
            }


            Currentposition += position;
           
            if (startTableY <= linesToPrint || Currentposition >= G.RowCount)
            {
                SUM.Clear();
                cutline.Clear();
                e.HasMorePages = false;
                Center.Clear();
                onetimestartColumns = 0;
                Currentposition = 0;
            }
            else
            {
                e.HasMorePages = true;
            }


        }

        // เลือก Font ตามใจฉัน
        public static Font FonT(float SizeFont, String Font, FontStyle Fontstyle)
        {
            Font F = new Font(Font, SizeFont, Fontstyle);
            return F;
        }

        // เปลี่ยนค่าเลขเป็น string by Mon 
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
                while (printedLines < linesToPrint)
                {
                    graphic.DrawString("Line: " + printedLines, font, brush, startX, startY + offsetY + (SpacePerRow * CurrentRows++));
                    //offsetY += (int)fontHeight;

                    ++printedLines;
                }
            }
            else
            {
                while (printedLines < linesToPrint)
                {
                    graphic.DrawString("Line: " + printedLines, font, brush, startX, startY + offsetY + (SpacePerRow * CurrentRows++));
                    //offsetY += (int)fontHeight;

                    ++printedLines;
                }
            }



            linesToPrint = linesToPrint * 2;
            pageNow++;

            if (pageNow == 2)
            {
                //graphic.DrawString("Line:OOP", font, brush, startX, startY + offsetY);
                e.HasMorePages = false;
                pageNow = 0;
                linesToPrint = 50;
                printedLines = 0;
            }
        }

    }
}
