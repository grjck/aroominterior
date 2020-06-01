using System.ComponentModel.DataAnnotations;

namespace ARoomInterior.Controllers
{
    public class DetailProjectModel
    {
        public string LastName { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Name), ResourceType = typeof(Resources.Projects))]
        public string Name { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Description), ResourceType = typeof(Resources.Projects))]
        public string Description { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.UserName), ResourceType = typeof(Resources.Projects))]
        public string UserName { get; set; }
        public string LawInfoContractNumber { get; set; }
        public string LawDescription { get; set; }
        public bool IsCustomer { get; set; }
    }
}