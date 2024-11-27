using HackathonProblem.Contracts;
using HackathonProblem.Utils.RandomExtension;

namespace HackathonProblem.Service.Transient;

public class WishlistGenerator
{
    public (
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists
    ) Generate(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors)
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
}
