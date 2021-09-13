using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example.Class.Print
{
    class PrintPreviewDialog
    {
        public static Font THsarabun30 = new Font("TH Sarabun New", 30, FontStyle.Bold); 
        public static Font THsarabun18 = new Font("TH Sarabun New", 18, FontStyle.Bold);
        public static Brush BrushBlack = Brushes.Black;
        static int pageNow = 0;
        static string Enter = "\r\n";
        public static void PrintMember(System.Drawing.Printing.PrintPageEventArgs e)
        {
            // X 850 = 22 cm เเนะนำ 800 //
            // A4 = 21 cm  {Width = 356.70163 Height = 136.230438} {Width = 356.70163 Height = 102.954086} // 
            int PageX = (e.PageBounds.Width);
            int PageY = (e.PageBounds.Height);
            int XP = 0;
            int XD = 0;
            int X = 50;
            int Y = 50;
            int SpacePerRow = 35;
            int CurrentRows = 0;


          


            //----------------------

            string D, M, y, m;
            D = "2/9/2564"; y = D; M = y;
            D = D.Remove(2, D.Length - 2); y = y.Remove(0, y.Length - 4); M = M.Remove(M.Length - 4, 4);
            m = M; m = m.Remove(0, 2);
            //------------------------

            // ส่วนหัว

            Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "ใบสมัครสมาชิกสหกรณ์ครู", THsarabun30, BrushBlack);
            Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "วิทยาลัยเทคโนโลยี อีอีซีเอ็นจิเนีย แหลมฉบัง", THsarabun30, BrushBlack);
            // วันที่
            string MemberID = "สมาชิกเลขที่ " /*+ dt.Rows[0][0].ToString()*/;
            string School = "เขียนที่ วิทยาลัยเทคโนโลยี อีอีซีเอ็นจิเนีย แหลมฉบัง";
            string Enter = "\r\n";

            // ข้อมูลส่วนตัว
            string Name = "ข้าพเจ้า " /*+ dt.Rows[0][1].ToString()*/;
            string IdCardNum = "เลขประจำตัวประชาชน "/* + dt.Rows[0][2].ToString()*/;
            string HouseNum = "อยู่บ้านเลขที่ "/*+ dt.Rows[0][3].ToString()*/;
            string Village = "หมู่ " /*+ dt.Rows[0][4].ToString()*/;
            string SubDistrict = "ตำบล " /*+ dt.Rows[0][5].ToString()*/;
            string District = "อำเภอ " /*+ dt.Rows[0][6].ToString()*/;
            string Province = "จังหวัด " /*+ dt.Rows[0][7].ToString()*/;
            string TelNo = "เบอร์โทร " /*+ dt.Rows[0][8].ToString()*/;

            //รายละเอียด
            string amountbauy = "ข้อ 2 ข้าพเจ้าขอถือหุ้นของกิจกรรมสหกรณ์ครู ซึ่งมีค่าหุ้นล่ะ 500 บาท";
            string buy = $"2.1 ข้อซื้อหุ้นจำนวน 500 หุ้น";
            string share = "2.2 รับโอนหุ้นจาก -";
            string Who = "สมาชิกเลขที่ -";
            string quantity = "จำนวน - หุ้น (ถ้ามี)";
            string price = "เเละชำระค่าหุ้น - บาท ทันทีที่ได้รับเเจ้งให้เข้าเป็นสมาชิก";

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{MemberID}{Enter}" +
                                                  $"{School}{Enter}" +
                                                  $"วันที่ {D} เดือน {m} พ.ศ. {y}{Enter}",
                      THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 400f, 200, false);

            Class.Print.SetPrintMedtods.CenterLeft(e, "ถึงคณะกรรมการดำเนืนการกิจกรรมสหกรณ์ครูวิทยาลัยเทคโนโลยีอีอีซี เอ็นจิเนีย เเหลมฉบัง", THsarabun18, BrushBlack, X + XD, Y + (SpacePerRow * CurrentRows++), XP, XD);


            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{Name} {IdCardNum}{Enter}" +
                                       $"{HouseNum} {Village}{Enter}" +
                                       $"{SubDistrict} {District}{Enter}" +
                                       $"{Province} {TelNo}{Enter}", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);


            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ได้ทราบข้อบังคับของกิจกรรมสหกรณ์ครูวิทยาลัยเทคโนโลยีอีอีซี เอ็นจิเนีย เเหลมฉบัง ขอสมัครเป็นสมาชิกของสหกรณ์ครู  เเละขอให้คำเป็นหลักฐานดังต่อไปนี้", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows), 750, 200, false);

            string status = "ข้อที่ 1 ข้าพเจ้าเป็นผู้มีคุณสมบัติถูกต้องตามข้อบังคับทุกประการ";
            string teacher = "1.เป็นครู - อาจารย์";
            string officer = "2.เป็นเจ้าหน้าที่ - ภารโรง";

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{status}{Enter}" +
                                        $"  {teacher}{Enter}" +
                                        $"  {officer}{Enter}", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, $"{amountbauy}{Enter}" +
                                       $"  {buy}{Enter}" +
                                       $"  {share} {Who}{Enter}" +
                                       $"{quantity} {price}{Enter}", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 700, 200, false);

            CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ข้อที่ 3 เมื่อ ข้าพเจ้าเป็นสมาชิกจะปฎิบัติตามข้อบังคับทุกประการ เเละจะพยายามส่งเสริมให้กิจกรรมสหกณ์ครูให้เจริญก้าวหน้ายิี่งขึ้นไป", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750, 700);
            //// ตกลง
            //CurrentRows += Centerset(e, "ลงชื่อ......................................................." +
            //                            "(..............................................................)", Normal01, Normal, X + 400, Y + (SpacePerRow * CurrentRows++) + 50, 400, 700);

            //CenterRight(e, "ผู้สมัคร", Normal01, Normal, X + 550, Y + (SpacePerRow * CurrentRows++) + 50, XP, XD);
        }

        public static void PrintLoan(System.Drawing.Printing.PrintPageEventArgs e)
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
            //----------------------

            if (pageNow == 0)
            {
                string D, M, y, m;
                D = "2/9/2564"; y = D; M = y;
                D = D.Remove(2, D.Length - 2); y = y.Remove(0, y.Length - 4); M = M.Remove(M.Length - 4, 4);
                m = M; m = m.Remove(0, 2);
                //------------------------
              
                // เลขที่
                string IDLoan = "สมาชิกเลขที่ 11/1";
                Class.Print.SetPrintMedtods.CenterRight(e, IDLoan, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);

                // ส่วนหัว
                string Hradder = "สัญญากู้ยืมเงินสวัสดิการพนักงาน";
                Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), Hradder, THsarabun30, BrushBlack);

                // ทำที่
                string School = "เขียนที่ วิทยาลัยเทคโนโลยี อีอีซีเอ็นจิเนีย แหลมฉบัง";
                Class.Print.SetPrintMedtods.CenterRight(e, School, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 10, XP, XD);

                // วันที่
                string Date = $"วันที่ {D}{M}{Y} ";
                Class.Print.SetPrintMedtods.CenterRight(e, Date, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);

                // ข้อมูลส่วนตัว
                string agreement = "สัญญาฉบับนี้ทำขึ้นมาระหว่างผู้เเทนวิทยาลัยเทคโนโลยีอีอีซีเอ็นจิเนียเเหลมฉบัง " +
                "(นางสาวภาตะวัน บูญจี๊ด) อยู๋ ณ เลขที่ 75/2 หมู่  10 ต.ทุ่งสุขลา อ. ศรีราชา จ. ชลบุรี ซึ่งต่อไปในสัญญานี้เรียกว่า 'วิทยาลัยเทคโนโลยีแหลมฉบัง'ฝ่ายหนึ่งกับ ปุ้น";


                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, agreement, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);

                // อยู่ ณ เลขที่
                string address = "115/2 ม.1 อ.ว่าไป จ.ว่าไป";
                Class.Print.SetPrintMedtods.CenterLeft(e, address, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);

                // เงื่อนไข 1
                string agreement1 = $"ซึ่งต่อไปในสัญญานี้เรียกว่า 'พนักงานครู / อาจารย์'อีกฝ่าย {Enter}" +
                    $"  ทั้งสองฝ่ายตกลงทำสัญญาดังมีข้อความดังต่อไปนี้ {Enter}" +
                    $"  ข้อ 1. พนักงานได้กู้ยืมเงินจากสหกรณ์ครูวิทยาลัยเทคโนโลยีเเหลมฉบัง ไปเป็นจำนวน {Enter}" +
                    $"500 บาท (500) เเละพนักงานครูได้รับเงินกู้ {Enter}" +
                    $"จำนวนดังกล่าวจากวิทยาลัยเทคโนโลยีเเหลมฉบังไปถูกต้องครบถ้วนเเล้สในขณะทำสัยญากู้ยืมเงินฉบังนี้";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, agreement1, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                // เงื่อนไข 2
                string agreement2 = $"  ข้อ 2. หนักงานครู จะชำระหนี้เงินกู้ยืมตาม ข้อ 1. ของสัญญานี้คืนให้เเก่วิทยาลัยสหกรณ์ครู {Enter}" +
                    $"เทคโนโลยีเเหบมฉบัง โดยผ่อนชำระเป็นรายเดือนในจำนวนไม่ต่ำกว่าเดือนละ 500 บาท{Enter}" +
                    $"(1000)ในวันที่พนักงานครูรับเงินค่าจ้างจากวิทยาลัยเทคโนโลยีอีอีซีเอ็นจิเนียเเหลมฉบัง{Enter}" +
                    $"เเละส่งให้สหกรณ์ก่อนวันที่ 3 ของเดือน ติดต่อกันจนกว่าจะชำระหนี้เงินกู้ยืมครบถ้วน เเละจะต้องชำระคืน{Enter}" +
                    $"ให้เสร็จสิ้นภายในเดือน ส.ค พ.ศ";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, agreement2, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                // เงื่อนไข 3
                string agreemant3 = $"  ข้อ 3. พนักงานยืนยอมให้วิทยาลัยเทคโนโลยีอีอีซีเอ็นจืเเหลมฉบัง หักเงินค่าตอบเเทนที่หนักงานครูมีสิทธิได้รับ" +
                    $"อันได้เเก่ ค่าจ้าง ค่าล่วงเวลา ค่าทำงานในวันหยุด เเละค่าจ้างล่วงเวลาในวันหยุด เพื่อเป็นการใช้คืนเงินกู้ตาม" +
                    $"สัญญานี้ หากจำนวนเงินที่วิทยาลัยเทคโนโลยีอีอีซีเอ็นจิเนียเเหลมฉบังหักไว้จากค่าตอบเเทนดังกล่าวในวรรคเเรก มีจำนวน" +
                    $"เกินกว่าหนึ่งในห้าของเงินค่าตอบเเทนที่พนักงานครูมีสิทธิได้รับไม่ว่าจะเป็นการหักเพื่อเหตุใดเหตุหนึ่ง หรือ" +
                    $"หลายเหตุรวมกันก็ตาม พนักงานครูยืนยอมให้วิทยาลัยเทคโนโลยีอีอีซีเอ็นจีเนียเเหลมฉบัง สามารถหักเงินได้ตามจำนวน" +
                    $"ดังกล่าวนจนครบถ้วน หากผู้กู้ยืมไม่ชำระเงินตามกำหนด จะต้องชำระอัตราดอกเบี้ยเพิ่มอีก 1 เท่า เเละผู้ค้ำ{Enter}" +
                    $"ประกันจะต้องรับผิดชอบชำระเเทนผู้กู้ยืมโดยไม่มีข้อทักท้วงใดๆ";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, agreemant3, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 300, false);
                // เงื่อนไข 4
                string agreemant4 = $"  ข้อที่ 4. หากพนักงานครูพ้นสภาพจากการเป็นลูกจ้างวิทยาลัยเทคโนโลยีอีอีซีเอ็นจิเเนียเหลมฉบังไม่ว่าจะด้วยสาเหตุ" +
                    $"ใดๆ ก็ตามพนักงานครูจะต้องชำระหนี้เงินกู้ในส่วนที่ยังค้างชำระอยู่ทั้งหมดคืนให้กับวิทยาลัยเทคโนโลยีอีอีซีเอ็นจิเนียเเเหลมฉบังในทันที";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, agreemant4, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 20, 750f, 300, false);
            }
            else
            {
                string Page2agreemant = $"สัญญานี้ทำขึ้น 2 ฉบัง มีข้อความตรงกัน คู่สัญญาทั้งสองฝ่ายได้อ่าน เเละเข้าใจข้อความในสัญญา{Enter}" +
                    $"ฉบับนี้โดยตลอดเเล้ว เห็นว่าถูกต้องเเละตรงตามความประสงค์เเล้ว จึงได้ลงลายมือชื่อไว้เป็นหลักฐานต่อหน้า" +
                    $"พยาน เเละเก็บสัญญาไว้ฝ่ายละฉบับ";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, Page2agreemant, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 300, false);

                string NameEmployer = $"ลงชื่อ นางสาวภาตะวัน บุญจี๊ด นายจ้าง{Enter}" +
                                      $"    (นางสาวภาตะวัน บุญจี๊ด)";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, NameEmployer, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);
                
                string NameLoan = $"ลงชื่อ ข้าวปุ้น ผู้กู้ยืม{Enter}" +
                                  $"    (ข้าวปุ้น)";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e,NameLoan, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);

                string NameGuarantor1 = $"ลง ตังค์ ผู้ค้ำประกัน 1{Enter}" +
                                        $"      (ตังค์)";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e,NameGuarantor1, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);

                string Name2Guarantor2 = $"ลง ม่อน ผู้ค้ำประกัน 2{Enter}" +
                                         $"     (ม่อน)";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, Name2Guarantor2, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);

                string Name2Guarantor3 = $"ลง เจโรเเมน ผู้ค้ำประกัน 3{Enter}" +
                                         $"     (เจโรเเมน)";
                CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, Name2Guarantor3, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);

                string signUpName = $"ลงชื่อรับเงิน ปุ้น วันที่6/9/2564";
                Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), signUpName,THsarabun18,BrushBlack);


            }

            pageNow++;
            if (pageNow == 2)
            {
                e.HasMorePages = false;
                pageNow = 0;
            }

        }

        static int printedLines = 0;
        static int linesToPrint = 50;

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

            if(pageNow == 0)
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
          
            if(pageNow == 2)
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
