using Microsoft.AspNetCore.Identity;
using TourView.Data;

namespace TourView.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
    }

}
