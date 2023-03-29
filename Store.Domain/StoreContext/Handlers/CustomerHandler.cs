using Flunt.Notifications;
using Store.Domain.StoreContext.Commands;
using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Services;
using Store.Domain.StoreContext.ValueObjects;
using Store.Shared.Commands;

namespace Store.Domain.StoreContext.Handlers;
public class CustomerHandler :
    Notifiable<Notification>,
    ICommandHandler<CreateCustomerCommand>,
    ICommandHandler<AddAddressCommand>,
    ICommandHandler<UpdateCustomerCommand>,
    ICommandHandler<DeleteCommand>
{
    private readonly ICustomerRepository _repository;
    private readonly IEmailService _emailService;

    public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateCustomerCommand command)
    {
        // Remove caracteres não numéricos e verifica se o CPF já está em uso
        command.Document = new string(command.Document.Where(char.IsDigit).ToArray());

        if (_repository.CheckDocument(command.Document))
            AddNotification("CheckDocument.Document", "CPF já está em uso");

        // Verifica se o email já está em uso    
        if (_repository.CheckEmail(command.Email))
            AddNotification("CheckEmail.Email", "Email já está em uso");

        // Cria os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document);
        var email = new Email(command.Email);
        var phone = new Phone(command.Phone);

        // Cria a entidade
        var customer = new Customer(name, document, email, phone);

        // Validações dos VOs e Entidades, agrupando em uma lista de notificações do Handler
        AddNotifications(name.Notifications);
        AddNotifications(document.Notifications);
        AddNotifications(email.Notifications);
        AddNotifications(phone.Notifications);
        AddNotifications(customer.Notifications);

        // Caso haja alguma notificação, interrompe o fluxo do processo.
        if (!IsValid)
            return new CommandResult(
                false,
                "Foram geradas notificações, verifique os dados informados",
                Notifications
            );

        // Persiste o cliente
        _repository.Save(customer);

        // Envia email de boas vindas
        _emailService.Send(email.Address, "ola@store.quatro4.four", "Mensagem boas vindas", "Bem vindo a loja");

        return new CommandResult(
            true,
            "Bem vindo a loja",
            new
            {
                Id = customer.Id,
                Name = name.ToString(),
                Email = email.Address,
                Document = document.Number,
                Phone = phone.Number
            }
        );
    }

    public ICommandResult Handle(AddAddressCommand command)
    {
        throw new NotImplementedException();
    }

    public ICommandResult Handle(UpdateCustomerCommand command)
    {
        // Remove caracteres não numéricos e verifica se o CPF já está em uso
        command.Document = new string(command.Document.Where(char.IsDigit).ToArray());

        if (!_repository.CheckCustomer(command.Id))
            AddNotification("CheckCustomer.Id", "Cliente não encontrado");

        // Permite alterar apenas o CPF de um mesmo cliente
        if (_repository.CheckDocumentForUpdate(command.Id, command.Document))
            AddNotification("CheckDocument.Document", "CPF já está em uso");

        // Permite alterar apenas o Email de um mesmo cliente   
        if (_repository.CheckEmailForUpdate(command.Id, command.Email))
            AddNotification("CheckEmail.Email", "Email já está em uso");

        // Cria os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document);
        var email = new Email(command.Email);
        var phone = new Phone(command.Phone);

        // Cria a entidade e atribui o Id
        var customer = new Customer(command.Id, name, document, email, phone);

        // Validações dos VOs e Entidades, agrupando em uma lista de notificações do Handler
        AddNotifications(name.Notifications);
        AddNotifications(document.Notifications);
        AddNotifications(email.Notifications);
        AddNotifications(phone.Notifications);
        AddNotifications(customer.Notifications);

        // Caso haja alguma notificação, interrompe o fluxo do processo.
        if (!IsValid)
            return new CommandResult(
                false,
                "Foram geradas notificações, verifique os dados informados",
                Notifications
            );

        // Atualiza o cliente
        _repository.Update(customer);

        return new CommandResult(
            true,
            "Dados atualizados com sucesso",
            new
            {
                Id = customer.Id,
                Name = name.ToString(),
                Email = email.Address,
                Document = document.Number,
                Phone = phone.Number
            }
        );
    }

    public ICommandResult Handle(DeleteCommand command)
    {
        if (!command.Valid())
            AddNotification("DeleteCustomer.Id", "Cliente não encontrado");

        // Caso haja alguma notificação, interrompe o fluxo do processo.
        AddNotifications(command);
        if (!IsValid)
            return new CommandResult(
                false,
                "Foram geradas notificações, verifique os dados informados",
                Notifications
            );

        // Exclui o cliente
        _repository.Delete(command.Id);

        return new CommandResult(
            true,
            "Registro removido com sucesso",
            new { Id = command.Id }
        );
    }
}