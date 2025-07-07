using Microsoft.AspNetCore.Components;
using PhoneApp.Models;
using System.Net.Http.Json;

namespace PhoneApp.Pages.Contacts
{
    public partial class Index
    {
        [Inject]
        HttpClient HttpClient { get; set; } = default!;
        private List<ContactModel>? _contacts;
        private List<ContactModel>? _filteredContacts;

        private bool showSearchForm = false;
        private string searchName = string.Empty;
        private string searchPhone = string.Empty;

        private List<ContactModel>? _contactList =>
        (_filteredContacts != null && _filteredContacts.Any()) ? _filteredContacts : _contacts;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _contacts = await HttpClient.GetFromJsonAsync<List<ContactModel>>("https://localhost:4000/api/Contact");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching contacts: {ex.Message}");
            }
        }

        private void ToggleSearchForm()
        {
            showSearchForm = !showSearchForm;
        }

        private void SearchContacts()
        {
            if (_contacts == null) return;

            _filteredContacts = _contacts
                .Where(c =>
                    (string.IsNullOrWhiteSpace(searchName) || c.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(searchPhone) || c.PhoneNumber.Contains(searchPhone))
                ).ToList();
        }

        private void ResetSearch()
        {
            searchName = string.Empty;
            searchPhone = string.Empty;
            _filteredContacts = _contacts;
        }
    }
}
