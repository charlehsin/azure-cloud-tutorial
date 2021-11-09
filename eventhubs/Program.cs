using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;

namespace eventhubs
{
    class Program
    {
        // TODO: Enter your own connectin string.
        // connection string to the Event Hubs namespace
        private const string connectionString = "<EVENT HUBS NAMESPACE - CONNECTION STRING>";
        
        // TODO: Enter your own even hub name.
        // name of the event hub
        private const string eventHubName = "<EVENT HUB NAME>";
        
        // TODO: Enter your own connection string.
        private const string blobStorageConnectionString = "<AZURE STORAGE CONNECTION STRING>";
        
        // TODO: Enter your own blob container name.
        private const string blobContainerName = "<BLOB CONTAINER NAME>";
        
        // number of events to be sent to the event hub
        private const int numOfEvents = 3;

        // The Event Hubs client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when events are being published or read regularly.
        static EventHubProducerClient producerClient;

        static BlobContainerClient storageClient;

        // The Event Hubs client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when events are being published or read regularly.
        static EventProcessorClient processor;

        static async Task Main()
        {
            Console.WriteLine("Starting the app");

            Console.Write("Enter 1 to try sender.");
            Console.Write($"{Environment.NewLine}Enter 2 to try receiver.");
            Console.Write($"{Environment.NewLine}Enter an integer: ");

            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out var userInputNumber))
            {
                switch (userInputNumber)
                {
                    case 1:
                        await RunSenderAsync();
                        break;
                    case 2:
                        await RunReceiver();
                        break;
                }
            }

            Console.WriteLine("Finishing the app");
        }

        // sender
        private static async Task RunSenderAsync()
        {
            // Create a producer client that you can use to send events to an event hub
            producerClient = new EventHubProducerClient(connectionString, eventHubName);

            Console.WriteLine("Client created");

            // Create a batch of events
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            Console.WriteLine("Event batch created");

            for (int i = 1; i <= numOfEvents; i++)
            {
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
                {
                    // if it is too large for the batch
                    throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
                }
            }

            Console.WriteLine("Events created, starting to send events.");

            try
            {
                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine($"A batch of {numOfEvents} events has been published.");
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }

        // receiver
        private static async Task RunReceiver()
        {
            // Read from the default consumer group: $Default
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use
            storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            Console.WriteLine("Blob container client created");

            // Create an event processor client to process events in the event hub
            processor = new EventProcessorClient(storageClient, consumerGroup, connectionString, eventHubName);

            Console.WriteLine("Event Processor client created");

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            Console.WriteLine("Staring processing");

            // Start the processing
            await processor.StartProcessingAsync();

            Console.WriteLine("Processing started. Waiting for 30 seconds....");

            // Wait for 30 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(30));

            Console.WriteLine("Stopping processing");

            // Stop the processing
            await processor.StopProcessingAsync();
        }

        // EventProcessorClient event callback
        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        // EventProcessorClient error call back
        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
