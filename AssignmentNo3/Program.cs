internal class Program
{
    private static void Main(string[] args)
    {
    }


    public static IEnumerable<IEnumerable<string>> OnlyBigCollections(List<IEnumerable<string>> toFilter)
    {
        // We are checking out max 6 elements 
        Func<IEnumerable<string>, bool> predicate = list => list.Skip(5).Any();

        return toFilter.Where(predicate);
    }

}