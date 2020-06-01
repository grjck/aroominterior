using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ARoomInterior.Models.DB
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Name), ResourceType = typeof(Resources.Projects))]
        public string Name { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Description), ResourceType = typeof(Resources.Projects))]
        public string Description { get; set; }
        public string Preview { get; set; }

        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public int ProjectTeamId { get; set; }
        public Team ProjectTeam;

        public string LawInfoContractNumber { get; set; }
        public ProjectLawInfo LawInfo { get; set; }

        public ICollection<ProjectElementObj> ElementObjects { get; set; }

        public Project()
        {
            ElementObjects = new List<ProjectElementObj>();
        }
    }
}