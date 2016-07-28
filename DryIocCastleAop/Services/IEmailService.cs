namespace DryIocCastleAop.Services
{
    public interface IEmailService
    {
        bool Send();
        void Resend();
    }
}
