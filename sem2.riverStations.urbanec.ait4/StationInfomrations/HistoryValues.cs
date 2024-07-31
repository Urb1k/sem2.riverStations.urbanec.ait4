using System.ComponentModel.DataAnnotations;

namespace sem2.riverStations.urbanec.ait4.StationInfomrations
{
    public class HistoryValues
    {
        [Key]
        public Guid ID { get; set; }
        public int StationID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }

        public virtual Station? Station { get; set; }
    }
}
