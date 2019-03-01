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
        private static string LoginToken = "";
        private static string CurrentChannel = "";
        private static bool isLoggedIn = false;
        private static Socket currentSocket;

        private static JObject FromObject = new JObject();

        public static void SetPublishDetails(string login, int id, string token) {            
            FromObject.Add("id", id);
            FromObject.Add("name", login);
            LoginToken = token;
        }

        private static readonly Dictionary<String, Action<String, ChatMessage>> CommandList = new Dictionary<string, Action<string, ChatMessage>>();

        public static void AddCommand(string command, Action<String, ChatMessage> action) {
            CommandList.Add(command, action);
        }

        public static void PublishMessage(string message) {
            if (CurrentChannel != "" && isLoggedIn) {
                var MessageObject = new JObject();
                MessageObject.Add("channel", "stream/" + CurrentChannel);
                MessageObject.Add("from", FromObject);
                MessageObject.Add("to", null);
                MessageObject.Add("text", message);

                Console.WriteLine($"Sending: {MessageObject.ToString()}");

                currentSocket.Emit("/chat/publish", (b) =>
                {
                    Console.WriteLine($"Message Sent {b}");
                }, MessageObject);
            }
        }

        public static void Start(string channelId) {
            Socket socket = IO.Socket("wss://chat.peka2.tv/", new IO.Options { Transports = ImmutableList.Create("websocket") });
            socket.On("/chat/message", b =>
            {
                ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(b.ToString());
                var cmd = CommandList.Where(item => msg.text.StartsWith(item.Key)).First();
                    cmd.Value.Invoke(msg.text.Substring(cmd.Key.Length), msg);
            });

            socket.On(Socket.EVENT_CONNECT, (a) =>
            {
                Console.WriteLine("Connected.");
                JoinChat(socket, channelId);
                if (LoginToken != "")
                {
                    var loginObject = new JObject();
                    loginObject.Add("token", LoginToken);
                    socket.Emit("/chat/login", (b) =>
                    {
                        ConnectionStatus msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ConnectionStatus>(b.ToString());
                        Console.WriteLine($"Login status: {msg.status}");
                        isLoggedIn = true;
                        currentSocket = socket;
                    }, loginObject);
                }
            });
        }

        static void JoinChat(Socket socket, string channelId)
        {
            var loginObject = new JObject();
            loginObject.Add("channel", "stream/" + channelId);

            socket.Emit("/chat/join", (b) =>
            {
                CurrentChannel = channelId;
                ConnectionStatus msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ConnectionStatus>(b.ToString());
                Console.WriteLine($"Joined to channel with status: {msg.status}");
            }, loginObject);
        }
    }
}
