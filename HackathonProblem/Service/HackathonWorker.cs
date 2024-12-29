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
    private readonly HackathonData hackathonData =
        configuration.GetSection("Hackathon").Get<HackathonData>()
        ?? throw new InvalidOperationException(
            "Hackathon section must be set in appsettings file."
        );

    private readonly SamplesData samplesData =
        configuration.GetSection("Samples").Get<SamplesData>()
        ?? throw new InvalidOperationException("Samples section must be set in appsettings file.");

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var teamLeads = CsvLoader.Load(samplesData.TeamLeads);
        var juniors = CsvLoader.Load(samplesData.Juniors);

        while (true)
        {
            Console.WriteLine("Choose your action:");
            Console.WriteLine("1 — run hackathon once;");
            Console.WriteLine(
                "2 — print the list of participants, teams and satisfaction index for the selected hackathon;"
            );
            Console.WriteLine("3 — derive the average satisfaction index for all hackathons.");
            Console.WriteLine("q — abandon.");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                {
                    Console.WriteLine("1");
                    break;
                }
                case "2":
                {
                    Console.WriteLine("2");
                    break;
                }
                case "3":
                {
                    Console.WriteLine("3");
                    break;
                }
                default:
                {
                    Console.WriteLine("Unexpected command. Try again:");
                    Console.Write("> ");
                    break;
                }
            }
        }

        /*
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
        */

        applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
