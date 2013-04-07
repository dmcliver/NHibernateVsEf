using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHibernateVsEf.Core.Domain.EntityFramework
{
    [Table("Artist")]
    public class ArtistEf
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}