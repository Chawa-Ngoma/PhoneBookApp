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

        private bool _showSearchForm = false;
        private string _searchName = string.Empty;
        private string _searchPhone = string.Empty;

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
            _showSearchForm = !_showSearchForm;
        }

        private void SearchContacts()
        {
            if (_contacts == null) return;

            _filteredContacts = _contacts
                .Where(c =>
                    (string.IsNullOrWhiteSpace(_searchName) || c.Name.Contains(_searchName, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(_searchPhone) || c.PhoneNumber.Contains(_searchPhone))
                ).ToList();
        }

        private void ResetSearch()
        {
            _searchName = string.Empty;
            _searchPhone = string.Empty;
            _filteredContacts = _contacts;
        }
    }
}
