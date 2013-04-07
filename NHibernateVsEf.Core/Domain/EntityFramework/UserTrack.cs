using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHibernateVsEf.Core.Domain.EntityFramework
{
    [Table("UserTrack")]
    public class UserTrackEf
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid TrackId { get; set; }

        [ForeignKey("UserId")]
        public UserProfileEf User { get; set; }
        
        [ForeignKey("TrackId")]
        public TrackEf Track { get; set; }
    }
}