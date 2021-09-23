using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Class.Print
{
    class PrintPreviewDialog
    {
        public static Font THsarabun30 = new Font("TH Sarabun New", 30, FontStyle.Bold);
        public static Font THsarabun18 = new Font("TH Sarabun New", 18, FontStyle.Bold);
        public static Brush BrushBlack = Brushes.Black;
        static int pageNow = 0;

        public static void PrintMember(System.Drawing.Printing.PrintPageEventArgs e, String SQLCode, String Day, String Month, String Year, String TeacherNo, String Amount)
        {
            if (TeacherNo != "")
            {
                if (Amount == "")
                {
                    Amount = example.GOODS.Menu.startAmountMin.ToString();
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
                          THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 400f, 200, false);

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
                //// ตกลง
                //CurrentRows += Centerset(e, "ลงชื่อ......................................................." +
                //                            "(..............................................................)", Normal01, Normal, X + 400, Y + (SpacePerRow * CurrentRows++) + 50, 400, 700);

                //CenterRight(e, "ผู้สมัคร", Normal01, Normal, X + 550, Y + (SpacePerRow * CurrentRows++) + 50, XP, XD);
            }

        }

        public static void PrintLoan(System.Drawing.Printing.PrintPageEventArgs e, String SQLCode, String Day, String Month, String Year, String TeacherNo, String LoanNo)
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
                    LimitMonthPay = example.GOODS.Menu.Month[PayNo].ToString() + " พ.ศ. " + (Yearpay + 543).ToString();
                    //LimitMonthPay = example.GOODS.Menu.Monthname.ToString() + " พ.ศ. " + (Yearpay + 543).ToString();

                }
                //----------------------

                if (pageNow == 0)
                {
                    //------------------------


                    Class.Print.SetPrintMedtods.CenterRight(e, "สมาชิกเลขที่" + LoanNo, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    Class.Print.SetPrintMedtods.Center(e, Y + (SpacePerRow * CurrentRows++), "สัญญากู้ยืมเงินสวัสดิการพนักงาน", THsarabun30, BrushBlack);
                    Class.Print.SetPrintMedtods.CenterRight(e, "เขียนที่ " + School, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 10, XP, XD);
                    Class.Print.SetPrintMedtods.CenterRight(e, "วันที่ " + Day + " " + Month + " " + Year, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "สัญญาฉบับนี้ทำขึ้นมาระหว่างผู้เเทน" + School +
                    "(" + Lender + ") อยู่ ณ เลขที่ 75/2 หมู่  10 ต.ทุ่งสุขลา อ. ศรีราชา จ. ชลบุรี ซึ่งต่อไปในสัญญานี้เรียกว่า '" + School + "' ฝ่ายหนึ่งกับ " + Borrower, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    Class.Print.SetPrintMedtods.CenterLeft(e, Borroweraddress, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), XP, XD);
                    //  เงื่อนไข 1
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ซึ่งต่อไปในสัญญานี้เรียกว่า 'พนักงานครู / อาจารย์'อีกฝ่าย \r\n" +
                        "  ทั้งสองฝ่ายตกลงทำสัญญาดังมีข้อความดังต่อไปนี้ \r\n" +
                        "  ข้อ 1. พนักงานได้กู้ยืมเงินจากสหกรณ์ครู" + School + " ไปเป็นจำนวน \r\n" +
                        "" + AmountLoan + " บาท ( " + NumToBath(AmountLoan) + " ) เเละพนักงานครูได้รับเงินกู้ \r\n" +
                        "จำนวนดังกล่าวจาก" + School + "ไปถูกต้องครบถ้วนเเล้วในขณะทำสัยญากู้ยืมเงินฉบังนี้", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    // เงื่อนไข 2
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "  ข้อ 2. หนักงานครู จะชำระหนี้เงินกู้ยืมตาม ข้อ 1. ของสัญญานี้คืนให้เเก่วิทยาลัยสหกรณ์ครู \r\n" +
                        "เทคโนโลยีเเหบมฉบัง โดยผ่อนชำระเป็นรายเดือนในจำนวนไม่ต่ำกว่าเดือนละ " + PayMin + " บาท\r\n" +
                        "( " + NumToBath(PayMin) + " )ในวันที่พนักงานครูรับเงินค่าจ้างจาก" + School + "\r\n" +
                        "เเละส่งให้สหกรณ์ก่อนวันที่ 3 ของเดือน ติดต่อกันจนกว่าจะชำระหนี้เงินกู้ยืมครบถ้วน เเละจะต้องชำระคืน\r\n" +
                        "ให้เสร็จสิ้นภายในเดือน " + LimitMonthPay, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 200, false);
                    // เงื่อนไข 3
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "  ข้อ 3. พนักงานยืนยอมให้" + School + "หักเงินค่าตอบเเทนที่หนักงานครูมีสิทธิได้รับ" +
                        "อันได้เเก่ ค่าจ้าง ค่าล่วงเวลา ค่าทำงานในวันหยุด เเละค่าจ้างล่วงเวลาในวันหยุด เพื่อเป็นการใช้คืนเงินกู้ตาม" +
                        "สัญญานี้ หากจำนวนเงินที่" + School + "หักไว้จากค่าตอบเเทนดังกล่าวในวรรคเเรก มีจำนวน" +
                        "เกินกว่าหนึ่งในห้าของเงินค่าตอบเเทนที่พนักงานครูมีสิทธิได้รับไม่ว่าจะเป็นการหักเพื่อเหตุใดเหตุหนึ่ง หรือ" +
                        "หลายเหตุรวมกันก็ตาม พนักงานครูยืนยอมให้" + School + " สามารถหักเงินได้ตามจำนวน" +
                        "ดังกล่าวนจนครบถ้วน หากผู้กู้ยืมไม่ชำระเงินตามกำหนด จะต้องชำระอัตราดอกเบี้ยเพิ่มอีก 1 เท่า เเละผู้ค้ำ\r\n" +
                        "ประกันจะต้องรับผิดชอบชำระเเทนผู้กู้ยืมโดยไม่มีข้อทักท้วงใดๆ", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 300, false);
                    // เงื่อนไข 4
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "  ข้อที่ 4. หากพนักงานครูพ้นสภาพจากการเป็นลูกจ้าง" + School + "ไม่ว่าจะด้วยสาเหตุ" +
                        "ใดๆ ก็ตามพนักงานครูจะต้องชำระหนี้เงินกู้ในส่วนที่ยังค้างชำระอยู่ทั้งหมดคืนให้กับ" + School + "ในทันที", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++) + 20, 750f, 300, false);
                }
                else
                {
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "สัญญานี้ทำขึ้น 2 ฉบับ มีข้อความตรงกัน คู่สัญญาทั้งสองฝ่ายได้อ่าน เเละเข้าใจข้อความในสัญญา\r\n" +
                        "ฉบับนี้โดยตลอดเเล้ว เห็นว่าถูกต้องเเละตรงตามความประสงค์เเล้ว จึงได้ลงลายมือชื่อไว้เป็นหลักฐานต่อหน้า" +
                        "พยาน เเละเก็บสัญญาไว้ฝ่ายละฉบับ", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 750f, 300, false);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ลงชื่อ ................................................ นายจ้าง\r\n" +
                                          "    (" + Lender + ")", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);
                    string NameLoan = $"ลงชื่อ ................................................ ผู้กู้ยืม\r\n" +
                                      "    (" + Borrower + ")";
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, NameLoan, THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ลง ................................................ ผู้ค้ำประกัน 1\r\n" +
                                            "      ( " + dt.Rows[1][1].ToString() + " )", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ลง................................................ผู้ค้ำประกัน 2\r\n" +
                                             "     ( " + dt.Rows[2][1].ToString() + " )", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);
                    CurrentRows += Class.Print.SetPrintMedtods.Centerset(e, "ลง ................................................ ผู้ค้ำประกัน 3\r\n" +
                                             "     ( " + dt.Rows[3][1].ToString() + " )", THsarabun18, BrushBlack, X, Y + (SpacePerRow * CurrentRows++), 300f, 300, false);
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

        public static void Printdatagridview(System.Drawing.Printing.PrintPageEventArgs e,DataGridView G,Bitmap bmp)
        {
                int height = G.Height;
                G.Height = G.RowCount * G.RowTemplate.Height * 2;
                bmp = new Bitmap(G.Width,G.Height);
                G.DrawToBitmap(bmp, new Rectangle(0, 0, G.Width, G.Height));
                G.Height = height;
                e.Graphics.DrawImage(bmp, 0, 0);
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
