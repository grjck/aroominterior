using System.ComponentModel.DataAnnotations;

namespace ARoomInterior.Models.DB
{
    public class ProjectLawInfo
    {
        [Key]
        public string ContractNumber { get; set; }
        public string Description { get; set; }
    }
}  