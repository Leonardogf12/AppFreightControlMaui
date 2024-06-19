using System.Globalization;

namespace FreightControlMaui.Controls
{
    public static class ConvertEntrysStringToDouble
    {
        public static Task<double> ConvertValue(string? valueStr)
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            if (double.TryParse(valueStr, NumberStyles.Number, cultureInfo, out double convertedValue))
            {
                return Task.FromResult(convertedValue);
            }

            return Task.FromResult(convertedValue);
        }
    }
}

