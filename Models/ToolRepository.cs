using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTI_Testing__Mobile_.Models;
using Newtonsoft.Json.Linq;

namespace MauiApp2.Models
{
    public static class ToolRepository
    {
        private static async Task<List<Tool>> ringo()
        {
            Uri thisUri = new Uri(App.uri, "getTools");
            
            
            var response = await App.myHttpClient.GetAsync(thisUri.ToString());
            var stringContent = await response.Content.ReadAsStringAsync();
            Console.Write(stringContent);
            List<Tool> toolList = new List<Tool>();

            JArray data = JArray.Parse(stringContent);

            Console.Write(stringContent);

            for (int i =0; i < data.Count; i++)
            {
                JObject toolObj = (JObject)data[i];
                toolList.Add(new Tool((int)toolObj["toolTypeId"], toolObj["toolName"].ToString(), "", 1));
            }
            return toolList;
        }
        public static List<Tool> _tools;
        public static async Task InitializeToolsAsync()
        {
            _tools = await ringo();
        }
        public static List<Tool> GetTools() => _tools;
        public static Tool GetToolById(int ToolId)
        {
            return _tools.FirstOrDefault(x => x.Id == ToolId);
        }

        public static List<Tool> SearchTools(string filterText)
        {
            var tools = _tools.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (tools == null || _tools.Count <= 0)
            {
                tools = _tools.Where(x => !string.IsNullOrWhiteSpace(x.Amount.ToString()) && x.Amount.ToString().StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            } else
            {
                return tools;
            }

            return tools;
        }
        public static Tool getSpecificTool(int tofind)
        {
            foreach(Tool i in _tools)
            {
                if (i.Id == tofind)
                {
                    return i;
                }
            }
            return new Tool(-1, "invalid", "invalid", -1);
        }
    }
}
