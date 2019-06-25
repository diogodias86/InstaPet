using InstaPet.DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaPet.Infrastructure.DataAccess.Contexts.Models
{
    public class DbSpecie
    {
        public Guid Id { get; set; }
        public Specie Specie { get; set; }
    }
}
