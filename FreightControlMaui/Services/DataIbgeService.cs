using FreightControlMaui.Constants;
using FreightControlMaui.MVVM.Models.IBGE;
using Newtonsoft.Json;

namespace FreightControlMaui.Services
{
    public class DataIbgeService
    {
        public static async Task<List<Municipio>> GetCitiesByCodeState(string state)
        {
            using HttpClient client = new();

            var codeState = $"{state}/municipios";

            try
            {
                if (!ToastFailConectionService.CheckIfConnectionIsSuccessful()) return null;

                HttpResponseMessage response = await client.GetAsync(StringConstants.urlDataIbgeService + codeState);

                if (response.IsSuccessStatusCode)
                {                    
                    /* //Todo - Old
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var decompressedStream = new GZipStream(stream, CompressionMode.Decompress);
                    using var reader = new StreamReader(decompressedStream);                   
                    var content = await reader.ReadToEndAsync();
                    */

                    //Todo - Testing
                    using var stream = await response.Content.ReadAsStreamAsync();               
                    using var reader = new StreamReader(stream);
                    string content = await reader.ReadToEndAsync();

                    var result = JsonConvert.DeserializeObject<List<Municipio>>(content);

                    return result;
                }

                return new List<Municipio>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Municipio>();
            }
        }
    }
}