using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonProblem.Database.Entity;

public class JuniorWishlistDto
{
    public int Id { get; set; }

    [ForeignKey("Juniors")]
    public int EmployeeId { get; set; }

    [ForeignKey("TeamLeads")]
    public int[] DesiredEmployee { get; set; }
}
