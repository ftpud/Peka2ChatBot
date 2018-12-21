using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPrototype
{
    // Объекты в которые десереализуются полученные сообщения из чата.
    public class From
    {
        public int id { get; set; }
        public string name { get; set; }
        public int color { get; set; }
    }

    public class Store
    {
        public List<object> bonuses { get; set; }
        public List<object> subscriptions { get; set; }
        public int icon { get; set; }
    }

    public class ChatMessage
    {
        public int id { get; set; }
        public string channel { get; set; }
        public From from { get; set; }
        public object to { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public int time { get; set; }
        public Store store { get; set; }
        public int parentId { get; set; }
        public bool anonymous { get; set; }
    }

    public class ConnectionStatus
    {
        public string status { get; set; }
    }
}
