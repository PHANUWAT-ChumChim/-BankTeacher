using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;


namespace BankTeacher.Class
{
    class ComboBoxPay
    {
        public String No { get; set; }
        public String Balance { get; set; }
        public String Type { get; set; }

        // No = ID
        // Balance = จำนวนเงิน
        // Type = ประเภท หุ้น / กู้
        public ComboBoxPay(String type, String balance, String no)
        {
            No = no;
            Balance = balance;
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }
    }

    class ComboBoxPayment
    {
        public String Name { get; set; }
        public String No { get; set; }


        public ComboBoxPayment(String name, String no)
        {
            Name = name;
            No = no;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    class ComboxAdd_item
    {
        // =============================================== เรียงลำดับข้อมูลตัวเลข =================================
        public static void NumberRanking(int SelectIndexRow,DataGridView G,ComboBox CB_Item,string Text)
        {
            // ============== ไม่ต้องยุ่ง ==========================
            List<string> Box_List = new List<string>();
            List<int> get = new List<int>();
            ComboBox[] cb_Array = new ComboBox[] { CB_Item };
            bool Is = true;
            string GetRemove = "";
            BankTeacher.Class.ComboBoxPayment Check_Position;
            if (SelectIndexRow != -1)
            {
                // ============== เก็บค่าที่ถูกลบ ==========================
                GetRemove = G.Rows[SelectIndexRow].Cells[1].Value.ToString();
                if (CB_Item.Items.Count != 0)
                {
                    //  ตำเเหน่ง 
                    int position = 0;
                    // ============== หาตำเเหน่งที่ค่่าควรจะอยู่ ==========================
                    for (int loop = 0; loop < CB_Item.Items.Count; loop++)
                    {
                        Check_Position = (CB_Item.Items[loop] as BankTeacher.Class.ComboBoxPayment);
                        if (Convert.ToInt32(GetRemove) < Convert.ToInt32(Check_Position.No))
                        {
                            position = loop;
                        }
                        else
                        {
                            position = loop;
                        }
                    }
                    Check_Position = (CB_Item.Items[position] as BankTeacher.Class.ComboBoxPayment);
                    if (Convert.ToInt32(GetRemove) < Convert.ToInt32(Check_Position.No))
                    {
                        // ============== เอาข้อมูลที่อยู่ใน combobox ออกมาทั้งหมด เเล้ว บวกด้วยค่าที่โดน ลบ ==========================
                        for (int loop = 0; loop < CB_Item.Items.Count + 1; loop++)
                        {
                            if (loop < CB_Item.Items.Count)
                            {
                                Check_Position = (CB_Item.Items[loop] as BankTeacher.Class.ComboBoxPayment);
                                Box_List.Add(Check_Position.No);
                            }
                            else
                            {
                                Box_List.Add(GetRemove);
                            }
                        }
                        // ============== จัดลำดับใหม่ ==========================
                        do
                        {
                            if (Box_List.Count() != 1)
                            {
                                if (Box_List.Count > 2)
                                {
                                    if (Convert.ToInt32(Box_List[0]) < Convert.ToInt32(Box_List[2]))
                                    {
                                        get.Add(Convert.ToInt32(Box_List[0]));
                                        Box_List.RemoveAt(0);
                                    }
                                    else
                                    {
                                        get.Add(Convert.ToInt32(Box_List[2]));
                                        Box_List.RemoveAt(2);
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(Box_List[0]) < Convert.ToInt32(Box_List[1]))
                                    {
                                        get.Add(Convert.ToInt32(Box_List[0]));
                                        Box_List.RemoveAt(0);
                                    }
                                    else
                                    {
                                        get.Add(Convert.ToInt32(Box_List[1]));
                                        Box_List.RemoveAt(1);
                                    }
                                }
                                Is = false;
                            }
                            else
                            {
                                get.Add(Convert.ToInt32(Box_List[0]));
                                Box_List.RemoveAt(0);
                                double around = get.Count / 1.5;
                                around = Math.Round(around);
                                Is = true;
                                // ============== เช็คว่าจัดลำดับถูกมั้ย ==========================
                                for (int Chcek = 0; Chcek < around; Chcek++)
                                {
                                    if (get[Chcek] > get[Chcek + 1])
                                    {
                                        for (int no_way = 0; no_way < get.Count; no_way++)
                                        {
                                            Box_List.Add(get[no_way].ToString());
                                        }
                                        get.Clear();
                                        Is = false;
                                    }
                                    if (Is == false)
                                    {
                                        break;
                                    }
                                }
                                // ============== ถ้าไม่ถูกทำการเช็คใหม่ เเล้ว เรียงใหม่อีกรอบ ==========================
                                if (Is == false)
                                {
                                    for (int loop = 0; loop < Box_List.Count(); loop++)
                                    {
                                        if (Box_List.Count() != 1)
                                        {
                                            if (Convert.ToInt32(Box_List[loop]) < Convert.ToInt32(Box_List[loop + 1]))
                                            {
                                                get.Add(Convert.ToInt32(Box_List[loop]));
                                                Box_List.RemoveAt(loop);
                                            }
                                            else
                                            {
                                                get.Add(Convert.ToInt32(Box_List[loop + 1]));
                                                Box_List.RemoveAt(loop + 1);
                                            }
                                            loop--;
                                        }
                                        else
                                        {
                                            get.Add(Convert.ToInt32(Box_List[0]));
                                            Box_List.RemoveAt(0);
                                            Is = true;
                                        }

                                    }
                                }
                            }
                        }
                        while (Is == false);
                        CB_Item.Items.Clear();
                        for (int come_backtoCB = 0; come_backtoCB < get.Count; come_backtoCB++)
                        {
                           cb_Array[0].Items.Add(new BankTeacher.Class.ComboBoxPayment(Text  + get[come_backtoCB], get[come_backtoCB].ToString()));
                        }
                    }
                    else
                    {
                       cb_Array[0].Items.Add(new BankTeacher.Class.ComboBoxPayment(Text   + G.Rows[SelectIndexRow].Cells[1].Value.ToString(), GetRemove.ToString()));
                    }
                }
                else
                {
                   cb_Array[0].Items.Add(new BankTeacher.Class.ComboBoxPayment(Text  + G.Rows[SelectIndexRow].Cells[1].Value.ToString(), GetRemove.ToString()));       
                }
                G.Rows.RemoveAt(SelectIndexRow);
            }
        }
        // =============================================== ค้นหารายการ ปี / เดือน ใน ข้อมูล Sql
        public static void Search_datetime(string SQL_Code,int Sqldate_Now,int Backdate,ComboBox CB,bool Year = true,bool Mounth = true)
        {
            DataTable dt;
            for (int Y = 0; Y < Backdate; Y++)
            {
                SQL_Code = SQL_Code.Replace("{Year}", $"{Sqldate_Now-Y}");
                dt = Class.SQLConnection.InputSQLMSSQL(SQL_Code);
                if(Mounth)
                for (int M = 1; M <= 12; M++)
                {
                    SQL_Code = SQL_Code.Replace("{Mounth}", $"{M}");
                    dt = Class.SQLConnection.InputSQLMSSQL(SQL_Code);
                    if(dt.Rows.Count != 0)
                        if (Mounth)  CB.Items.Add(M); 
                }
                if (Year)
                    if (dt.Rows.Count != 0)
                        if (Year) { CB.Items.Add(Sqldate_Now - Y); }
                SQL_Code = SQL_Code.Replace($"{Sqldate_Now - Y}", "{Year}");
                if (Mounth)
                    break;
            }
          
        }

    }
}
