using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ARoomInterior.Models.DB
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Name), ResourceType = typeof(Resources.Projects))]
        public string Name { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Description), ResourceType = typeof(Resources.Projects))]
        public string Description { get; set; }

        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Invite> Invites { get; set; }

        public Team()
        {
            ApplicationUsers = new List<ApplicationUser>();
            Invites = new List<Invite>();
        }
    }
}  