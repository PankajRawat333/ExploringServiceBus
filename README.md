# ExploringServiceBus

Background

I have created this project to explore basic capability of ServiceBus. My project requirement was an interaction between microservices using message queue and I was comfortable to use storage queue instead of serviceBus, however storage queue have 64k max message size limit and our case that become a bottleneck.


>Setup your ServiceBus and change connection string

<a href='https://github.com/PankajRawat333/ExploringServiceBus/tree/master/ExploringServiceBus/BasicSender'>BasicSender</a> - This project containing two methods 
1. Send messages to Servicebus.
2. Send messages with header to servicebus.

<a href='https://github.com/PankajRawat333/ExploringServiceBus/tree/master/ExploringServiceBus/BasicReceiver'>BasicReceiver</a>
This is Azure function, listen message form services bus


<a href='https://github.com/PankajRawat333/ExploringServiceBus/tree/master/ExploringServiceBus/BatchSender'>BatchSender</a>
1. Send .net serialize class object as message.
2. Send messages using batch.

<a href='https://github.com/PankajRawat333/ExploringServiceBus/tree/master/ExploringServiceBus/BasicSenderWithRetryPolicy'>BasicSenderWithRetryPolicy</a>

(Wrong Project name) Setting MaxDeliveryCount of queue.  By default value is 10 and we have steup this 3.

<a href='https://github.com/PankajRawat333/ExploringServiceBus/tree/master/ExploringServiceBus/BasicReceiverWithRetry'>BasicReceiverWithRetry</a>
(Wrong Project name) Azure function would validate message should deliver only 3 times instead of 10 (default).

<a href='https://github.com/PankajRawat333/ExploringServiceBus/tree/master/ExploringServiceBus/DeadLetterQueueReceiver'>DeadLetterQueueReceiver</a>
Read message from dead letter queue
