using System.ComponentModel.DataAnnotations;

namespace TourView.Models
{
    public class Location
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Schedule { get; set; }
        public string Menu { get; set; }
        public string PhotoUrl { get; set; }
        public double Rating { get; set; }
        // public List<string> Cuisine { get; set; }


    }

}
