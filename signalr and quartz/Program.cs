using Microsoft.EntityFrameworkCore;
using Quartz;
using signalr_and_quartz.Hubs;
using signalr_and_quartz.Jobs;
using signalr_and_quartz.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/////// quartz options ///////

builder.Services.AddQuartz(opt =>
    {
        opt.UseMicrosoftDependencyInjectionJobFactory();
        var jobkey = new JobKey("SimpleJob");
        opt.AddJob<SimpleJob>(o => o.WithIdentity(jobkey));

        opt.AddTrigger(o =>
        {
            o.ForJob(jobkey)
                .WithIdentity("SimpleJob-Trigger")
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(5).RepeatForever());

        });

    }
);
builder.Services.AddQuartzHostedService(q=>q.WaitForJobsToComplete=true);

//----------------------------------------------------------------------

/////// signalr ///////

builder.Services.AddSignalR();

//----------------------------------------------------------------------

/////// DbContext ///////

var conf = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(conf));

//----------------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("not-hub");

app.Run();
