using HackathonProblem.Contracts;
using HackathonProblem.Service.Transient;

namespace HackathonProblemTest;

[TestClass]
public sealed class GenerateWishlistTest
{
    private WishlistGenerator _wishlistGenerator = new();

    [TestMethod]
    public void SizesTest()
    {
        var teamLeads = new List<Employee>(
            [
                new Employee(1, "Alexander"),
                new Employee(2, "Maria"),
                new Employee(3, "Dmitry"),
                new Employee(4, "Elena"),
                new Employee(5, "Ivan"),
            ]
        );

        var juniors = new List<Employee>(
            [
                new Employee(1, "Sofia"),
                new Employee(2, "Nikolai"),
                new Employee(3, "Anastasia"),
                new Employee(4, "Maxim"),
                new Employee(5, "Olga"),
            ]
        );

        var (teamLeadsWishlists, juniorsWishlists) = _wishlistGenerator.Generate(
            teamLeads,
            juniors
        );
        Assert.AreEqual(
            teamLeads.Count,
            teamLeadsWishlists.Count(),
            "Number of wishlists must be equal with number of team leads"
        );
        Assert.AreEqual(
            juniors.Count,
            juniorsWishlists.Count(),
            "Number of wishlist must be equal with number of juniors"
        );

        foreach (var wishlist in teamLeadsWishlists)
        {
            Assert.AreEqual(
                juniors.Count,
                wishlist.DesiredEmployees.Length,
                "Number of desired team leads should be equal to number of teamleads"
            );
        }

        foreach (var wishlist in juniorsWishlists)
        {
            Assert.AreEqual(
                teamLeads.Count,
                wishlist.DesiredEmployees.Length,
                "Number of desired juniors should be equal to number of juniors"
            );
        }
    }

    [TestMethod]
    public void WrongSizesTest()
    {
        var teamLeads = new List<Employee>(
            [new Employee(1, "Alexander"), new Employee(2, "Maria"), new Employee(3, "Dmitry")]
        );

        var juniors = new List<Employee>([new Employee(1, "Sofia"), new Employee(2, "Nikolai")]);

        Assert.ThrowsException<ArgumentException>(() =>
        {
            var (teamLeadsWishlists, juniorsWishlists) = _wishlistGenerator.Generate(
                teamLeads,
                juniors
            );
        });
    }

    [TestMethod]
    public void ContainsEmployeeTest()
    {
        var teamLeads = new List<Employee>(
            [
                new Employee(1, "Alexander"),
                new Employee(2, "Maria"),
                new Employee(3, "Dmitry"),
                new Employee(4, "Elena"),
                new Employee(5, "Ivan"),
            ]
        );

        var juniors = new List<Employee>(
            [
                new Employee(1, "Sofia"),
                new Employee(2, "Nikolai"),
                new Employee(3, "Anastasia"),
                new Employee(4, "Maxim"),
                new Employee(5, "Olga"),
            ]
        );

        var (teamLeadsWishlists, juniorsWishlists) = _wishlistGenerator.Generate(
            teamLeads,
            juniors
        );

        foreach (var wishlist in teamLeadsWishlists)
        {
            foreach (var junior in juniors)
            {
                var isFound = false;
                foreach (var desiredEmployee in wishlist.DesiredEmployees)
                {
                    if (desiredEmployee == junior.Id)
                        isFound = true;
                }
                Assert.IsTrue(isFound, "One of junior not found in wishlist");
            }
        }

        foreach (var wishlist in juniorsWishlists)
        {
            foreach (var teamLead in teamLeads)
            {
                var isFound = false;
                foreach (var desiredEmployee in wishlist.DesiredEmployees)
                {
                    if (desiredEmployee == teamLead.Id)
                        isFound = true;
                }
                Assert.IsTrue(isFound, "One of team lead not found in wishlist");
            }
        }
    }
}
