using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.Devices.Client;

namespace SendEvents
{
    class Program
    {
        static string iotHubUri = "<IOT Hub URI>"; // ! put in value !
        static string deviceId = "<Device ID>"; // ! put in value !
        static string deviceKey = "<Device Shared access key>"; // ! put in value !
       
        static void Main(string[] args)
        {
            Random random = new Random();
            int temp = 0;
            int pressure = 0;
            int dId = 0;
            DateTime timestamp1;
            var deviceClient = DeviceClient.Create(iotHubUri,
                    AuthenticationMethodFactory.
                        CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey),
                    TransportType.Http1); 
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    temp = random.Next(35, 55);
                    pressure = random.Next(200, 300);
                    timestamp1 = DateTime.UtcNow;
                    dId = random.Next(1, 5);
                    Event info = new Event()
                    {
                        TimeStamp1 = timestamp1,
                        DeviceId = deviceId,
                        Temperature = temp.ToString(),
                        Pressure = pressure.ToString(),
                    };
                    Console.WriteLine("Enter temperature (press CTRL+Z to exit):");
                    var readtemp = Console.ReadLine();
                    if (readtemp != null)
                        info.Temperature = readtemp;
                    var serializedString = JsonConvert.SerializeObject(info);
                    Message data = new Message(Encoding.UTF8.GetBytes(serializedString));
                    // Send the metric to Event Hub
                    var task = Task.Run(async () => await deviceClient.SendEventAsync(data));

                    //Write the values to your debug console                            
                    Console.WriteLine("DeviceID: " + dId.ToString());
                    Console.WriteLine("Timestamp: " + info.TimeStamp1.ToString());
                    Console.WriteLine("Temperature: " + info.Temperature.ToString() + " deg C");
                    Console.WriteLine("Pressure: " + info.Pressure.ToString() + " Pa");
                    Console.WriteLine("------------------------------");
                    //Message data = new Message(Encoding.UTF8.GetBytes("myDeviceId2,19,Ban-EGL,14804022344554"));
                    //// Send the metric to Event Hub
                    //var task = Task.Run(async () => await deviceClient.SendEventAsync(data));
                    Task.Delay(3000).Wait();
                }
            }
            catch (Exception e)
            {
            }
            
        }
    }
}
