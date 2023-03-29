using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Store.Domain.StoreContext.Handlers;
using Store.Tests.Mocks;

namespace Store.Tests.Handlers;

[TestClass]
public class CustomerHandlerTests
{
    [TestMethod]
    public void ShouldRegisterCustomerWhenCommandIsValid()
    {
        var command = new CreateCustomerCommand();
        command.FirstName = "João";
        command.LastName = "Carvalho";
        command.Document = "250.517.200-56"; // Gerado com https://www.4devs.com.br/gerador_de_cpf
        command.Email = "joao@mydomainmock.com.test";
        command.Phone = "11 1 2345 6789";

        var handler = new CustomerHandler(
            new FakeCustomerRepository(),
            new FakeEmailService()
        );

        var result = handler.Handle(command);

        Assert.AreNotEqual(null, result);
        Assert.AreEqual(true, handler.IsValid);
    }
}