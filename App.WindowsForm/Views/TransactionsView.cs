using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;
using App.WindowsForm.Forms;
using App.Core.Enums;

namespace App.WindowsForm.Views
{
    public partial class TransactionsView : UserControl, IRecordCountSource
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private bool _loaded = false;

        /// <summary>Number of rows currently shown in the grid (for the status bar).</summary>
        public int RecordCount => bindingSource1.Count;

        public TransactionsView(ITransactionService transactionService, IAccountService accountService, ICategoryService categoryService)
        {
            ArgumentNullException.ThrowIfNull(transactionService);
            ArgumentNullException.ThrowIfNull(accountService);
            ArgumentNullException.ThrowIfNull(categoryService);

            _transactionService = transactionService;
            _accountService = accountService;
            _categoryService = categoryService;

            InitializeComponent();

            // Bind the DataGridView to the BindingSource
            dgvTransactions.DataSource = bindingSource1;

            // Enable client-side column sorting once columns are generated.
            dgvTransactions.DataBindingComplete += (s, e) => GridSorting.EnableColumnSorting(dgvTransactions);

            // Wire toolbar buttons
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnView.Click += BtnView_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            // Wire filter buttons
            btnApply.Click += BtnApply_Click;
            btnClear.Click += BtnClear_Click;

            // Load data when view is first displayed (only once).
            // Async so the grid loads without freezing the UI thread.
            Load += async (s, e) =>
            {
                if (!_loaded)
                {
                    _loaded = true;
                    InitializeFilters();
                    await RefreshGridAsync();
                }
            };
        }

