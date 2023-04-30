
using Microsoft.AspNetCore.Identity;

namespace ITCareerProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Tickets = new List<Ticket>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
