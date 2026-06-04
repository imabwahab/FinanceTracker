using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;

namespace App.WindowsForm.Views
{
    /// <summary>
    /// Dashboard with two charts driven by real database data.
    ///
    /// Both charts are intentionally simple aggregations of GetAll():
    ///   1. Pie chart  - sum of Amount per Category (all-time)
    ///   2. Column chart - OpeningBalance per Account
    ///
    /// The "RefreshCharts()" method is public so MainForm can call it
    /// after the user changes data elsewhere (Add/Edit/Delete a transaction).
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private readonly ITransactionService _txnService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;

        public DashboardView(
            ITransactionService txnService,
            ICategoryService categoryService,
            IAccountService accountService)
        {
            ArgumentNullException.ThrowIfNull(txnService);
            ArgumentNullException.ThrowIfNull(categoryService);
            ArgumentNullException.ThrowIfNull(accountService);

            InitializeComponent();

            _txnService = txnService;
            _categoryService = categoryService;
            _accountService = accountService;

            btnRefresh.Click += (s, e) => RefreshCharts();
            Load += (s, e) => RefreshCharts();
        }

        /// <summary>
        /// Public so MainForm can call it after a mutation in another view.
        /// </summary>
        public void RefreshCharts()
        {
            BuildAmountByCategoryPie();
            BuildOpeningBalanceByAccountColumn();
        }

        /// <summary>
        /// Pie chart: total transaction Amount per Category
        /// </summary>
        private void BuildAmountByCategoryPie()
        {
            try
            {
                var allTxns = _txnService.GetAll();
                var categories = _categoryService.GetAll();

                // Pre-group transactions by CategoryId once (O(n))
                var totalsByCategory = allTxns
                    .GroupBy(t => t.CategoryId)
                    .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

                // Then build slices by joining categories to the pre-computed totals (O(m))
                var slices = categories
                    .Select(c => new
                    {
                        Name = c.Name,
                        Total = totalsByCategory.TryGetValue(c.Id, out var total) ? total : 0m
                    })
                    .Where(x => x.Total > 0)   // skip categories with no transactions
                    .OrderByDescending(x => x.Total)
                    .ToList();

                // Build a PieSeries for each category.
                var series = slices
                    .Select(s => new PieSeries<decimal>
                    {
                        Name = s.Name,
                        Values = new[] { s.Total }
                    })
                    .Cast<ISeries>()
                    .ToArray();

                pieChart.Series = series;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pie chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Column chart: OpeningBalance per Account
        /// </summary>
        private void BuildOpeningBalanceByAccountColumn()
        {
            try
            {
                var accounts = _accountService.GetAll();

                var balances = accounts.Select(a => a.OpeningBalance).ToList();
                var labels = accounts.Select(a => a.Name).ToArray();

                var columnSeries = new ColumnSeries<decimal>
                {
                    Name = "Opening Balance",
                    Values = balances
                };

                cartesianChart.Series = new ISeries[] { columnSeries };

                cartesianChart.XAxes = new[]
                {
                    new Axis { Labels = labels }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading column chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}