using System.Windows.Controls;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.Service
{
    public static class DataGridService
    {
        public static double GetColumnWidthByName(DataGrid dataGrid, string columnName)
        {
            foreach (var column in dataGrid.Columns)
            {
                var dataGridColumn = column;
                if (dataGridColumn != null && dataGridColumn.Header?.ToString() == columnName)
                {
                    return dataGridColumn.ActualWidth;
                }
            }
            return double.NaN;
        }
    }
}
