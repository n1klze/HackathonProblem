using HackathonProblem.Contracts;
using HackathonProblem.SatisfactionCalculationMethod;
using HackathonProblem.Service.Transient;

namespace HackathonProblemTest;

[TestClass]
public sealed class HrDirectorTest()
{
    private readonly double precision = 1e-9;

    [TestMethod]
    public void CorrectAnswerTest()
    {
        var teamLeads = new List<Employee>(
            [new Employee(1, "Alexander"), new Employee(2, "Maria"), new Employee(3, "Dmitry")]
        );

        var juniors = new List<Employee>(
            [new Employee(1, "Sofia"), new Employee(2, "Nikolai"), new Employee(3, "Anastasia")]
        );

        var teamLeadsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2, 3]), new Wishlist(2, [2, 3, 1]), new Wishlist(3, [3, 1, 2])]
        );

        var juniorsWishlists = new List<Wishlist>(
            [new Wishlist(1, [1, 2, 3]), new Wishlist(2, [2, 3, 1]), new Wishlist(3, [3, 1, 2])]
        );

        var teams = new List<Team>(
            [
                new Team(teamLeads[0], juniors[0]),
                new Team(teamLeads[1], juniors[1]),
                new Team(teamLeads[2], juniors[2]),
            ]
        );

        var hrDirector = new HrDirector(new HarmonicMeanCalculator());
        var satisfactionIndex = hrDirector.CalculateSatisfactionIndex(
            teams,
            teamLeadsWishlists,
            juniorsWishlists
        );
        Assert.AreEqual(satisfactionIndex, 3, precision, "HR Director gives the wrong answer.");
    }
}
