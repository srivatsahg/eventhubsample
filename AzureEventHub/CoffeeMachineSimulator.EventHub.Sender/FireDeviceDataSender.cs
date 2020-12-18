using FireDeviceSimulator.EventHub.Sender.Model;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDeviceSimulator.EventHub.Sender
{
    public interface IFireDeviceDataSender
    {
        Task SendDataAsync(Model.FireDeviceDataModel coffeeMachineData);
        Task SendDataAsync(IEnumerable<Model.FireDeviceDataModel> coffeeMachineDataList);
    }
    public class FireDeviceData : IFireDeviceDataSender
    {
        EventHubClient _eventHubClient;

        public FireDeviceData(string eventHubConnectionString)
        {
           _eventHubClient  = EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }

        public async Task SendDataAsync(Model.FireDeviceDataModel coffeeMachineData)
        {
            EventData eventData = CreateEventData(coffeeMachineData);
            await _eventHubClient.SendAsync(eventData);
        }

        /// <summary>
        /// Batching the data into a set to send to the Event Hub
        /// </summary>
        /// <param name="coffeeMachineDataList"></param>
        /// <returns></returns>
        public async Task SendDataAsync(IEnumerable<Model.FireDeviceDataModel> coffeeMachineDataList)
        {
            var eventDatas = coffeeMachineDataList.Select(coffeeMachineData => CreateEventData(coffeeMachineData));

            var eventDataBatch = _eventHubClient.CreateBatch();

            foreach (var eventData in eventDatas)
            {
                if (!eventDataBatch.TryAdd(eventData))
                {
                    //First send
                    await _eventHubClient.SendAsync(eventDataBatch);
                    //create another batch
                    eventDataBatch = _eventHubClient.CreateBatch();
                    //send again
                    eventDataBatch.TryAdd(eventData);
                }
            }

            if(eventDataBatch.Count > 0)
            {
                await _eventHubClient.SendAsync(eventDataBatch);
            }
        }

        private static EventData CreateEventData(Model.FireDeviceDataModel coffeeMachineData)
        {
            var jsondata = JsonConvert.SerializeObject(coffeeMachineData);
            var eventData = new EventData(Encoding.UTF8.GetBytes(jsondata));
            return eventData;
        }
    }
}
