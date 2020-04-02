namespace Bla
{
    public interface ISMSGateway
    {
        void SendSms(string phoneNumber, string message);
    }
}
