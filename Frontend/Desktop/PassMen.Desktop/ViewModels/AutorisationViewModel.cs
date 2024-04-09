using PassMen.Desktop.Views;

using ModelClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;



namespace PassMen.Desktop.ViewModels
{
    public class AutorisationViewModel : ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        private string _user;

        public void GetUserCommand()
        {
            GetUser();
        }
        private async void GetUser()
        {
            //if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            //{
            //    Message = "Заполните все поля";
            //    return;
            //}
            //else
            //{
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7205");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/UserPass");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserPassDto>>();
                    if(result != null)
                    {
                        foreach (var item in result)
                        {

                        }
                    }
                }
           // }
        }
    }
}
