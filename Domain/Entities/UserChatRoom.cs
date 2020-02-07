using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserChatRoom : BaseEntity
    {
        public string UserId { get; set; }
        public long ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
