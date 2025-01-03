using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace Consumer
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; //Создаем фабрику для создания подключений
            factory.UserName = "guest"; //Значение по умолчанию.
            factory.Password = "guest"; //Значение по умолчанию.

            var connection = await factory.CreateConnectionAsync(); //Создаем подключение

            var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);
             consumer.ReceivedAsync += Consumer_Received;
            await channel.BasicConsumeAsync("a", true, consumer);

            while (true)
            {

            }
        }

        private static async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());

            await Task.Delay(250);

            Console.WriteLine($"End processing {message}");
        }
    }
}