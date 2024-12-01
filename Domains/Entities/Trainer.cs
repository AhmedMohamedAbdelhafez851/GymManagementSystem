using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Trainer
    {
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Trainer name is required.")]
        [MaxLength(100, ErrorMessage = "Trainer name cannot exceed 100 characters.")]
        public string TrainerName { get; set; } = "";

        [Required(ErrorMessage = "Specialization is required.")]
        [MaxLength(100, ErrorMessage = "Specialization cannot exceed 100 characters.")]
        public string Specialization { get; set; } = "";

        // One-to-many relationship with Class
        public ICollection<Class>? Classes { get; set; } = new HashSet<Class>();

        public string? UserId { get; set; } 
        public ApplicationUser? User { get; set; }
    }
}
