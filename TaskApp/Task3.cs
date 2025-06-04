using System.Runtime.CompilerServices;

namespace TaskApp;

public static class Task3
{
    private static List<Person> Crew = [];
    public static List<Person> FindSubordinate(string name)
    {
        List<Person> subordinates = [];

        // inicializace posádky
        Crew = CreateCrew();

        // nalezení zadané osoby z posádky
        Person inputPerson = FindPerson(name);
        
        // v případě, že zadaná osoba nemá podřízené, metoda vrací prázný list
        if (inputPerson.Subordinates == null)
        {
            return subordinates;
        }

        // přidání přímo-podřízených osob do seznamu
        subordinates.AddRange(inputPerson.Subordinates);

        // procházení přidaných podřízených v cyklu,
        // který je dynamicky rozšiřuje procházené pole
        int i = 0;
        while (i < subordinates.Count)
        {
            // rozšíření seznamu o podřízené testované osoby
            if (subordinates[i].Subordinates != null)
            {
                subordinates.AddRange(subordinates[i].Subordinates);
            }
            i++;
        }

        return subordinates;
    }

    public static void PrintSubordinateResults(List<Person> subordinates, string name)
    {
        if (subordinates.Count > 1)
        {
            Console.WriteLine($"{name} má {subordinates.Count} podřízených:\n");
            foreach (var person in subordinates)
            {
                Console.WriteLine(person.Name);
            }
        }
        else
        {
            Console.WriteLine($"{name} nemá žádné podřízené.");
        }
    }

    public static List<Person> FindInfected(string name)
    {
        List<Person> infected = [];

        // inicializace posádky
        Crew = CreateCrew();
        Person inputPerson = FindPerson(name);      
        int? superiorId = inputPerson.IdSuperior;
        if (superiorId == null) // osoba nemá žádné nadřízené
        {
            return infected;            
        }
        else if (superiorId == 0) // osoba má nadřízeného už jenom kapitána
        {
            infected = FindSubordinate(name);
            return infected;
        }
        else  // nadřízený není kapitánem
        {
            // hladání nejvyššího nadřízeného, který ještě není kapitánem
            while (superiorId != 0)
            {
                int? nextSuperiorId = FindSuperior(superiorId);
                if (nextSuperiorId == 0)
                {
                    break;
                }
                else
                {
                    superiorId = nextSuperiorId;
                }
            }

            Person topInfected = FindPerson(superiorId);

            // nalezení všech podřízených příslušného nadřízeného
            infected = FindSubordinate(topInfected.Name);

            // do seznamu zahrnout i nejvyššího nadřízeného
            infected.Add(topInfected);

            // naopak odebrat prvotního nakženého (úkol je vypsat všechny DALŠÍ členy)
            infected.Remove(inputPerson);

            return infected;
        }
    }

    public static void PrintInfectedResults(List<Person> infected, string name)
    {
        if (infected.Count > 1)
        {
            Console.WriteLine($"{name} nakazil {infected.Count} členů posádky, než nakazili kapitána:\n");
            foreach (var person in infected)
            {
                Console.WriteLine(person.Name);
            }
        }
        else
        {
            Console.WriteLine($"Nakazil se přímo kapitán");
        }
    }

    private static int? FindSuperior(int? id)
    {
        Person person = FindPerson(id);
        return person.IdSuperior;
    }

    private static Person FindPerson(string name)
    {

        Person? inputPerson = Crew.Where(p => p.Name == name).FirstOrDefault();
        if (inputPerson == null)
        {
            throw new ArgumentException($"Jméno {name} nebylo v posádce nalezeno.");
        }
        return inputPerson;
    }

    private static Person FindPerson(int? id)
    {
        
        Person? inputPerson = Crew.Where(p => p.Id == id).FirstOrDefault();
        if (inputPerson == null)
        {
            throw new ArgumentException($"ID {id} nebylo v posádce nalezeno.");
        }
        return inputPerson;
    }

    // Posádka je vytvořena natvrdo dle zadání úkolu
    private static List<Person> CreateCrew()
    {

        Person AlexanderR = new Person(15, "Alexander Rozhenko", 12, null);
        Person JulianB = new Person(16, "Julian Bashir", 14, null);
        Person AllyssaO = new Person(14, "Alyssa Ogawa", 6, new List<Person> { JulianB });
        Person WeslleyC = new Person(13, "Weslley Crusher", 6, null);
        Person KEhleyr = new Person(12, "K'Ehleyr", 4, new List<Person> { AlexanderR });
        Person TashaY = new Person(11, "Tasha Yar", 4, null);
        Person MillesO = new Person(10, "Milles O'Brien", 3, null);
        Person MrData = new Person(9, "Mr. Data", 3, null);
        Person ReginaldB = new Person(8, "Reginald Barkley", 2, null);
        Person LwaxanaT = new Person(7, "Lwaxana Troi", 2, null);
        Person BeverlyC = new Person(6, "Beverly Crusher", 1,
                                new List<Person> { WeslleyC, AllyssaO });
        Person Guinan = new Person(5, "Guinan", 1, null);
        Person WorfSoM = new Person(4, "Worf son of Mog", 1,
                                new List<Person> { TashaY, KEhleyr });
        Person JordiL = new Person(3, "Jordi La Forge", 0,
                                new List<Person> { MrData, MillesO });
        Person DeanaT = new Person(2, "Deana Troi", 0,
                                new List<Person> { ReginaldB, LwaxanaT });
        Person WilliamR = new Person(1, "William Riker", 0,
                                new List<Person> { WorfSoM, Guinan, BeverlyC });
        Person JeanLuc = new Person(0, "Jean Luc Pickard", null,
                                new List<Person> { WilliamR, DeanaT, JordiL });

        Crew.AddRange(
            JeanLuc, WilliamR, DeanaT, JordiL, WorfSoM, Guinan, BeverlyC, LwaxanaT,
            ReginaldB, MrData, MillesO, TashaY, KEhleyr, WeslleyC, AllyssaO, JulianB,
            AlexanderR);

        return Crew;
    }

}

public class Person
{
    public int Id;
    public string Name;
    public List<Person>? Subordinates; // possible null for no subordinates
    public int? IdSuperior; // possible null for no superior

    public Person(int id, string name, int? idSuperior, List<Person>? subordinates)
    {
        Id = id;
        Name = name;
        IdSuperior = idSuperior;
        Subordinates = subordinates;
    }
}
