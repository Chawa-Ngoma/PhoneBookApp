using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// The contact request 
    /// </summary>
    public class CreateContactRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }
    }
}
