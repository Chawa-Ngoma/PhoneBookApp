using Microsoft.AspNetCore.Components;
using PhoneApp.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace PhoneApp.Pages.Contacts
{
    public partial class EditContact
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public NavigationManager NavManager { get; set; } = default!;

        [Parameter]
        public int Id { get; set; }

        private ContactModel? _contacts { get; set; }
        private InternalContactModel _internalContactModel = new();
        private JsonNode _errors { get; set; } = new JsonObject();
        private string _apiErrorMessage = string.Empty;

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

        private async Task SaveContact()
        {
            var response = await HttpClient.PutAsJsonAsync("https://localhost:4000/api/Contact/" + Id, _internalContactModel);

            if (response.IsSuccessStatusCode)
            {
                //redirect to list of Contacts
                NavManager.NavigateTo("/Contacts");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Json Response: \n" + strResponse);

                try
                {
                    var jsonResponse = JsonNode.Parse(strResponse);
                    _errors = jsonResponse?["errors"] ?? new JsonObject();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        private void Cancel()
        {
            //redirect to list of Contacts
            NavManager.NavigateTo("/Contacts");
        }

    }
}
