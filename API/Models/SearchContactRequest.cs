namespace API.Models
{
    /// <summary>
    /// The contact search request 
    /// </summary>
    public sealed class SearchContactRequest
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
