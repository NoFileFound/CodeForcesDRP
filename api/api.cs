using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CodeForcesDRP.api
{
    public class ApiImplementation
    {
        private static JObject? _info;

        public static void InvokeError(string title, string message)
        {
            ErrorWindow errorWindow = new()
            {
                Title = title,
                Message = message,
            };
            errorWindow.ShowDialog();
        }
        
        public static async Task<JObject> GetProblemsSet()
        {
            if (_info != null) return _info;
            HttpClient httpClient = new();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://codeforces.com/api/problemset.problems");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("received data");
                    _info = JObject.Parse(await response.Content.ReadAsStringAsync());
                    return _info;
                }
                else
                {
                    InvokeError("CodeForces", "Unable to connect");
                }
            }
            catch (Exception ex)
            {
                InvokeError("CodeForces", ex.Message);
            }
            return null;
        }
        
        public Problem GetContestProblemInfo(JObject response, int contestID, char problemLetter)
        {
            JToken contensts = response["result"]["problems"];
            if(contensts != null)
            {
                foreach(var contestInfo in contensts)
                {
                    int _contestId = contestInfo["contestId"].ToObject<int>();
                    string _contestProblemLetter = contestInfo["index"].ToObject<string>();
                    if (_contestId == contestID && _contestProblemLetter[0] == problemLetter)
                    {
                        Problem x = new();
                        x.ContestID = contestID;
                        x.Name = contestInfo["name"].ToObject<string>();
                        x.Type = contestInfo["type"].ToObject<string>().ToLower();
                        if(contestInfo["rating"] != null)
                        {
                            x.Rating = contestInfo["rating"].ToObject<int>();
                        }
                        x.Tags = contestInfo["tags"].ToObject<List<string>>();
                        return x;
                    }
                }
            }
            InvokeError("CodeForces", "Problem not found");
            return default;
        }
    }
}
