using Coravel.Invocable;

namespace CoravelTry
{
    public class HourlyMaintenance : IInvocable
    {
        Task IInvocable.Invoke()
        {
            Console.WriteLine(DateTime.Now + " HourlyMaintenance.Invoke()");
            return Task.CompletedTask;
        }
    }
}
