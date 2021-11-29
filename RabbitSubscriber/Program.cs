using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();

            factory.UserName = ConnectionFactory.DefaultUser;
            factory.Password = ConnectionFactory.DefaultPass;
            factory.VirtualHost = ConnectionFactory.DefaultVHost;
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;


            using var conn = factory.CreateConnection();

            var channel = conn.CreateModel();
            //creating queue is on the both side has problem, if publisher cant creates, subscribes creates. But the parameters are must be same
            //channel.QueueDeclare("hello-queue", true, false, false);


            var consumer = new EventingBasicConsumer(channel);

            //second parameter when true rabbit will delete the message from queue without checking is revieved true
            //real solitions has to be with false parameter we should check, is that proceed true
            channel.BasicConsume("hello-queue", true, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Console.WriteLine("Message Recieved: " + message);
            };

            Console.ReadLine();


        }
 
    }
}
