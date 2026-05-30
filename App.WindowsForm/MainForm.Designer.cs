#nullable enable

namespace App.WindowsForm
{
    public partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                components?.Dispose();
                _regularFont?.Dispose();
                _boldFont?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlSide = new Panel();
            btnDashboard = new Button();
            btnAccounts = new Button();
            btnCategories = new Button();
            btnTransaction = new Button();
            pnlContent = new Panel();
            pnlSide.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSide
            // 
            pnlSide.BackColor = Color.FromArgb(240, 240, 240);
            pnlSide.Controls.Add(btnTransaction);
            pnlSide.Controls.Add(btnCategories);
            pnlSide.Controls.Add(btnAccounts);
            pnlSide.Controls.Add(btnDashboard);
            pnlSide.Dock = DockStyle.Left;
            pnlSide.Location = new Point(0, 0);
            pnlSide.Name = "pnlSide";
            pnlSide.Size = new Size(220, 700);
            pnlSide.TabIndex = 0;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.ForeColor = Color.FromArgb(33, 33, 33);
            btnDashboard.Height = 60;
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Location = new Point(0, 0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(20, 0, 0, 0);
            btnDashboard.Size = new Size(220, 60);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.Click += BtnDashboard_Click;
            // 
            // btnAccounts
            // 
            btnAccounts.Dock = DockStyle.Top;
            btnAccounts.FlatStyle = FlatStyle.Flat;
            btnAccounts.ForeColor = Color.FromArgb(33, 33, 33);
            btnAccounts.Height = 60;
            btnAccounts.ImageAlign = ContentAlignment.MiddleLeft;
            btnAccounts.Location = new Point(0, 60);
            btnAccounts.Name = "btnAccounts";
            btnAccounts.Padding = new Padding(20, 0, 0, 0);
            btnAccounts.Size = new Size(220, 60);
            btnAccounts.TabIndex = 1;
            btnAccounts.Text = "Accounts";
            btnAccounts.TextAlign = ContentAlignment.MiddleLeft;
            btnAccounts.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAccounts.UseVisualStyleBackColor = true;
            btnAccounts.FlatAppearance.BorderSize = 0;
            btnAccounts.Click += BtnAccounts_Click;
            // 
            // btnCategories
            // 
            btnCategories.Dock = DockStyle.Top;
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.ForeColor = Color.FromArgb(33, 33, 33);
            btnCategories.Height = 60;
            btnCategories.ImageAlign = ContentAlignment.MiddleLeft;
            btnCategories.Location = new Point(0, 120);
            btnCategories.Name = "btnCategories";
            btnCategories.Padding = new Padding(20, 0, 0, 0);
            btnCategories.Size = new Size(220, 60);
            btnCategories.TabIndex = 2;
            btnCategories.Text = "Categories";
            btnCategories.TextAlign = ContentAlignment.MiddleLeft;
            btnCategories.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.FlatAppearance.BorderSize = 0;
            btnCategories.Click += BtnCategories_Click;
            // 
            // btnTransaction
            // 
            btnTransaction.Dock = DockStyle.Top;
            btnTransaction.FlatStyle = FlatStyle.Flat;
            btnTransaction.ForeColor = Color.FromArgb(33, 33, 33);
            btnTransaction.Height = 60;
            btnTransaction.ImageAlign = ContentAlignment.MiddleLeft;
            btnTransaction.Location = new Point(0, 180);
            btnTransaction.Name = "btnTransaction";
            btnTransaction.Padding = new Padding(20, 0, 0, 0);
            btnTransaction.Size = new Size(220, 60);
            btnTransaction.TabIndex = 3;
            btnTransaction.Text = "Transactions";
            btnTransaction.TextAlign = ContentAlignment.MiddleLeft;
            btnTransaction.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTransaction.UseVisualStyleBackColor = true;
            btnTransaction.FlatAppearance.BorderSize = 0;
            btnTransaction.Click += BtnTransaction_Click;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(220, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(980, 700);
            pnlContent.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlContent);
            Controls.Add(pnlSide);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Finance Tracker";
            pnlSide.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSide = null!;
        private Button btnDashboard = null!;
        private Button btnAccounts = null!;
        private Button btnCategories = null!;
        private Button btnTransaction = null!;
        private Panel pnlContent = null!;
    }
}