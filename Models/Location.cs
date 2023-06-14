using System.ComponentModel.DataAnnotations;

namespace TourView.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "The maximum length must be 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Schedule is required")]
        public string? Schedule { get; set; }

        [Required(ErrorMessage = "Menu is required")]
        public string? Menu { get; set; }

        public string? PhotoUrl { get; set; }
        public double? Rating { get; set; }
        public string? ManagerId { get; set; }

        // public List<string> Cuisine { get; set; }


    }

}
