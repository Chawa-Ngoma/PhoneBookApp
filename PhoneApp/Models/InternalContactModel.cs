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
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "The phone is required"), Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
