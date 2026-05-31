#nullable enable

namespace App.WindowsForm.Forms
{
    partial class TransactionForm
    {
        private System.ComponentModel.IContainer? components = null;

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
            lblAccount = new Label();
            cmbAccount = new ComboBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblAmount = new Label();
            numAmount = new NumericUpDown();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblRecurring = new Label();
            chkRecurring = new CheckBox();
            lblFrequency = new Label();
            cmbFrequency = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            panelButtons = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblAccount, 0, 0);
            tableLayoutPanel.Controls.Add(cmbAccount, 1, 0);
            tableLayoutPanel.Controls.Add(lblCategory, 0, 1);
            tableLayoutPanel.Controls.Add(cmbCategory, 1, 1);
            tableLayoutPanel.Controls.Add(lblAmount, 0, 2);
            tableLayoutPanel.Controls.Add(numAmount, 1, 2);
            tableLayoutPanel.Controls.Add(lblDate, 0, 3);
            tableLayoutPanel.Controls.Add(dtpDate, 1, 3);
            tableLayoutPanel.Controls.Add(lblDescription, 0, 4);
            tableLayoutPanel.Controls.Add(txtDescription, 1, 4);
            tableLayoutPanel.Controls.Add(lblRecurring, 0, 5);
            tableLayoutPanel.Controls.Add(chkRecurring, 1, 5);
            tableLayoutPanel.Controls.Add(lblFrequency, 0, 6);
            tableLayoutPanel.Controls.Add(cmbFrequency, 1, 6);
            tableLayoutPanel.Controls.Add(lblStatus, 0, 7);
            tableLayoutPanel.Controls.Add(cmbStatus, 1, 7);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(11, 13);
            tableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(0, 0, 11, 13);
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel.Size = new Size(435, 479);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblAccount
            // 
            lblAccount.AutoSize = true;
            lblAccount.Dock = DockStyle.Fill;
            lblAccount.Location = new Point(3, 0);
            lblAccount.Name = "lblAccount";
            lblAccount.Size = new Size(131, 47);
            lblAccount.TabIndex = 0;
            lblAccount.Text = "Account:";
            lblAccount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbAccount
            // 
            cmbAccount.Dock = DockStyle.Fill;
            cmbAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccount.Location = new Point(140, 4);
            cmbAccount.Margin = new Padding(3, 4, 3, 4);
            cmbAccount.Name = "cmbAccount";
            cmbAccount.Size = new Size(281, 28);
            cmbAccount.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.Location = new Point(3, 47);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(131, 47);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Category:";
            lblCategory.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbCategory
            // 
            cmbCategory.Dock = DockStyle.Fill;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(140, 51);
            cmbCategory.Margin = new Padding(3, 4, 3, 4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(281, 28);
            cmbCategory.TabIndex = 3;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Dock = DockStyle.Fill;
            lblAmount.Location = new Point(3, 94);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(131, 47);
            lblAmount.TabIndex = 4;
            lblAmount.Text = "Amount:";
            lblAmount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numAmount
            // 
            numAmount.DecimalPlaces = 2;
            numAmount.Dock = DockStyle.Fill;
            numAmount.Location = new Point(140, 98);
            numAmount.Margin = new Padding(3, 4, 3, 4);
            numAmount.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(281, 27);
            numAmount.TabIndex = 5;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Dock = DockStyle.Fill;
            lblDate.Location = new Point(3, 141);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(131, 47);
            lblDate.TabIndex = 6;
            lblDate.Text = "Date:";
            lblDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpDate
            // 
            dtpDate.Dock = DockStyle.Fill;
            dtpDate.Location = new Point(140, 145);
            dtpDate.Margin = new Padding(3, 4, 3, 4);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(281, 27);
            dtpDate.TabIndex = 7;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Location = new Point(3, 188);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(131, 80);
            lblDescription.TabIndex = 8;
            lblDescription.Text = "Description:";
            lblDescription.TextAlign = ContentAlignment.TopLeft;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(140, 192);
            txtDescription.Margin = new Padding(3, 4, 3, 4);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(281, 72);
            txtDescription.TabIndex = 9;
            // 
            // lblRecurring
            // 
            lblRecurring.AutoSize = true;
            lblRecurring.Dock = DockStyle.Fill;
            lblRecurring.Location = new Point(3, 268);
            lblRecurring.Name = "lblRecurring";
            lblRecurring.Size = new Size(131, 47);
            lblRecurring.TabIndex = 10;
            lblRecurring.Text = "Recurring:";
            lblRecurring.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkRecurring
            // 
            chkRecurring.AutoSize = true;
            chkRecurring.Dock = DockStyle.Left;
            chkRecurring.Location = new Point(140, 272);
            chkRecurring.Margin = new Padding(3, 4, 3, 4);
            chkRecurring.Name = "chkRecurring";
            chkRecurring.Size = new Size(15, 39);
            chkRecurring.TabIndex = 11;
            chkRecurring.UseVisualStyleBackColor = true;
            // 
            // lblFrequency
            // 
            lblFrequency.AutoSize = true;
            lblFrequency.Dock = DockStyle.Fill;
            lblFrequency.Location = new Point(3, 315);
            lblFrequency.Name = "lblFrequency";
            lblFrequency.Size = new Size(131, 47);
            lblFrequency.TabIndex = 12;
            lblFrequency.Text = "Frequency:";
            lblFrequency.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbFrequency
            // 
            cmbFrequency.Dock = DockStyle.Fill;
            cmbFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFrequency.Enabled = false;
            cmbFrequency.Location = new Point(140, 319);
            cmbFrequency.Margin = new Padding(3, 4, 3, 4);
            cmbFrequency.Name = "cmbFrequency";
            cmbFrequency.Size = new Size(281, 28);
            cmbFrequency.TabIndex = 13;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 362);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(131, 47);
            lblStatus.TabIndex = 14;
            lblStatus.Text = "Status:";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbStatus
            // 
            cmbStatus.Dock = DockStyle.Fill;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(140, 366);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(281, 28);
            cmbStatus.TabIndex = 15;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(11, 492);
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
            // TransactionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 565);
            Controls.Add(tableLayoutPanel);
            Controls.Add(panelButtons);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TransactionForm";
            Padding = new Padding(11, 13, 11, 13);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transaction";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private ComboBox cmbAccount = null!;
        private ComboBox cmbCategory = null!;
        private NumericUpDown numAmount = null!;
        private DateTimePicker dtpDate = null!;
        private TextBox txtDescription = null!;
        private CheckBox chkRecurring = null!;
        private ComboBox cmbFrequency = null!;
        private ComboBox cmbStatus = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private TableLayoutPanel tableLayoutPanel = null!;
        private Label lblAccount = null!;
        private Label lblCategory = null!;
        private Label lblAmount = null!;
        private Label lblDate = null!;
        private Label lblDescription = null!;
        private Label lblRecurring = null!;
        private Label lblFrequency = null!;
        private Label lblStatus = null!;
        private Panel panelButtons = null!;
    }
}