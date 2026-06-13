namespace App.WindowsForm
{
    /// <summary>
    /// Implemented by views that display a countable list of records (e.g. grids).
    /// MainForm reads this off the currently displayed view to populate the
    /// "Records" label in the status bar. Views that have no record list
    /// (such as the Dashboard) simply don't implement it.
    /// </summary>
    public interface IRecordCountSource
    {
        int RecordCount { get; }
    }
}
