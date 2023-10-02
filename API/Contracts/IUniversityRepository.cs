using API.Models;

namespace API.Contracts;

// Defines interface class for UniversityRepository
public interface IUniversityRepository
{
    // Declares all method in IUniversityRepository interface
    IEnumerable<University> GetAll();
    University? GetByGuid(Guid guid);
    University? Create(University university);
    bool Update(University university);
    bool Delete(University university);
}
