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
                    Tool add = new Tool((int)toolObj["toolID"], toolObj["toolName"].ToString(), toolObj["takenBy"].ToString());
                    toolList.Add(add);
                }
                else if(where == "getUserTools")
                {
                    JObject toolObj = (JObject)data[i];
                    Tool ret = await parseTool((int)toolObj["toolID"]);
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
        //Temporary disable to the search
        
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
            Tool ret = new Tool((int)tooldata["toolID"], tooldata["toolName"].ToString(), tooldata["takenBy"].ToString());
            return ret;
        }
        public static async void checkoutTool(Tool tool)
        {
            // Append the ID to the URL as a query parameter
            Uri checkUri = new Uri($"{App.uri}checkoutTool?id={tool.Id}");
            // Treat like a GET although it is a POST
            var response = await App.myHttpClient.PostAsync(checkUri, null);
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringContent);

        }
        public static async void returnTool(Tool tool)
        {
            Uri checkUri = new Uri($"{App.uri}returnTool?id={tool.Id}");
            // Treat like a GET although it is a POST
            var response = await App.myHttpClient.PostAsync(checkUri, null);
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringContent);
        }
    }
}
