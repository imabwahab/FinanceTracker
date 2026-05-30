using System.Configuration;
using App.Core.Services;

namespace App.WindowsForm
{
    public partial class MainForm : Form
    {
        // Service fields typed as interfaces (dependency inversion)
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;

        private Button? _activeButton;
        private readonly Color _activeColor = Color.FromArgb(200, 220, 255);      // Light blue
        private readonly Color _activeTextColor = Color.FromArgb(0, 102, 204);    // Blue text
        private readonly Color _inactiveColor = Color.FromArgb(240, 240, 240);    // Light gray
        private readonly Color _inactiveTextColor = Color.FromArgb(33, 33, 33);   // Dark text

        // Cache fonts to avoid repeated allocations
        private Font? _regularFont;
        private Font? _boldFont;

        /// <summary>
        /// Parameterless constructor for WinForms designer.
        /// Reads connection string from App.config and creates services.
        /// </summary>
        public MainForm() : this(CreateDefaultServices())
        {
        }

        /// <summary>
        /// Constructor with dependency injection for services.
        /// Allows for testing and flexible service implementations.
        /// </summary>
        public MainForm(IAccountService accountService, ICategoryService categoryService, ITransactionService transactionService)
        {
            ArgumentNullException.ThrowIfNull(accountService);
            ArgumentNullException.ThrowIfNull(categoryService);
            ArgumentNullException.ThrowIfNull(transactionService);

            _accountService = accountService;
            _categoryService = categoryService;
            _transactionService = transactionService;

            InitializeComponent();
            InitializeUI();
        }

        /// <summary>
        /// Factory method to create default services from App.config.
        /// </summary>
        private static (IAccountService, ICategoryService, ITransactionService) CreateDefaultServices()
        {
            // Read connection string from App.config - ONE source of truth
            string? connStr = ConfigurationManager.ConnectionStrings["FinanceTrackerDB"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ConfigurationErrorsException(
                    "Missing or invalid connection string 'FinanceTrackerDB' in App.config. " +
                    "Ensure App.config contains: <add name=\"FinanceTrackerDB\" connectionString=\"...\" />");
            }

            // Build services once. Concrete types only mentioned here.
            var accountService = new DbAccountService(connStr);
            var categoryService = new DbCategoryService(connStr);
            var transactionService = new DbTransactionService(connStr);

            return (accountService, categoryService, transactionService);
        }

        /// <summary>
        /// Initialize UI components (fonts, icons, sidebar setup).
        /// </summary>
        private void InitializeUI()
        {
            InitializeFonts();
            LoadButtonIcons();
            SetupSidebarTabs();
            SelectTab(btnDashboard);
        }

        private void InitializeFonts()
        {
            if (btnDashboard.Font != null)
            {
                _regularFont = new Font(btnDashboard.Font, FontStyle.Regular);
                _boldFont = new Font(btnDashboard.Font, FontStyle.Bold);
            }
        }

        private void LoadButtonIcons()
        {
            try
            {
                // Get the application's base directory
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string assetsPath = Path.Combine(basePath, "Assets");

                // Load and assign icons, then release file handles immediately
                AssignButtonIcon(btnDashboard, Path.Combine(assetsPath, "dashboard.png"));
                AssignButtonIcon(btnAccounts, Path.Combine(assetsPath, "accounts.png"));
                AssignButtonIcon(btnCategories, Path.Combine(assetsPath, "categories.png"));
                AssignButtonIcon(btnTransaction, Path.Combine(assetsPath, "transactions.png"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading icons: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void AssignButtonIcon(Button button, string imagePath)
        {
            if (File.Exists(imagePath))
            {
                // Load bitmap into memory, assign it, then release the file
                using (var bitmap = new Bitmap(imagePath))
                {
                    // Clone the bitmap so the file can be released
                    button.Image = (Bitmap)bitmap.Clone();
                }
            }
        }

        private void SetupSidebarTabs()
        {
            var buttons = new[] { btnDashboard, btnAccounts, btnCategories, btnTransaction };

            foreach (var btn in buttons)
            {
                // Hover effect
                btn.MouseEnter += (s, e) =>
                {
                    if (btn != _activeButton)
                    {
                        btn.BackColor = Color.FromArgb(220, 220, 220);
                    }
                };

                btn.MouseLeave += (s, e) =>
                {
                    if (btn != _activeButton)
                    {
                        btn.BackColor = _inactiveColor;
                    }
                };
            }
        }

        private void SelectTab(Button? selectedButton)
        {
            if (selectedButton == null)
                return;

            var buttons = new[] { btnDashboard, btnAccounts, btnCategories, btnTransaction };

            // Reset all buttons to inactive state
            foreach (var btn in buttons)
            {
                btn.BackColor = _inactiveColor;
                btn.ForeColor = _inactiveTextColor;
                btn.Font = _regularFont;
                btn.FlatAppearance.BorderSize = 0;
            }

            // Set selected button to active state
            _activeButton = selectedButton;
            selectedButton.BackColor = _activeColor;
            selectedButton.ForeColor = _activeTextColor;
            selectedButton.Font = _boldFont;

            // Add a left border effect
            selectedButton.FlatAppearance.BorderColor = Color.FromArgb(0, 102, 204);
            selectedButton.FlatAppearance.BorderSize = 3;
        }

        private void BtnDashboard_Click(object? sender, EventArgs e)
        {
            SelectTab(btnDashboard);
            pnlContent.Controls.Clear();
            // Load Dashboard UserControl here
            // pnlContent.Controls.Add(new DashboardControl());
        }

        private void BtnAccounts_Click(object? sender, EventArgs e)
        {
            SelectTab(btnAccounts);
            pnlContent.Controls.Clear();
            // TODO: Pass _accountService to AccountsView
            // pnlContent.Controls.Add(new AccountsView(_accountService));
        }

        private void BtnCategories_Click(object? sender, EventArgs e)
        {
            SelectTab(btnCategories);
            pnlContent.Controls.Clear();
            // TODO: Pass _categoryService to CategoriesView
            // pnlContent.Controls.Add(new CategoriesView(_categoryService));
        }

        private void BtnTransaction_Click(object? sender, EventArgs e)
        {
            SelectTab(btnTransaction);
            pnlContent.Controls.Clear();
            // TODO: Pass _transactionService to TransactionsView
            // pnlContent.Controls.Add(new TransactionsView(_transactionService));
        }
    }
}