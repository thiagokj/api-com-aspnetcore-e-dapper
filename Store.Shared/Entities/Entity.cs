using Flunt.Notifications;

namespace Store.Shared.Entities;
public abstract class Entity : Notifiable<Notification>
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}