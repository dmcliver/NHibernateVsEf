using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NHibernateVsEf.Core.Domain.EntityFramework
{
    [Table("Track")]
    public class TrackEf
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public Guid ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public ArtistEf ArtistEf { get; set; }
    }
}