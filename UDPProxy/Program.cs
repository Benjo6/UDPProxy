using System;

namespace UDPProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Proxy worker = new Proxy();
            worker.Start();
        }
    }
}
