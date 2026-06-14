namespace App.WindowsForm.Views
{
    partial class AccountsView
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
            dgvAccounts = new DataGridView();
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
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
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";

            // btnAdd
            btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnAdd.Image = null;
            btnAdd.ImageTransparentColor = Color.Magenta;
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(29, 22);
            btnAdd.Text = "Add";
            btnAdd.ToolTipText = "Add new account";

            // btnEdit
            btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnEdit.Image = null;
            btnEdit.ImageTransparentColor = Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(31, 22);
            btnEdit.Text = "Edit";
            btnEdit.ToolTipText = "Edit selected account";

            // btnView
            btnView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnView.Image = null;
            btnView.ImageTransparentColor = Color.Magenta;
            btnView.Name = "btnView";
            btnView.Size = new Size(33, 22);
            btnView.Text = "View";
            btnView.ToolTipText = "View selected account";

            // btnDelete
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnDelete.Image = null;
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(44, 22);
            btnDelete.Text = "Delete";
            btnDelete.ToolTipText = "Delete selected account";

            // btnRefresh
            btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefresh.Image = null;
            btnRefresh.ImageTransparentColor = Color.Magenta;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(52, 22);
            btnRefresh.Text = "Refresh";
            btnRefresh.ToolTipText = "Refresh data";

            // dgvAccounts
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AllowUserToDeleteRows = false;
            dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccounts.Dock = DockStyle.Fill;
            dgvAccounts.Location = new Point(0, 25);
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.ReadOnly = true;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.Size = new Size(800, 575);
            dgvAccounts.TabIndex = 1;

            // AccountsView
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvAccounts);
            Controls.Add(toolStrip1);
            Name = "AccountsView";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip toolStrip1;
        private ToolStripButton btnAdd;
        private ToolStripButton btnEdit;
        private ToolStripButton btnView;
        private ToolStripButton btnDelete;
        private ToolStripButton btnRefresh;
        private DataGridView dgvAccounts;
        private BindingSource bindingSource1;
    }
}