using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class ChatMessageDto
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public long ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public string Text { get; set; }
    }
}
