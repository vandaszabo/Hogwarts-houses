namespace Codecool.HogwartsHouses.Model;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; } //Max 3 students, same gender, same house

    public Room(Guid id, string name)
    {
        Id = id;
        Name = name;
        Students = new List<Student>();
    }

    public bool AddStudent(Student student)
    {
        if (Students.Count == 0 || Students.Count < 3 && Students.All(s=>s.HogwartsHouse == student.HogwartsHouse && s.Gender == student.Gender))
        {
            Students.Add(student);
            return true;
        }
        return false;
    }

    public void RemoveStudent(Student student)
    {
        if(Students.Contains(student))
        {
            Students.Remove(student);
        }

        throw new Exception($"Student: {student.Name} is not in this room");
    }
}