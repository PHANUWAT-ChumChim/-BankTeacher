
namespace BankTeacher.Bank
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle106 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle107 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle110 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle111 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle112 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle108 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle109 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.CLpassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TBSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeColumns = false;
            this.DGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle106.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle106;
            this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle107.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle107.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle107.Font = new System.Drawing.Font("TH Sarabun New", 16.2F);
            dataGridViewCellStyle107.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle107.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle107.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle107.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle107;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CLpassword,
            this.CLname});
            dataGridViewCellStyle110.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle110.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle110.Font = new System.Drawing.Font("TH Sarabun New", 16.2F);
            dataGridViewCellStyle110.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle110.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle110.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle110.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.DefaultCellStyle = dataGridViewCellStyle110;
            this.DGV.Location = new System.Drawing.Point(31, 59);
            this.DGV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            dataGridViewCellStyle111.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle111.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle111.Font = new System.Drawing.Font("TH Sarabun New", 16.2F);
            dataGridViewCellStyle111.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle111.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle111.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle111.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle111;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidth = 51;
            dataGridViewCellStyle112.Font = new System.Drawing.Font("TH Sarabun New", 16.2F);
            this.DGV.RowsDefaultCellStyle = dataGridViewCellStyle112;
            this.DGV.RowTemplate.Height = 24;
            this.DGV.Size = new System.Drawing.Size(771, 375);
            this.DGV.TabIndex = 0;
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // CLpassword
            // 
            dataGridViewCellStyle108.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLpassword.DefaultCellStyle = dataGridViewCellStyle108;
            this.CLpassword.HeaderText = "รหัส";
            this.CLpassword.MinimumWidth = 6;
            this.CLpassword.Name = "CLpassword";
            this.CLpassword.ReadOnly = true;
            this.CLpassword.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CLpassword.Width = 150;
            // 
            // CLname
            // 
            this.CLname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle109.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLname.DefaultCellStyle = dataGridViewCellStyle109;
            this.CLname.HeaderText = "ชื่อ-สกุล";
            this.CLname.MinimumWidth = 6;
            this.CLname.Name = "CLname";
            this.CLname.ReadOnly = true;
            this.CLname.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TBSearch
            // 
            this.TBSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TBSearch.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBSearch.Location = new System.Drawing.Point(108, 10);
            this.TBSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBSearch.Name = "TBSearch";
            this.TBSearch.Size = new System.Drawing.Size(694, 43);
            this.TBSearch.TabIndex = 82;
            this.TBSearch.Text = " ";
            this.TBSearch.TextChanged += new System.EventHandler(this.TBTeacherNo_TextChanged);
            this.TBSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "ค้นหา";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.TBSearch);
            this.panel2.Controls.Add(this.DGV);
            this.panel2.Location = new System.Drawing.Point(-16, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 487);
            this.panel2.TabIndex = 4;
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "search";
            this.Load += new System.EventHandler(this.Search_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.TextBox TBSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLpassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLname;
    }
}