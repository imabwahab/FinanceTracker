using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Forms;

namespace App.WindowsForm
{
    /// <summary>
    /// Smoke test form to verify LiveCharts2 package is installed and renders correctly.
    /// This form demonstrates:
    /// 1. CartesianChart with column series (for X/Y data)
    /// 2. PieChart with pie series (for parts-of-a-whole)
    /// </summary>
    public partial class ChartSmokeTestForm : Form
    {
        public ChartSmokeTestForm()
        {
            InitializeComponent();
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            // Create a table layout panel to hold both charts
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new System.Windows.Forms.Padding(10)  // Explicitly use WinForms Padding
            };

            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Create CartesianChart for column series
            var cartesianChart = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Name = "Monthly Revenue",
                        Values = new double[] { 2, 5, 4, 3, 8, 10, 6 }
                    },
                    new ColumnSeries<double>
                    {
                        Name = "Monthly Expenses",
                        Values = new double[] { 1, 3, 2, 2, 5, 7, 4 }
                    }
                },
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            // Create PieChart for pie series
            var pieChart = new PieChart
            {
                Series = new ISeries[]
                {
                    new PieSeries<double>
                    {
                        Name = "Food",
                        Values = new double[] { 30 }
                    },
                    new PieSeries<double>
                    {
                        Name = "Transportation",
                        Values = new double[] { 25 }
                    },
                    new PieSeries<double>
                    {
                        Name = "Utilities",
                        Values = new double[] { 20 }
                    },
                    new PieSeries<double>
                    {
                        Name = "Entertainment",
                        Values = new double[] { 15 }
                    },
                    new PieSeries<double>
                    {
                        Name = "Other",
                        Values = new double[] { 10 }
                    }
                },
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            // Add charts to layout
            tableLayout.Controls.Add(cartesianChart, 0, 0);
            tableLayout.Controls.Add(pieChart, 1, 0);

            // Add layout to form
            Controls.Add(tableLayout);
        }
    }
}