using HackathonProblem.Contracts;

namespace HackathonProblem.Utils.CsvExtension;

public static class CsvLoader
{
    public static IList<Employee> Load(string filePath)
    {
        var employees = new List<Employee>();

        using (var reader = new StreamReader(filePath))
        {
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                var values = line.Split(';');

                int id = int.Parse(values[0]);
                string name = values[1];

                employees.Add(new Employee(id, name));
            }
        }

        return employees;
    }
}
