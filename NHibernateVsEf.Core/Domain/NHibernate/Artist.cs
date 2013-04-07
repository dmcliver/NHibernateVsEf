using System;
using NHibernate.Mapping.Attributes;

namespace NHibernateVsEf.Core.Domain.NHibernate
{
    [Class]
    public class Artist
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "guid.comb")]
        public virtual Guid Id { get; set; }

        [Property(Name = "Name", NotNull = true)]
        public virtual string Name { get; set; }
    }
}