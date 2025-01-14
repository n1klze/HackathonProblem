using HackathonProblem.Database.Context;
using HackathonProblem.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Service.Transient;

public class HackathonPrinter(HackathonContext hackathonContext)
{
    public void Print(Guid hackathonId)
    {
        HackathonDto hackathon;
        try
        {
            hackathon = hackathonContext
                .Hackathons.Where(h => h.Id == hackathonId)
                .Include(h => h.TeamLeads)
                .Include(h => h.Juniors)
                .Include(h => h.Teams)
                .ThenInclude(t => t.TeamLead)
                .Include(h => h.Teams)
                .ThenInclude(t => t.Junior)
                .AsSplitQuery()
                .First();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Hackathon not found: {e.Message}");
            return;
        }
        Console.WriteLine();
        Console.WriteLine($"Hackathon ID: {hackathonId}");

        Console.WriteLine();
        Console.WriteLine("Team leads:");
        Console.WriteLine($"{"Id", 4}  |  {"Name", 10}");
        Console.WriteLine("------+------------------------");
        foreach (var tl in hackathon.TeamLeads)
            Console.WriteLine($"{tl.Id, 4}  |  {tl.Name}");

        Console.WriteLine();
        Console.WriteLine("Juniors:");
        Console.WriteLine($"{"Id", 4}  |  {"Name", 10}");
        Console.WriteLine("------+------------------------");
        foreach (var j in hackathon.Juniors)
            Console.WriteLine($"{j.Id, 4}  |  {j.Name}");

        Console.WriteLine();
        Console.WriteLine("Teams:");
        Console.WriteLine($"{"Team lead", 24}  |  {"Junior"}");
        Console.WriteLine("--------------------------+--------------------------");
        foreach (var t in hackathon.Teams)
            Console.WriteLine($"{t.TeamLead.Name, 24}  |  {t.Junior.Name}");

        Console.WriteLine();
        Console.WriteLine($"Satisfaction index: {hackathon.SatisfactionIndex}");
        Console.WriteLine();
    }
}
