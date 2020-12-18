using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDeviceSimulator.EventHub.Sender.Model
{
    public class FireDeviceDataModel
    {
        public string City { get; set; }
        public string SerialNumber { get; set; }
        public string SensorType { get; set; }
        public int SensorValue { get; set; }
        public DateTime RecordingTime { get; set; }

        public override string ToString()
        {
            return $"Time : {RecordingTime:HH:mm:ss} | {SensorType} : {SensorValue} |"
                    + $"City: {City} | Serialnumber: {SerialNumber}";
        }
    }
}
