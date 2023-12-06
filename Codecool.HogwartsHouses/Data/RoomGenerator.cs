using Codecool.HogwartsHouses.Model;

namespace Codecool.HogwartsHouses.Data
{
    public class RoomGenerator : IRoomGenerator
    {
        private static int roomCount = 1;

        public IEnumerable<Room> Generate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Room(Guid.NewGuid(), $"Room-{roomCount++}");
            }
        }
    }
}
