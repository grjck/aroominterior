using System.ComponentModel.DataAnnotations;

namespace ARoomInterior.Models.DB
{
    public class Invite
    {
        [Key]
        public int InviteId { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int CurrentTeamId { get; set; }
        public Team CurrentTeam { get; set; }
    }
}  