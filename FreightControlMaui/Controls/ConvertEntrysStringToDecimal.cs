using System.Globalization;

namespace FreightControlMaui.Controls
{
    public static class ConvertEntrysStringToDecimal
    {
        public static Task<decimal> ConvertValue(string valueStr)
        {
            decimal convertedValue;

            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            if (decimal.TryParse(valueStr, NumberStyles.Number, cultureInfo, out convertedValue))
            {
                return Task.FromResult(convertedValue);
            }

            return Task.FromResult(convertedValue);
        }
    }
}

