﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Bank
{
    public partial class CancelMembership : Form
    {
        public CancelMembership()
        {
            InitializeComponent();
        }

        private void CancelMembership_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void CancelMembership_Load(object sender, EventArgs e)
        {

        }
    }
}
