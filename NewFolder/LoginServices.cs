using HtmlAgilityPack;  // For parsing HTML responses
using KTI_Testing__Mobile_.Models; // For accessing the UserInfo model
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                using var client = new HttpClient();

                //OLD
                //Uri uri = new Uri("http://178.28.32.1:3000/login");
                //ROMAN'S PC, USE ONLY FOR DEBUGGING
                Uri uri = new Uri("http://10.3.9.41:3000/login");
                //NEW AND GOOD AND USE THIS ONE
                //Uri uri = new Uri("http://10.0.2.2:3000/login"); 

                // Prepare form data for the POST request
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("mobile", "antonia"), // Static "mobile" field
                });
                
                // Send the POST request and await the rgesponse
                var myHttpClient = new HttpClient();
                var response = await myHttpClient.PostAsync(uri.ToString(), formContent);
                var stringContent = await response.Content.ReadAsStringAsync(); // Read response as string
                Console.WriteLine(stringContent); // Log response for debugging

                JObject data = JObject.Parse(stringContent);
                if ((bool?)data["login"] == true)
                {
                    Console.WriteLine("LOGIN TRUE");
                    userinfo.UserId = data["userid"].ToString();
                    userinfo.Email = username;
                    userinfo.Name = data["username"].ToString();
                }
                else
                {
                    Console.WriteLine("LOGIN FAIL");

                    userinfo.Error = "Incorrect Username and/or password";
                }
                    /*
                    // Load the response into an HTML document for parsing
                    HtmlDocument htmlSnippet = new HtmlDocument();
                    htmlSnippet.LoadHtml(stringContent);

                    // Extract errors from the response
                    List<string> errors = new List<string>();
                    HtmlNodeCollection nodelist = htmlSnippet.DocumentNode.SelectNodes("//error");
                    if (nodelist != null)
                    {
                        // Add each error found to the list
                        foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//error"))
                        {
                            string att = link.InnerHtml;
                            errors.Add(att.Trim());
                        }
                    }

                    // Extract user information from the response
                    HtmlNode userelement = htmlSnippet.DocumentNode.SelectSingleNode("//userinfo");
                    string userid = "";
                    if (userelement != null)
                    {
                        userid = userelement.InnerHtml; // Extract user information content
                    }

                    Console.WriteLine(errors.ToString()); // Log errors for debugging

                    // Process the extracted data
                    if (errors.Count == 0)
                    {
                        string[] info = userid.Split(','); // Split user data by commas
                        userinfo.UserId = (info[0].Trim()); // Extract user ID
                        userinfo.Name = (info[1].Trim());   // Extract user name
                        userinfo.Email = (info[2].Trim());  // Extract user email
                    }
                    else if (errors[1] == "400") // Check if error code 400 exists (may cause index error)
                    {
                        Console.WriteLine("Errored: " + errors[0]);
                        userinfo.Error = errors[0]; // Assign error message
                    }
                    */
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
