using Store.Domain.StoreContext.Contracts;
using Store.Domain.StoreContext.Enums;
using Store.Shared.Entities;

namespace Store.Domain.StoreContext.Entities;
public class Order : Entity
{
    private readonly IList<OrderItem> _items = new List<OrderItem>();
    private readonly IList<Delivery> _deliveries = new List<Delivery>();

    public Order(Customer customer)
    {
        Customer = customer;
        CreateDate = DateTime.Now;
        Status = EOrderStatus.Create;

        // AddNotifications(new OrderContract(this));
    }

    public Customer Customer { get; private set; }
    public string? Number { get; private set; }
    public DateTime CreateDate { get; private set; }
    public EOrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
    public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

    // Adiciona um item ao pedido
    public void AddItem(OrderItem item)
    {
        _items.Add(item);

        AddNotifications(new OrderItemContract(item));
    }

    // Adiciona uma entrega ao pedido
    public void AddDelivery(Delivery delivery)
    {
        _deliveries.Add(delivery);
    }

    // Gera o pedido
    public void PlaceOrder()
    {
        // Gera o numero do pedido
        Number = Guid
            .NewGuid()
            .ToString()
            .Replace("-", "")
            .Substring(0, 8)
            .ToUpper();
    }

    // Realiza o pagamento do pedido
    public void Pay()
    {
        // Ao pagar o pedido, o status do pedido é alterado para pago
        Status = EOrderStatus.Paid;
    }

    // Processo de separação dos itens do pedido para envio.
    public void Ship()
    {
        var deliveries = new List<Delivery>();
        var shipItems = new List<OrderItem>();
        var maxItemsPerDelivery = 5;

        foreach (var item in _items)
        {
            shipItems.Add(item);

            if (shipItems.Count == maxItemsPerDelivery)
            {
                var delivery = new Delivery(DateTime.Now.AddDays(5));
                deliveries.Add(delivery);
                shipItems.Clear();
            }
        }

        // Verifica se há itens restantes e cria uma nova entrega.
        if (shipItems.Any())
        {
            var delivery = new Delivery(DateTime.Now.AddDays(6));
            deliveries.Add(delivery);
        }

        // Chama o método Ship da entidade Delivery e altera o status da entrega para Enviado.
        deliveries.ForEach(x => x.Ship());

        // Adiciona todas as entregas ao pedido.
        deliveries.ForEach(x => _deliveries.Add(x));
    }

    // Permite cancelar o pedido
    public void Cancel()
    {
        Status = EOrderStatus.Canceled;

        // Verifica cada entrega na lista de entregas e chama o método Cancel da Entidade Delivery.
        // O método Cancel permite cancelar a entrega caso o status seja diferente de já entregue.
        _deliveries.ToList().ForEach(x => x.Cancel());
    }
}