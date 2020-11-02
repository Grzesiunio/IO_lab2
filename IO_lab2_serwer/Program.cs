using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ServerEchoLib;

namespace IO_lab2_serwer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerEchoAPM server = new ServerEchoAPM(IPAddress.Parse("127.0.0.1"), 7777);
            try
            {
                server.Port = -1;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            server.Start();
        }
    }
}