        /// <summary>
        /// Initialize filter dropdowns with data from services.
        /// </summary>
        private void InitializeFilters()
        {
            try
            {
                // Initialize Account filter
                var accounts = _accountService.GetAll();
                var accountList = new List<(string Id, string Name)>
                {
                    ("", "All Accounts")
                };
                accountList.AddRange(accounts.Select(a => (a.Id, a.Name)));
                cmbFilterAccount.DataSource = accountList;
                cmbFilterAccount.DisplayMember = "Name";
                cmbFilterAccount.ValueMember = "Id";
                cmbFilterAccount.SelectedIndex = 0;

                // Initialize Category filter
                var categories = _categoryService.GetAll();
                var categoryList = new List<(string Id, string Name)>
                {
                    ("", "All Categories")
                };
                categoryList.AddRange(categories.Select(c => (c.Id, c.Name)));
                cmbFilterCategory.DataSource = categoryList;
                cmbFilterCategory.DisplayMember = "Name";
                cmbFilterCategory.ValueMember = "Id";
                cmbFilterCategory.SelectedIndex = 0;

                // Initialize Status filter
                var statusList = new List<(TransactionStatusEnum? Status, string Name)>
                {
                    (null, "All Statuses"),
                    (TransactionStatusEnum.Cleared, "Cleared"),
                    (TransactionStatusEnum.Pending, "Pending")
                };
                cmbFilterStatus.DataSource = statusList;
                cmbFilterStatus.DisplayMember = "Name";
                cmbFilterStatus.ValueMember = "Status";
                cmbFilterStatus.SelectedIndex = 0;

                // Initialize date pickers to a reasonable range (past 12 months to today)
                dtpTo.Value = DateTime.UtcNow.Date;
                dtpFrom.Value = DateTime.UtcNow.Date.AddMonths(-12);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing filters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh the DataGridView by fetching all transactions from the service.
        /// Used by the synchronous mutation flows (Add/Edit/Delete/Clear).
        /// </summary>
        private void RefreshGrid()
        {
            try
            {
                // Wrap in SortableBindingList so DataGridView headers can sort in memory.
                bindingSource1.DataSource = new SortableBindingList<Transaction>(_transactionService.GetAll());
                (ParentForm as MainForm)?.UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Async refresh used for the (potentially large) full transaction list.
        /// Shows a wait cursor and disables Refresh while the DB call is in flight,
        /// so the UI thread stays responsive and the button can't double-fire.
        /// </summary>
        private async Task RefreshGridAsync()
        {
            btnRefresh.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                var data = await _transactionService.GetAllAsync();

                // Wrap in SortableBindingList so DataGridView headers can sort in memory.
                bindingSource1.DataSource = new SortableBindingList<Transaction>(data);
                (ParentForm as MainForm)?.UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnRefresh.Enabled = true;
            }
        }

        /// <summary>
        /// Get the currently selected transaction from the BindingSource.
        /// Returns null if no selection.
        /// </summary>
        private Transaction? SelectedTransaction => bindingSource1.Current as Transaction;

        /// <summary>
        /// Apply filters: extract values from controls and call Search.
        /// </summary>
        private void BtnApply_Click(object? sender, EventArgs e)
        {
            try
            {
                // Extract filter values
                string? searchText = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
                string? accountId = cmbFilterAccount.SelectedIndex == 0 ? null : (string)cmbFilterAccount.SelectedValue!;
                string? categoryId = cmbFilterCategory.SelectedIndex == 0 ? null : (string)cmbFilterCategory.SelectedValue!;
                TransactionStatusEnum? status = cmbFilterStatus.SelectedIndex == 0
                    ? (TransactionStatusEnum?)null
                    : (TransactionStatusEnum)cmbFilterStatus.SelectedValue!;

                // Call Search with the filter parameters
                var results = _transactionService.Search(
                    searchText,
                    accountId,
                    categoryId,
                    dtpFrom.Value,
                    dtpTo.Value,
                    status);

                // Wrap in SortableBindingList so filtered results stay sortable too.
                bindingSource1.DataSource = new SortableBindingList<Transaction>(results);
                (ParentForm as MainForm)?.UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clear all filters and reset to default view.
        /// </summary>
        private void BtnClear_Click(object? sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                cmbFilterAccount.SelectedIndex = 0;
                cmbFilterCategory.SelectedIndex = 0;
                cmbFilterStatus.SelectedIndex = 0;
                dtpFrom.Value = DateTime.UtcNow.Date.AddMonths(-12);
                dtpTo.Value = DateTime.UtcNow.Date;

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing filters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Add button click — open TransactionForm in Add mode.
        /// </summary>
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var f = new TransactionForm(TransactionFormMode.Add, null, _accountService, _categoryService))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _transactionService.Add(f.Result);
                        RefreshGrid();
                        MessageBox.Show("Transaction added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Edit button click — open TransactionForm in Edit mode.
        /// </summary>
        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedTransaction;
                if (selected == null)
                {
                    MessageBox.Show("Select a transaction first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var f = new TransactionForm(TransactionFormMode.Edit, selected, _accountService, _categoryService))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        // Check if update was successful
                        if (_transactionService.Update(f.Result))
                        {
                            RefreshGrid();
                            MessageBox.Show("Transaction updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            (ParentForm as MainForm)?.RefreshDashboardIfCached();
                        }
                        else
                        {
                            RefreshGrid();
                            MessageBox.Show("Transaction could not be updated (it may have been removed).", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// View button click — open TransactionForm in View mode (read-only).
        /// </summary>
        private void BtnView_Click(object? sender, EventArgs e)
        {
            try
            {
                var selected = SelectedTransaction;
                if (selected == null)
                {
                    MessageBox.Show("Select a transaction first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var f = new TransactionForm(TransactionFormMode.View, selected, _accountService, _categoryService))
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
                var selected = SelectedTransaction;
                if (selected == null)
                {
                    MessageBox.Show("Select a transaction first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete this transaction?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _transactionService.Delete(selected.Id);
                    RefreshGrid();
                    MessageBox.Show("Transaction deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    (ParentForm as MainForm)?.RefreshDashboardIfCached();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh button click — reload data from service asynchronously.
        /// async void is acceptable here because this is a top-level event handler.
        /// </summary>
        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            await RefreshGridAsync();
        }
    }
}