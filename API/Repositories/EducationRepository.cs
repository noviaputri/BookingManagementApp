using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named EducationRepository that inherits from GeneralRepository<Education> and implements the IEducationRepository interface.
public class EducationRepository : GeneralRepository<Education>, IEducationRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public EducationRepository(BookingManagementDbContext context) : base(context) { }
}
