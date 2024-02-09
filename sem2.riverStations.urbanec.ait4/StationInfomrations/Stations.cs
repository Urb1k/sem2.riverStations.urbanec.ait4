using System.ComponentModel.DataAnnotations;

namespace sem2.riverStations.urbanec.ait4.StationInfomrations
{
    public class Stations
    {
        [Key]
        public int ID { get; set; }
        public string River { get; set; }
        public int FloodWarningValue { get; set; }
        public int DroughtWarningValue { get; set; }
        public int TimeToleranceInMin { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
