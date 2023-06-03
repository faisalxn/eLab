using Microsoft.AspNetCore.Identity;

namespace eLab.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
