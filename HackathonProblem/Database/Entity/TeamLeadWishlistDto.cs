using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonProblem.Database.Entity;

public class TeamLeadWishlistDto
{
    public int Id { get; set; }

    [ForeignKey("Teamleads")]
    public int EmployeeId { get; set; }

    [ForeignKey("Juniors")]
    public int[] DesiredEmployee { get; set; }
}
