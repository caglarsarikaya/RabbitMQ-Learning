using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            // factory.Uri = new Uri("amqp://guest:guest@localhost:15672");

            factory.UserName = ConnectionFactory.DefaultUser;
            factory.Password = ConnectionFactory.DefaultPass;
            factory.VirtualHost = ConnectionFactory.DefaultVHost;
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;


            using var conn = factory.CreateConnection();

            var channel = conn.CreateModel();
            //first is name for queue
            //second when shut down, record on hdd or not
            //third connectable only from here. It means you can publish here but you cant subscribe from somewhere else
            //fourth close the queue when all connections are closed
            channel.QueueDeclare("hello-queue", true, false, false);


            while(1==1)
            {
                string message = Console.ReadLine();
                if (message == "0") return;

                //rabbitmq accepts the messages as byte array
                var messageBody = Encoding.UTF8.GetBytes(message);

                //first for selecting excehange
                //second when didnt select an exchange, you have to send queue name
                //third is settings
                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

                Console.WriteLine("Message is sent");
            }
             


        }
    }
}
