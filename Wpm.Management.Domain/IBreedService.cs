using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain
{
    public interface IBreedService
    {
        Breed? GetBreed(Guid id);
    }

    public class FakeBreedService : IBreedService
    {
        public readonly List<Breed> breeds =
            [
                new Breed(Guid.NewGuid(), "Labrador Retriever", new WeightRange(10, 20), new WeightRange(55, 80)),
                new Breed(Guid.NewGuid(), "German Shepherd", new WeightRange(28, 48), new WeightRange(50, 90)),
            ];

        public Breed? GetBreed(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Breed is not Valid");
            }
            var result = breeds.Find(b => b.Id == id);
            return result ?? throw new ArgumentException("Breed was not found");
        }
    }

    public interface IManagementRepository
    {
        Pet? GetById(Guid id);
        IEnumerable<Pet> GetAll();
        void Insert(Pet pet);
        void Update(Pet pet);
        void Delete(Pet pet);
    }
}
