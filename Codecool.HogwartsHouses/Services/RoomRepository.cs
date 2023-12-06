using System.Data;
using Codecool.HogwartsHouses.Model;
using Npgsql;

namespace Codecool.HogwartsHouses.Services;

public class RoomRepository : IRoomRepository
{
    private readonly NpgsqlConnection _connection;

    public RoomRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<Room> GetAll()
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter("SELECT * FROM rooms", _connection);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var table = dataSet.Tables[0];

        var queryResult = new List<Room>();
        foreach (DataRow row in table.Rows)
        {
            queryResult.Add(new Room(
                (Guid)row["room_id"],
                (string)row["room_name"]
            ));
        }

        _connection.Close();

        return queryResult;
    }

    public Room GetById(Guid id)
    {
        var query = $"SELECT * FROM rooms WHERE room_id = :id";
        _connection.Open();
        
        var adapter = new NpgsqlDataAdapter(query, _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);
        
        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var row = dataSet.Tables[0].Rows[0];

        var queryResult = new Room(
            (Guid)row["room_id"],
            (string)row["room_name"]
        );
        _connection.Close();
        
        return queryResult;
    }

    public Guid Create(Room room)
    {
        var query =
            "INSERT INTO rooms (room_id, room_name) VALUES (:room_id, :room_name) RETURNING room_id";
        _connection.Open();

        var adapter = new NpgsqlDataAdapter(query, _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":room_id", room.Id);
        adapter.SelectCommand?.Parameters.AddWithValue(":room_name", room.Name);

        var lastInsertId = (Guid)adapter.SelectCommand?.ExecuteScalar();
        _connection.Close();

        return lastInsertId;
    }

    public void Delete(Guid id)
    {
        var query = $"DELETE FROM rooms WHERE room_id = :id";
        _connection.Open();

        var adapter = new NpgsqlDataAdapter(query, _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);
        
        adapter.SelectCommand?.ExecuteNonQuery();
        _connection.Close();
    }
}