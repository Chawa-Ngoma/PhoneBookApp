using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Model used for DB
    /// </summary>
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public sealed class ContactModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? EmailAddress { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}
