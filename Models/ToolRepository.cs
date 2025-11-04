using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTI_Testing__Mobile_.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MauiApp2.Models
{
    public static class ToolRepository
    {
        public static async Task<List<Tool>> ringo(string where)
        {
            Uri thisUri = new Uri(App.uri, where);


            var response = await App.myHttpClient.GetAsync(thisUri.ToString());
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.Write(stringContent);
            List<Tool> toolList = new List<Tool>();

            JArray data = JArray.Parse(stringContent);

            Console.Write(stringContent);

            for (int i = 0; i < data.Count; i++)
            {
                if (where == "getTools")
                {
                    JObject toolObj = (JObject)data[i];
                    Tool add = new Tool((int)toolObj["toolId"], toolObj["toolName"].ToString(), toolObj["takenBy"].ToString());
                    toolList.Add(add);
                }
                else if(where == "getUserTools")
                {
                    JObject toolObj = (JObject)data[i];
                    Tool ret = await parseTool((int)toolObj["toolId"]);
                    toolList.Add(ret);
                }
            }
            return toolList;
        }
        public static List<Tool> _tools;
        public static async Task InitializeToolsAsync()
        {
            _tools = await ringo("getTools");
        }
        public static List<Tool> GetTools() => _tools;
        public static Tool GetToolById(int ToolId)
        {
            return _tools.FirstOrDefault(x => x.Id == ToolId);
        }
        
        public static List<Tool> SearchTools(string filterText)
        {
            var tools = _tools.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();


            return tools;
        }
        
        public static Tool getSpecificTool(int tofind)
        {
            foreach (Tool i in _tools)
            {
                if (i.Id == tofind)
                {
                    return i;
                }
            }
            return new Tool(-1, "invalid", "DNE");
        }
        public static async Task<Tool> parseTool(int id)
        {
            Uri invUri = new Uri(App.uri, "getTool?id=" + id);
            var response = await App.myHttpClient.GetAsync(invUri.ToString());
            Console.WriteLine(response);
            var stringContent = await response.Content.ReadAsStringAsync();
            JObject tooldata = JObject.Parse(stringContent);
            Tool ret = new Tool((int)tooldata["toolId"], tooldata["toolName"].ToString(), tooldata["takenBy"].ToString());
            return ret;
        }
        public static async Task<bool> checkoutTool(Tool tool)
        {
            // Append the ID to the URL as a query parameter
            var stringContent = "";
            if (tool.Status == true)
            {
                Uri checkUri = new Uri($"{App.uri}checkoutTool?id={tool.Id}");
                // Treat like a GET although it is a POST
                var response = await App.myHttpClient.PostAsync(checkUri, null);
                stringContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(stringContent);
                return true;
            }
            return false;

        }
        public static async void returnTool(Tool tool)
        {
            Uri checkUri = new Uri($"{App.uri}returnTool?id={tool.Id}");
            // Treat like a GET although it is a POST
            var response = await App.myHttpClient.PostAsync(checkUri, null);
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringContent);
        }
        public static async Task<List<HistoryObject>> userToolHistory()
        {
            Uri historyUri = new Uri($"{App.uri}getMyHistory");
            var response = await App.myHttpClient.GetAsync(historyUri.ToString());
            var stringContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringContent);
            JArray tooldata = JArray.Parse(stringContent);
            List<HistoryObject> ret = new List<HistoryObject>();
            for (int i = 0; i < tooldata.Count; i++)
            {
                JObject toolObj = (JObject)tooldata[i];
                int recordId = (int)toolObj["recordId"];
                int toolId = (int)toolObj["toolId"];
                string userId = toolObj["userId"].ToString();
                DateTime checkoutTime = (DateTime)toolObj["timeTaken"];
                DateTime returnTime = new DateTime();
                if (toolObj["timeReturned"] == null || toolObj["timeReturned"].Type == JTokenType.Null)
                {
                    returnTime =  new DateTime(0001, 1, 1);
                }
                else
                {
                    returnTime = (DateTime)toolObj["timeReturned"];
                }
                HistoryObject h = new HistoryObject(recordId, toolId, userId, checkoutTime, returnTime);
                ret.Add(h);
            }
            return ret;
        }
    }
}
