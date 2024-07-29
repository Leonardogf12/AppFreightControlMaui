using System.Windows.Input;

namespace FreightControlMaui.Models
{
    public class HeaderButtonFreight
    {
        public required string Text { get; set; }
        public required ICommand Command { get; set; }
    }
}