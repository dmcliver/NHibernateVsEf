using System;
using NHibernate.Mapping.Attributes;

namespace NHibernateVsEf.Core.Domain.NHibernate
{
    [Class]
    public class UserTrack
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "guid.comb")]
        public virtual Guid Id { get; set; }

        [ManyToOne(Name = "UserProfile", Column = "UserId", NotNull = true)]
        public virtual UserProfile UserProfile { get; set; }

        [ManyToOne(Name = "Track", Column = "TrackId", NotNull = true)]
        public virtual Track Track { get; set; }
    }
}