using System;
using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace CoffeeMachineSimulator.Receiver.Direct
{
    class Program
    {
        const string eventhubConnectionString = "Endpoint=sb://glss-eventhub-ns.servicebus.windows.net/;SharedAccessKeyName=sendreceive;SharedAccessKey=uWDsZSxdF1Feb0LwSL3v5VuViW7uqyt7O1e0fJeXCoU=;EntityPath=glss-sensor-events";
        //office
        //const string eventhubConnectionString = "Endpoint=sb://glss-eventhub-ns.servicebus.windows.net/;SharedAccessKeyName=SendListenPolicy;SharedAccessKey=Pf6Kbmsrx3r+Kn2HvU1f0HOMxKzxX7JDlDgzlKrPowI=;EntityPath=glss-ehub";

        static void Main(string[] args)
        {
            Console.WriteLine("Receiving events from the Coffee Machine");
            MainAsyncMethod().Wait();
        }

        private static async Task MainAsyncMethod()
        {
            Console.WriteLine("Connecting to the eventhub");

            var eventHubClient = EventHubClient.CreateFromConnectionString(eventhubConnectionString);

            var runtimeInformation = await eventHubClient.GetRuntimeInformationAsync();

            Console.WriteLine("Waiting for the events");

            //specifying the partition
            var partitionReceivers = runtimeInformation.PartitionIds.Select(partitionid =>
                eventHubClient.CreateReceiver("$Default", partitionid, EventPosition.FromStart())
            ).ToList();


            foreach (var partitionRx in partitionReceivers)
            {
                partitionRx.SetReceiveHandler(new SensorDataPartitionReceiverHandler(partitionRx.PartitionId));
            }
            
            Console.WriteLine("Press any key to shutdown");
            Console.ReadLine();

            await eventHubClient.CloseAsync();
        }
    }
}
