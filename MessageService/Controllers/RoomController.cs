using Domain.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.ChatRooms.Select(m => m.ToChatRoomDto()).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetEntity(long id)
        {
            var room = _context.ChatRooms.FirstOrDefault(r => r.Id == id);

            if (room is null)
                return NotFound();

            return Ok(room);
        }

        [HttpGet("/user/{userId}")]
        public IActionResult GetUserChatRooms(string userId)
        {
            var roomDtos = _context.UserChatRooms.Include(ur => ur.ChatRoom)
                                                 .Where(ur => ur.UserId == userId)
                                                 .Select(ur => ur.ChatRoom.ToChatRoomDto());

            return Ok(roomDtos);
        }

        [HttpGet("{chatRoomId}/users")]
        public IActionResult GetChatRoomUsers(long chatRoomId)
        {
            var users = _context.UserChatRooms.Where(r => r.ChatRoomId == chatRoomId)
                                              .Select(r => r.UserId)
                                              .ToList();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateChatRoom(ChatRoomDto chatRoomDto)
        {
            var chatRoom = new ChatRoom(chatRoomDto);
            _context.ChatRooms.Add(chatRoom);
            _context.SaveChanges();
            return Ok(chatRoom.Id);
        }

        [HttpPut("{chatRoomId}")]
        public IActionResult UpdateChatRoom(long chatRoomId, ChatRoomDto chatRoomDto)
        {
            if (chatRoomDto is null)
                return BadRequest();

            var chatRoom = _context.ChatRooms.FirstOrDefault(r => r.Id == chatRoomId);

            if (chatRoom is null)
                return NotFound();

            chatRoom.FromChatRoomDto(chatRoomDto);
            _context.ChatRooms.Update(chatRoom);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{chatRoomId}")]
        public IActionResult DeleteChatRoom(long chatRoomId)
        {
            var room = _context.ChatRooms.Find(chatRoomId);

            if (room is null)
                return BadRequest();

            _context.ChatRooms.Remove(room);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("add-user/{chatRoomId}/{userId}")]
        public IActionResult AddUserToChatRoom(long chatRoomId, string userId)
        {
            if (_context.UserChatRooms.Any(uc => uc.ChatRoomId == chatRoomId && uc.UserId == userId))
                return BadRequest("Already exists");

            _context.UserChatRooms.Add(new UserChatRoom { ChatRoomId = chatRoomId, UserId = userId });
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("remove-user/{chatRoomId}/{userId}")]
        public IActionResult RemoveUserAtChatRoom(long chatRoomId, string userId)
        {
            var userChatRoom = _context.UserChatRooms.FirstOrDefault(uc => uc.ChatRoomId == chatRoomId && uc.UserId == userId);

            if (userChatRoom is null)
                return BadRequest();

            _context.UserChatRooms.Remove(userChatRoom);
            _context.SaveChanges();
            return Ok();
        }
    }
}
