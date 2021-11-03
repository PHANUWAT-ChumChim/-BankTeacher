using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTeacher.Class.Print
{
    class SetPrintMedtods
    {
        // ถังขยะ 
        static float nu;
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
            SizeF SizeText = e.Graphics.MeasureString("", Class.Print.PrintPreviewDialog.THsarabun18);
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
            if (TextToCut.Length > SetCut)
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
                    SizeText = e.Graphics.MeasureString(splitCut, Class.Print.PrintPreviewDialog.THsarabun18);
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
                SizeText = e.Graphics.MeasureString(TextToCut, Class.Print.PrintPreviewDialog.THsarabun18);
                Y += SizeText.Height;
                OverMax.Add(Y);

                SizeText = e.Graphics.MeasureString(TextToCut, Class.Print.PrintPreviewDialog.THsarabun18);
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
            , float X, float Y, string Text, int SetCut, int Loca, Font F, int p, int c, out int position, out int Currentposition)
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
                    // เช็คตำเเหน่งที่เคยวาดไปเเล้ว 
                    if (RowsNo >= c)
                    {
                        // ค่าที่ไม่เกิน กำหนดการขึ้นหน้าใหม่
                        if (notover <= Class.Print.PrintPreviewDialog.linesToPrint)
                        {
                            // นับตำเเหน่ง Rows ที่ถูกเทียบไปทั้งหมด
                            p++;
                            cm = CutingCharAndString(e, G.Rows[RowsNo].Cells[Loca - 1].Value.ToString()
                         , SetCut, X, Y, out nu, out over, 2);
                            notover += over;
                            Values.Add(cm);
                        }
                    }
                }

            }
            // บอกตำเเหน่งที่ถูกนับไปเเล้ว
            Currentposition = 0;
            position = 0;
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
        public static void Tabletotal(System.Drawing.Printing.PrintPageEventArgs e, Pen pen, List<float> array, Brush B, float Fontsize, float x, float y)
        {
            float sum;
            // ยอดที่รวมได้ทั้งหมด 
            sum = array.Sum();
            SizeF SizeSUM = e.Graphics.MeasureString(Class.Print.PrintPreviewDialog.NumToBath(sum.ToString()), Class.Print.PrintPreviewDialog.FonT(Fontsize, "TH Sarabun New"));
            // ตัวหนังสือเลข
            e.Graphics.DrawString(Class.Print.PrintPreviewDialog.NumToBath(sum.ToString()), Class.Print.PrintPreviewDialog.FonT(Fontsize, "TH Sarabun New"), B, x, y);
            // เส้น
            e.Graphics.DrawRectangle(pen, x, y, SizeSUM.Width, SizeSUM.Height);
            // ตัวเลข
            e.Graphics.DrawString($"{sum.ToString()} บาท", Class.Print.PrintPreviewDialog.FonT(Fontsize, "TH Sarabun New"), B, (750 - SizeSUM.Width) / 2 + SizeSUM.Width, y);
            // เส้น
            e.Graphics.DrawRectangle(pen, SizeSUM.Width + x, y, 750 - SizeSUM.Width, SizeSUM.Height);
        }
        // ของปุ้น
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
        // ของปุ้น





        // พี่ตังค์
        //----------------------- MedtodPrint -------------------- ////////
        //public static int CurrentRows = 0;
        //public static void Header(System.Drawing.Printing.PrintPageEventArgs e, Brush brush)
        //{
        //    int Y = 50;
        //    int SpacePerRow = 25;
        //    //int CurrentRows = 0;
        //    Font Header01 = new Font("TH Sarabun New", 20, FontStyle.Bold);
        //    //Font Normal01 = new Font("TH Sarabun New", 18, FontStyle.Regular);
        //    String[] Head = new String[] { "APPLICATION FOR EMPLOYMENT", "ใบสมัครงาน", "กรอกข้อมูลด้วยตัวท่านเอง", "(To be completed in own handwriting)" };

        //    for (int Num = 0; Num < 4; Num++)
        //    {
        //        if (Num == 2)
        //            Header01 = new Font("TH Sarabun New", 18, FontStyle.Regular);
        //        SizeF SizeString = e.Graphics.MeasureString(Head[Num], Header01);
        //        float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2;
        //        e.Graphics.DrawString(Head[Num],
        //        Header01, brush, new PointF(StartLoc, Y + (SpacePerRow * CurrentRows++)));
        //    }
        //}
        //// Comment!
        //public static void StartCenter(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        //{
        //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
        //    float StartLoc = e.PageBounds.Width / 2;
        //    e.Graphics.DrawString(Text,
        //        fontText, brush, new PointF(StartLoc, LocY));
        //}
        //// Comment!
        //public static void CenterT(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        //{
        //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
        //    float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2;
        //    e.Graphics.DrawString(Text,
        //        fontText, brush, new PointF(StartLoc, LocY));
        //}
        //// Comment!
        //public static void KeepRight(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        //{
        //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
        //    float StartLoc = (e.PageBounds.Width - 50) - SizeString.Width;
        //    e.Graphics.DrawString(Text,
        //        fontText, brush, new PointF(StartLoc, LocY));
        //}
        //// Comment!
        //public static void KeepLeft(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        //{
        //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
        //    float StartLoc = 50;
        //    e.Graphics.DrawString(Text,
        //        fontText, brush, new PointF(StartLoc, LocY));
        //}
        //// Comment!
        //public static void CenterKeepRight(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        //{
        //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
        //    float StartLoc = ((e.PageBounds.Width / 2) + (e.PageBounds.Width / 2) / 2) - SizeString.Width;
        //    e.Graphics.DrawString(Text,
        //        fontText, brush, new PointF(StartLoc, LocY));
        //}

        //// Comment!
        //public static void Rect(System.Drawing.Printing.PrintPageEventArgs e, Pen ColorRect, int WidthSize, int HeightSize, float LocY, float LocX)
        //{
        //    //float x = e.PageBounds.Width - 50 - 125;
        //    e.Graphics.DrawRectangle(ColorRect, LocX, LocY, WidthSize, HeightSize);
        //}
        //// Comment!
        //public static void PrintBody(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font font, Brush brush)
        //{
        //    float LocX = 96;
        //    e.Graphics.DrawString(Text, font, brush, LocX, LocY);
        //}
        //// Comment!
        //public static void PrintCheckBoxList(System.Drawing.Printing.PrintPageEventArgs e, float LocX, float LocY, Font font, Brush brush, List<String> AllCheckBox, float SpaceCheckList)
        //{
        //    Pen ColorRect = new Pen(Color.Black);

        //    for (int Num = 0; Num < AllCheckBox.Count; Num++)
        //    {
        //        SizeF SizeText = e.Graphics.MeasureString(AllCheckBox[Num], font);
        //        e.Graphics.DrawString(AllCheckBox[Num], font, brush, LocX + (SpaceCheckList * (Num + 1)), LocY);
        //        Rect(e, ColorRect, 15, 15, LocY + 20, LocX + (SpaceCheckList * (Num + 1)) - 17);
        //        LocX += SizeText.Width;
        //    }

        //}

        //----------------------- End code -------------------- ////////
        // Comment!
        //public void Rect(System.Drawing.Printing.PrintPageEventArgs e, Pen ColorRect, int WidthSize, int HeightSize, float LocY, float LocX)
        //{
        //    //float x = e.PageBounds.Width - 50 - 125;
        //    e.Graphics.DrawRectangle(ColorRect, LocX, LocY, WidthSize, HeightSize);
        //}
        //// Comment!
        //public void PrintBody(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font font, Brush brush)
        //{
        //    float LocX = 96;
        //    e.Graphics.DrawString(Text, font, brush, LocX, LocY);
        //}
        //// Comment!
        //public void PrintCheckBoxList(System.Drawing.Printing.PrintPageEventArgs e, float LocX, float LocY, Font font, Brush brush, List<String> AllCheckBox, float SpaceCheckList)
        //{
        //    Pen ColorRect = new Pen(Color.Black);

        //    for (int Num = 0; Num < AllCheckBox.Count; Num++)
        //    {
        //        SizeF SizeText = e.Graphics.MeasureString(AllCheckBox[Num], font);
        //        e.Graphics.DrawString(AllCheckBox[Num], font, brush, LocX + (SpaceCheckList * (Num + 1)), LocY);
        //        Rect(e, ColorRect, 15, 15, LocY + 20, LocX + (SpaceCheckList * (Num + 1)) - 17);
        //        LocX += SizeText.Width;
        //    }

        //}
        //----------------------- End Medtod -------------------- ////////
        //----------------------- End Medtod -------------------- ////////
        // พี่ตังค์








        public void ExpPrint()
        {
            //int ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(Rfwidth) + 1);
            // loop text //
            //for (int x = 0; x < 20; x++)
            //    e.Graphics.DrawString("Test",
            //        Normal01, Normal, new PointF(X, Y + (SpacePerRow * CurrentRows++)));

            //กำหนด จุดสิ้นสุด * วาดตาราง //
            //e.Graphics.DrawString("Testtttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt",
            //    Normal01, Normal, new RectangleF(X, Y + (SpacePerRow * CurrentRows++), 200f, 200f));

            //print normal //
            //e.Graphics.DrawString("Testtttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt",
            //    Normal01, Normal, new PointF(X, Y + (SpacePerRow * CurrentRows++)-50));

            //e.Graphics.DrawString(DTPStartDate.Text, new Font("TH Sarabun New",18, FontStyle.Regular), Brushes.Black, new PointF(0, 60));

            //SizeF SizeString = e.Graphics.MeasureString(Text, Header01);
            //float StartLoc = PageX / 2 - SizeString.Width / 2;
            //e.Graphics.DrawString(Text,
            //    Header01, Normal, new PointF(StartLoc, Y + (SpacePerRow * CurrentRows++)-10));

            //----------------------- MetodPrintf -------------------- ////////
            // Comment!
            //public void Center(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
            //{
            //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            //    float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2; e.Graphics.DrawString(Text, fontText, brush, new PointF(StartLoc, LocY));
            //}
            //// Comment!
            //public void CenterRight(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float X, float Y, float Pointplus, float Pointdelete)
            //{
            //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            //    X = X - SizeString.Width; e.Graphics.DrawString(Text, fontText, brush, new PointF(Pointplus + X - Pointdelete, Y));
            //}
            //// Comment!
            //public void CenterLeft(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float X, float Y, float Pointplus, float Pointdelete)
            //{
            //    e.Graphics.DrawString(Text, fontText, brush, new PointF(Pointplus + X - Pointdelete, Y));
            //}
            //// Comment!
            //public int Centerset(System.Drawing.Printing.PrintPageEventArgs e, string Text, Font fontText, Brush brush, float X, float Y, float Rfwidth, float height, Boolean Debug = false)
            //{
            //    SizeF SizeString = e.Graphics.MeasureString(Text, fontText, (int)Rfwidth);
            //    float startingpoint = X + 750 - Rfwidth;
            //    if (Debug)
            //    {
            //        e.Graphics.DrawRectangle(Pens.Black, startingpoint, Y, Rfwidth, height);
            //    }
            //    float upheight = 40; int ExtraRow;
            //    if (SizeString.Height < height)
            //    {
            //        ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(upheight) + 1);
            //    }
            //    else
            //    {
            //        SizeString.Height = height;
            //        ExtraRow = (Convert.ToInt32(SizeString.Height) / Convert.ToInt32(upheight) + 1);
            //    }
            //    e.Graphics.DrawString(Text, fontText, brush, new RectangleF(startingpoint, Y, Rfwidth, height));
            //    return ExtraRow;
            //}
            //----------------------- End code -------------------- ////////
        }
    }
}
