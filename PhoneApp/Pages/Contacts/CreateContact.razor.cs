using Microsoft.AspNetCore.Components;
using PhoneApp.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace PhoneApp.Pages.Contacts
{
    public partial class CreateContact
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;
        [Inject]
        public NavigationManager NavManager { get; set; } = default!;
        private InternalContactModel _internalContactModel = new();
        private string _errorMessage = string.Empty;

        private async Task HandleValidSubmit()
        {
            _errorMessage = string.Empty;

            if (!string.IsNullOrWhiteSpace(_internalContactModel.EmailAddress) &&
                !IsValidEmail(_internalContactModel.EmailAddress))
            {
                _errorMessage = "Invalid email format.";
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
                    _errorMessage = jsonResponse?["message"]?.ToString() ?? "Failed to create contact.";
                }
                catch
                {
                    _errorMessage = strResponse;
                }
            }
        }


        private void Cancel()
        {
            //redirect to list of contacts
            NavManager.NavigateTo("/Contacts");
        }

        public bool IsValidEmail(string emailaddress)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$"); 
            return validateEmailRegex.IsMatch(emailaddress);
        }
    }
}
