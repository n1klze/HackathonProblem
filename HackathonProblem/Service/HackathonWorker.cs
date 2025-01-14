using HackathonProblem.Service.Transient;
using HackathonProblem.Settings;
using HackathonProblem.Utils.CsvExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Service;

public class HackathonWorker(
    Hackathon hackathon,
    IHostApplicationLifetime applicationLifetime,
    IConfiguration configuration,
    EmployeeLoader employeeLoader,
    AvgSatisfactionCalculator avgSatisfactionCalculator,
    HackathonPrinter hackathonPrinter
) : IHostedService
{
    private readonly HackathonData hackathonData =
        configuration.GetSection("Hackathon").Get<HackathonData>()
        ?? throw new InvalidOperationException(
            "Hackathon section must be set in appsettings file."
        );

    public Task StartAsync(CancellationToken cancellationToken)
    {
        employeeLoader.Load();

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
                    var hackathonId = hackathon.Run();
                    Console.WriteLine($"Hackathon ID: {hackathonId}");
                    break;
                }
                case "2":
                {
                    Console.WriteLine("Enter the hackathon ID");
                    Console.Write("> ");
                    var stringGuid = Console.ReadLine();
                    try
                    {
                        var id = Guid.Parse(stringGuid);
                        hackathonPrinter.Print(id);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("The string to be parsed is null.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Bad format: {stringGuid}");
                    }
                    break;
                }
                case "3":
                {
                    Console.WriteLine(
                        $"Average satisfaction index for all hackathons is {avgSatisfactionCalculator.calculate()}"
                    );
                    break;
                }
                case "q":
                {
                    applicationLifetime.StopApplication();
                    return Task.CompletedTask;
                }
                default:
                {
                    Console.WriteLine("Unexpected command. Try again:");
                    Console.Write("> ");
                    break;
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
