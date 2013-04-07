using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NHibernateVsEf.Core.Domain.NHibernate;

namespace NHibernateVsEf.Core.Domain.EntityFramework
{
    [Table("UserProfile")]
    public class UserProfileEf
    {
        [Key]
        public string Id { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }
}