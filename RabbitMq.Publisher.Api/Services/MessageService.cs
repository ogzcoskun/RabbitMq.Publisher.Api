using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RabbitMq.Publisher.Api.Models;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Publisher.Api.Services
{

    public class MessageService : IMessageService
    {

        private readonly IConfiguration _config;

        public MessageService(IConfiguration config)
        {
            _config = config;
        }


        public async Task<ServiceResponse> SendMessage(MessageModel message)
        {
            try
            {

                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri(_config["RabbitMqConnection"]);


                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("TestQueue", false, false, true);

                    byte[] bytemessage = Encoding.UTF8.GetBytes(message.Message);
                    channel.BasicPublish(exchange: "", routingKey: "TestQueue", body: bytemessage);

                }


                return new ServiceResponse()
                {
                    Success = true,
                    Message = "Message send successfully"
                };
            }catch(Exception ex)
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
