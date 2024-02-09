using System.ComponentModel.DataAnnotations;

namespace sem2.riverStations.urbanec.ait4.StationInfomrations
{
    public class HistoryValues
    {
        [Key]
        public int ID { get; set; }
        public int StationsID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }

        public virtual Stations Stations { get; set; }
    }
}
