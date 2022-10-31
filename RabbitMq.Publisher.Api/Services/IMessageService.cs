using RabbitMq.Publisher.Api.Models;
using System.Threading.Tasks;

namespace RabbitMq.Publisher.Api.Services
{
    public interface IMessageService
    {
        Task<ServiceResponse> SendMessage(MessageModel message);
    }
}
