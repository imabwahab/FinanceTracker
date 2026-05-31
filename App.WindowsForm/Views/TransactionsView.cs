using App.Core.Models;
using App.Core.Services;
using App.WindowsForm.Forms;

namespace App.WindowsForm.Views
{
    public partial class TransactionsView : UserControl
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private bool _loaded = false;

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
        /// Refresh the DataGridView by fetching all transactions from the service.
        /// </summary>
        private void RefreshGrid()
        {
            try
            {
                bindingSource1.DataSource = _transactionService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get the currently selected transaction from the BindingSource.
        /// Returns null if no selection.
        /// </summary>
        private Transaction? SelectedTransaction => bindingSource1.Current as Transaction;

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh button click — reload data from service.
        /// </summary>
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}