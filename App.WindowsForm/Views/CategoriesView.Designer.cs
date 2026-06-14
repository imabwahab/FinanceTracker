namespace App.WindowsForm.Views
{
    partial class CategoriesView
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
            dgvCategories = new DataGridView();
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
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

            // Buttons
            btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnAdd.Name = "btnAdd";
            btnAdd.Text = "Add";
            btnAdd.ToolTipText = "Add new category";

            btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnEdit.Name = "btnEdit";
            btnEdit.Text = "Edit";
            btnEdit.ToolTipText = "Edit selected category";

            btnView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnView.Name = "btnView";
            btnView.Text = "View";
            btnView.ToolTipText = "View selected category";

            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnDelete.Name = "btnDelete";
            btnDelete.Text = "Delete";
            btnDelete.ToolTipText = "Delete selected category";

            btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Text = "Refresh";
            btnRefresh.ToolTipText = "Refresh data";

            // dgvCategories
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.Location = new Point(0, 25);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(800, 575);
            dgvCategories.TabIndex = 1;

            // CategoriesView
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvCategories);
            Controls.Add(toolStrip1);
            Name = "CategoriesView";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip toolStrip1 = null!;
        private ToolStripButton btnAdd = null!;
        private ToolStripButton btnEdit = null!;
        private ToolStripButton btnView = null!;
        private ToolStripButton btnDelete = null!;
        private ToolStripButton btnRefresh = null!;
        private DataGridView dgvCategories = null!;
        private BindingSource bindingSource1 = null!;
    }
}   