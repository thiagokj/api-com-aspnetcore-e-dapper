using Store.Domain.StoreContext.Services;

namespace Store.Tests.Mocks;
public class FakeEmailService : IEmailService
{
    public void Send(string to, string from, string subject, string body)
    {
        // Não precisa retornar nada
    }
}