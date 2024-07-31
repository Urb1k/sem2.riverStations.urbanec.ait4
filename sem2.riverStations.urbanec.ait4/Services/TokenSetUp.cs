using System.ComponentModel.DataAnnotations;

namespace sem2.riverStations.urbanec.ait4.Services
{
    public class TokenSetUp
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; }
    }
}
