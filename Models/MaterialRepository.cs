using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                    Material add = new Material((int)matObj["materialId"], matObj["materialName"].ToString(), float.Parse(matObj["amount"].ToString()), float.Parse(matObj["currentAmount"].ToString()));
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
            Material ret = new Material((int)tooldata["materialId"], tooldata["materialName"].ToString(), float.Parse(tooldata["amount"].ToString().ToString()), float.Parse(tooldata["currentAmount"].ToString()));
            return ret;
        }
        public static async Task<bool> checkoutMaterial(Material mat, float q)
        {
            // Append the ID to the URL as a query parameter
            var stringContent = "";/*
            var formContent = new FormUrlEncodedContent(new[]
{
                    new KeyValuePair<string, string>("id", mat.Id.ToString()),
                    new KeyValuePair<string, string>("quantity", q.ToString()),
                });
            Uri checkUri = new Uri(App.uri, "checkoutMaterial");*/
            Uri checkUri = new Uri($"{App.uri}checkoutMaterial?id={mat.Id}&quantity={q.ToString()}");
            // Treat like a GET although it is a POST
            var response = await App.myHttpClient.PostAsync(checkUri, null);
            stringContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringContent);
            JObject tooldata = JObject.Parse(stringContent);
            bool ret = false;
            if((bool)tooldata["success"] == true)
            {
                ret = true;
            }
            return ret;
            

        }
        public static Material getSpecificMaterial(int tofind)
        {
            foreach (Material i in _materials)
            {
                if (i.Id == tofind)
                {
                    return i;
                }
            }
            return new Material(-1, "invalid", 0, 0);
        }
    }

}
