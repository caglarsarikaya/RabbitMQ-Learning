using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

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
            channel.BasicConsume("hello-queue", false, consumer);


            //qos settings for sending how many messages in one time to one subscriber if you say 5 and false, it will sends 5 by 5. If you say 5 and true, it will distribute 5 by deviding subsriber count
            //if you have 2 subs it will sent 2 and 3 , or if you have 5 subs it will send 1 to each of them
            //first param for message size
            channel.BasicQos(0, 30, true);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Console.WriteLine("Message Recieved: " + message);

                //this is works when consume ack is false, it means you have to say to rabbit, you can delete the message from queue
                //first is which message wil delete, second is if there are many message on memory and they are proceed. Say to delete all,
                //BasicQos settings you can say send messages 5 by 5 to subscribers. you can use like that if the message proceed easly, if its heavy you have to send one by one
                //If you sen messages multiples, it will claim all and store in ram. Subscriber will not speak very often with rabbit, it will read messages from memory, when finished from memory
                //Connecting rabbit and taking new messages if there is..
                //if its false accurasy is really less, 1000 messages 300 pieces didnt tracked, but if true, track perfectly
                channel.BasicAck(e.DeliveryTag, true);


                Thread.Sleep(500);
            };

            Console.ReadLine();


        }
 
    }
}
