using Coravel;
using Coravel.Scheduling.Schedule;
using Coravel.Scheduling.Schedule.Interfaces;

namespace CoravelTry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<MinutelyMaintenance>(); // Register our class that contains the scheduled task
            builder.Services.AddTransient<HourlyMaintenance>(); // Register our class that contains the scheduled task
            builder.Services.AddScheduler(); // Register Coravel's Scheduler.
            builder.Services.AddControllers();
            var app = builder.Build();

            app.Services.UseScheduler(scheduler =>
            {
                // schedule your task here
                scheduler
                    .Schedule<MinutelyMaintenance>()
                    .EveryMinute();

                // schedule another task
                scheduler
                    .Schedule<HourlyMaintenance>()
                    .Hourly();

                // you may also schedule tasks without implementing an IInvocable
                scheduler
                    .Schedule(() =>
                    {
                        Console.WriteLine(DateTime.Now + " every 10 seconds");
                    })
                    .EverySeconds(10);
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Lifetime.ApplicationStarted.Register(() => OnApplicationStarted(app));

            app.Run();
        }

        static void OnApplicationStarted(WebApplication app)
        {
            // In my (Arvin) opinion, this is the best place to put your start up codes.
            // Putting code here frees the Main from unnecessary clutter.

            var scheduler = app.Services.GetRequiredService<IScheduler>();
            scheduler
                .Schedule(() =>
                {
                    Console.WriteLine(DateTime.Now + " scheduled from OnApplicationStarted");
                })
                .EverySeconds(5);
        }
    }
}
