using System;
using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace CoffeeMachineSimulator.Receiver.Direct
{
    public class SensorDataPartitionReceiverHandler : IPartitionReceiveHandler
    {
        private string partitionId;

        private int maxBatchSize = 10;
        public string PartitionId { get => partitionId; set => partitionId = value; }
        public int MaxBatchSize { get => maxBatchSize; set => maxBatchSize = value; }


        public SensorDataPartitionReceiverHandler(string partitionId)
        {
            this.PartitionId = partitionId;
        }

        public Task ProcessErrorAsync(Exception error)
        {
            Console.WriteLine($"Exception : {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(IEnumerable<EventData> eventDatas)
        {
            if (eventDatas != null)
            {
                foreach (var eventData in eventDatas)
                {
                    var dataasjson = Encoding.UTF8.GetString(eventData.Body.Array);
                    Console.WriteLine($"{dataasjson} | Partition-ID: {PartitionId}");
                }
            }
            return Task.CompletedTask;
        }
    }
}
