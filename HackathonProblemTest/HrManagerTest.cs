using HackathonProblem.Contracts;
using HackathonProblem.Service.Transient;
using HackathonProblem.TeamBuildingStrategy;
using Moq;

namespace HackathonProblemTest;

[TestClass]
public sealed class HrManagerTest
{
    [TestMethod]
    public void SizesTest()
    {
        var teamLeads = new List<Employee>(
            [new Employee(1, "Alexander"), new Employee(2, "Maria")]
        );

        var juniors = new List<Employee>([new Employee(1, "Sofia"), new Employee(2, "Nikolai")]);

        var teamLeadsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2]), new Wishlist(2, [2, 1])]
        );

        var juniorsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2]), new Wishlist(2, [2, 1])]
        );

        var hrManager = new HrManager(new GaleShapleyAlgorithm());

        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.AreEqual(
            2,
            teams.Count(),
            "Number of teams should be equal to number of juniors (team leads)."
        );
    }

    [TestMethod]
    public void CorrectResultTest()
    {
        var teamLeads = new List<Employee>(
            [new Employee(1, "Alexander"), new Employee(2, "Maria")]
        );

        var juniors = new List<Employee>([new Employee(1, "Sofia"), new Employee(2, "Nikolai")]);

        var teamLeadsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2]), new Wishlist(2, [2, 1])]
        );

        var juniorsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2]), new Wishlist(2, [2, 1])]
        );

        var teams = new List<Team>(
            [new Team(teamLeads[0], juniors[0]), new Team(teamLeads[1], juniors[1])]
        );

        var hrManager = new HrManager(new GaleShapleyAlgorithm());

        var result = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        foreach (var (t, r) in teams.Zip(result))
        {
            Assert.AreEqual(t, r, "HR Manager gives the wrong answer.");
        }
    }

    [TestMethod]
    public void GenerateTeamsStrategyCalledOnce()
    {
        var teamLeads = new List<Employee>(
            [new Employee(1, "Alexander"), new Employee(2, "Maria")]
        );

        var juniors = new List<Employee>([new Employee(1, "Sofia"), new Employee(2, "Nikolai")]);

        var teamLeadsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2]), new Wishlist(2, [2, 1])]
        );

        var juniorsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2]), new Wishlist(2, [2, 1])]
        );

        var mock = new Mock<ITeamBuildingStrategy>();
        mock.Setup(e =>
                e.BuildTeams(
                    It.IsAny<IEnumerable<Employee>>(),
                    It.IsAny<IEnumerable<Employee>>(),
                    It.IsAny<IEnumerable<Wishlist>>(),
                    It.IsAny<IEnumerable<Wishlist>>()
                )
            )
            .Returns(new List<Team>());

        var hrManager = new HrManager(mock.Object);

        hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        mock.Verify(
            s =>
                s.BuildTeams(
                    It.IsAny<List<Employee>>(),
                    It.IsAny<List<Employee>>(),
                    It.IsAny<List<Wishlist>>(),
                    It.IsAny<List<Wishlist>>()
                ),
            Times.Once
        );
    }
}
