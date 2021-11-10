using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace servicebus
{
    class Program
    {
        // TODO: Enter your own connectin string.
        // connection string to your Service Bus namespace
        static string connectionString = "<NAMESPACE CONNECTION STRING>";
        
        // TODO: Enter your own queue name.
        // name of your Service Bus queue
        static string queueName = "<QUEUE NAME>";
        
        // TODO: Enter your own topic name.
        // name of your Service Bus topic
        static string topicName = "<TOPIC NAME>";
        
        // TODO: Enter your own subscription name.
        // name of the subscription to the topic
        static string subscriptionName = "<SERVICE BUS - TOPIC SUBSCRIPTION NAME>";
        
        // the client that owns the connection and can be used to create senders and receivers
        static ServiceBusClient client;

        // the sender used to publish messages to the queue
        static ServiceBusSender sender;

        // number of messages to be sent to the queue
        private const int numOfMessages = 3;

        // the processor that reads and processes messages from the queue
        static ServiceBusProcessor processor;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting the app");

            Console.Write("Enter 1 to try sender.");
            Console.Write($"{Environment.NewLine}Enter 2 to try receiver.");
            Console.Write($"{Environment.NewLine}Enter 3 to try sending using topic.");
            Console.Write($"{Environment.NewLine}Enter 4 to try receiving using topic and subscription.");
            Console.Write($"{Environment.NewLine}Enter an integer: ");

            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out var userInputNumber))
            {
                switch (userInputNumber)
                {
                    case 1:
                        await RunSenderAsync(false);
                        break;
                    case 2:
                        await RunReceiverAsync(false);
                        break;
                    case 3:
                        await RunSenderAsync(true);
                        break;
                    case 4:
                        await RunReceiverAsync(true);
                        break;
                }
            }

            Console.WriteLine("Finishing the app");
        }

        // sender
        private static async Task RunSenderAsync(bool isTopicUsed)
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // Create the clients that we'll use for sending and processing messages.
            client = new ServiceBusClient(connectionString);
            Console.WriteLine("Client created");

            sender = client.CreateSender(isTopicUsed ? topicName : queueName);
            Console.WriteLine("Sender created");

            // create a batch
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            Console.WriteLine("Message batch created");

            for (int i = 1; i <= numOfMessages; i++)
            {
                // try adding a message to the batch
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }
            Console.WriteLine("Messages added");

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        // receiver
        private static async Task RunReceiverAsync(bool isTopicUsed)
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //

            // Create the client object that will be used to create sender and receiver objects
            client = new ServiceBusClient(connectionString);
            Console.WriteLine("Client created");

            // create a processor that we can use to process the messages
            if (isTopicUsed)
            {
                processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());
            }
            else
            {
                processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
            }
            Console.WriteLine("Receiver created");

            try
            {
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        // handle received messages
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            // complete the message. messages is deleted from the queue.
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
