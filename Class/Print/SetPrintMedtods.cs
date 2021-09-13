using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example.Class.Print
{
    class SetPrintMedtods
    {
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
