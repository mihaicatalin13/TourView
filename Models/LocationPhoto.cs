using System.ComponentModel.DataAnnotations;

namespace TourView.Models
{
    public class LocationPhoto
    {
        [Key]
        public int PhotoId { get; set; }
        public string LocationId { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
