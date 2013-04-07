using System;
using NHibernate.Mapping.Attributes;

namespace NHibernateVsEf.Core.Domain.NHibernate
{
    [Class]
    public class Track
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "guid.comb")]
        public virtual Guid Id { get; set; }

        [Property(Name = "Title", NotNull = true)]
        public virtual string Title { get; set; }

        [ManyToOne(Name = "Artist", Column = "ArtistId", NotNull = true, ClassType = typeof(Artist))]
        public virtual Artist Artist { get; set; }
    }
}