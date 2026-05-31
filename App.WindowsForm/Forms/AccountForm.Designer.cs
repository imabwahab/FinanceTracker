#nullable enable

namespace App.WindowsForm.Forms
{
    partial class AccountForm
    {
        private System.ComponentModel.IContainer? components = null;  // CHANGED: was null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblType = new Label();
            cmbAccountType = new ComboBox();
            lblOpeningBalance = new Label();
            numOpeningBalance = new NumericUpDown();
            lblCurrency = new Label();
            txtCurrency = new TextBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            panelButtons = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numOpeningBalance).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 1, 0);
            tableLayoutPanel.Controls.Add(lblType, 0, 1);
            tableLayoutPanel.Controls.Add(cmbAccountType, 1, 1);
            tableLayoutPanel.Controls.Add(lblOpeningBalance, 0, 2);
            tableLayoutPanel.Controls.Add(numOpeningBalance, 1, 2);
            tableLayoutPanel.Controls.Add(lblCurrency, 0, 3);
            tableLayoutPanel.Controls.Add(txtCurrency, 1, 3);
            tableLayoutPanel.Controls.Add(lblStatus, 0, 4);
            tableLayoutPanel.Controls.Add(cmbStatus, 1, 4);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(11, 13);
            tableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(0, 0, 11, 13);
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.Size = new Size(435, 261);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Fill;
            lblName.Location = new Point(3, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(131, 47);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Location = new Point(140, 4);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(281, 27);
            txtName.TabIndex = 1;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Dock = DockStyle.Fill;
            lblType.Location = new Point(3, 47);
            lblType.Name = "lblType";
            lblType.Size = new Size(131, 47);
            lblType.TabIndex = 2;
            lblType.Text = "Type:";
            lblType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbAccountType
            // 
            cmbAccountType.Dock = DockStyle.Fill;
            cmbAccountType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountType.Location = new Point(140, 51);
            cmbAccountType.Margin = new Padding(3, 4, 3, 4);
            cmbAccountType.Name = "cmbAccountType";
            cmbAccountType.Size = new Size(281, 28);
            cmbAccountType.TabIndex = 3;
            // 
            // lblOpeningBalance
            // 
            lblOpeningBalance.AutoSize = true;
            lblOpeningBalance.Dock = DockStyle.Fill;
            lblOpeningBalance.Location = new Point(3, 94);
            lblOpeningBalance.Name = "lblOpeningBalance";
            lblOpeningBalance.Size = new Size(131, 47);
            lblOpeningBalance.TabIndex = 4;
            lblOpeningBalance.Text = "Opening Balance:";
            lblOpeningBalance.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numOpeningBalance
            // 
            numOpeningBalance.DecimalPlaces = 2;
            numOpeningBalance.Dock = DockStyle.Fill;
            numOpeningBalance.Location = new Point(140, 98);
            numOpeningBalance.Margin = new Padding(3, 4, 3, 4);
            numOpeningBalance.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numOpeningBalance.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
            numOpeningBalance.Name = "numOpeningBalance";
            numOpeningBalance.Size = new Size(281, 27);
            numOpeningBalance.TabIndex = 5;
            // 
            // lblCurrency
            // 
            lblCurrency.AutoSize = true;
            lblCurrency.Dock = DockStyle.Fill;
            lblCurrency.Location = new Point(3, 141);
            lblCurrency.Name = "lblCurrency";
            lblCurrency.Size = new Size(131, 47);
            lblCurrency.TabIndex = 6;
            lblCurrency.Text = "Currency:";
            lblCurrency.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCurrency
            // 
            txtCurrency.Dock = DockStyle.Fill;
            txtCurrency.Location = new Point(140, 145);
            txtCurrency.Margin = new Padding(3, 4, 3, 4);
            txtCurrency.Name = "txtCurrency";
            txtCurrency.Size = new Size(281, 27);
            txtCurrency.TabIndex = 7;
            txtCurrency.Text = "PKR";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 188);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(131, 60);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status:";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbStatus
            // 
            cmbStatus.Dock = DockStyle.Fill;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(140, 192);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(281, 28);
            cmbStatus.TabIndex = 9;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(11, 274);
            panelButtons.Margin = new Padding(3, 4, 3, 4);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(435, 60);
            panelButtons.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(240, 13);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 33);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(337, 13);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 33);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 347);
            Controls.Add(tableLayoutPanel);
            Controls.Add(panelButtons);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AccountForm";
            Padding = new Padding(11, 13, 11, 13);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Account";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numOpeningBalance).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TextBox txtName = null!;
        private ComboBox cmbAccountType = null!;
        private Label lblOpeningBalance;
        private NumericUpDown numOpeningBalance = null!;
        private TextBox txtCurrency = null!;
        private ComboBox cmbStatus = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private TableLayoutPanel tableLayoutPanel = null!;  // CHANGED: added = null!;
        private Label lblName = null!;                      // CHANGED: added = null!;
        private Label lblType = null!;                      // CHANGED: added = null!;
        private Label lblCurrency = null!;                  // CHANGED: added = null!;
        private Label lblStatus = null!;                    // CHANGED: added = null!;
        private Panel panelButtons = null!;                 // CHANGED: added = null!;
    }
}