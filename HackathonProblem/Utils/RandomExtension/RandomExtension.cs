namespace HackathonProblem.Utils.RandomExtension;

public static class RandomExtension
{
    private static readonly Random _random = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            var i = _random.Next(n);
            --n;
            (list[i], list[n]) = (list[n], list[i]);
        }
    }
}