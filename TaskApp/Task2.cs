using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace TaskApp;

public static class Task2
{
    static readonly HttpClient client = new HttpClient();
    
    public static async Task<List<StarShip>> FindStarships(string planetName)
    {
        List<StarShip> starships_peopleFrom_Kashyyyk = [];
        try
        {
            // 1) najdu si planetu, která má zadaný název 
            string url = "https://swapi.info/api/planets";
            using HttpResponseMessage responsPlanets = await client.GetAsync(url);
            var jsonResponsePlanets = await responsPlanets.Content.ReadAsStringAsync();

            
            List<Planet> planets = JsonConvert.DeserializeObject<List<Planet>>(jsonResponsePlanets);
            Planet p_Kashyyyk = planets.Where(p => p.name == planetName).FirstOrDefault();

            // 2) u nalezené planety jsou k dispozici URL rezidentů
            //    tyto rezidenty si uložím do listu peopleFrom_Kashyyyk
            List<People> peopleFrom_Kashyyyk = [];
            foreach (var urlPeople in p_Kashyyyk.residents)
            {
                using HttpResponseMessage responsPeople = await client.GetAsync(urlPeople);
                var jsonResponsePeople = await responsPeople.Content.ReadAsStringAsync();
                peopleFrom_Kashyyyk.Add(JsonConvert.DeserializeObject<People>(jsonResponsePeople)); 
            }

            // 3) procházím uložené rezidenty a ukládám si jejich StarShips
            foreach (var people in peopleFrom_Kashyyyk)
            {
                foreach (var urlStarship in people.starships)
                {
                    using HttpResponseMessage responsStarship = await client.GetAsync(urlStarship);
                    var jsonResponseStarship = await responsStarship.Content.ReadAsStringAsync();
                    starships_peopleFrom_Kashyyyk.Add(JsonConvert.DeserializeObject<StarShip>(jsonResponseStarship));
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Exception Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }

        return starships_peopleFrom_Kashyyyk;
    }

    public static void PrintStarships(List<StarShip> starships)
    {
        foreach (StarShip starship in starships)
        {
            Console.WriteLine($"\t{starship.name}");
        }
    }
}

class People
{
    public string name { get; set; }
    public string height { get; set; }
    public string mass { get; set; }
    public string hair_color { get; set; }
    public string skin_color { get; set; }
    public string eye_color { get; set; }
    public string birth_year { get; set; }
    public string gender { get; set; }
    public string homeworld { get; set; }
    public List<string> films { get; set; }
    public List<string> species { get; set; }
    public List<string> vehicles { get; set; }
    public List<string> starships { get; set; }
    public DateTime created { get; set; }
    public DateTime edited { get; set; }
    public string url { get; set; }
}

public class StarShip
{
    public string name { get; set; }
    public string model { get; set; }
    public string manufacturer { get; set; }
    public string cost_in_credits { get; set; }
    public string length { get; set; }
    public string max_atmosphering_speed { get; set; }
    public string crew { get; set; }
    public string passengers { get; set; }
    public string cargo_capacity { get; set; }
    public string consumables { get; set; }
    public string hyperdrive_rating { get; set; }
    public string MGLT { get; set; }
    public string starship_class { get; set; }
    public List<string> pilots { get; set; }
    public List<string> films { get; set; }
    public DateTime created { get; set; }
    public DateTime edited { get; set; }
    public string url { get; set; }
}

class Planet
{
    public string name { get; set; }
    public string rotation_period { get; set; }
    public string orbital_period { get; set; }
    public string diameter { get; set; }
    public string climate { get; set; }
    public string gravity { get; set; }
    public string terrain { get; set; }
    public string surface_water { get; set; }
    public string population { get; set; }
    public List<string> residents { get; set; }
    public List<string> films { get; set; }
    public DateTime created { get; set; }
    public DateTime edited { get; set; }
    public string url { get; set; }
}
