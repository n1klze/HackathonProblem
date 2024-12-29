namespace HackathonProblem.Database.Entity;

public class Wishlist
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int[] DesiredEmployee { get; set; }
}
