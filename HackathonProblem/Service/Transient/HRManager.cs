using HackathonProblem.Contracts;

namespace HackathonProblem.Service.Transient;

public class HrManager(ITeamBuildingStrategy teamBuildingStrategy)
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return teamBuildingStrategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
    }
}