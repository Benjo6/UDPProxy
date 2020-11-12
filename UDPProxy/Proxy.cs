using ModelLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace UDPProxy
{
    class Proxy
    {
        private string URI = "http://localhost:10915/api/Sensor";
        public void Start()
        {
            UdpClient client = new UdpClient(10100);

            byte[] buffer;

            Console.WriteLine("Start");
            while (true)
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                buffer = client.Receive(ref remoteEP);

                Console.WriteLine($"pakke fra IP {remoteEP.Address} og port {remoteEP.Port}");
                string str = Encoding.UTF8.GetString(buffer);

                Console.WriteLine("tekst modtaget = " + str);

                PostItemAsync(str);
            }
        }
        public async void PostItemAsync(string jsonStr)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                throw new ArgumentException("opret fejlede");
            }
        }
    }
}
