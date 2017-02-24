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
        static string iotHubUri = "FudDemoIOTHub.azure-devices.net"; // ! put in value !
        static string deviceId = "MyDotNetDevice"; // ! put in value !
        static string deviceKey = "+cqjyMhPv2zQ14HTXqze7eJZS+Rv6rAf/S1JY7V4Lng="; // ! put in value !
        //static string iotHubUri = "testingIOTgs.azure-devices.net"; // ! put in value !
        //static string deviceId = "MyTestDevice2"; // ! put in value !
        //static string deviceKey = "xHx+rRVay2VsQqLH2m/egl9xtZHFWakaeDfztH0I9nU="; // ! put in value !
       
        //static string iotHubUri = "testIOTgsNew.azure-devices.net"; // ! put in value !
        //static string deviceId = "testdevice1"; // ! put in value !
        //static string deviceKey = "71H8Q7iUkge4Y9d9T6ek3GjK6SWoyXNjw3UQ9jf5x50="; // ! put in value !

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
