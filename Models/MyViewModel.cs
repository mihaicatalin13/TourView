using Microsoft.EntityFrameworkCore;

namespace TourView.Models
{
    public class MyViewModel
    {
        public IEnumerable<Review>? reviewsIEn;
        public IEnumerable<Location>? locationsIEn;
        public IEnumerable<ApplicationUser>? usersIEn;
        public DbSet<ApplicationUser>? users;

        public MyViewModel()
        {
        }
    }
}
