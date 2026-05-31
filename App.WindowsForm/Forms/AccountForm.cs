using App.Core.Enums;
using App.Core.Models;

namespace App.WindowsForm.Forms
{
    /// <summary>
    /// Mode-driven form for Add/Edit/View Account.
    /// One form handles all three modes via AccountFormMode enum.
    /// Exposes Result property containing the populated Account after Save.
    /// </summary>
    public partial class AccountForm : Form
    {
        private readonly AccountFormMode _mode;
        private readonly Account? _input;

        /// <summary>
        /// After Save, the populated Account is exposed here.
        /// Caller reads this after ShowDialog() returns OK.
        /// </summary>
        public Account Result { get; private set; } = null!;

        /// <summary>
        /// Constructor accepts mode and optional input Account.
        /// For Add: pass null. For Edit/View: pass the Account to populate.
        /// </summary>
        public AccountForm(AccountFormMode mode, Account? input)
        {
            InitializeComponent();
            _mode = mode;
            _input = input;

            // Bind enum dropdowns
            cmbAccountType.DataSource = Enum.GetValues(typeof(AccountTypeEnum));
            cmbStatus.DataSource = Enum.GetValues(typeof(AccountStatusEnum));

            // Configure the form for the given mode
            ConfigureForMode();

            // If editing or viewing, populate the fields from input
            if (_input != null)
            {
                PopulateFields(_input);
            }

            // Wire up button click handlers
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        /// <summary>
        /// Configure form UI based on mode: button labels, field editability, title.
        /// </summary>
        private void ConfigureForMode()
        {
            switch (_mode)
            {
                case AccountFormMode.Add:
                    Text = "Add Account";
                    btnSave.Text = "Save";
                    break;

                case AccountFormMode.Edit:
                    Text = "Edit Account";
                    btnSave.Text = "Update";
                    break;

                case AccountFormMode.View:
                    Text = "View Account";
                    btnSave.Visible = false;
                    btnCancel.Text = "Close";
                    // Make all controls read-only
                    txtName.ReadOnly = true;
                    txtCurrency.ReadOnly = true;
                    numOpeningBalance.Enabled = false;
                    cmbAccountType.Enabled = false;
                    cmbStatus.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Populate form fields from an existing Account (for Edit/View modes).
        /// </summary>
        private void PopulateFields(Account account)
        {
            txtName.Text = account.Name;
            cmbAccountType.SelectedItem = account.AccountType;
            numOpeningBalance.Value = account.OpeningBalance;
            txtCurrency.Text = account.Currency ?? "PKR";
            cmbStatus.SelectedItem = account.AccountStatus;
        }

        /// <summary>
        /// Validate form data before saving.
        /// Returns true if valid, false if validation fails.
        /// </summary>
        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCurrency.Text))
            {
                MessageBox.Show("Currency is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrency.Focus();
                return false;
            }

            if (cmbAccountType.SelectedItem == null)
            {
                MessageBox.Show("Account Type is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAccountType.Focus();
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Status is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Save button click: validate, populate Result, and return OK.
        /// </summary>
        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            // For Edit: reuse the existing Account (preserves Id)
            // For Add: create new Account with required Name set
            if (_input != null)
            {
                // Edit mode: update existing Account
                Result = _input;
            }
            else
            {
                // Add mode: create new Account
                // Must initialize required Name to satisfy the compiler
                Result = new Account { Name = string.Empty };
            }

            // Populate form values into Result
            Result.Name = txtName.Text.Trim();
            Result.AccountType = (AccountTypeEnum)cmbAccountType.SelectedItem!;
            Result.OpeningBalance = numOpeningBalance.Value;
            Result.Currency = txtCurrency.Text.Trim();
            Result.AccountStatus = (AccountStatusEnum)cmbStatus.SelectedItem!;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}