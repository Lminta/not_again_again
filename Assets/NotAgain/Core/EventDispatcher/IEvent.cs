namespace NotAgain.Core.EventDispatcher
{
    public interface IEvent
    {
        
    }

    public abstract class Event : IEvent
    {
    }

    public abstract class OneArgEvent<T> : IEvent
    {
    }
}