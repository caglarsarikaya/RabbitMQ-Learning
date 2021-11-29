# RabbitMQ-Learning
I am catching the rabbit :)


run rabbit on docker /n
docker run -d -p 5672:5672 -p 15672:15672 --name rbtContainer rabbitmq:3.8.26-management

acces the heart of rabbit if it is closed 
docker container exec -it rbtContainer rabbitmq-plugins enable rabbitmq_management
