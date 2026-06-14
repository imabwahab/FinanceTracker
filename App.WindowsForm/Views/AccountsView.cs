using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;
using App.WindowsForm.Forms;

namespace App.WindowsForm.Views
{
    public partial class AccountsView : UserControl
    {
        private const string CurrentBalanceColumn = "CurrentBalance";

        private readonly IAccountService _service;
        private bool _loaded = false;

        // Current (cleared) balance per account Id, refreshed alongside the grid.
        private Dictionary<string, decimal> _currentBalances = new Dictionary<string, decimal>();

        public AccountsView(IAccountService service)
        {
            ArgumentNullException.ThrowIfNull(service);

            _service = service;
            InitializeComponent();

            // Bind the DataGridView to the BindingSource
            dgvAccounts.DataSource = bindingSource1;

            // Auto-generated columns are rebuilt on every bind, so (re)add and
            // fill the computed "Current Balance" column once binding completes.
            dgvAccounts.DataBindingComplete += (s, e) => PopulateCurrentBalanceColumn();

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
        /// Public hook so MainForm can refresh balances after a transaction
        /// changes them in another view.
        /// </summary>
        public void ReloadData() => RefreshGrid();

        /// <summary>
        /// Refresh the DataGridView by fetching all accounts from the service.
        /// Balances are fetched first so they're ready when binding completes.
        /// </summary>
        private void RefreshGrid()
        {
            try
            {
                _currentBalances = _service.GetCurrentBalances();
                bindingSource1.DataSource = _service.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ensure the computed "Current Balance" column exists (it's unbound, so
        /// auto-column generation drops it on each rebind) and fill each row from
        /// the precomputed balances, matched by the row's account Id.
        /// </summary>
        private void PopulateCurrentBalanceColumn()
        {
            if (!dgvAccounts.Columns.Contains(CurrentBalanceColumn))
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = CurrentBalanceColumn,
                    HeaderText = "Current Balance",
                    ReadOnly = true
                };
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvAccounts.Columns.Add(column);

                // Place it right after Opening Balance for an at-a-glance comparison.
                if (dgvAccounts.Columns.Contains("OpeningBalance"))
                {
                    column.DisplayIndex = dgvAccounts.Columns["OpeningBalance"].DisplayIndex + 1;
                }
            }

            foreach (DataGridViewRow row in dgvAccounts.Rows)
            {
                if (row.DataBoundItem is Account account &&
                    _currentBalances.TryGetValue(account.Id, out var balance))
                {
                    row.Cells[CurrentBalanceColumn].Value = balance;
                }
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