using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class ChatRoomDto
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Name { get; set; }
    }
}
