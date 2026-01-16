using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MauiApp2;
using Microsoft.Maui;
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
                    Material ret = await parseMaterial((int)toolObj["materialId"]);
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
        public static async Task<List<HistoryObject>> specificMaterialHistory(int id)
        {
            Uri historyUri = new Uri($"{App.uri}getMaterialHistory?id={id}");
            var response = await App.myHttpClient.GetAsync(historyUri.ToString());
            var stringContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringContent);
            JArray tooldata = JArray.Parse(stringContent);
            List<HistoryObject> ret = new List<HistoryObject>();
            for (int i = 0; i < tooldata.Count; i++)
            {
                JObject matObj = (JObject)tooldata[i];
                int recordId = (int)matObj["recordId"];
                int matId = (int)matObj["materialId"];
                string userId = matObj["userId"].ToString();
                DateTime checkoutTime = (DateTime)matObj["timeTaken"];
                DateTime returnTime = new DateTime();
                float takenQ = float.Parse(matObj["takenQuantity"].ToString());
                float returnedQ = float.Parse(matObj["returnedQuantity"].ToString());
                if (matObj["timeReturned"] == null || matObj["timeReturned"].Type == JTokenType.Null)
                {
                    returnTime = new DateTime(0001, 1, 1);
                }
                else
                {
                    returnTime = (DateTime)matObj["timeReturned"];
                }
                HistoryObject h = new HistoryObject(recordId, matId, userId, checkoutTime, returnTime, takenQ, returnedQ);
                ret.Add(h);
            }
            return ret;
        }
        public static async Task<List<HistoryObject>> userMaterialHistory()
        {
            Uri historyUri = new Uri(App.uri, "getUserMaterials");
            var response = await App.myHttpClient.GetAsync(historyUri);
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringContent);
            JArray tooldata = JArray.Parse(stringContent);
            List<HistoryObject> ret = new List<HistoryObject>();
            for (int i = 0; i < tooldata.Count; i++)
            {
                JObject toolObj = (JObject)tooldata[i];
                int recordId = (int)toolObj["recordId"];
                int toolId = (int)toolObj["materialId"];
                string userId = toolObj["userId"].ToString();
                DateTime checkoutTime = (DateTime)toolObj["timeTaken"];
                DateTime returnTime = new DateTime();
                if (toolObj["timeReturned"] == null || toolObj["timeReturned"].Type == JTokenType.Null)
                {
                    returnTime = new DateTime(0001, 1, 1);
                }
                else
                {
                    returnTime = (DateTime)toolObj["timeReturned"];
                }
                float takenQ = float.Parse(toolObj["takenQuantity"].ToString());
                float returnedQ = float.Parse(toolObj["returnedQuantity"].ToString());
                HistoryObject h = new HistoryObject(recordId, toolId, userId, checkoutTime, returnTime, takenQ, returnedQ);
                ret.Add(h);
            }
            return ret;
        }
        public static async Task<bool> returnMaterial(Material m, float q)
        {
            List<HistoryObject> his = await userMaterialHistory();
            // compare if return quantity is greater than or less than amount taken
            float compareQ = 0;
            foreach(HistoryObject h in his)
            {
                if(h.Id == m.Id)
                {
                    compareQ += h.TakenQ;
                    break;
                }
            }
            if (q <= compareQ)
            {
                //Initiate return
                Uri returnUri = new Uri($"{App.uri}returnMaterial?id={m.Id}&quantity={q}");
                var response = await App.myHttpClient.PostAsync(returnUri, null);
                var stringContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(stringContent);
                if (stringContent.Contains("Cannot POST"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                //Stinky
                return false;
            }

        }
    }

}
