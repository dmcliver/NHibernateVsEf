using NHibernate.Mapping.Attributes;

namespace NHibernateVsEf.Core.Domain.NHibernate
{
    [Class]
    public class UserProfile
    {
        [Id(Name = "Id")]
        public virtual string Id { get; set; }

        [Property(Name = "Gender")]
        public virtual Gender? Gender { get; set; }

        [Property(Name = "Age")]
        public virtual int Age { get; set; }

        [Property(Name = "Country")]
        public virtual string Country { get; set; }
    }
}