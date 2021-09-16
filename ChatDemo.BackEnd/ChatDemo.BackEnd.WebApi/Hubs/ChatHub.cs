using ChatDemo.BackEnd.WebApi.Extensions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.BackEnd.WebApi.Hubs
{
    public class ChatHub: Hub
    {
        public readonly static ConnectionMappingExtension<string> _connections =
                    new ConnectionMappingExtension<string>();

        public override Task OnConnectedAsync()
        {
            var username = Context.GetHttpContext().Request.Query["username"];

            _connections.Add(username, Context.ConnectionId);

            Clients.AllExcept(Context.ConnectionId).SendAsync("UserConnected", username);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.GetHttpContext().Request.Query["username"];

            _connections.Remove(username, Context.ConnectionId);

            Clients.All.SendAsync("UserDisconnected", username);

            return base.OnDisconnectedAsync(exception);
        }

        public void OnSendMsg(string username, string msg)
        {
            msg = msg.Replace("\n", "<br>");
            Clients.All.SendAsync("RecieveMsg", username, msg);
        }
    }
}
