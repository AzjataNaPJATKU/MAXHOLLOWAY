using goodDayBruw.Models;
using goodDayBruw.Models.TDOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace goodDayBruw.Controllers;

[ApiController]
//[Route("api/animals")]
[Route(("api/[controller]"))]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult GetAnimals()
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        // Definicja command

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * from Animal";
        
        //wykonanie zapytania
        var reader = sqlCommand.ExecuteReader();

        List<Animal> animals = new List<Animal>();

        int idAnimal = reader.GetOrdinal("idAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                idAnimal = reader.GetInt32(idAnimal),
                //Name = reader["Name"].ToString()
                Name = reader.GetString(nameOrdinal),
                
            });
        }
        
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        // Definicja command

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO Animal Values (@animalName,'','','')";
        sqlCommand.Parameters.AddWithValue("@animalName", addAnimal.Name);
        
        return Created();
    }
    
}