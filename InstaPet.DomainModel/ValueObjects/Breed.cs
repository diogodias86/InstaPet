using System;
using System.Collections.Generic;
using System.Text;

namespace InstaPet.DomainModel.ValueObjects
{
    public struct Breed
    {
        public Specie Specie { get; set; }
        public string Name { get; set; }

        public Breed(Specie specie, string name)
        {
            Specie = specie;
            Name = name;
        }

        public Breed(string specie, string name)
        {
            Specie = new Specie(specie);
            Name = name;
        }

        public static Breed Parse(string breedStr)
        {
            var splittedValue = breedStr.Split('|');
            Specie specie = splittedValue[0];
            
            return new Breed(specie, splittedValue[1]);
        }

        public override int GetHashCode()
        {
            var hashCode = 1281875670;
            hashCode = hashCode * -1521134295 + EqualityComparer<Specie>.Default.GetHashCode(Specie);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Breed breed &&
                   EqualityComparer<Specie>.Default.Equals(Specie, breed.Specie) &&
                   Name == breed.Name;
        }

        public override string ToString()
        {
            return $"{Specie.Name}|{Name}";
        }
    }
}
