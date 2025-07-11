using System.ComponentModel.DataAnnotations;

namespace Client.Models.ViewModels
{
    public class PlantViewModel
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public bool IsFlowerBearing { get; set; }
        public decimal PlantPrice { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ProfileFile { get; set; }
        public string InsecticideName { get; set; } = null!;
        public string Fertilizer { get; set; } = null!;
        public virtual ICollection<PlantCareType> PlantCareTypes { get; set; } = new List<PlantCareType>();
    }
}
