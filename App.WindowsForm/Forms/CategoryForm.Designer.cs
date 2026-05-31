#nullable enable

namespace App.WindowsForm.Forms
{
    partial class CategoryForm
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
            lblName = new Label();
            txtName = new TextBox();
            lblType = new Label();
            cmbCategoryType = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            chkHasBudget = new CheckBox();
            lblMonthlyBudget = new Label();
            numMonthlyBudget = new NumericUpDown();
            panelButtons = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMonthlyBudget).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();

            // tableLayoutPanel
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 1, 0);
            tableLayoutPanel.Controls.Add(lblType, 0, 1);
            tableLayoutPanel.Controls.Add(cmbCategoryType, 1, 1);
            tableLayoutPanel.Controls.Add(lblStatus, 0, 2);
            tableLayoutPanel.Controls.Add(cmbStatus, 1, 2);
            tableLayoutPanel.Controls.Add(chkHasBudget, 0, 3);
            tableLayoutPanel.Controls.Add(lblMonthlyBudget, 0, 4);
            tableLayoutPanel.Controls.Add(numMonthlyBudget, 1, 4);
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

            // lblName
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Fill;
            lblName.Location = new Point(3, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(131, 47);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            lblName.TextAlign = ContentAlignment.MiddleLeft;

            // txtName
            txtName.Dock = DockStyle.Fill;
            txtName.Location = new Point(140, 4);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(281, 27);
            txtName.TabIndex = 1;

            // lblType
            lblType.AutoSize = true;
            lblType.Dock = DockStyle.Fill;
            lblType.Location = new Point(3, 47);
            lblType.Name = "lblType";
            lblType.Size = new Size(131, 47);
            lblType.TabIndex = 2;
            lblType.Text = "Type:";
            lblType.TextAlign = ContentAlignment.MiddleLeft;

            // cmbCategoryType
            cmbCategoryType.Dock = DockStyle.Fill;
            cmbCategoryType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryType.Location = new Point(140, 51);
            cmbCategoryType.Margin = new Padding(3, 4, 3, 4);
            cmbCategoryType.Name = "cmbCategoryType";
            cmbCategoryType.Size = new Size(281, 28);
            cmbCategoryType.TabIndex = 3;

            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 94);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(131, 47);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status:";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            // cmbStatus
            cmbStatus.Dock = DockStyle.Fill;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(140, 98);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(281, 28);
            cmbStatus.TabIndex = 5;

            // chkHasBudget
            chkHasBudget.AutoSize = true;
            chkHasBudget.Location = new Point(140, 145);
            chkHasBudget.Margin = new Padding(3, 4, 3, 4);
            chkHasBudget.Name = "chkHasBudget";
            chkHasBudget.Size = new Size(170, 24);
            chkHasBudget.TabIndex = 6;
            chkHasBudget.Text = "Has monthly budget";
            chkHasBudget.UseVisualStyleBackColor = true;

            // lblMonthlyBudget
            lblMonthlyBudget.AutoSize = true;
            lblMonthlyBudget.Dock = DockStyle.Fill;
            lblMonthlyBudget.Location = new Point(3, 188);
            lblMonthlyBudget.Name = "lblMonthlyBudget";
            lblMonthlyBudget.Size = new Size(131, 60);
            lblMonthlyBudget.TabIndex = 7;
            lblMonthlyBudget.Text = "Monthly Budget:";
            lblMonthlyBudget.TextAlign = ContentAlignment.MiddleLeft;

            // numMonthlyBudget
            numMonthlyBudget.DecimalPlaces = 2;
            numMonthlyBudget.Dock = DockStyle.Fill;
            numMonthlyBudget.Enabled = false;
            numMonthlyBudget.Location = new Point(140, 192);
            numMonthlyBudget.Margin = new Padding(3, 4, 3, 4);
            numMonthlyBudget.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numMonthlyBudget.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numMonthlyBudget.Name = "numMonthlyBudget";
            numMonthlyBudget.Size = new Size(281, 27);
            numMonthlyBudget.TabIndex = 8;

            // panelButtons
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(11, 274);
            panelButtons.Margin = new Padding(3, 4, 3, 4);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(435, 60);
            panelButtons.TabIndex = 1;

            // btnSave
            btnSave.Location = new Point(240, 13);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 33);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;

            // btnCancel
            btnCancel.Location = new Point(337, 13);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 33);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;

            // CategoryForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 347);
            Controls.Add(tableLayoutPanel);
            Controls.Add(panelButtons);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CategoryForm";
            Padding = new Padding(11, 13, 11, 13);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Category";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMonthlyBudget).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TextBox txtName = null!;
        private ComboBox cmbCategoryType = null!;
        private ComboBox cmbStatus = null!;
        private CheckBox chkHasBudget = null!;
        private NumericUpDown numMonthlyBudget = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private TableLayoutPanel tableLayoutPanel = null!;
        private Label lblName = null!;
        private Label lblType = null!;
        private Label lblStatus = null!;
        private Label lblMonthlyBudget = null!;
        private Panel panelButtons = null!;
    }
}