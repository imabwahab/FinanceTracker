using System.Configuration;
using App.Core.Enums;
using App.Core.Services;
using App.WindowsForm.Views;

namespace App.WindowsForm
{
    public partial class MainForm : Form
    {
        // Service fields typed as interfaces (dependency inversion)
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;

        // Cache-aside pattern: views are created once and reused
        private readonly Dictionary<Type, UserControl> _views = new Dictionary<Type, UserControl>();

        // The view currently shown in the content panel (used by the status bar
        // to report the record count of whatever the user is looking at).
        private UserControl? _currentView;

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
        public MainForm()
        {
            var (accountService, categoryService, transactionService) = CreateDefaultServices();
            
            _accountService = accountService;
            _categoryService = categoryService;
            _transactionService = transactionService;

            InitializeComponent();
            InitializeUI();
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

            // Populate the status bar immediately so the balance shows on startup,
            // before any view has been opened.
            UpdateStatusBar();
        }

        /// <summary>
        /// Cache-aside pattern: display a view, creating it once via factory if needed.
        /// Subsequent clicks reuse the same view instance, preserving its state.
        /// </summary>
        /// <typeparam name="T">UserControl type to display</typeparam>
        /// <param name="factory">Lambda that constructs the view with dependencies</param>
        private void ShowView<T>(Func<T> factory) where T : UserControl
        {
            ArgumentNullException.ThrowIfNull(factory);

            var key = typeof(T);

            // Cache miss → factory builds it, store it
            if (!_views.TryGetValue(key, out var view))
            {
                view = factory();
                view.Dock = DockStyle.Fill;
                _views[key] = view;
            }

            // Ensure it's attached to the content panel
            if (!pnlContent.Controls.Contains(view))
            {
                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(view);
            }

            view.Visible = true;
            view.BringToFront();

            // Remember what's on screen and refresh the status bar so the record
            // count reflects this view (even when switching to an already-cached one).
            _currentView = view;
            UpdateStatusBar();
        }

        /// <summary>
        /// Recalculates and updates the bottom status bar:
        ///   • total balance across all ACTIVE accounts,
        ///   • record count of the currently displayed view,
        ///   • timestamp of this (the most recent) action.
        ///
        /// Public so views can call it after they mutate or reload data via
        /// (ParentForm as MainForm)?.UpdateStatusBar().
        /// </summary>
        public void UpdateStatusBar()
        {
            // Total balance — a single GetAll() summed in memory. Cheap for the
            // handful of accounts this app deals with. (See README viva notes.)
            try
            {
                decimal totalBalance = _accountService.GetAll()
                    .Where(a => a.AccountStatus == AccountStatusEnum.Active)
                    .Sum(a => a.OpeningBalance);

                lblBalance.Text = $"Total Active Balance: {totalBalance:N2}";
            }
            catch
            {
                lblBalance.Text = "Total Active Balance: (unavailable)";
            }

            // Record count of the view currently on screen, if it exposes one.
            int recordCount = (_currentView as IRecordCountSource)?.RecordCount ?? -1;
            lblRecordCount.Text = recordCount >= 0 ? $"Records: {recordCount}" : "Records: —";

            lblLastAction.Text = $"Last action: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}";
        }

        /// <summary>
        /// Dispose all cached views and event handlers on form close.
        /// Prevents resource leaks from cached UserControl instances.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Dispose all cached views to release resources, event handlers, timers, etc.
            foreach (var cachedView in _views.Values)
            {
                cachedView?.Dispose();
            }
            _views.Clear();

            // Dispose fonts
            _regularFont?.Dispose();
            _boldFont?.Dispose();

            base.OnFormClosed(e);
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

#if DEBUG
            // Add SmokeTest button (development/debug only - not shipped to production)
            var btnSmokeTest = new Button
            {
                Text = "🧪 Chart Test",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.FromArgb(33, 33, 33),
                Font = _regularFont,
                FlatStyle = FlatStyle.Flat
            };

            btnSmokeTest.Click += (s, e) =>
            {
                var testForm = new ChartSmokeTestForm();
                testForm.Show();
            };

            pnlSide.Controls.Add(btnSmokeTest);
#endif
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

        /// <summary>
        /// Display Dashboard view. Factory creates it once; subsequent clicks reuse it.
        /// </summary>
        private void BtnDashboard_Click(object? sender, EventArgs e)
        {
            SelectTab(btnDashboard);
            ShowView(() => new DashboardView(_transactionService, _categoryService, _accountService));
        }

        /// <summary>
        /// Called from views after they mutate data.
        /// Refreshes the Dashboard if it's been opened at least once.
        /// </summary>
        public void RefreshDashboardIfCached()
        {
            if (_views.TryGetValue(typeof(DashboardView), out var view) && view is DashboardView dash)
            {
                dash.RefreshCharts();
            }
        }

        /// <summary>
        /// Display Accounts view. Factory creates it once; subsequent clicks reuse it.
        /// State (filters, scroll, selection) is preserved across clicks.
        /// </summary>
        private void BtnAccounts_Click(object? sender, EventArgs e)
        {
            SelectTab(btnAccounts);
            ShowView(() => new AccountsView(_accountService));
        }

        /// <summary>
        /// Display Categories view. Factory creates it once; subsequent clicks reuse it.
        /// State (filters, scroll, selection) is preserved across clicks.
        /// </summary>
        private void BtnCategories_Click(object? sender, EventArgs e)
        {
            SelectTab(btnCategories);
            ShowView(() => new CategoriesView(_categoryService));
        }

        /// <summary>
        /// Display Transactions view. Factory creates it once; subsequent clicks reuse it.
        /// State (filters, scroll, selection) is preserved across clicks.
        /// </summary>
        private void BtnTransaction_Click(object? sender, EventArgs e)
        {
            SelectTab(btnTransaction);
            ShowView(() =>
                new TransactionsView(_transactionService, _accountService, _categoryService));
        }
    }
}