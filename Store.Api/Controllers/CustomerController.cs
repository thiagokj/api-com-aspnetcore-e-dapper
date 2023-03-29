using Microsoft.AspNetCore.Mvc;
using Store.Domain.StoreContext.Commands;
using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Store.Domain.StoreContext.Handlers;
using Store.Domain.StoreContext.Queries;
using Store.Domain.StoreContext.Repositories;
using Store.Shared.Commands;

namespace Store.Api.Controllers;
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly CustomerHandler _handler;

    public CustomerController(ICustomerRepository repository, CustomerHandler handler)
    {
        _repository = repository;
        _handler = handler;
    }

    [HttpGet]
    [Route("v1/customers")]
    public IEnumerable<ListCustomerQueryResult> Get()
    {
        return _repository.Get();
    }

    [HttpGet]
    [Route("v1/customers/{id}")]
    public GetCustomerQueryResult? GetById(Guid id)
    {
        return _repository.Get(id);
    }

    [HttpGet]
    [Route("v1/customers/{id}/orders")]
    public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
    {
        return _repository.GetOrders(id);
    }

    [HttpPost]
    [Route("v1/customers")]
    public ICommandResult Post([FromBody] CreateCustomerCommand command)
    {
        var result = (CommandResult)_handler.Handle(command);
        return result;
    }

    [HttpPut]
    [Route("v1/customers/{id}")]
    public ICommandResult Put(Guid id, [FromBody] UpdateCustomerCommand command)
    {
        command.Id = id;
        var result = (CommandResult)_handler.Handle(command);
        return result;
    }

    [HttpDelete]
    [Route("v1/customers/{id}")]
    public ICommandResult Delete(string id, DeleteCommand command)
    {
        command.Id = id.ToString();
        var result = (CommandResult)_handler.Handle(command);
        return result;
    }
}