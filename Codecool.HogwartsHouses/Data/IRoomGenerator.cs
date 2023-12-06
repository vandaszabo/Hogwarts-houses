using Codecool.HogwartsHouses.Model;

namespace Codecool.HogwartsHouses.Data;

/// <summary>
/// Sample data generator
/// </summary>
public interface IRoomGenerator
{
    IEnumerable<Room> Generate(int count);
}