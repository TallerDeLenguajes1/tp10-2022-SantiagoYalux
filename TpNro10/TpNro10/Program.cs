
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


civilizationsAge resultado = new civilizationsAge();
int eleccion = 0;
string url = @"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
var request = (HttpWebRequest)WebRequest.Create(url);

Random rnd = new Random();

request.Method = "GET";
request.ContentType = "application/json";
request.Accept = "application/json";

using (WebResponse response = request.GetResponse())
{
    using (Stream strReader = response.GetResponseStream())
    {
        if(strReader != null)
        {
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();

                resultado = JsonSerializer.Deserialize<civilizationsAge>(responseBody); 
                

            }
        }
    }
}

if(resultado.Civilizations.Count > 0)
{
    foreach (Civilization civilization in resultado.Civilizations)
    {
        civilization.mostrarDatos();
    }

    Console.WriteLine("\n");
    Console.WriteLine("Elija una en particular, por el ID");
    eleccion = int.Parse(Console.ReadLine());
    resultado.Civilizations.ElementAt(eleccion).mostrarDatos();
}
public class Civilization
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("expansion")]
    public string Expansion { get; set; }

    [JsonPropertyName("army_type")]
    public string ArmyType { get; set; }

    [JsonPropertyName("unique_unit")]
    public List<string> UniqueUnit { get; set; }

    [JsonPropertyName("unique_tech")]
    public List<string> UniqueTech { get; set; }

    [JsonPropertyName("team_bonus")]
    public string TeamBonus { get; set; }

    [JsonPropertyName("civilization_bonus")]
    public List<string> CivilizationBonus { get; set; }

    public void mostrarDatos()
    {
        Console.WriteLine("\n");
        Console.WriteLine("--------------------------");
        Console.Write($"ID = {Id}");
        Console.Write($"NOMBRE = {Name}");
        Console.Write($"Expansion = {Expansion}");
        Console.Write($"ArmyType = {ArmyType}");

        foreach (var item in UniqueUnit)
        {
            Console.Write($"UniqueUnit = {item}");
        }

        foreach (var item in UniqueTech)
        {
            Console.Write($"UniqueTech = {item}");
        }

        Console.Write($"TeamBonus = {TeamBonus}");

        foreach (var item in CivilizationBonus)
        {
            Console.Write($"CivilizationBonus = {item}");
        }
        Console.WriteLine("--------------------------");
    }
}

public class civilizationsAge
{
    [JsonPropertyName("civilizations")]
    public List<Civilization> Civilizations { get; set; }
}
