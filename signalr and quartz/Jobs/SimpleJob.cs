using Quartz;
using System.Diagnostics;
using signalr_and_quartz.Hubs;
using Microsoft.AspNetCore.SignalR;
using signalr_and_quartz.Models;

namespace signalr_and_quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class SimpleJob : IJob
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ApplicationDbContext _context;
        public SimpleJob(IHubContext<NotificationHub> hubContext, ApplicationDbContext context)
        {
            _hubContext = hubContext;
            _context = context; 
        }

        NotificationHub hb =new NotificationHub();

        public async Task Execute(IJobExecutionContext context)
        {

           if(hb.GetTotal() > 0)
           {

                var list = _context.Orders.Where(o => o.IsDeliverd == false && o.ExpectedDate < DateTime.Now).ToList();

                for(int i = 0; i < list.Count; i++)
                {
                    await _hubContext.Clients.All.SendAsync("RecieveMessage", $"order with id : {list[i].Id} has not deliverd yet!");
                }

                Debug.WriteLine($"time : {DateTime.Now}");
               
            }
          

        }
    }
}
