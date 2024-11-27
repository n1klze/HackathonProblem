using HackathonProblem.Contracts;

namespace HackathonProblem.TeamBuildingStrategy;

public class GaleShapleyAlgorithm : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamLeads,
        IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists
    )
    {
        var teamLeadsList = teamLeads.ToList();
        var juniorsList = juniors.ToList();

        var freeJuniorsQueue = new Queue<int>(juniors.Select(j => j.Id));
        var teamLeadsDesiredJuniors = teamLeadsWishlists.ToDictionary(
            w => w.EmployeeId,
            w => w.DesiredEmployees
        );
        var juniorsDesiredTeamLeads = juniorsWishlists.ToDictionary(
            w => w.EmployeeId,
            w => w.DesiredEmployees
        );

        var teamLeadIdToJuniorIdPair = new Dictionary<int, int>();

        while (freeJuniorsQueue.Count > 0)
        {
            var currentJuniorId = freeJuniorsQueue.Dequeue();
            var desiredTeamLeads = juniorsDesiredTeamLeads[currentJuniorId];

            foreach (var dt in desiredTeamLeads)
            {
                if (teamLeadIdToJuniorIdPair.TryGetValue(dt, out var selectedJuniorId))
                {
                    var desiredJuniors = teamLeadsDesiredJuniors[dt];
                    if (
                        !PrefersCurrenMoreThanSelectedOne(
                            currentJuniorId,
                            selectedJuniorId,
                            desiredJuniors
                        )
                    )
                    {
                        continue;
                    }
                    teamLeadIdToJuniorIdPair[dt] = currentJuniorId;
                    freeJuniorsQueue.Enqueue(selectedJuniorId);
                    break;
                }

                teamLeadIdToJuniorIdPair[dt] = currentJuniorId;
                break;
            }
        }

        return teamLeadIdToJuniorIdPair.Select(e =>
            MakeTeam(e.Value, e.Key, juniorsList, teamLeadsList)
        );
    }

    private static Team MakeTeam(
        int juniorId,
        int teamLeadId,
        IEnumerable<Employee> juniors,
        IEnumerable<Employee> teamleads
    )
    {
        var junior = juniors.First(j => j.Id == juniorId);
        var teamLead = teamleads.First(t => t.Id == teamLeadId);

        return new Team(teamLead, junior);
    }

    private static bool PrefersCurrenMoreThanSelectedOne(
        in int currentJuniorId,
        in int selectedJuniorId,
        in int[] desiredJuniors
    )
    {
        foreach (var dj in desiredJuniors)
        {
            if (dj == currentJuniorId)
            {
                return true;
            }

            if (dj == selectedJuniorId)
            {
                return false;
            }
        }

        return false;
    }
}
