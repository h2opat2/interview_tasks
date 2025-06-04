// úkol č. 1)
//
// máte pole, které má 1 000 000 položek (int) - náhodně vygenerujte
// - vypište všechny duplicity (duplicitní hodnoty)

using TaskApp;

Console.WriteLine("Úkol č. 1:\n");
Task1.FindDuplicities(1000000);

// úkol č. 3)
// Vytvořte datovou reprezentaci posádky lodi Enterprise a splňte
// následující dotazy
//
// a) Napište funkci, která vrátí všechny podřízené zadaného člověka
// b) Osoba X se nakazila virem, vypište všechny další členy posádky,
//    kterí se mohli nakazit, než nakazili kapitána lodi

Console.WriteLine("Úkol č. 3:\n");
string name = "Julian Bashir";
List<Person> subordinates = Task3.FindSubordinate(name); // vrací podřízené zadaného člověka
Task3.PrintSubordinateResults(subordinates,name);        // výpis podřízených

List<Person> infected = Task3.FindInfected(name);
Task3.PrintInfectedResults(infected,name);        // výpis podřízených



