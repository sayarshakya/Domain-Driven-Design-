
using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Infrastructure;
using Wpm.Management.Domain;
using Wpm.Management.Domain.Events;

namespace Wpm.Management.Api.Application
{
    public class SetWetCommandHandler : ICommandHandler<SetWeightCommand>
    {
        private readonly ManagementDbContext managementDbContext;
        private readonly IBreedService breedService;

        public SetWetCommandHandler(ManagementDbContext managementDbContext,
                                    IBreedService breedService)
        {
            this.managementDbContext = managementDbContext;
            this.breedService = breedService;

            DomainEvents.PetWeightUpdated.Subscribe((domainEvent) => 
            {

            });
        }

        public async Task Handle(SetWeightCommand command)
        {
            var pet = await managementDbContext.Pets.FindAsync(command.Id);
            pet.SetWeight(command.Weight, breedService);
            await managementDbContext.SaveChangesAsync();
        }
    }
}
