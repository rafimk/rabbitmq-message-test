using Message.Consumer.DAL;
using Message.Consumer.Services;
using Message.Shared;
using Message.Shared.Deduplication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMessaging(c => c.AddDeduplication<NotifyDbContext>(builder.Configuration));
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddHostedService<MessagingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "Notify Service!");

var scope = app.Services.CreateScope();
var ctx = scope.ServiceProvider.GetRequiredService<NotifyDbContext>();
ctx.Database.EnsureCreated();

app.Run();