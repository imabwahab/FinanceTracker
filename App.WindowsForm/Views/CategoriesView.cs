using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;
using App.WindowsForm.Forms;

namespace App.WindowsForm.Views
{
    /// <summary>
    /// List view for Categories with Add/Edit/View/Delete/Refresh toolbar.
    /// Mirrors AccountsView pattern; uses CategoryForm for modal dialogs.
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        private readonly ICategoryService _service;
        private bool _loaded = false;

        public CategoriesView(ICategoryService service)
        {
            ArgumentNullException.ThrowIfNull(service);
            
            _service = service;
            InitializeComponent();

            // Bind the DataGridView to the BindingSource
            dgvCategories.DataSource = bindingSource1;

            // Wire toolbar buttons
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnView.Click += BtnView_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            // Load data when view is first displayed (only once)
            Load += (s, e) =>
            {
                if (!_loaded)
                {
                    _loaded = true;
                    RefreshGrid();
                }
            };
        }

        private void RefreshGrid()
        {
            try
            {
                bindingSource1.DataSource = _service.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Category? SelectedCategory => bindingSource1.Current as Category;

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var f = new CategoryForm(CategoryFormMode.Add, null))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _service.Add(f.Result);
                        RefreshGrid();
                        MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        (ParentForm as MainForm)?.RefreshDashboardIfCached();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedCategory;
                if (selected == null)
                {
                    MessageBox.Show("Select a category first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var f = new CategoryForm(CategoryFormMode.Edit, selected))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (_service.Update(f.Result))
                        {
                            RefreshGrid();
                            MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            (ParentForm as MainForm)?.RefreshDashboardIfCached();
                        }
                        else
                        {
                            RefreshGrid();
                            MessageBox.Show("Category could not be updated (it may have been removed).", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnView_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedCategory;
                if (selected == null)
                {
                    MessageBox.Show("Select a category first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var f = new CategoryForm(CategoryFormMode.View, selected))
                {
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedCategory;
                if (selected == null)
                {
                    MessageBox.Show("Select a category first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Delete category '{selected.Name}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (_service.Delete(selected.Id))
                    {
                        RefreshGrid();
                        MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        (ParentForm as MainForm)?.RefreshDashboardIfCached();
                    }
                    else
                    {
                        RefreshGrid();
                        MessageBox.Show("Category could not be deleted (it may have been removed).", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}