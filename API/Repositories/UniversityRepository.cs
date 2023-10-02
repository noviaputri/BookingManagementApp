using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named UniversityRepository that inherits from GeneralRepository<University> and implements the IUniversityRepository interface.
public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public UniversityRepository(BookingManagementDbContext context) : base(context) { }
}
