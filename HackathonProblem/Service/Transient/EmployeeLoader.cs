using HackathonProblem.Database.Context;
using HackathonProblem.Database.Entity;
using HackathonProblem.Settings;
using HackathonProblem.Utils.CsvExtension;
using Microsoft.Extensions.Configuration;

namespace HackathonProblem.Service.Transient;

public class EmployeeLoader(IConfiguration configuration, HackathonContext hackathonContext)
{
    private readonly SamplesData samplesData =
        configuration.GetSection("Samples").Get<SamplesData>()
        ?? throw new InvalidOperationException("Samples section must be set in appsettings file.");

    public void Load()
    {
        var teamLeads = CsvLoader.Load(samplesData.TeamLeads);
        var juniors = CsvLoader.Load(samplesData.Juniors);

        var teamLeadsEntities = teamLeads
            .Select(tl =>
            {
                var teamLeadDto = new TeamLeadDto();
                teamLeadDto.Id = tl.Id;
                teamLeadDto.Name = tl.Name;
                return teamLeadDto;
            })
            .ToList();

        var juniorsEntities = juniors
            .Select(j =>
            {
                var juniorDto = new JuniorDto();
                juniorDto.Id = j.Id;
                juniorDto.Name = j.Name;
                return juniorDto;
            })
            .ToList();

        hackathonContext.TeamLeads.RemoveRange(hackathonContext.TeamLeads);
        hackathonContext.Juniors.RemoveRange(hackathonContext.Juniors);

        hackathonContext.TeamLeads.AddRange(teamLeadsEntities);
        hackathonContext.Juniors.AddRange(juniorsEntities);
        hackathonContext.SaveChanges();
    }
}
