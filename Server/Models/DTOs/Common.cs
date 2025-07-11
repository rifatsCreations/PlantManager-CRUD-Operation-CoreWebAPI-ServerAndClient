using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs
{
    public class Common
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public bool IsFlowerBearing { get; set; }
        public decimal PlantPrice { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? PlantCareTypes { get; set; } = null!;
    }
}
