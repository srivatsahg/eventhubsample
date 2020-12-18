using FireDeviceSimulator.EventHub.Sender;
using FireDeviceSimulator.EventHub.Sender.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace FireDeviceSimulator.UI.ViewModel
{
    public class MainViewModel: BindableBase
    {
        private int alarmCounter;
        private int alertCounter;
        private string city;
        private string serialNumber;
        private IFireDeviceDataSender _coffeeMachineDataSender;
        private int _boilerTemp;
        private int _beanLevel;
        private bool _isSendingPeriodically;
        private DispatcherTimer dispatcherTimer;

        public MainViewModel(IFireDeviceDataSender coffeeMachineDataSender)
        {
            _coffeeMachineDataSender = coffeeMachineDataSender;
            serialNumber = System.Guid.NewGuid().ToString().Substring(0, 8);
            TriggerAlarmCommand = new DelegateCommand(TriggerAlarm);
            TriggerAlertCommand = new DelegateCommand(TriggerAlert);
            Logs = new ObservableCollection<string>();

            dispatcherTimer = new DispatcherTimer {
                Interval = TimeSpan.FromSeconds(2)
            };

            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var boilerTemperature = CreateCoffeeMachineData(nameof(BoilerTemperature), BoilerTemperature);
            var beanLevel = CreateCoffeeMachineData(nameof(BeanLevel), BeanLevel);

            //await SendDataAsync(boilerTemperature);
            //await SendDataAsync(beanLevel);

            //Sending data in batch
            await SendDataAsync(new[] { boilerTemperature, beanLevel });
        }

        public ICommand TriggerAlarmCommand { get; }
        public ICommand TriggerAlertCommand { get; }
        public ObservableCollection<string> Logs { get; }

        private async void TriggerAlarm()
        {
            AlarmCounter++;
            EventHub.Sender.Model.FireDeviceDataModel coffeeMachineData = CreateCoffeeMachineData(nameof(MainViewModel.AlarmCounter), AlarmCounter);
            await SendDataAsync(coffeeMachineData);
        }

        private async void TriggerAlert()
        {
            AlertCounter++;
            EventHub.Sender.Model.FireDeviceDataModel coffeeMachineData = CreateCoffeeMachineData(nameof(MainViewModel.AlertCounter), AlertCounter);
            await SendDataAsync(coffeeMachineData);
        }

        public int AlarmCounter
        {
            get
            {
                return alarmCounter;
            }
            set
            {
                alarmCounter = value;
                RaisePropertyChanged();
            }
        }

        public int AlertCounter
        {
            get
            {
                return alertCounter;
            }
            set
            {
                alertCounter = value;
                RaisePropertyChanged();
            }
        }

        public int BoilerTemperature
        {
            get
            {
                return _boilerTemp;
            }
            set
            {
                _boilerTemp = value;
                RaisePropertyChanged();
            }
        }

        public int BeanLevel
        {
            get
            {
                return _beanLevel;
            }
            set
            {
                _beanLevel = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSendingPeriodically
        {
            get
            {
                return _isSendingPeriodically;
            }
            set
            {
                if(_isSendingPeriodically != value)
                {
                    _isSendingPeriodically = value;

                    if (_isSendingPeriodically)
                    {
                        dispatcherTimer.Start();
                    }
                    else
                    {
                        dispatcherTimer.Stop();
                    }

                    RaisePropertyChanged();
                }
            }
        }

        public string SerialNumber
        { get { return serialNumber; } set { serialNumber = value; RaisePropertyChanged(); } }

        public string City
        { get { return city; } set { city = value; RaisePropertyChanged(); } }


        private FireDeviceDataModel CreateCoffeeMachineData(string sensorType, int sensorValue)
        {
            return new FireDeviceDataModel
            {
                City = City,
                SerialNumber = SerialNumber,
                SensorType = sensorType,
                SensorValue = sensorValue,
                RecordingTime = DateTime.Now
            };
        }

        private async Task SendDataAsync(FireDeviceDataModel coffeeMachineData)
        {
            try
            {
                WriteLog($"Send Data : {coffeeMachineData}");
                await _coffeeMachineDataSender.SendDataAsync(coffeeMachineData);
            }
            catch(Exception ex)
            {
                WriteLog($"Exception : {ex.Message}");
            }
        }

        private async Task SendDataAsync(IEnumerable<FireDeviceDataModel> coffeeMachineDataList)
        {
            try
            {
                foreach(FireDeviceDataModel coffeeMachineData in coffeeMachineDataList)
                {
                    WriteLog($"Send Data : {coffeeMachineData}");
                }
                
                await _coffeeMachineDataSender.SendDataAsync(coffeeMachineDataList);
            }
            catch (Exception ex)
            {
                WriteLog($"Exception : {ex.Message}");
            }
        }

        private void WriteLog(string logMessage)
        {
            Logs.Insert(0, logMessage);
        }
    }
}
