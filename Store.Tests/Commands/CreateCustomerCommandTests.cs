using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;

namespace Store.Tests.Commands;
[TestClass]
public class CreateCustomerCommandTests
{
    [TestMethod]
    public void ShouldReturnSuccessWhenCommandIsValid()
    {
        var command = new CreateCustomerCommand();
        command.FirstName = "João";
        command.LastName = "Four";
        command.Document = "007.513.740-24"; //Gerado com https://www.4devs.com.br/gerador_de_cpf
        command.Email = "joao@quatro4.com";
        command.Phone = "11 1 2345 6789";

        Assert.IsTrue(command.Valid());
    }
}