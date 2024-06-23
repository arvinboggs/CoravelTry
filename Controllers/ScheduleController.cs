using Coravel.Scheduling.Schedule.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoravelTry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        readonly IScheduler _scheduler;

        public ScheduleController(IScheduler scheduler)
        {
            // scheduler 
            _scheduler = scheduler;
        }

        [HttpGet("Add")]
        public string Add()
        {
            _scheduler.Schedule(() =>
            {
                Console.WriteLine(DateTime.Now + " scheduled via ScheduleController.Add");
            })
            .EverySeconds(7);

            return DateTime.Now.ToString();
        }
    }
}
