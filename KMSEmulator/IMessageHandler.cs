namespace KMSEmulator
{
    public interface IMessageHandler
    {
        byte[] HandleRequest(byte[] request);
    }
}
