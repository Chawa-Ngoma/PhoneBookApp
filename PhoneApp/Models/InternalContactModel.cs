using System.ComponentModel.DataAnnotations;

namespace PhoneApp.Models
{
    /// <summary>
    /// Internal class to save data
    /// </summary>
    public sealed class InternalContactModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required")]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The phone is required"), Phone]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;
    }
}
