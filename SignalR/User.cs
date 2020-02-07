using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR
{
    public class User
    {
        public string ConnectionId { get; set; }
        public long ChatRoomId { get; set; }
        public string Name { get; set; }
    }
}
