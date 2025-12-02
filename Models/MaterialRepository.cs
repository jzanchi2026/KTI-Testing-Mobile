using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp2;
using Newtonsoft.Json.Linq;

namespace KTI_Testing__Mobile_.Models
{
    public static class MaterialRepository
    {
        public static async Task<List<Material>> dingo(string where)
        {
            Uri thisUri = new Uri(App.uri, where);


            var response = await App.myHttpClient.GetAsync(thisUri.ToString());
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.Write(stringContent);
            List<Material> matList = new List<Material>();

            JArray data = JArray.Parse(stringContent);

            Console.Write(stringContent);

            for (int i = 0; i < data.Count; i++)
            {
                if (where == "getMaterials")
                {
                    JObject matObj = (JObject)data[i];
                    Material add = new Material((int)matObj["materialId"], matObj["materialName"].ToString(), (int)matObj["amount"], matObj["currentAmount"].ToString());
                    matList.Add(add);
                }
                else if (where == "getUserMaterials")
                {
                    JObject toolObj = (JObject)data[i];
                    Material ret = await parseMaterial((int)toolObj["toolId"]);
                    matList.Add(ret);
                }
            }
            return matList;
        }
        public static List<Material> _materials;
        public static async Task InitializeMaterialsAsync()
        {
            _materials = await dingo("getMaterials");
        }
        public static List<Material> GetMaterials() => _materials;
        public static async Task<Material> parseMaterial(int id)
        {
            Uri invUri = new Uri(App.uri, "getMaterial?id=" + id);
            var response = await App.myHttpClient.GetAsync(invUri.ToString());
            Console.WriteLine(response);
            var stringContent = await response.Content.ReadAsStringAsync();
            JObject tooldata = JObject.Parse(stringContent);
            Material ret = new Material((int)tooldata["materialId"], tooldata["materialName"].ToString(), (int)tooldata["amount"], tooldata["currentAmount"].ToString());
            return ret;
        }
    }

}
