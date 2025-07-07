using Microsoft.AspNetCore.Components;
using PhoneApp.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace PhoneApp.Pages.Contacts
{
    public partial class CreateContact
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;
        [Inject]
        public NavigationManager NavManager { get; set; } = default!;
        private ContactModel? _contacts { get; set; }
        private InternalContactModel _internalContactModel = new();
        private JsonNode _errors { get; set; } = new JsonObject();
        private string _apiErrorMessage = string.Empty;

        private async Task SaveContact()
        {
            _errors = new JsonObject(); 
            _apiErrorMessage = string.Empty; 

            if (_internalContactModel.PhoneNumber.Length != 10 || !_internalContactModel.PhoneNumber.All(char.IsDigit))
            {
                _errors["PhoneNumber"] = new JsonArray("Phone number must be exactly 10 digits and contain only numbers.");
                return;
            }

            var response = await HttpClient.PostAsJsonAsync("https://localhost:4000/api/Contact", _internalContactModel);

            if (response.IsSuccessStatusCode)
            {
                //redirect to list of contacts
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
                    _apiErrorMessage = strResponse;
                }
            }
        }

        private void Cancel()
        {
            //redirect to list of contacts
            NavManager.NavigateTo("/Contacts");
        }
    }
}
