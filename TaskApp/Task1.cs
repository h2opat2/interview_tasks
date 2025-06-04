public static class Task1
{
    // metoda k ukolu c.1
    public static void FindDuplicities(int itemsCount)
    {
        int[] items = new int[itemsCount];
        var rand = new Random();

        for (int i = 0; i < itemsCount; i++)
        {
            items[i] = rand.Next(0, itemsCount + 1);
        }

        // LINQ dotaz na data, která seskupím podle jejich hodnot. Vyberu takové záznamy,
        // kde počet seskupených hodnot je vyšší než 1.
        // Vytvořím abstrakní prvek, který obsahuje duplicitní hodnotu a počet, kolikrát se 
        // objevila
        var duplicities = items.GroupBy(i => i)
                                .Where(g => g.Count() > 1)
                                .Select(d => new
                                {
                                    Value = d.Key,
                                    Counter = d.Count()
                                }).ToList();

        int duplicityCount = duplicities.Count();

        Console.WriteLine($"Celkem nalezeno: {duplicityCount} duplicitních položek.");
        Console.WriteLine($"\t{"Duplicita",-10} {"Počet"}");

        // duplicitních záznamů může být mnoho, zobrazím pouze prvních 10
        if (duplicityCount > 10)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\t{duplicities[i].Value,-10} {duplicities[i].Counter}");
            }
            Console.WriteLine($"... a dalších {duplicityCount - 10} položek.");
        }

    }
}
