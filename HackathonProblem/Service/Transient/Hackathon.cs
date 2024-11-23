using HackathonProblem.Contracts;
using HackathonProblem.Utils.RandomExtension;

namespace HackathonProblem.Service.Transient;

public class Hackathon
{
    public (IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists) GenerateWishlists(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors)
    {
        var teamLeadsWishlists = new List<Wishlist>();
        var juniorsWishlists = new List<Wishlist>();

        var teamLeadsList = teamLeads.ToList();
        foreach (var j in juniors)
        {
            teamLeadsList.Shuffle();
            juniorsWishlists.Add(new Wishlist(j.Id, teamLeadsList.Select(l => l.Id).ToArray()));
        }

        var juniorsList = juniors.ToList();
        foreach (var l in teamLeads)
        {
            juniorsList.Shuffle();
            teamLeadsWishlists.Add(new Wishlist(l.Id, juniorsList.Select(j => j.Id).ToArray()));
        }

        return (teamLeadsWishlists, juniorsWishlists);
    }

    public double Run(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, HrManager hrManager, HrDirector hrDirector)
    {
        var (teamLeadsWishlists, juniorsWishlists) = GenerateWishlists(teamLeads, juniors);
        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        var satisfactionIndex = hrDirector.CalculateSatisfactionIndex(teams, teamLeadsWishlists, juniorsWishlists);

        return satisfactionIndex;
    }
}
