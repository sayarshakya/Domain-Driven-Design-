using static Wpm.Management.Domain.Entities.Pet;

namespace Wpm.Management.Api.Application
{
    public record CreatePetCommand(Guid Id,
                                   string Name,
                                   int Age,
                                   string Color,
                                   SexOfPet SexOfPet,
                                   Guid BreedId); 
   
}
