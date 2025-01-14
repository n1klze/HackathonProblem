using HackathonProblem.Contracts;
using HackathonProblem.Database.Context;
using HackathonProblem.SatisfactionCalculationMethod;
using HackathonProblem.Service;
using HackathonProblem.Service.Transient;
using HackathonProblem.TeamBuildingStrategy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (hostContext, services) =>
        {
            services.AddHostedService<HackathonWorker>();
            services.AddTransient<Hackathon>();
            services.AddTransient<ITeamBuildingStrategy, GaleShapleyAlgorithm>();
            services.AddTransient<HrManager>();
            services.AddTransient<HrDirector>();
            services.AddTransient<WishlistGenerator>();
            services.AddTransient<ISatisfactionCalculationMethod, HarmonicMeanCalculator>();
            services.AddDbContext<HackathonContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;"
                        + "Port=5434;"
                        + "Database=hackathon-db;"
                        + "Username=admin;"
                        + "Password=admin"
                );
            });
            services.AddTransient<EmployeeLoader>();
            services.AddTransient<AvgSatisfactionCalculator>();
            services.AddTransient<HackathonPrinter>();
        }
    )
    .Build();
host.Run();
