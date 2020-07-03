using Flunt.Notifications;
using MongoDB.Bson;

namespace Boticario.EuRevendedor.Core.Extensions
{
    public static class ObjectIdExtensions
    {
        public static ObjectId ObjectIdCast(this Notifiable notifiable, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                notifiable.AddNotification(new Notification(null, "O id é Obrigatório!"));
                return ObjectId.Empty;
            }

            if (ObjectId.TryParse(id, out var objectId))
            {
                return objectId;
            }
            else
            {
                notifiable.AddNotification(new Notification(null, "O id é Obrigatório!"));
            }

            return ObjectId.Empty;
        }
    }
}
