using System.Text;
using FreightControlMaui.Controls.Alerts;
using FreightControlMaui.MVVM.Models;
using FreightControlMaui.Repositories;
using GemBox.Pdf;
using GemBox.Pdf.Content;

namespace FreightControlMaui.Services.Exportation
{
    public class ExportData : IExportData
    {
        private readonly ToFuelRepository _toFuelRepository;

        public ExportData()
        {
            _toFuelRepository = new();
        }
      
        public async Task CreateDocumentExcelAsync(List<FreightModel> list)
        {
            if (list == null) return;

            string nameFile = $"fretes{DateTime.Now:dd-MM-yy-hh-mm-ss}.csv";

            string filePath = SetFilePathByDevice(nameFile);
           
            var utf8 = new UTF8Encoding(true);

            var totalFreight = list.Select(x => x.FreightValue).Sum();

            using (var writer = new StreamWriter(filePath, false, utf8))
            {
                await writer.WriteAsync($"Código #;Data;Origem;Destino;Distância (KM);Valor (R$);Observação");

                foreach (var freight in list)
                {
                    await writer.WriteAsync($"\n# {freight.Id};" +
                                            $"{freight.TravelDate.ToShortDateString()};" +
                                            $"{freight.Origin} - {freight.OriginUf};" +
                                            $"{freight.Destination} - {freight.DestinationUf};" +
                                            $"{freight.Kilometer};" +
                                            $"{freight.FreightValue:c};" +
                                            $"{freight.Observation}");
                }


                await writer.WriteLineAsync();
                await writer.WriteAsync($"-;-;-;-;-;Total Valor: {totalFreight:c};-");
                await writer.WriteLineAsync();
                await writer.WriteLineAsync();
                await writer.WriteAsync($"Código #;Data;Litros (Lt);Valor (R$);Valor/Litro (R$);Despesas (R$);Observação");

                double totalLiters = 0;
                decimal totalValue = 0;
                decimal totalExpenses = 0;

                foreach (var item in list)
                {
                    var supplies = await GetFreightSupplies(item);

                    foreach (var fuel in supplies)
                    {
                        await writer.WriteAsync($"\n# {fuel.FreightModelId};" +
                                                $"{fuel.Date.ToShortDateString()};" +
                                                $"{fuel.Liters};" +
                                                $"{fuel.AmountSpentFuel:c};" +
                                                $"{fuel.ValuePerLiter:c};" +
                                                $"{fuel.Expenses:c};" +
                                                $"{fuel.Observation}");

                        totalLiters += fuel.Liters;
                        totalValue += fuel.AmountSpentFuel;
                        totalExpenses += fuel.Expenses;
                    }
                }

                await writer.WriteLineAsync();
                await writer.WriteAsync($"-;-;Total Litros: {totalLiters};Total Valor: {totalValue:c};-;Total Despesas: {totalExpenses:c};-");
                await writer.WriteLineAsync();
                await writer.WriteAsync($"Total Geral: {totalFreight - totalValue - totalExpenses:c};-;-;-;-;-;-");
            }

            await ControlAlert.DefaultAlert("Sucesso", "Arquivo exportado com sucesso. O arquivo foi salvo em: Documentos.");            
        }

        public async Task<string> CreateDocumentPdfAsync(int pageCount = 1)
        {
            using var document = new PdfDocument();

            var stepper = new Stepper
            {
                Minimum = 1,
                Maximum = 10,
                Value = pageCount
            };

            for (int i = 0; i < stepper.Value; ++i)
                document.Pages.Add();

            using (var formattedText = new PdfFormattedText())
            {
                formattedText.FontFamily = new PdfFontFamily("MontserratRegular");
                formattedText.FontSize = 24;

                formattedText.AppendLine($"Confrete - {DateTime.Now.ToShortDateString()}");
                document.Pages[0].Content.DrawText(formattedText, new PdfPoint(100,100));

                var image = PdfImage.Load(await FileSystem.OpenAppPackageFileAsync("dotnet_botraw.png"));
                document.Pages[0].Content.DrawImage(image, new PdfPoint(100, 200));               

            }
          
            string nameFile = $"fretes{DateTime.Now:dd-MM-yy-hh-mm-ss}.pdf";
            string filePath = SetFilePathByDevice(nameFile);

            await Task.Run(() => document.Save(filePath));

            await ControlAlert.DefaultAlert("Sucesso", "PDF exportado com sucesso. O arquivo foi salvo em: Documentos.");

            return filePath;
        }

        public async Task OpenLauncher(string filePath) => await Launcher.OpenAsync(new OpenFileRequest(Path.GetFileName(filePath), new ReadOnlyFile(filePath)));

        public async Task<List<ToFuelModel>> GetFreightSupplies(FreightModel item)
        {
            var list = await _toFuelRepository.GetAllById(item.Id);

            return list.ToList();
        }

        public static string SetFilePathByDevice(string nameFile)
        {
            string path = string.Empty;
#if ANDROID
            path = Path.Combine(Android.App.Application.Context.FilesDir.AbsolutePath, "/storage/emulated/0/Documents/");
#else
//Todo Implement local storage to save csv file.
#endif
            return Path.Combine(path, nameFile);
        }

    }
}

