using System.Data;
using Codecool.HogwartsHouses.Model;
using Npgsql;

namespace Codecool.HogwartsHouses.Services;

public class StudentRepository : IStudentRepository
{
    private readonly NpgsqlConnection _connection;

    public StudentRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }
    
    public IEnumerable<Student> GetAll()
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter("SELECT * FROM students", _connection);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var table = dataSet.Tables[0];

        var queryResult = new List<Student>();
        foreach (DataRow row in table.Rows)
        {
            queryResult.Add(new Student(
                (Guid)row["student_id"],
                (string)row["student_name"],
                (Gender)row["gender"],
                (House)row["house"],
                (Pet)row["pet"],
                (Guid)row["room_id"]
            ));
        }

        _connection.Close();

        return queryResult;
    }

    public Guid Create(Student student)
    {
        var query =
            "INSERT INTO students (student_id, student_name, gender, house, pet, room_id) VALUES (:student_id, :student_name, :gender, :house, :pet, :room_id) RETURNING student_id";
        _connection.Open();
        
        var adapter = new NpgsqlDataAdapter(query, _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":student_id", student.Id);
        adapter.SelectCommand?.Parameters.AddWithValue(":student_name", student.Name);
        adapter.SelectCommand?.Parameters.AddWithValue(":gender", student.Gender);
        adapter.SelectCommand?.Parameters.AddWithValue(":house", student.HogwartsHouse);
        adapter.SelectCommand?.Parameters.AddWithValue(":pet", student.OwnedPet);
        if (student.RoomId.HasValue)
        {
            adapter.SelectCommand?.Parameters.AddWithValue(":room_id", student.RoomId);
        }
        else
        {
            adapter.SelectCommand?.Parameters.AddWithValue(":room_id", DBNull.Value);
        }

        var lastInsertId = (Guid)adapter.SelectCommand?.ExecuteScalar();
        _connection.Close();

        return lastInsertId;
    }

    public Student GetStudentById(Guid id)
    {
        var query = $"SELECT * FROM students WHERE student_id = :id";
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(query, _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);
        
        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var row = dataSet.Tables[0].Rows[0];

        var queryResult = new Student(
            (Guid)row["student_id"],
            (string)row["student_name"],
            (Gender)row["gender"],
            (House)row["house"],
            (Pet)row["pet"],
            (Guid)row["room_id"]
        );
        _connection.Close();
        
        return queryResult;
    }

    public void DeleteStudent(Guid id)
    {
        var query = $"DELETE FROM students WHERE student_id = :id";
        _connection.Open();

        var adapter = new NpgsqlDataAdapter(query, _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);
        
        adapter.SelectCommand?.ExecuteNonQuery();
        _connection.Close();
    }
}