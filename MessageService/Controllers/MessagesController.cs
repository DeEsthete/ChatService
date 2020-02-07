using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Entities;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(long? chatRoomId)
        {
            var messages = _context.ChatMessages;

            if (chatRoomId.HasValue)
                messages.Where(m => m.ChatRoomId == chatRoomId);

            return Ok(messages.Select(m => m.ToChatMessageDto()).ToList());
        }

        [HttpGet("room/{chatRoomId}")]
        public IActionResult GetMessagesByRoom(long chatRoomId)
        {
            var messages = _context.ChatMessages.Where(m => m.ChatRoomId == chatRoomId);
            return Ok(messages.Select(m => m.ToChatMessageDto()).ToList());
        }

        [HttpGet("{pageIndex}/{pageSize}/{chatRoomId}")]
        public IActionResult GetPageChatRoomMessages(int pageIndex, int pageSize, long chatRoomId)
        {
            var messages = _context.ChatMessages.Where(m => m.ChatRoomId == chatRoomId)
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .ToList();

            return Ok(messages);
        }

        [HttpGet("{id}")]
        public IActionResult GetEntity(long id)
        {
            var message = _context.ChatMessages.FirstOrDefault(m => m.Id == id);

            if (message is null)
                return NotFound();

            return Ok(message.ToChatMessageDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEntity(long id, ChatMessageDto messageDto)
        {
            if (messageDto is null)
                return BadRequest();

            var message = _context.ChatMessages.FirstOrDefault(m => m.Id == id);

            if (message is null)
                return NotFound();

            message.FromChatMessageDto(messageDto);

            _context.ChatMessages.Update(new ChatMessage(messageDto));
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateEntity(ChatMessageDto messageDto)
        {
            _context.ChatMessages.Add(new ChatMessage(messageDto));
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntity(long id)
        {
            var message = _context.ChatMessages.FirstOrDefault(m => m.Id == id);

            if (message is null)
                return NotFound();

            _context.ChatMessages.Remove(message);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
