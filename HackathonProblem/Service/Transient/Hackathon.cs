using HackathonProblem.Contracts;

namespace HackathonProblem.Service.Transient;

public class Hackathon 
{
    public (IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists) Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors)
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

public static class RandomExtension
{
    private static readonly Random _random = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            var i = _random.Next(n);
            --n;
            (list[i], list[n]) = (list[n], list[i]);
        }
    }
}
