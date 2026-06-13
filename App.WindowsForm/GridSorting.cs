using System.Windows.Forms;

namespace App.WindowsForm
{
    /// <summary>
    /// Helper for enabling client-side column sorting on a DataGridView.
    ///
    /// Columns in these grids are auto-generated from the bound data source, so
    /// SortMode can only be set AFTER binding completes. Call this from the grid's
    /// DataBindingComplete event. Combined with a <see cref="SortableBindingList{T}"/>
    /// data source, clicking a header sorts the rows in memory (ascending / descending).
    /// </summary>
    internal static class GridSorting
    {
        public static void EnableColumnSorting(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
    }
}
