using Coravel.Invocable;

namespace CoravelTry
{
    public class MinutelyMaintenance : IInvocable
    {
        Task IInvocable.Invoke()
        {
            Console.WriteLine(DateTime.Now + " MinutelyMaintenance.Invoke()");
            return Task.CompletedTask;
        }
    }
}
