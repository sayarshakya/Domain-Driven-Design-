using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain.Entities
{
    public class Breed : Entity
    {
        public string Name { get; init; }
        public WeightRange MaleIdealWeight { get; init; }
        public WeightRange FemaleIdealWeight { get; init; }

        public Breed(Guid id,
            string name,
            WeightRange maleIdealWeight,
            WeightRange femaleIdealWeight) 
        {
            Id = id;
            Name = name;
            MaleIdealWeight = maleIdealWeight;
            FemaleIdealWeight = femaleIdealWeight;
        }
    }

    
}
