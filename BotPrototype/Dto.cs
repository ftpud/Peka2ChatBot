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
        public int Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
    }

    public class Store
    {
        public List<object> Bonuses { get; set; }
        public List<object> Subscriptions { get; set; }
        public int Icon { get; set; }
    }

    public class ChatMessage
    {
        public int Id { get; set; }
        public string Channel { get; set; }
        public From From { get; set; }
        public object To { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public int Time { get; set; }
        public Store Store { get; set; }
        public int ParentId { get; set; }
        public bool Anonymous { get; set; }
    }

    public class ConnectionStatus
    {
        public string Status { get; set; }
    }
}
