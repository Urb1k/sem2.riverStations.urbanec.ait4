using System.ComponentModel.DataAnnotations;

namespace sem2.riverStations.urbanec.ait4.StationInfomrations
{
    public class Station
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string River { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Flood Warning Value must be a positive number.")]
        public int FloodWarningValue { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Drought Warning Value must be a positive number.")]
        public int DroughtWarningValue { get; set; }
        [Required]
        [Range(30, int.MaxValue, ErrorMessage = "Time Tolerance must be at least 30 minutes.")]
        public int TimeToleranceInMin { get; set; }
        public string? CreatedByUser { get; set; }
        public DateTime CreatedOn { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FloodWarningValue <= DroughtWarningValue)
            {
                yield return new ValidationResult("Flood Warning Value must be greater than Drought Warning Value.", new[] { "FloodWarningValue", "DroughtWarningValue" });
            }
        }
    }
}
