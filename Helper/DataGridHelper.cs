using System.Windows.Controls;
namespace BiliBili_Anchor_Assistant.Helper;

public static class DataGridHelper
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
