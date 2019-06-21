using System;
using System.Collections.Generic;
using System.Text;

namespace InstaPet.DomainModel.ValueObjects
{
    //Value Object
    public struct Specie : IComparable<Specie>
    {
        public string Name { get; set; }

        public Specie(string name)
        {
            Name = name;
        }

        public static implicit operator string(Specie specie)
        {
            return specie.Name;
        }


        public static implicit operator Specie(string specieName)
        {
            return new Specie(specieName);
        }

        public static bool operator ==(Specie specie1, Specie specie2)
        {
            if (specie1.Name == specie2.Name)
                return true;
            return false;
        }

        public static bool operator !=(Specie specie1, Specie specie2)
        {
            if (specie1.Name != specie2.Name)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Specie other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
