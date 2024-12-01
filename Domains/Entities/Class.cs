using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Class
    {
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [MaxLength(100, ErrorMessage = "Class name cannot exceed 100 characters.")]
        public string ClassName { get; set; } = "";

        [Required(ErrorMessage = "Schedule is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date and time format.")]
        public DateTime Schdule { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Trainer ID is required.")]
        public int TrainerId { get; set; }

        public Trainer? Trainer { get; set; }

        //[Required(ErrorMessage = "At least one user is required.")]
        //public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }

}
