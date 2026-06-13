using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace App.WindowsForm
{
    /// <summary>
    /// A <see cref="BindingList{T}"/> that supports column sorting in a DataGridView.
    ///
    /// Why this exists: a plain <c>List&lt;T&gt;</c> bound through a BindingSource does NOT
    /// implement IBindingListView, so clicking a DataGridView column header does nothing
    /// (DataGridView reports the source as un-sortable). Wrapping the list in this class
    /// lets the grid sort the rows IN MEMORY by the clicked property — no extra database
    /// query is issued.
    ///
    /// Sorting is generic: it reflects the value of the clicked property on each row and
    /// compares with the default comparer, which works for string, decimal, DateTime,
    /// bool, and enum properties alike.
    /// </summary>
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor? _sortProperty;

        public SortableBindingList()
        {
        }

        public SortableBindingList(IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _isSorted;
        protected override ListSortDirection SortDirectionCore => _sortDirection;
        protected override PropertyDescriptor? SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            // BindingList<T>'s backing store is a List<T> by default, so this cast is safe.
            if (Items is List<T> items)
            {
                items.Sort((x, y) => Compare(prop, x, y, direction));
            }

            _isSorted = true;

            // Tell any bound control (the DataGridView) to refresh the whole list.
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }

        private static int Compare(PropertyDescriptor prop, T x, T y, ListSortDirection direction)
        {
            object? xValue = prop.GetValue(x);
            object? yValue = prop.GetValue(y);

            int result;
            if (xValue == null && yValue == null) result = 0;
            else if (xValue == null) result = -1;
            else if (yValue == null) result = 1;
            else result = Comparer<object>.Default.Compare(xValue, yValue);

            return direction == ListSortDirection.Descending ? -result : result;
        }
    }
}
