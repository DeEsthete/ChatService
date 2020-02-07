using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ChatRoom : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<UserChatRoom> Users { get; set; }
        public IEnumerable<ChatMessage> Messages { get; set; }

        public ChatRoom()
        {

        }

        public ChatRoom(ChatRoomDto chatRoomDto)
        {
            FromChatRoomDto(chatRoomDto);

            Id = chatRoomDto.Id;
            CreationDate = chatRoomDto?.CreationDate ?? DateTime.Now;
        }

        public void FromChatRoomDto(ChatRoomDto chatRoomDto)
        {
            Name = chatRoomDto.Name;
        }

        public ChatRoomDto ToChatRoomDto()
        {
            var chatRoomDto = new ChatRoomDto
            {
                Id = Id,
                CreationDate = CreationDate,
                Name = Name,
            };

            return chatRoomDto;
        }
    }
}
