using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Plant
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

        public virtual ICollection<PlantCareType> PlantCareTypes { get; set; } = new List<PlantCareType>();
    }

    public class PlantCareType
    {
        public int PlantCareTypeId { get; set; }
        public string InsecticideName { get; set; } = null!;
        public string Fertilizer { get; set; } = null!;
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; } = null!;
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op)
        { }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantCareType> PlantCareTypes { get; set; }
    }
}
