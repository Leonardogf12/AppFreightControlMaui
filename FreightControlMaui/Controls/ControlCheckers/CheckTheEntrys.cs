using System.Text.RegularExpressions;

namespace FreightControlMaui.Controls.ControlCheckers
{
    public static class CheckTheEntrys
    {
        public const string patternKilometer = @"^[1-9][0-9]*(\.[0-9]{1,2})?$";
        public const string patternMoney = @"^\d+(\.\d{1,2})?$";
        public const string patternLiters = @"^[0-9]{1,4}$";

        public static bool IsValidEntry(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
    }
}