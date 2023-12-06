using Codecool.HogwartsHouses.Data;
using Codecool.HogwartsHouses.Model;
using Codecool.HogwartsHouses.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Codecool.HogwartsHouses.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly string _connectionString = Connection.GetConnectionString();

    [HttpGet]
    public IActionResult GetAll()
    {
        var rep = new RoomRepository(new NpgsqlConnection(_connectionString));
        return Ok(rep.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var rep = new RoomRepository(new NpgsqlConnection(_connectionString));
        return Ok(rep.GetById(id));
    }
    
    [HttpPost]
    public IActionResult Create()
    {
        IRoomGenerator RoomGenerator = new RoomGenerator();
        IEnumerable<Room> _rooms = RoomGenerator.Generate(1);
        var rep = new RoomRepository(new NpgsqlConnection(_connectionString));
        var room = _rooms.First();
        return Ok(rep.Create(room));
    }

    [HttpPost("{roomId}/students/{studentId}")]
    public IActionResult AssignStudentToRoom(Guid roomId, Guid studentId)
    {
        var roomRep = new RoomRepository(new NpgsqlConnection(_connectionString));
        var studentRep = new StudentRepository(new NpgsqlConnection(_connectionString));
        var student = studentRep.GetStudentById(studentId);
        var room = roomRep.GetById(roomId);

        return Ok(room.AddStudent(student));
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(Guid id)
    {
        var rep = new RoomRepository(new NpgsqlConnection(_connectionString));
        rep.Delete(id);

        return Ok();
    }
}