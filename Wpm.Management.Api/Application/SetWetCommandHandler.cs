
using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Infrastructure;
using Wpm.Management.Domain;

namespace Wpm.Management.Api.Application
{
    public class SetWetCommandHandler(ManagementDbContext managementDbContext,
                                      IBreedService breedService) : ICommandHandler<SetWeightCommand>
    {
        public async Task Handle(SetWeightCommand command)
        {
            var pet = await managementDbContext.Pets.FindAsync(command.Id);
            pet.SetWeight(command.Weight, breedService);
            await managementDbContext.SaveChangesAsync();
        }
    }
}
