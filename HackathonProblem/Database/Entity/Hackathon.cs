namespace HackathonProblem.Database.Entity;

public class Hackathon
{
    public Guid Id { get; set; }
    public IList<TeamLead> TeamLeads { get; set; } = new List<TeamLead>();
    public IList<Junior> Juniors { get; set; } = new List<Junior>();
    public IList<Wishlist> TeamLeadsWishlists { get; set; } = new List<Wishlist>();
    public IList<Wishlist> JuniorsWishlists { get; set; } = new List<Wishlist>();
    public IList<Team> Teams { get; set; } = new List<Team>();
    public double SatisfactionIndex { get; set; }
}
