using SQLite;

namespace FreightControlMaui.MVVM.Models
{
    public class FreightModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public required string UserLocalId { get; set; }

        public DateTime TravelDate { get; set; }

        public required string OriginUf { get; set; }

        public required string Origin { get; set; }

        public required string DestinationUf { get; set; }

        public required string Destination { get; set; }

        public double Kilometer { get; set; }

        public decimal FreightValue { get; set; }

        public string? Observation { get; set; }

        public string TravelDateCustom => TravelDate.ToShortDateString();
    }
}

