using App.Core.Models;
using App.Core.Services;

namespace App.WindowsForm.Views
{
    public partial class AccountsView : UserControl
    {
        private readonly IAccountService _service;

        public AccountsView(IAccountService service)
        {
            ArgumentNullException.ThrowIfNull(service);

            _service = service;
            InitializeComponent();

            // Bind the DataGridView to the BindingSource
            dgvAccounts.DataSource = bindingSource1;

            // Wire toolbar buttons
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnView.Click += BtnView_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            // Load data when view is first displayed
            Load += (s, e) => RefreshGrid();
        }

        /// <summary>
        /// Refresh the DataGridView by fetching all accounts from the service.
        /// </summary>
        private void RefreshGrid()
        {
            try
            {
                bindingSource1.DataSource = _service.GetAll();
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
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Implement AccountForm in M5.2
                // using (var f = new AccountForm(AccountFormMode.Add, null))
                // {
                //     if (f.ShowDialog() == DialogResult.OK)
                //     {
                //         _service.Add(f.Result);
                //         RefreshGrid();
                //     }
                // }
                MessageBox.Show("Add functionality will be available in M5.2 (AccountForm).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Edit button click — open AccountForm in Edit mode.
        /// </summary>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var selected = SelectedAccount;
                if (selected == null)
                {
                    MessageBox.Show("Select an account first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // TODO: Implement AccountForm in M5.2
                // using (var f = new AccountForm(AccountFormMode.Edit, selected))
                // {
                //     if (f.ShowDialog() == DialogResult.OK)
                //     {
                //         _service.Update(f.Result);
                //         RefreshGrid();
                //     }
                // }
                MessageBox.Show("Edit functionality will be available in M5.2 (AccountForm).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// View button click — open AccountForm in View mode (read-only).
        /// </summary>
        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                var selected = SelectedAccount;
                if (selected == null)
                {
                    MessageBox.Show("Select an account first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // TODO: Implement AccountForm in M5.2
                // using (var f = new AccountForm(AccountFormMode.View, selected))
                // {
                //     f.ShowDialog();  // View-only: don't care about result
                // }
                MessageBox.Show($"Viewing Account:\nID: {selected.Id}\nName: {selected.Name}\nType: {selected.AccountType}\nBalance: {selected.OpeningBalance}", "View Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Delete button click — prompt for confirmation and delete if confirmed.
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
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
                    _service.Delete(selected.Id);
                    RefreshGrid();
                    MessageBox.Show("Account deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}