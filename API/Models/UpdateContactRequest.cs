namespace API.Models
{
    /// <summary>
    /// The contact update request 
    /// </summary>
    public class UpdateContactRequest : CreateContactRequest
    {
        public int Id { get; set; }
    }
}
