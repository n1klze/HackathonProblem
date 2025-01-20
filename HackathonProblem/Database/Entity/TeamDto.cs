namespace HackathonProblem.Database.Entity;

public class TeamDto
{
    public int Id { get; set; }
    public TeamLeadDto TeamLead { get; set; }
    public JuniorDto Junior { get; set; }
}
