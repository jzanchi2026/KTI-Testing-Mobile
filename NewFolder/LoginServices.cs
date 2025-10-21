using HtmlAgilityPack;  // For parsing HTML responses
using KTI_Testing__Mobile_.Models; // For accessing the UserInfo model
using MauiApp2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
namespace KTI_Testing__Mobile_.NewFolder
{
    public class LoginServices : ILoginRepos
    {
        // Login method to authenticate the user
        public async Task<UserInfo> Login(string username, string password)
        {
            // Check if the device has internet connectivity
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var userinfo = new UserInfo();

                // Create an HttpClient instance to send the request

                //OLD
                //Uri uri = new Uri("http://178.28.32.1:3000/login");
                //ROMAN'S PC, USE ONLY FOR DEBUGGING
                //Uri uri = new Uri("http://10.3.9.41:3000/login");
                //NEW AND GOOD AND USE THIS ONE
                //Uri uri = new Uri("https://develmets.skiscratcher.com/");
                
                // Prepare form data for the POST request
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("mobile", "antonia"), // Static "mobile" field
                });
                Uri loginUri = new Uri(App.uri, "login");
                // Send the POST request and await the rgesponse
                
                var response = await App.myHttpClient.PostAsync(loginUri.ToString(), formContent);
                var stringContent = await response.Content.ReadAsStringAsync(); // Read response as string
                Console.WriteLine(stringContent); // Log response for debugging


                JObject data = JObject.Parse(stringContent);

                if ((bool?)data["login"] == true)
                {
                    Console.WriteLine("LOGIN TRUE");
                    userinfo.UserId = data["userid"].ToString();
                    userinfo.Email = data["email"].ToString();
                    userinfo.Name = data["username"].ToString();
                    userinfo.Status = int.Parse(data["userType"].ToString());

                    var shell = Shell.Current as AppShell; // get the current shell
                    if (userinfo.Status == 2)
                    {
                        if (shell != null)
                        {
                            // Create your ShellContent
                            var adminPage = new ShellContent
                            {
                                Title = "Admin Page",
                                ContentTemplate = new DataTemplate(typeof(AdminPage)),
                                Route = "AdminPage"
                            };

                            // Add it to the Shell
                            shell.Items.Add(adminPage);

                        }
                    }
                }
                else
                {
                    Console.WriteLine("LOGIN FAIL");

                    userinfo.Error = "Incorrect Username and/or password";
                }
                    return userinfo; // Return the user information or error data
            }
            else
            {
                // No internet connection
                return null;
            }
        }
    }
}
