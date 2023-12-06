using Codecool.HogwartsHouses.Model;

namespace Codecool.HogwartsHouses.Services;

public interface IRoomRepository
{
    IEnumerable<Room> GetAll();

    Guid Create(Room room);
    //...
}