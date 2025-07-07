namespace PhoneApp.Models
{
    /// <summary>
    /// The model used for reading contact information
    /// </summary>
    public sealed class ContactModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? EmailAddress { get; set; } 
        public DateTime CreatedDate { get; set; }
    }

}
