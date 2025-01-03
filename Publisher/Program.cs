using RabbitMQ.Client;
using System;

namespace Publisher
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

            while (true) 
            {
                
                byte[] message = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine());
                await channel.BasicPublishAsync("Sergey", "myQueue", false, message);
            }

        }
    }
}