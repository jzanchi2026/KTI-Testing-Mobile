using HtmlAgilityPack;
using KTI_Testing__Mobile_.Models;

namespace KTI_Testing__Mobile_.NewFolder
{
    public class LoginServices : ILoginRepos
    {
        public async Task<UserInfo> Login(string username, string password)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var userinfo = new UserInfo();

                using var client = new HttpClient();
                Uri uri = new Uri("http://10.0.2.2:3000/login");
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("mobile", "antonia"),
                });

                var myHttpClient = new HttpClient();
                var response = await myHttpClient.PostAsync(uri.ToString(), formContent);
                var stringContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(stringContent);

                HtmlDocument htmlSnippet = new HtmlDocument();
                htmlSnippet.LoadHtml(stringContent);

                List<string> errors = new List<string>();
                HtmlNodeCollection nodelist = htmlSnippet.DocumentNode.SelectNodes("//error");
                if(nodelist != null)
                {
                    foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//error"))
                    {
                        string att = link.InnerHtml;
                        errors.Add(att.Trim());
                    }
                }

                HtmlNode userelement = htmlSnippet.DocumentNode.SelectSingleNode("//userinfo");
                string userid = "";
                if(userelement != null)
                {
                    userid = userelement.InnerHtml;
                }
                
                Console.WriteLine(errors.ToString());
                if (errors.Count == 0)
                {
                    string[] info = userid.Split(',');
                    userinfo.UserId = (info[0].Trim()); // id
                    userinfo.Name = (info[1].Trim()); // name
                    userinfo.Email = (info[2].Trim()); // email
                }
                else if (errors[1] == "400")
                {
                    Console.WriteLine("Errored: " + errors[0]);
                    userinfo.Error = errors[0];
                }
                
                return userinfo;
            }   
            else 
            {
                return null;
            }
        }
    }
}
