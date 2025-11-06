using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            
            string email = EmailEntry.Text?.Trim();
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Basic validation
            if (
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill out all fields.", "OK");
                return;
            }
            
            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Invalid email", "OK");
                return;
            }
            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var formContent = new FormUrlEncodedContent(new[]
{
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("mobile", "antonia"), // Static "mobile" field
                });
            Uri signUpUri = new Uri(App.uri, "register");
            var response = await App.myHttpClient.PostAsync(signUpUri.ToString(), formContent);
            var stringContent = await response.Content.ReadAsStringAsync(); // Read response as string
            Console.WriteLine(stringContent); // Log response for debugging

            await DisplayAlert("Success", "Your account creation request has been sent", "OK");
            await Shell.Current.GoToAsync("..");
        }

        private async void OnGoogleSignUpClicked(object sender, EventArgs e)
        {
            // TODO: Integrate Google OAuth or Firebase Auth
            await DisplayAlert("Google Sign-Up", "Google Sign-Up coming soon!", "OK");
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            // Navigate to login page
            await Shell.Current.GoToAsync("..");
        }
        static bool IsValidEmail(string email)
        {
            // Simple email regex pattern
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
