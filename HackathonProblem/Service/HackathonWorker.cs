using HackathonProblem.Service.Transient;
using HackathonProblem.Settings;
using HackathonProblem.Utils.CsvExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Service;

public class HackathonWorker(
    Hackathon hackathon,
    WishlistGenerator wishlistGenerator,
    HrManager hrManager,
    HrDirector hrDirector,
    IHostApplicationLifetime applicationLifetime,
    IConfiguration configuration
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        HackathonData hackathonData =
            configuration.GetSection("Hackathon").Get<HackathonData>()
            ?? throw new InvalidOperationException(
                "Hackathon section must be set in appsettings file."
            );

        SamplesData samplesData =
            configuration.GetSection("Samples").Get<SamplesData>()
            ?? throw new InvalidOperationException(
                "Samples section must be set in appsettings file."
            );

        var teamLeads = CsvLoader.Load(samplesData.TeamLeads);
        var juniors = CsvLoader.Load(samplesData.Juniors);

        double totalSatisfaction = 0;
        for (var i = 0; i < hackathonData.Number; ++i)
        {
            var satisfactionIndex = hackathon.Run(
                teamLeads,
                juniors,
                wishlistGenerator,
                hrManager,
                hrDirector
            );
            totalSatisfaction += satisfactionIndex;
            Console.WriteLine($"{i + 1} : Satisfaction index is {satisfactionIndex}");
        }

        Console.WriteLine(
            $"Average satisfaction index after {hackathonData.Number} hackathons is {totalSatisfaction / hackathonData.Number}"
        );

        applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
