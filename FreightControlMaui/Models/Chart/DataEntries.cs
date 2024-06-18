using SkiaSharp;

namespace FreightControlMaui.Models.Chart
{
    public class DataEntries
    {
        public float Value { get; set; }
        public string? Label { get; set; }
        public string? ValueLabel { get; set; }
        public static SKColor ColorDefault => SKColor.Parse("#27AAE7");
        public static SKColor ValueLabelColorDefault => SKColor.Parse("#FFFFFF");
        public static SKColor TextColorDefault => SKColor.Parse("#FFFFFF");
    }
}

