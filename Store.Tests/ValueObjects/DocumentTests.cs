
using Store.Domain.StoreContext.ValueObjects;

namespace Store.Tests.ValueObjects;
[TestClass]
public class DocumentTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentIsInvalid()
    {
        var document = new Document("1234");
        Assert.Fail();
    }
    [TestMethod]
    public void ShouldReturnSuccessWhenDocumentIsValid()
    {
        // CPF gerado com https://www.4devs.com.br/gerador_de_cpf
        var document = new Document("655.898.510-17");
        Assert.IsTrue(document.IsValid);
    }
}