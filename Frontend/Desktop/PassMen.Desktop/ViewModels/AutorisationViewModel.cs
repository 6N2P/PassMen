using PassMen.Desktop.Views;

using ModelClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ReactiveUI;
using System.Linq;



namespace PassMen.Desktop.ViewModels
{
    public class AutorisationViewModel : ViewModelBase
    {
       
        public event EventHandler<DataEventArgs> UserReceived;

        private string _login;
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        private string _user;

        public void GetUserCommand()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {                
                Message = "Заполните все поля";
            }
             else  GetUser();
        }
        public void CreateUserCommand()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                Message = "Заполните все поля";
            }
            else CreateUser();
        }
        private async void GetUser()
        {

            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7205");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/UserPass/username/{Login}");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UserPassDto>();
                    // var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserPassDto>>();
                    if (result != null)
                    {
                        UserPassDto user = result;
                        if (user.Username == Login && user.Password == Password)
                        {
                            Message = "Вход выполнен";
                            OnUserReceived(new DataEventArgs(user));
                        }
                        else if (user.Username == Login || user.Password != Password)
                        {
                            Message = "Неверный пароль";
                        }

                    }
                    else Message = "Пользователь не найден";
                }
            }
            catch (Exception ex)
            {
                Message = "Пользователь не найден";
            }
        }

        private async void CreateUser()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7205");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/UserPass");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                   // var result = await response.Content.ReadFromJsonAsync<UserPassDto>();
                     var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserPassDto>>();
                    if (result != null)
                    {
                        UserPassDto user = result.FirstOrDefault(u => u.Username == Login);
                        if (user != null)
                        {
                            Message = "Такой пользователь уже существует";
                        }
                        else
                        {
                            UserPassDto userPass = new UserPassDto()
                            {
                                Username = Login, 
                                Password = Password 
                            };
                            JsonContent content = JsonContent.Create(userPass);
                            HttpResponseMessage postResponse = await client.PostAsync("api/UserPass", content);
                            postResponse.EnsureSuccessStatusCode();
                            if (postResponse.IsSuccessStatusCode)
                            {
                                Message = "Пользователь создан";
                                OnUserReceived(new DataEventArgs(userPass));
                            }
                        }            
                    }
                    
                }
            }
            catch (Exception ex) { }
        }

        protected virtual void OnUserReceived(DataEventArgs e)
        {
            UserReceived?.Invoke(this, e);
        }
    }
}
