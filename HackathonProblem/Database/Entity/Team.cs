namespace HackathonProblem.Database.Entity;

public class Team
{
    public int Id { get; set; }
    public TeamLead TeamLead { get; set; }
    public Junior Junior { get; set; }
}
