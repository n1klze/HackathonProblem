using HackathonProblem.Contracts;

namespace HackathonProblem.Service.Transient;

public class Hackathon
{
    public double Run(
        IEnumerable<Employee> teamLeads,
        IEnumerable<Employee> juniors,
        WishlistGenerator wishlistGenerator,
        HrManager hrManager,
        HrDirector hrDirector
    )
    {
        var (teamLeadsWishlists, juniorsWishlists) = wishlistGenerator.Generate(teamLeads, juniors);
        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        var satisfactionIndex = hrDirector.CalculateSatisfactionIndex(
            teams,
            teamLeadsWishlists,
            juniorsWishlists
        );

        return satisfactionIndex;
    }
}
