using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ARoomInterior.Models.DB
{
    public class ElementObj
    {
        [Key]
        public int ElementId { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Name), ResourceType = typeof(Resources.Projects))]
        public string Name { get; set; }
        [Required]
        [Display(Name = nameof(Resources.Projects.Description), ResourceType = typeof(Resources.Projects))]
        public string Description { get; set; }
        public string Preview { get; set; } 
        public string File { get; set; }
    }
}