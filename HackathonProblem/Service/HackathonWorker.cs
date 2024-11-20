using HackathonProblem.Service.Transient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using HackathonProblem.CsvExtension;

namespace HackathonProblem.Service;

public class HackathonWorker(
    Hackathon hackathon,
    HrManager hrManager,
    HrDirector hrDirector,
    IHostApplicationLifetime applicationLifetime,
    IConfiguration configuration
) : IHostedService
{
    private readonly int _numberOfHackathons = configuration.GetValue<int>("NumberOfHackathons");
    private readonly string _teamLeadsSample = configuration["Samples:TeamLeads"] 
        ?? throw new InvalidOperationException("Path to file with team leads sample must be set.");
    private readonly string _juniorsSample = configuration["Samples:Juniors"] 
        ?? throw new InvalidOperationException("Path to file with juniors sample must be set."); 

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var teamLeads = CsvLoader.Load(_teamLeadsSample);
        var juniors = CsvLoader.Load(_juniorsSample);

        double totalSatisfaction = 0;
        for (var i = 0; i < _numberOfHackathons; ++i)
        {
            var (teamLeadsWishlists, juniorsWishlists) = hackathon.Organize(teamLeads, juniors);
            var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
            var satisfactionIndex = hrDirector.CalculateSatisfactionIndex(teams, teamLeadsWishlists, juniorsWishlists);
            totalSatisfaction += satisfactionIndex;
            Console.WriteLine($"{i + 1} : Satisfaction index is {satisfactionIndex}");
        }

        Console.WriteLine($"Average satisfaction index after {_numberOfHackathons} hackathons is {totalSatisfaction / _numberOfHackathons}");

        applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}