using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using HackathonProblem.Service;
using HackathonProblem.Contracts;
using HackathonProblem.Service.Transient;
using HackathonProblem.TeamBuildingStrategy;
  
var host = Host.CreateDefaultBuilder(args) 
        .ConfigureServices((hostContext, services) => 
	{ 
        services.AddHostedService<HackathonWorker>(); 
        services.AddTransient<Hackathon>(); 
        services.AddTransient<ITeamBuildingStrategy, GaleShapleyAlgorithm>(); 
        services.AddTransient<HrManager>();
        services.AddTransient<HrDirector>();
  	}).Build(); 
host.Run(); 
