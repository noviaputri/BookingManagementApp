using API.Models;

namespace API.Contracts;

// Defines interface class for EducationRepository
public interface IEducationRepository
{
    // Declares all method in IEducationRepository interface
    IEnumerable<Education> GetAll();
    Education? GetByGuid(Guid guid);
    Education? Create(Education education);
    bool Update(Education education);
    bool Delete(Education education);
}
