using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Second name is required.")]
        [MaxLength(50, ErrorMessage = "Second name cannot exceed 50 characters.")]
        public string SecondName { get; set; } = "";
          
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Customer), nameof(ValidateAge))]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; } = "";

        [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "Join date is required.")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        public int MemberShipsPlanId { get; set; }

        public MemberShipsPlan? MemberShipsPlan { get; set; }

        // Custom Validation for Age
        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

            if (age < 18)
            {
                return new ValidationResult("Customer must be at least 18 years old.");
            }

            return ValidationResult.Success;
        }
    }
}
