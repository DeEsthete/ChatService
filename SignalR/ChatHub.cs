using Domain.Data;
using Domain.Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalR
{
    public class ChatHub : Hub
    {
        private IServiceProvider _sp;
        static List<User> Users = new List<User>();

        public ChatHub(IServiceProvider sp)
        {
            _sp = sp;
        }

        public void Send(ChatMessageDto message)
        {
            using (var scope = _sp.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.ChatMessages.Add(new ChatMessage(message));
                dbContext.SaveChanges();
            }

            Clients.All.addMessage(message);
        }

        public void Connect(long chatRoomId, string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                Clients.Caller.onConnected(id, userName, Users);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
