using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.Collections.Immutable;
using Quobject.SocketIoClientDotNet.Client;

namespace BotPrototype
{
    public static class Bot
    {
        private static readonly Dictionary<String, Action<String, ChatMessage>> CommandList = new Dictionary<string, Action<string, ChatMessage>>();

        public static void AddCommand(string command, Action<String, ChatMessage> action) {
            CommandList.Add(command, action);
        }

        public static void Start(string channelId) {
            Socket socket = IO.Socket("wss://chat.peka2.tv/", new IO.Options { Transports = ImmutableList.Create("websocket") });
            socket.On("/chat/message", b =>
            {
                ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(b.ToString());
                var cmd = CommandList.Where(item => msg.Text.StartsWith(item.Key)).First();
                    cmd.Value.Invoke(msg.Text.Substring(cmd.Key.Length), msg);
            });

            socket.On(Socket.EVENT_CONNECT, (a) =>
            {
                Console.WriteLine("Connected.");
                JoinChat(socket, channelId);
            });
        }

        static void JoinChat(Socket socket, string channelId)
        {
            var loginObject = new JObject();
            loginObject.Add("channel", "stream/" + channelId);

            socket.Emit("/chat/join", (b) =>
            {
                ConnectionStatus msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ConnectionStatus>(b.ToString());
                Console.WriteLine($"Joined to channel with status: {msg.Status}");
            }, loginObject);
        }
    }
}
