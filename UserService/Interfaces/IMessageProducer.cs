namespace UserService.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}