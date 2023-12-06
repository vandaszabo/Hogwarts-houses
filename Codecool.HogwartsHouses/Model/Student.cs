namespace Codecool.HogwartsHouses.Model;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public House HogwartsHouse { get; set; }
    public Pet OwnedPet { get; set; }
    public Guid? RoomId { get; set; }


    public Student(Guid id, string name, Gender gender, House house, Pet pet, Guid? roomId)
    {
        Id = id;
        Name = name;
        Gender = gender;
        HogwartsHouse = house;
        OwnedPet = pet;
        RoomId = roomId;
    }

    // public void AddRoom(Guid roomId)
    // {
    //     RoomId = roomId;
    // }
}