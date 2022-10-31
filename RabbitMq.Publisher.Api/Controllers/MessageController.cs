using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RabbitMq.Publisher.Api.Models;
using RabbitMq.Publisher.Api.Services;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace RabbitMq.Publisher.Api.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<ServiceResponse> PublishMessage(MessageModel message)
        {
            try
            {
                var response = await _messageService.SendMessage(message);

                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
