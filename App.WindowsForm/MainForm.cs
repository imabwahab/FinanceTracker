namespace App.WindowsForm
{
    public partial class MainForm : Form
    {
        private Button? _activeButton;
        private readonly Color _activeColor = Color.FromArgb(200, 220, 255);      // Light blue
        private readonly Color _activeTextColor = Color.FromArgb(0, 102, 204);    // Blue text
        private readonly Color _inactiveColor = Color.FromArgb(240, 240, 240);    // Light gray
        private readonly Color _inactiveTextColor = Color.FromArgb(33, 33, 33);   // Dark text

        // Cache fonts to avoid repeated allocations (declared only here, not in Designer)
        private Font? _regularFont;
        private Font? _boldFont;

        public MainForm()
        {
            InitializeComponent();
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

                // Load icons from Assets folder (24x24 recommended)
                string dashboardPath = Path.Combine(assetsPath, "dashboard.png");
                if (File.Exists(dashboardPath))
                {
                    btnDashboard.Image = new Bitmap(dashboardPath);
                }

                string accountsPath = Path.Combine(assetsPath, "accounts.png");
                if (File.Exists(accountsPath))
                {
                    btnAccounts.Image = new Bitmap(accountsPath);
                }

                string categoriesPath = Path.Combine(assetsPath, "categories.png");
                if (File.Exists(categoriesPath))
                {
                    btnCategories.Image = new Bitmap(categoriesPath);
                }

                string transactionsPath = Path.Combine(assetsPath, "transactions.png");
                if (File.Exists(transactionsPath))
                {
                    btnTransaction.Image = new Bitmap(transactionsPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading icons: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Load Accounts UserControl here
            // pnlContent.Controls.Add(new AccountsControl());
        }

        private void BtnCategories_Click(object? sender, EventArgs e)
        {
            SelectTab(btnCategories);
            pnlContent.Controls.Clear();
            // Load Categories UserControl here
            // pnlContent.Controls.Add(new CategoriesControl());
        }

        private void BtnTransaction_Click(object? sender, EventArgs e)
        {
            SelectTab(btnTransaction);
            pnlContent.Controls.Clear();
            // Load Transactions UserControl here
            // pnlContent.Controls.Add(new TransactionsControl());
        }
    }
}