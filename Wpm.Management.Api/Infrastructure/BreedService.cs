using Wpm.Management.Domain;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Infrastructure
{
    public class BreedService : IBreedService
    {
        public readonly List<Breed> breeds =
        [
            new Breed(Guid.Parse("1c10f44e-83b1-4094-b6b1-4298991d9d71"), "Labrador Retriever", new WeightRange(10, 20), new WeightRange(55, 80)),
            new Breed(Guid.Parse("63386cae-79c2-4180-8630-60c6cdf2f5f1"), "German Shepherd", new WeightRange(28, 48), new WeightRange(50, 90)),
        ];

        public Breed? GetBreed(Guid id)
        {
            if(id == Guid.Empty )
            {
                throw new ArgumentException("Breed is not valid");
            }

            var result = breeds.Find(breeds => breeds.Id == id);
            return result ?? throw new ArgumentException("Breed not found");
        }
    }
}
