namespace App.WindowsForm.Views
{
    partial class TransactionsView
    {
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            toolStrip1 = new ToolStrip();
            btnAdd = new ToolStripButton();
            btnEdit = new ToolStripButton();
            btnView = new ToolStripButton();
            btnDelete = new ToolStripButton();
            btnRefresh = new ToolStripButton();
            pnlFilter = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblAccount = new Label();
            cmbFilterAccount = new ComboBox();
            lblCategory = new Label();
            cmbFilterCategory = new ComboBox();
            lblStatus = new Label();
            cmbFilterStatus = new ComboBox();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            btnApply = new Button();
            btnClear = new Button();
            dgvTransactions = new DataGridView();
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit(); 
            pnlFilter.SuspendLayout();
            SuspendLayout();

            // toolStrip1
            toolStrip1.Items.AddRange(new ToolStripItem[]
            {
                btnAdd,
                btnEdit,
                btnView,
                btnDelete,
                new ToolStripSeparator(),
                btnRefresh
            });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1000, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";

            // btnAdd
            btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnAdd.Image = null;
            btnAdd.ImageTransparentColor = Color.Magenta;
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(29, 22);
            btnAdd.Text = "Add";
            btnAdd.ToolTipText = "Add new transaction";

            // btnEdit
            btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnEdit.Image = null;
            btnEdit.ImageTransparentColor = Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(31, 22);
            btnEdit.Text = "Edit";
            btnEdit.ToolTipText = "Edit selected transaction";

            // btnView
            btnView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnView.Image = null;
            btnView.ImageTransparentColor = Color.Magenta;
            btnView.Name = "btnView";
            btnView.Size = new Size(33, 22);
            btnView.Text = "View";
            btnView.ToolTipText = "View selected transaction";

            // btnDelete
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnDelete.Image = null;
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(44, 22);
            btnDelete.Text = "Delete";
            btnDelete.ToolTipText = "Delete selected transaction";

            // btnRefresh
            btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefresh.Image = null;
            btnRefresh.ImageTransparentColor = Color.Magenta;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(52, 22);
            btnRefresh.Text = "Refresh";
            btnRefresh.ToolTipText = "Refresh data";

            // pnlFilter
            pnlFilter.Controls.Add(lblSearch);
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(lblAccount);
            pnlFilter.Controls.Add(cmbFilterAccount);
            pnlFilter.Controls.Add(lblCategory);
            pnlFilter.Controls.Add(cmbFilterCategory);
            pnlFilter.Controls.Add(lblStatus);
            pnlFilter.Controls.Add(cmbFilterStatus);
            pnlFilter.Controls.Add(lblFrom);
            pnlFilter.Controls.Add(dtpFrom);
            pnlFilter.Controls.Add(lblTo);
            pnlFilter.Controls.Add(dtpTo);
            pnlFilter.Controls.Add(btnApply);
            pnlFilter.Controls.Add(btnClear);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 25);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1000, 60);
            pnlFilter.TabIndex = 1;
            pnlFilter.BackColor = Color.FromArgb(245, 245, 245);

            // lblSearch
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(10, 10);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(47, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";

            // txtSearch
            txtSearch.Location = new Point(60, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search description...";
            txtSearch.Size = new Size(120, 23);
            txtSearch.TabIndex = 1;

            // lblAccount
            lblAccount.AutoSize = true;
            lblAccount.Location = new Point(190, 10);
            lblAccount.Name = "lblAccount";
            lblAccount.Size = new Size(57, 15);
            lblAccount.TabIndex = 2;
            lblAccount.Text = "Account:";

            // cmbFilterAccount
            cmbFilterAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterAccount.Location = new Point(250, 8);
            cmbFilterAccount.Name = "cmbFilterAccount";
            cmbFilterAccount.Size = new Size(110, 23);
            cmbFilterAccount.TabIndex = 3;

            // lblCategory
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(370, 10);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(63, 15);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "Category:";

            // cmbFilterCategory
            cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterCategory.Location = new Point(435, 8);
            cmbFilterCategory.Name = "cmbFilterCategory";
            cmbFilterCategory.Size = new Size(110, 23);
            cmbFilterCategory.TabIndex = 5;

            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(555, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(43, 15);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status:";

            // cmbFilterStatus
            cmbFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterStatus.Location = new Point(600, 8);
            cmbFilterStatus.Name = "cmbFilterStatus";
            cmbFilterStatus.Size = new Size(80, 23);
            cmbFilterStatus.TabIndex = 7;

            // lblFrom
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(690, 10);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(35, 15);
            lblFrom.TabIndex = 8;
            lblFrom.Text = "From:";

            // dtpFrom
            dtpFrom.Location = new Point(728, 8);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(90, 23);
            dtpFrom.TabIndex = 9;

            // lblTo
            lblTo.AutoSize = true;
            lblTo.Location = new Point(10, 38);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(22, 15);
            lblTo.TabIndex = 10;
            lblTo.Text = "To:";

            // dtpTo
            dtpTo.Location = new Point(60, 36);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(90, 23);
            dtpTo.TabIndex = 11;

            // btnApply
            btnApply.Location = new Point(160, 36);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(70, 23);
            btnApply.TabIndex = 12;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;

            // btnClear
            btnClear.Location = new Point(240, 36);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(70, 23);
            btnClear.TabIndex = 13;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;

            // dgvTransactions
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.AllowUserToDeleteRows = false;
            dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransactions.Dock = DockStyle.Fill;
            dgvTransactions.Location = new Point(0, 85);
            dgvTransactions.Name = "dgvTransactions";
            dgvTransactions.ReadOnly = true;
            dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransactions.Size = new Size(1000, 415);
            dgvTransactions.TabIndex = 2;

            // TransactionsView

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvTransactions);
            Controls.Add(pnlFilter);
            Controls.Add(toolStrip1);
            Name = "TransactionsView";
            Size = new Size(1000, 500);
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip toolStrip1;
        private ToolStripButton btnAdd;
        private ToolStripButton btnEdit;
        private ToolStripButton btnView;
        private ToolStripButton btnDelete;
        private ToolStripButton btnRefresh;
        private Panel pnlFilter;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblAccount;
        private ComboBox cmbFilterAccount;
        private Label lblCategory;
        private ComboBox cmbFilterCategory;
        private Label lblStatus;
        private ComboBox cmbFilterStatus;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Button btnApply;
        private Button btnClear;
        private DataGridView dgvTransactions;
        private BindingSource bindingSource1;
    }
}