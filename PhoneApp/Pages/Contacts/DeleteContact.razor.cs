using Microsoft.AspNetCore.Components;
using PhoneApp.Models;
using System.Net.Http.Json;

namespace PhoneApp.Pages.Contacts
{
    public partial class DeleteContact
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public NavigationManager NavManager { get; set; } = default!;

        [Parameter]
        public int Id { get; set; }

        private ContactModel? _contacts { get; set; }
        private InternalContactModel _internalContactModel = new();

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                _contacts = await HttpClient.GetFromJsonAsync<ContactModel>("https://localhost:4000/api/Contact/" + Id);
                _internalContactModel.Id = Id;
                _internalContactModel.Name = _contacts?.Name ?? string.Empty;
                _internalContactModel.PhoneNumber = _contacts?.PhoneNumber ?? string.Empty;
                _internalContactModel.EmailAddress = _contacts?.EmailAddress ?? string.Empty;

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching contacts: {ex.Message}");
            }
        }
        private async Task Delete()
        {
            var response = await HttpClient.DeleteAsync("https://localhost:4000/api/Contact/" + Id);

            //redirect to list of contact
            NavManager.NavigateTo("/Contacts");
        }

        private void Cancel()
        {
            //redirect to list of contact
            NavManager.NavigateTo("/Contacts");
        }
    }
}
