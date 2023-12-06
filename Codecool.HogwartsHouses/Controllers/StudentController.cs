using Codecool.HogwartsHouses.Model;
using Codecool.HogwartsHouses.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Codecool.HogwartsHouses.Controllers;


[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly string _connectionString = Connection.GetConnectionString();

    [HttpGet]
    public IActionResult GetAll()
    {
        var rep = new StudentRepository(new NpgsqlConnection(_connectionString));
        return Ok(rep.GetAll());
    }
    
    [HttpPost]
    public IActionResult CreateStudent(Student student)
    {
        var rep = new StudentRepository(new NpgsqlConnection(_connectionString));
        return Ok(rep.Create(student));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var rep = new StudentRepository(new NpgsqlConnection(_connectionString));
        rep.DeleteStudent(id);

        return Ok();
    }
}