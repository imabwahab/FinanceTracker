using App.Core.Enums;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsForm.Forms
{
    /// <summary>
    /// Mode-driven form for Add/Edit/View Transaction.
    /// One form handles all three modes via TransactionFormMode enum.
    /// Takes three services to populate Account and Category dropdowns.
    /// Exposes Result property containing the populated Transaction after Save.
    /// </summary>
    public partial class TransactionForm : Form
    {
        private readonly TransactionFormMode _mode;
        private readonly Transaction? _input;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// After Save, the populated Transaction is exposed here.
        /// Caller reads this after ShowDialog() returns OK.
        /// </summary>
        public Transaction Result { get; private set; } = null!;

        /// <summary>
        /// Constructor accepts mode, optional input Transaction, and two services for dropdowns.
        /// For Add: pass null as input. For Edit/View: pass the Transaction to populate.
        /// </summary>
        public TransactionForm(TransactionFormMode mode, Transaction? input, IAccountService accountService, ICategoryService categoryService)
        {
            ArgumentNullException.ThrowIfNull(accountService);
            ArgumentNullException.ThrowIfNull(categoryService);

            InitializeComponent();
            _mode = mode;
            _input = input;
            _accountService = accountService;
            _categoryService = categoryService;

            // Populate Account dropdown
            try
            {
                var accounts = _accountService.GetAll();
                cmbAccount.DataSource = accounts;
                cmbAccount.DisplayMember = "Name";
                cmbAccount.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Populate Category dropdown
            try
            {
                var categories = _categoryService.GetAll();
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Configure numeric field for Amount (2 decimal places)
            numAmount.DecimalPlaces = 2;
            numAmount.Maximum = 999999999;
            numAmount.Minimum = 0;

            // Bind Status enum dropdown
            cmbStatus.DataSource = Enum.GetValues(typeof(TransactionStatusEnum));

            // Bind Frequency enum dropdown
            cmbFrequency.DataSource = Enum.GetValues(typeof(RecurringFrequencyEnum));

            // Wire recurring checkbox to enable/disable frequency dropdown
            chkRecurring.CheckedChanged += ChkRecurring_CheckedChanged;

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
        /// Handle recurring checkbox change to enable/disable frequency dropdown.
        /// </summary>
        private void ChkRecurring_CheckedChanged(object? sender, EventArgs e)
        {
            cmbFrequency.Enabled = chkRecurring.Checked;
        }

        /// <summary>
        /// Configure form UI based on mode: button labels, field editability, title.
        /// </summary>
        private void ConfigureForMode()
        {
            switch (_mode)
            {
                case TransactionFormMode.Add:
                    Text = "Add Transaction";
                    btnSave.Text = "Save";
                    break;

                case TransactionFormMode.Edit:
                    Text = "Edit Transaction";
                    btnSave.Text = "Update";
                    break;

                case TransactionFormMode.View:
                    Text = "View Transaction";
                    btnSave.Visible = false;
                    btnCancel.Text = "Close";
                    // Make all controls read-only
                    cmbAccount.Enabled = false;
                    cmbCategory.Enabled = false;
                    numAmount.Enabled = false;
                    dtpDate.Enabled = false;
                    txtDescription.ReadOnly = true;
                    chkRecurring.Enabled = false;
                    cmbFrequency.Enabled = false;
                    cmbStatus.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Populate form fields from an existing Transaction (for Edit/View modes).
        /// </summary>
        private void PopulateFields(Transaction transaction)
        {
            cmbAccount.SelectedValue = transaction.AccountId;
            cmbCategory.SelectedValue = transaction.CategoryId;
            numAmount.Value = transaction.Amount;
            dtpDate.Value = transaction.TransactionDate;
            txtDescription.Text = transaction.Description ?? string.Empty;
            chkRecurring.Checked = transaction.IsRecurring;
            if (transaction.RecurringFrequency.HasValue)
            {
                cmbFrequency.SelectedItem = transaction.RecurringFrequency.Value;
            }
            cmbStatus.SelectedItem = transaction.TransactionStatus;
        }

        /// <summary>
        /// Validate form data before saving.
        /// Returns true if valid, false if validation fails.
        /// </summary>
        private bool ValidateData()
        {
            if (cmbAccount.SelectedItem == null)
            {
                MessageBox.Show("Account is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAccount.Focus();
                return false;
            }

            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Category is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (numAmount.Value <= 0)
            {
                MessageBox.Show("Amount must be greater than 0.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAmount.Focus();
                return false;
            }

            if (chkRecurring.Checked && cmbFrequency.SelectedItem == null)
            {
                MessageBox.Show("Recurring Frequency is required when Recurring is enabled.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFrequency.Focus();
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

            // For Edit: reuse the existing Transaction (preserves Id)
            // For Add: create new Transaction with required fields set
            if (_input != null)
            {
                // Edit mode: update existing Transaction
                Result = _input;
            }
            else
            {
                // Add mode: create new Transaction
                // Must initialize required fields to satisfy the compiler
                Result = new Transaction { AccountId = string.Empty, CategoryId = string.Empty };
            }

            // Populate form values into Result
            Result.AccountId = (string)cmbAccount.SelectedValue!;
            Result.CategoryId = (string)cmbCategory.SelectedValue!;
            Result.Amount = numAmount.Value;
            Result.TransactionDate = dtpDate.Value.Date;
            Result.Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
            Result.IsRecurring = chkRecurring.Checked;
            Result.RecurringFrequency = chkRecurring.Checked
                ? (RecurringFrequencyEnum?)cmbFrequency.SelectedItem
                : null;
            Result.TransactionStatus = (TransactionStatusEnum)cmbStatus.SelectedItem!;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}