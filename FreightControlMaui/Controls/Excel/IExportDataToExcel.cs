using FreightControlMaui.MVVM.Models;

namespace FreightControlMaui.Controls.Excel
{
    public interface IExportDataToExcel
	{
        Task ExportData(List<FreightModel> list);
    }
}

