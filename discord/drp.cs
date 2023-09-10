using System;
using System.Threading.Tasks;
using NetDiscordRpc;
using NetDiscordRpc.RPC;
using CodeForcesDRP.api;

namespace CodeForcesDRP.discord
{
    public class Drp
    {
        private static DiscordRPC? client;

        public static Task SetStatus(Problem _problename, char _problemletter)
        {
            client = new DiscordRPC(""); // your application id.
            client.Initialize();

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
            client.Invoke();
            return Task.CompletedTask;
        }
    }
}
