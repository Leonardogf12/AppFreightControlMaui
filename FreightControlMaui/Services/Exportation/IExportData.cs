using FreightControlMaui.MVVM.Models;

namespace FreightControlMaui.Services.Exportation
{
    public interface IExportData
	{
        Task CreateDocumentExcelAsync(List<FreightModel> list);

        Task<string> CreateDocumentPdfAsync(int pageCount = 1);

        Task OpenLauncher(string filePath);
    }
}

