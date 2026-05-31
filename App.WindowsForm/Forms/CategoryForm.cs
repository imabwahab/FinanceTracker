using App.Core.Enums;
using App.Core.Models;

namespace App.WindowsForm.Forms
{
    /// <summary>
    /// Mode-driven form for Add/Edit/View Category.
    /// One form handles all three modes via CategoryFormMode enum.
    /// Includes nullable MonthlyBudget UI: checkbox toggles NumericUpDown.
    /// </summary>
    public partial class CategoryForm : Form
    {
        private readonly CategoryFormMode _mode;
        private readonly Category? _input;

        /// <summary>
        /// After Save, the populated Category is exposed here.
        /// </summary>
        public Category Result { get; private set; } = null!;

        /// <summary>
        /// Constructor accepts mode and optional input Category.
        /// </summary>
        public CategoryForm(CategoryFormMode mode, Category? input)
        {
            InitializeComponent();
            _mode = mode;
            _input = input;

            // Bind enum dropdowns
            cmbCategoryType.DataSource = Enum.GetValues(typeof(CategoryTypeEnum));
            cmbStatus.DataSource = Enum.GetValues(typeof(CategoryStatusEnum));

            // Configure the form for the given mode
            ConfigureForMode();

            // If editing or viewing, populate the fields from input
            if (_input != null)
            {
                PopulateFields(_input);
            }

            // Wire checkbox AFTER populating fields to avoid firing during initialization
            chkHasBudget.CheckedChanged += (s, e) => numMonthlyBudget.Enabled = chkHasBudget.Checked;

            // Wire up button click handlers
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        /// <summary>
        /// Configure form UI based on mode.
        /// </summary>
        private void ConfigureForMode()
        {
            switch (_mode)
            {
                case CategoryFormMode.Add:
                    Text = "Add Category";
                    btnSave.Text = "Save";
                    break;

                case CategoryFormMode.Edit:
                    Text = "Edit Category";
                    btnSave.Text = "Update";
                    break;

                case CategoryFormMode.View:
                    Text = "View Category";
                    btnSave.Visible = false;
                    btnCancel.Text = "Close";
                    // Make all controls read-only
                    txtName.ReadOnly = true;
                    cmbCategoryType.Enabled = false;
                    cmbStatus.Enabled = false;
                    chkHasBudget.Enabled = false;
                    numMonthlyBudget.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Populate form fields from an existing Category.
        /// </summary>
        private void PopulateFields(Category category)
        {
            txtName.Text = category.Name;
            cmbCategoryType.SelectedItem = category.CategoryType;
            cmbStatus.SelectedItem = category.CategoryStatus;

            // Handle nullable MonthlyBudget
            if (category.MonthlyBudget.HasValue)
            {
                chkHasBudget.Checked = true;
                numMonthlyBudget.Value = category.MonthlyBudget.Value;
            }
            else
            {
                chkHasBudget.Checked = false;
                numMonthlyBudget.Value = 0;
            }
        }

        /// <summary>
        /// Validate form data before saving.
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

            if (cmbCategoryType.SelectedItem == null)
            {
                MessageBox.Show("Category Type is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoryType.Focus();
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Status is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return false;
            }

            // Warn if budget enabled but value is 0
            if (chkHasBudget.Checked && numMonthlyBudget.Value == 0)
            {
                var result = MessageBox.Show(
                    "Monthly budget is set but value is 0. Continue?",
                    "Validation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
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

            // For Edit: reuse the existing Category (preserves Id)
            // For Add: create new Category with required Name set
            if (_input != null)
            {
                Result = _input;
            }
            else
            {
                Result = new Category { Name = string.Empty };
            }

            // Populate form values into Result
            Result.Name = txtName.Text.Trim();
            Result.CategoryType = (CategoryTypeEnum)cmbCategoryType.SelectedItem!;
            Result.CategoryStatus = (CategoryStatusEnum)cmbStatus.SelectedItem!;

            // Handle nullable MonthlyBudget
            Result.MonthlyBudget = chkHasBudget.Checked ? (decimal?)numMonthlyBudget.Value : null;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}