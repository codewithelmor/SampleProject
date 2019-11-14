using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.DomainObject
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTimeOffset.Now;
            IsActive = true;
            IsDeleted = false;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }

        public string CreatedBy { get; set; }
        
        public DateTimeOffset? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
        
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}