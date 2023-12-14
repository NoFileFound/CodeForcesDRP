using DiscordRPC;
using CodeForcesDRP.api;
using CodeForcesDRP.parser;

namespace CodeForcesDRP.discord
{
    public class Drp
    {
        private static DiscordRpcClient? client = null;

        public static void InitializationRPC(ConfigInfo config, Problem _problename, char _problemletter, bool _isPrivate)
        {
            if(client != null)
            {
                client.Dispose();
            }
            client = new DiscordRpcClient(config.ApplicationID);
            client.Initialize();

            if (!_isPrivate)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = _problename.Name + " (" + _problename.Type + ")",
                    State = "Rating: " + _problename.Rating.ToString() + " | Tags: " + string.Join(", ", _problename.Tags),
                    Timestamps = Timestamps.Now,
                    Buttons = new Button[]
                    {
                        new() { Label = "View problem", Url = "https://codeforces.com/problemset/problem/"+_problename.ContestID.ToString() + "/" + _problemletter}
                    }
                });
            }
            else
            {
                client.SetPresence(new RichPresence()
                {
                    Details = "Problem is unavailable.",
                    Timestamps = Timestamps.Now
                });
            }

            client.Invoke();
        }
    }
}