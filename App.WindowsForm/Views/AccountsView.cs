using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;
using App.WindowsForm.Forms;

namespace App.WindowsForm.Views
{
    public partial class AccountsView : UserControl, IRecordCountSource
    {
        private readonly IAccountService _service;
        private bool _loaded = false;

        /// <summary>Number of rows currently shown in the grid (for the status bar).</summary>
        public int RecordCount => bindingSource1.Count;

        public AccountsView(IAccountService service)
        {
            ArgumentNullException.ThrowIfNull(service);

            _service = service;
            InitializeComponent();

            // Bind the DataGridView to the BindingSource
            dgvAccounts.DataSource = bindingSource1;

            // Enable client-side column sorting once columns are generated.
            dgvAccounts.DataBindingComplete += (s, e) => GridSorting.EnableColumnSorting(dgvAccounts);

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

        /// <summary>
        /// Refresh the DataGridView by fetching all accounts from the service.
        /// </summary>
        private void RefreshGrid()
        {
            try
            {
                // Wrap in SortableBindingList so DataGridView headers can sort in memory.
                bindingSource1.DataSource = new SortableBindingList<Account>(_service.GetAll());
                (ParentForm as MainForm)?.UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get the currently selected account from the BindingSource.
        /// Returns null if no selection.
        /// </summary>
        private Account? SelectedAccount => bindingSource1.Current as Account;

        /// <summary>
        /// Add button click — open AccountForm in Add mode.
        /// </summary>
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var f = new AccountForm(AccountFormMode.Add, null))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _service.Add(f.Result);
                        RefreshGrid();
                        MessageBox.Show("Account added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        (ParentForm as MainForm)?.RefreshDashboardIfCached();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Edit button click — open AccountForm in Edit mode.
        /// </summary>
        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedAccount;
                if (selected == null)
                {
                    MessageBox.Show("Select an account first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var f = new AccountForm(AccountFormMode.Edit, selected))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        // Check if update was successful
                        if (_service.Update(f.Result))
                        {
                            RefreshGrid();
                            MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            (ParentForm as MainForm)?.RefreshDashboardIfCached();
                        }
                        else
                        {
                            RefreshGrid();
                            MessageBox.Show("Account could not be updated (it may have been removed).", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// View button click — open AccountForm in View mode (read-only).
        /// </summary>
        private void BtnView_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedAccount;
                if (selected == null)
                {
                    MessageBox.Show("Select an account first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var f = new AccountForm(AccountFormMode.View, selected))
                {
                    f.ShowDialog();  // View-only: don't care about result
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Delete button click — prompt for confirmation and delete if confirmed.
        /// </summary>
        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedAccount;
                if (selected == null)
                {
                    MessageBox.Show("Select an account first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Delete account '{selected.Name}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (_service.Delete(selected.Id))
                    {
                        RefreshGrid();
                        MessageBox.Show("Account deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        (ParentForm as MainForm)?.RefreshDashboardIfCached();
                    }
                    else
                    {
                        RefreshGrid();
                        MessageBox.Show("Account could not be deleted (it may have been removed).", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh button click — reload the grid.
        /// </summary>
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}