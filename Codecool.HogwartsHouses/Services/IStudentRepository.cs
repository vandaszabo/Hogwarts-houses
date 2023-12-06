using Codecool.HogwartsHouses.Model;

namespace Codecool.HogwartsHouses.Services;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll();

    Guid Create(Student student);
    //...
}