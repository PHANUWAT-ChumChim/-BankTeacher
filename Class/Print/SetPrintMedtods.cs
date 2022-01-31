using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace BankTeacher.Class.Print
{
    class SetPrintMedtods
    {
        // ถังขยะ 
        public static float nu;
        // Array ไม่เเน่ใจในการใช้งาน
        static List<float> OverMax = new List<float>();


        /// <summary>
        /// <para> การทำงาน </para>
        /// <para> เก็บ Rows เเละ Columns เข้า Array เพื่อนำไปใช้งาน เเละสามารถ เก็บ เป็น Array2D ได้ (ถ้าต้องการColmns ให้สร้างเอง เพราะผู้สร้างไม่ได้ใช้) </para>>
        /// <para> ควมาสามารภหลักๆ คือ การเช็คข้อมูลที่เก็บเข้าไปใน Array ว่าเกิน ระยะเเกน Y ที่กำหนดรึยังถ้าเกินให้เก็บใส่กล่องใหม่ จนกว่าจะครบ </para>
        /// <para> วิธีใช้งาน </para>
        /// <para> กรุณาเรียก System Pring ออกมาด้วย ตัวมันจำเป็นต้องเรียกใช้ใต้class / ขนาดการตัดของความ setcut ส่งให้ Metod / ตาราง G </para>
        /// <para> Array ที่เข้าไปเเทนข้อมูลในตาราง Textpage / Array ที่รวมข้อมูลเป็นกล่องๆสำหรับใช้งาน โดยวิธีใช้ก็ loop ตัวมันเลย โดยจะมี Rows เเละ Cells ในตัว forTextpage / กำนหดจุดตัดเเกน Y lineToprint / ขนาดเริ่มของเเกน Y หรือ จุดเริ่มวาด เเกน Y RowsY </para> 
        /// </summary>
        public static List<string[]> CutString(System.Drawing.Printing.PrintPageEventArgs e, int SetCut, System.Windows.Forms.DataGridView G
                     , List<string[]> Textpage, List<string> forTextpage, float linesToPrint, float RowsY)
        {
            // เช็คการเปิดใช้งานการเก็บ Array
            int OpengetArray = 0;
            // รับค่า Columns And Rows Count มา loop
            int[] CountG = { G.ColumnCount, G.RowCount };
            // ตัวเช็ค ตำเเหน่ง Array for CountG
            int CG = 0;
            // loop ตั้งเเต่ Columns ไป Rows เเค่ 2 ตัว
            for (int around = -1; around < CG; around++)
            {
                // loop ตามจำนวน Array ในทั้งหมด ที่มีอยู่ 
                for (int somtring = 0; somtring < CountG[CG]; somtring++)
                {
                    if (CG == 0) // สำหรับ Columns
                    {
                        // แปปยังไม่ หรือ จะทำอะไรดีในนี้ **************************
                        //CutingCharAndString(e, G.Columns[somtring].HeaderText, SetCut,X,Y);
                    }
                    else if (CG == 1)// สำหรับ Rows 
                    {
                        // loop cells ทั้งหมด ใน Array ที่มีอยู่
                        for (int cells = 0; cells < G.Rows[somtring].Cells.Count; cells++)
                        {
                            // เเทนค่า ตาราง G ที่ได้รับมาเก็บไว้ ใน array สำหรับการ Add
                            forTextpage.Add(G.Rows[somtring].Cells[cells].Value.ToString());
                            // เก็บขนาข้อความที่ได้มา เพื่อเอาไปตวรจสอบใน โรงงานการตัด
                            RowsY += CutingCharAndString(e, G.Rows[somtring].Cells[cells].Value.ToString(), SetCut, 0, RowsY, out nu, out nu, 0);
                            // ตรวจสอบว่า ขนาดของ Y มากกว่า หน้า page ที่กำหนดไว้มั้ย
                            if (RowsY >= linesToPrint)
                            {
                                if (forTextpage.Count != 0)
                                {
                                    Textpage.Add(forTextpage.ToArray());
                                    forTextpage.Clear();
                                    RowsY = 50;
                                    // ปิดการใช้งาน การเก็บ Array ตกหล่น
                                    OpengetArray++;
                                }
                            } // ต้องเป็น loop สุดท้ายของ loop ใหญ่ เเละ ต้องเป็น Cells สุดท้าย ของ loop นี้ ไม่ว่ายังไงก็ได้เข้า loop
                            else if (somtring == CountG[CG] - 1 && cells == G.Rows[somtring].Cells.Count - 1)
                            {
                                if (forTextpage.Count != 0)
                                {
                                    Textpage.Add(forTextpage.ToArray());
                                    forTextpage.Clear();
                                    // ปิดการใช้งาน การเก็บ Array ตกหล่น
                                    OpengetArray++;
                                }
                            }
                        } // เก็บ Array ตกหล่น ที่เหลือ เเต่จรืงๆ ยังมันก็ไม่ได้เก็บเเต่เพื่อความสบายใจ ;>
                        if (OpengetArray == 0 && somtring == CountG[CG] - 1)
                        {
                            Textpage.Add(forTextpage.ToArray());
                            forTextpage.Clear();
                            OpengetArray = 0;
                        }
                    }
                    // เช็คเงื่อนไขว่า loop สุดท้ายรึยัง && CG ต้องไม่เคยบวกมาก่อน 0 && Rows ต้องไม่เท่ากับ 0 
                    if (somtring == CountG[CG] - 1 && CG == 0 && CountG[CG] != 0)
                    {
                        RowsY = 50;
                        CG++;
                    }
                }
            }
            return Textpage;
        }

        /// <summary>
        /// การทำงาน
        /// ตัดข้อความที่เกิน / บวกตำเเหน่งเป็นPoint / ตวรจสอบสระ / เช็คขนาด / อื่นสามารถเพิ่มเติ่มต่อได้
        /// <para> ตัด String Chcek ขนาดความ ยาว เเละ ระยะความกว้าง </para>
        /// <para> วิธีใช้ งาน </para>
        /// <para> ส่วนที่ขาดไม่ได้ PrintEvent ตัวมันจำเป็นต้องประกาศใต้class ที่จะใช้งาน </para>
        /// <para> Text ที่่จะตัด / setcut กำหนดการเว้น / XY ตำเเหน่งที่ทำการปริ้นในเเบบ  </para>
        /// <para> ค่าที่ใช่กำหนด Rectangle โดยสามารภเรียกใช้งาน โดยประกาศค่า out เพื่อส่งค่ากลับ / ถ้าไม่ได้ใช้ สามารภทิ้งค่าโดย ประกาศค่า out nu </para>
        /// <para> หลักๆ สามารถเลือกการ Return ได้ 2 ค่า ได้ เเก่ Y == 0 X == 1 โดยกำหนดที่ select   </para>
        /// <para> X ใช้ในกรณีที่มีการใช้งาน ปริ้นเเบบ เว้นบรรดทัด เพื่อ ทราบขนาดที่เเน่นอน </para>
        ///        Y ใช้สำหรับการตวรจสอบ ความยาวที่มากที่สุดเมื่อมีการ ตัด หรือ ไม่ก็ได้ เพื่อบอกเเบบปริ้นที่อยู่ปัจจุบัน
        /// </summary>
        public static float CutingCharAndString(System.Drawing.Printing.PrintPageEventArgs e, string TextToCut
            , int SetCut, float X, float Y, out float ReturnX, out float ReturnY, int selectresult)
        {
            // ขนาด Point ข้อความ
            SizeF SizeText = e.Graphics.MeasureString("", Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Regular));
            // สระ
            Char[] Word = { 'ำ', 'ะ', 'ั', 'ี', 'ุ', 'ึ', 'เ', '็', '้', '๋', 'ิ', 'ื', '์', '.', ' ' }; //  ,'่'
            // ตัด สระ
            string oneWrod = "";
            // ตัด ข้อความ
            string storeCut = "", splitCut = "";
            // ตัวบวกๆ
            int NumberoFWord = 0, one = 0;
            // สร้างไว้แปปป
            int UP = 2 * 1, u = 1;
            // (ตัวทำงาน โดย มีหน้าที่ตัด string and Char ทั้ง2เเบบ) โดย การเอา TextG มา ตัดเพื่อวัดขนาดที่ต้องตัด
            // โดย สิ่่งที่ได้จะส่วน นี้ คือ ระยะ ความยาว ของ Rows การเว้นบรรดทัด เมื่อ ข้อความเกิน ขนาด
            // ถ้าขนาดของข้อความมีขนาดมากกว่ากำหนดการตัด เข้าเงื่อนไขได้
            if (TextToCut.Length > SetCut )
            {
                // ถ้าขนาดข้อความมากกว่ากำหนด เข้าเงื่อไขได้ เพื่อ loop ตัดไปเรื่อยเพื่อเช็คมากขนาดยังเกินกำหนดหรือยัง
                while (TextToCut.Length >= SetCut)
                {
                    // (ตัด Char )
                    // เเทนค่าข้อความใหม่ทุการก loop
                    string GetWord = TextToCut;
                    // เช็คCharตามกำหนดการตัด
                    for (int w = 0; w < SetCut; w++)
                    {
                        // ตัดตั้งเเต่ 1 - กำหนด  เก็บ ไว้ ตวรจสอบ Char
                        oneWrod = GetWord.Remove(1, GetWord.Length - 1);
                        //  ตัดตั้งเเต่ 0 - 1 เก็บค่าที่ตัดเเล้ว
                        GetWord = GetWord.Remove(0, 1);
                        // loop หา สระ หรือ อักษร ที่ เราต้องการ ใน Array
                        for (int word = 1; word < Word.Count(); word++)
                        {
                            // เช็คก่อนว่า ค่าข้อความไม่ได้อยู่หลัง สุด ไม่อย่างงั้น จะเเย่เอานะ
                            if (GetWord.Length >= SetCut)
                            {
                                // ตวรจสอบ สระ หรือ อักษร
                                if (GetWord == Word[word].ToString())
                                {
                                    // บวก ระยะ การตั้ง
                                    NumberoFWord++;
                                }
                            }
                        }
                    }
                    if (TextToCut.Length >= 25)
                        u++;
                    // (ตัด ขนาดข้อความที่เกินกำหนด)
                    // บวกขนาดการตัด หลังจากรับ สระ ทั้งที่เจอเเล้ว
                    SetCut = SetCut + NumberoFWord;
                    //  เริ่มตัดตั้งเเต่ 0 - กำหนด  เเล้วเก็บค่านี้ไปตัดต่อรอบหน้า   (เก็บไว้ตัดรอบหน้า)
                    storeCut = TextToCut.Remove(0, SetCut);
                    //  เริ่มตัดตั้งเเต่  กำหนด - ค่าที่เเยกไว้ตัดรอบหน้า         (เเยกออกจากข้อความเลย)
                    splitCut = TextToCut.Remove(SetCut, storeCut.Length);
                    // เเทนค่าที่เเยกออกมาสู่กระบวนการผลิต
                    TextToCut = storeCut;
                    // ตรวจสอบขนาด ข้อคววาม ความยาว
                    SizeText = e.Graphics.MeasureString(splitCut, Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Regular));
                    // ทำการเก็บขนาดข้อความที่ยาวที่สุดไว้
                    if (one == 0)
                    {
                        X = SizeText.Width;
                        one++;
                    }
                    Y += SizeText.Height * UP;
                    u++;
                }
                OverMax.Add(Y);
                one--;

            }
            else
            {
                SizeText = e.Graphics.MeasureString(TextToCut, Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Regular));
                Y += SizeText.Height;
                OverMax.Add(Y);

                SizeText = e.Graphics.MeasureString(TextToCut, Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Regular));
                X += SizeText.Width;
            }
            // ส่งกรอบกำหนด เเก X กลับไป กว้าง
            ReturnX = X;
            // ส่งกรอบกำหนด เเก Y กลับไป ยาว
            ReturnY = Y;
            // ส่ง ค่าความยาวกลับ สำหรับการตรวจสอบ Rows
            if (selectresult == 0)
                return Y;
            else
                return X;

        }
        /// <summary>
        /// เช็คระยะ ห่างของ ข้อความ ไม่สมบูรณ์
        /// </summary>
        public static float Chcekspan(System.Drawing.Printing.PrintPageEventArgs e, System.Windows.Forms.DataGridView G
            , float X, float Y, string Text, int SetCut, int Loca, Font F, int p, int c)
        {
            List<float> Values = new List<float>();
            //SizeF Size = e.Graphics.MeasureString("", Class.Print.PrintPreviewDialog.THsarabun18);
            float v = 0;
            // ตัวรับขนาด pixel ของข้อความ
            float cm = 0;
            // ตัวเช็คขนาดความกว้าง ของ Y
            float notover = Y, over = 0;
            // นับตำเเหน่ง Rows ที่เคยอยู่

            if (G.ColumnCount != 0)
            {
                // เก็บขนาดที่ตัดมาเเล้ว
                cm = CutingCharAndString(e, Text, SetCut, X, Y, out nu, out nu, 2);
                // เก็บค่าที่ได้เข้า  Values เเล้วใช้สำหรับเปลียบเทียบ ค่าที่มากที่สุด
                Values.Add(cm);
            }
            if (G.RowCount != 0)
            {
                for (int RowsNo = 0; RowsNo < G.RowCount; RowsNo++)
                {
                    //// เช็คตำเเหน่งที่เคยวาดไปเเล้ว 
                    //if (RowsNo >= c)
                    //{
                    // นับตำเเหน่ง Rows ที่ถูกเทียบไปทั้งหมด
                    //p++;
                        cm = CutingCharAndString(e, G.Rows[RowsNo].Cells[Loca - 1].Value.ToString()
                        , SetCut, X, Y, out nu, out over, 2);
                    
                        Values.Add(cm);
                    //}
                }

            }
            // เก็บค่าที่มากที่สุดของระยะวาดต่อไป
            v = MaxValues(v, Values);
            // ส่งค่ากลับไปวาด
            v = v - 50;
            return v;
        }

        /// <summary>
        /// <para> วิธีใช้งาน </para>
        /// <para> ตัวแปรที่จะเก็บค่า Max (ในกรณีที่ค่าที่นำมาเเทนมากกว่า จะไม่เก็บค่า Max)</para>
        /// ค่าใน List ที่รวมมาทั้งหมด จะถูกเช็คขนาด เเละเก็บ ไว้ใน Max ห่างขนาด Max น้อยกว่า ค่าที่อยู่ ใน array
        /// </summary>
        public static float MaxValues(float Max, List<float> Values)
        {
            int N = 0;
            //ทำการหาค่าที่มากที่สุดเเล้วเก็บค่านั้นไว้ 
            foreach (float V in Values)
            {
                if (Max < Values[N])
                {
                    Max = Values[N];
                }
                N++;
            }
            //คืนค่ากลับ
            OverMax.Clear();
            return Max;
        }

        /// <summary>
        /// <para> การทำงาน </para>>
        /// <para> สร้างตารางสำหรับรวมค่าตัวเลขทั้งหมด โดยสร้างต่อจากเเบบปริ้นหลังปริ้นเส้น </para>>
        /// <para> วิธีการใช้งาน  </para>>
        /// ประกาศคำสั่ง System ตาม Metod เลย / Pen คือ ปากกา ที่ใช้ว่าโดยกำหนดค่าเองได้เลย / array  ที่เอาไว้เเทน ค่าตัวเลขที่เราต้องการ รวม (ต้องเก็บค่าไว้ให้ด้วย) / แปรง สี B / ขนาดตัวหนังสือ Fontsize / x = ขนาดเริ่มวาดครั้งเเรก / y ขนาดที่เริ่มวาดไปทั้งหมดเเล้ว
        /// </summary>
        public static void Tabletotal(System.Drawing.Printing.PrintPageEventArgs e, Pen pen, List<int> array, Brush B, float Fontsize, float x, float y,float Sizepaper)
        {
            int sum;
            // ยอดที่รวมได้ทั้งหมด 
            sum = Convert.ToInt32(array.Sum());
            SizeF SizeSUM = e.Graphics.MeasureString(Class.Print.PrintPreviewDialog.NumToBath(sum.ToString()), Class.Print.PrintPreviewDialog.Font(Fontsize, "TH Sarabun New", FontStyle.Bold));
            // ตัวหนังสือเลข
            e.Graphics.DrawString(Class.Print.PrintPreviewDialog.NumToBath(sum.ToString()), Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Bold), B, x, y);
            // เส้น
            e.Graphics.DrawRectangle(pen, x, y, SizeSUM.Width, SizeSUM.Height);
            // ตัวเลข
            e.Graphics.DrawString($"{comma(sum).ToString()} บาท", Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Bold), B, ((Sizepaper - 50) - SizeSUM.Width) / 2 + SizeSUM.Width, y);
            // เส้น
            e.Graphics.DrawRectangle(pen, SizeSUM.Width + x, y, (Sizepaper-50) - SizeSUM.Width, SizeSUM.Height);
        }

        public static void confirmation(System.Drawing.Printing.PrintPageEventArgs e,Pen Pen, Brush Brush, int Print_two,float SizeForm_Width,float move)
        {
            string script = "";
            if (Print_two == 0)
            {
                script = "ต้นฉบับ";
            }
            else
            {
                script = "สำเนา";
            }
            // ต้นฉบับ  สำเนา
            SizeF Size = e.Graphics.MeasureString(script, Class.Print.PrintPreviewDialog.Font(30, "TH Sarabun New", FontStyle.Bold));
            // พื้นหลัง
            e.Graphics.FillRectangle(Brushes.White, SizeForm_Width - Size.Width - move, 50, Size.Width, Size.Height - 10);
            // ข้อความ
            e.Graphics.DrawString(script, Class.Print.PrintPreviewDialog.Font(30, "TH Sarabun New", FontStyle.Bold), Brush, SizeForm_Width - Size.Width - move, 50);
            // กล่อง
            e.Graphics.DrawRectangle(Pen, SizeForm_Width - Size.Width - move, 50, Size.Width, Size.Height - 10);
        }
        public static float Box(System.Drawing.Printing.PrintPageEventArgs e,string string_Text1,string string_Text2, string Textinfo1, string Textinfo2, float Box_SizeX ,float Box_SizeY, float location_Box,float Line2_x)
        {
            SizeF Size,Size1;
            Brush BrushBlack = Brushes.Black;
            Pen PenBlack = new Pen(Color.Black);
            string Text;
            string Textinfo;
            for (int loopbox = 0; loopbox < 2; loopbox++)
            {
                if (loopbox == 0)
                {
                    Text = string_Text1;
                    Textinfo = Textinfo1;
                }
                else
                {
                    Text = string_Text2;
                    Textinfo = Textinfo2;
                }
                Size = e.Graphics.MeasureString(Textinfo,Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Bold));
                Size1 = e.Graphics.MeasureString(Text,Class.Print.PrintPreviewDialog.Font(16, "TH Sarabun New", FontStyle.Regular));
                e.Graphics.DrawString(Text,Class.Print.PrintPreviewDialog.Font(16, "TH Sarabun New", FontStyle.Regular), BrushBlack, (Line2_x - Box_SizeX) - Size1.Width, location_Box + 5);
                // ข้อความ
                e.Graphics.DrawString(Textinfo,Class.Print.PrintPreviewDialog.Font(18, "TH Sarabun New", FontStyle.Bold), BrushBlack, Line2_x - (Box_SizeX / 2) - Size.Width / 2, location_Box);
                // กล่อง
                e.Graphics.DrawRectangle(PenBlack, Line2_x - Box_SizeX, location_Box, Box_SizeX, Box_SizeY);
                location_Box += Box_SizeY;
            }
            return location_Box;
        }
        public static string comma(int GetNumber)
        {
            List<string> Sort = new List<string>();
            string Number = "";
            int up = 1;
            string getRemove = GetNumber.ToString();
            string getNum = "";
            for (int loop = 0; loop < GetNumber.ToString().Length; loop++)
            {
                getNum = getRemove.Remove(0, 1);
                getRemove = getRemove.Remove(1, getRemove.Length - 1);
                Sort.Add(getRemove);
                getRemove = getNum;
            }
            for (int loop = 0; loop < Sort.Count; loop++)
            {
                if (loop == 3 * up)
                {
                    Number += ",";
                    loop--;
                    up++;
                }
                else
                    Number += Sort[Sort.Count() - (loop + 1)];
            }
            char[] charArray = Number.ToCharArray();
            Array.Reverse(charArray);
            for (int loop = 0; loop < charArray.Count(); loop++)
            {
                getNum += charArray[loop];
            }
            return getNum;
        }

       
        public static void Center(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2; e.Graphics.DrawString(Text, fontText, brush, new PointF(StartLoc, LocY));
        }
        // Comment!
        public static void CenterRight(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float X, float Y, float Pointplus, float Pointdelete)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            X = 800 - SizeString.Width; e.Graphics.DrawString(Text, fontText, brush, new PointF(Pointplus + X - Pointdelete, Y));
        }
        // Comment!
        public static void CenterLeft(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float X, float Y, float Pointplus, float Pointdelete)
        {
            e.Graphics.DrawString(Text, fontText, brush, new PointF(Pointplus + X - Pointdelete, Y));
        }
        // Comment!
        public static int Centerset(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float X, float Y, float Rfwidth, float height, Boolean Debug = false)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText, (int)Rfwidth);
            float startingpoint = X + 750 - Rfwidth;
            if (Debug)
            {
                e.Graphics.DrawRectangle(Pens.Black, startingpoint, Y, Rfwidth, height);
            }
            float upheight = 40; int ExtraRow;
            if (SizeString.Height < height)
            {
                ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(upheight) + 1);
            }
            else
            {
                SizeString.Height = height;
                ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(upheight) + 1);
            }
            e.Graphics.DrawString(Text, fontText, brush, new RectangleF(startingpoint, Y, Rfwidth, height));
            return ExtraRow;
        }
    }
}
