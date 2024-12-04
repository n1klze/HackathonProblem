using HackathonProblem.Contracts;
using HackathonProblem.SatisfactionCalculationMethod;
using HackathonProblem.Service;
using HackathonProblem.Service.Transient;
using HackathonProblem.TeamBuildingStrategy;
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
        }
    )
    .Build();
host.Run();
