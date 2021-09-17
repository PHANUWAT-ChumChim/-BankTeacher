using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example.Class
{
    class ComboBoxPay
    {
        public String No { get; set; }
        public String Balance { get; set; }
        public String Type { get; set; }


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
}
