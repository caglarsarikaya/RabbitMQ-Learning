# RabbitMQ-Learning
I am catching the rabbit :)
*************************************************

run rabbit on docker => docker run -d -p 5672:5672 -p 15672:15672 --name LazyRabbit rabbitmq

acces the heart of rabbit if it is closed => docker container exec -it LazyRabbit rabbitmq-plugins enable rabbitmq_management
