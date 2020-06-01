using ARoomInterior.Models;
using System.ComponentModel.DataAnnotations;

namespace ARoomInterior.Controllers
{
    public class DetailTeamModel
    {
        public string LastName { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Name), ResourceType = typeof(Resources.Projects))]
        public string Name { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Description), ResourceType = typeof(Resources.Projects))]
        public string Description { get; set; }
        [Required]
        [Display(Name = nameof(Resources.TeamDetail.Customer), ResourceType = typeof(Resources.TeamDetail))]
        public string Customer { get; set; }
        public bool IsCustomer { get; set; }
        public bool HasInvite { get; set; }
    }
}