using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public long ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public string SenderId { get; set; }
        public string Text { get; set; }

        public ChatMessage()
        {

        }

        public ChatMessage(ChatMessageDto messageDto)
        {
            FromChatMessageDto(messageDto);
            Id = messageDto.Id;
            CreationDate = messageDto?.CreationDate ?? DateTime.Now;
        }

        public void FromChatMessageDto(ChatMessageDto messageDto)
        {
            ChatRoomId = messageDto.ChatRoomId;
            SenderId = messageDto.SenderId;
            Text = messageDto.Text;
        }

        public ChatMessageDto ToChatMessageDto()
        {
            ChatMessageDto message = new ChatMessageDto
            {
                Id = Id,
                CreationDate = CreationDate,
                ChatRoomId = ChatRoomId,
                SenderId = SenderId,
                Text = Text
            };

            return message;
        }
    }
}
