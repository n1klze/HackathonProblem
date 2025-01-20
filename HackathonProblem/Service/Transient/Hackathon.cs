using HackathonProblem.Contracts;
using HackathonProblem.Database.Context;
using HackathonProblem.Database.Entity;

namespace HackathonProblem.Service.Transient;

public class Hackathon(
    WishlistGenerator wishlistGenerator,
    HrManager hrManager,
    HrDirector hrDirector,
    HackathonContext hackathonContext
)
{
    public Guid Run()
    {
        var teamLeadsDtoList = hackathonContext.TeamLeads.ToList();
        var teamLeads = teamLeadsDtoList.Select(tlDto => new Employee(tlDto.Id, tlDto.Name));
        var juniorsDtoList = hackathonContext.Juniors.ToList();
        var juniors = juniorsDtoList.Select(jDto => new Employee(jDto.Id, jDto.Name));

        var (teamLeadsWishlists, juniorsWishlists) = wishlistGenerator.Generate(teamLeads, juniors);
        var teamLeadsWishlistsDto = teamLeadsWishlists
            .Select(tlWishlist =>
            {
                var dto = new TeamLeadWishlistDto();
                dto.EmployeeId = tlWishlist.EmployeeId;
                dto.DesiredEmployee = tlWishlist.DesiredEmployees;
                return dto;
            })
            .ToList();
        hackathonContext.TeamLeadsWishlists.AddRange(teamLeadsWishlistsDto);
        var juniorsWishlistsDto = juniorsWishlists
            .Select(jWishlist =>
            {
                var dto = new JuniorWishlistDto();
                dto.EmployeeId = jWishlist.EmployeeId;
                dto.DesiredEmployee = jWishlist.DesiredEmployees;
                return dto;
            })
            .ToList();
        hackathonContext.JuniorsWishlists.AddRange(juniorsWishlistsDto);

        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        var teamsDto = teams
            .Select(t =>
            {
                var dto = new TeamDto();
                dto.TeamLead = teamLeadsDtoList.Where(l => l.Id == t.TeamLead.Id).Single();
                dto.Junior = juniorsDtoList.Where(j => j.Id == t.Junior.Id).Single();
                return dto;
            })
            .ToList();
        hackathonContext.Teams.AddRange(teamsDto);

        var satisfactionIndex = hrDirector.CalculateSatisfactionIndex(
            teams,
            teamLeadsWishlists,
            juniorsWishlists
        );

        var id = Guid.NewGuid();
        var hackathonDto = new HackathonDto
        {
            Id = id,
            TeamLeads = teamLeadsDtoList,
            Juniors = juniorsDtoList,
            TeamLeadsWishlists = teamLeadsWishlistsDto,
            JuniorsWishlists = juniorsWishlistsDto,
            Teams = teamsDto,
            SatisfactionIndex = satisfactionIndex,
        };
        hackathonContext.Hackathons.Add(hackathonDto);

        hackathonContext.SaveChanges();
        return id;
    }
}
