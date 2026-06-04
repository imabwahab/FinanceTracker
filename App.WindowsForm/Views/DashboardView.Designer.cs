#nullable enable

namespace App.WindowsForm.Views
{
    partial class DashboardView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnRefresh = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            pieChart = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            cartesianChart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1000, 50);
            pnlHeader.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(880, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 50);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(pieChart, 0, 0);
            tableLayoutPanel1.Controls.Add(cartesianChart, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 50);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1000, 550);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // pieChart
            // 
            pieChart.Dock = DockStyle.Fill;
            pieChart.Location = new Point(3, 3);
            pieChart.Name = "pieChart";
            pieChart.Size = new Size(494, 544);
            pieChart.TabIndex = 0;
            // 
            // cartesianChart
            // 
            cartesianChart.Dock = DockStyle.Fill;
            cartesianChart.Location = new Point(503, 3);
            cartesianChart.Name = "cartesianChart";
            cartesianChart.Size = new Size(494, 544);
            cartesianChart.TabIndex = 1;
            // 
            // DashboardView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pnlHeader);
            Name = "DashboardView";
            Size = new Size(1000, 600);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Button btnRefresh;
        private TableLayoutPanel tableLayoutPanel1;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieChart;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart;
    }
}