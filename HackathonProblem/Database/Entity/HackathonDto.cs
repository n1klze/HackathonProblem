namespace HackathonProblem.Database.Entity;

public class HackathonDto
{
    public Guid Id { get; set; }
    public IList<TeamLeadDto> TeamLeads { get; set; } = new List<TeamLeadDto>();
    public IList<JuniorDto> Juniors { get; set; } = new List<JuniorDto>();
    public IList<TeamLeadWishlistDto> TeamLeadsWishlists { get; set; } =
        new List<TeamLeadWishlistDto>();
    public IList<JuniorWishlistDto> JuniorsWishlists { get; set; } = new List<JuniorWishlistDto>();
    public IList<TeamDto> Teams { get; set; } = new List<TeamDto>();
    public double SatisfactionIndex { get; set; }
}
