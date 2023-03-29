using Store.Domain.StoreContext.Enums;
using Store.Shared.Entities;

namespace Store.Domain.StoreContext.Entities;
public class Delivery : Entity
{
    public Delivery(DateTime estimatedDeliveryDate)
    {
        CreateDated = DateTime.Now;
        EstimatedDeliveryDate = estimatedDeliveryDate;
        Status = EDeliveryStatus.Waiting;
    }

    public DateTime CreateDated { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public EDeliveryStatus Status { get; set; }

    // Envia a entrega
    public void Ship()
    {
        Status = EDeliveryStatus.Shipped;
    }

    public void Cancel()
    {
        if (Status != EDeliveryStatus.Delivered)
            Status = EDeliveryStatus.Canceled;
    }
}
