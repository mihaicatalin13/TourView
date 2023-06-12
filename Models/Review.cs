using Microsoft.AspNetCore.Identity;
using TourView.Data;

namespace TourView.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }

}
