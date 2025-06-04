using TaskApp;

// úkol č. 1)
//
// máte pole, které má 1 000 000 položek (int) - náhodně vygenerujte
// - vypište všechny duplicity (duplicitní hodnoty)

Console.WriteLine("Úkol č. 1:\n");
Task1.FindDuplicities(1000000);


// úkol č. 2)
// Použijte REST API https://swapi.dev/
//
// Aplikace by měla ukázat, jak z API načtete data / objekty
// Vyhledejte všechny lodě jejichž pilot je z planety "Kashyyyk"

Console.WriteLine("\nÚkol č. 2:\n");
List<StarShip> starShips = await Task2.FindStarships("Kashyyyk");
Task2.PrintStarships(starShips);

// úkol č. 3)
// Vytvořte datovou reprezentaci posádky lodi Enterprise a splňte
// následující dotazy
//
// a) Napište funkci, která vrátí všechny podřízené zadaného člověka
// b) Osoba X se nakazila virem, vypište všechny další členy posádky,
//    kterí se mohli nakazit, než nakazili kapitána lodi

Console.WriteLine("\nÚkol č. 3:\n");
// a)
string name = "Worf son of Mog";
List<Person> subordinates = Task3.FindSubordinate(name); // vrací podřízené zadaného člověka
Task3.PrintSubordinateResults(subordinates,name);        // výpis podřízených

// b)
name = "Julian Bashir";
List<Person> infected = Task3.FindInfected(name);
Task3.PrintInfectedResults(infected, name);        // výpis podřízených

Console.WriteLine("\n\nStisknutím liboolné klávesy zavřete okno...");
Console.ReadKey();



