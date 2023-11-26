using Microsoft.AspNetCore.SignalR;
using System;

namespace signalr_and_quartz.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {

        public static int Total { get; set; } = 0;

        
        public override  async Task OnConnectedAsync()
        {
            Total++;
            await Clients.All.UpdateTotalUsers(Total);
            await Clients.All.RecieveMessage($"{Context.ConnectionId} has joined");
         
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Total--;
            await Clients.All.UpdateTotalUsers(Total);
            await Clients.All.RecieveMessage($"{Context.ConnectionId} has left");
            
        }
       
        public int GetTotal()
        {
            return Total;
        }
       

    }


    public interface INotificationHub
    {
        Task RecieveMessage(string message);
        Task UpdateTotalUsers(int num);
    }
}
