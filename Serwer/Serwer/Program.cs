using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Serwer
{
    class Program
    {
        static void Main(string[] args)
        {
            Program main = new Program();
            main.start();

            Console.WriteLine("Serwer pracuje");
            Console.ReadLine();

        }
        TcpListener serwer = new TcpListener(IPAddress.Any, 7777);

        private void start()
        {
            serwer.Start();
            akceptowanie();
        }
        private void akceptowanie()
        {
            serwer.BeginAcceptTcpClient(polaczenie, serwer);
        }
        private void polaczenie(IAsyncResult rezultat)
        {
           
            int Buffer_size = 1024;
            akceptowanie();
            TcpClient klient = serwer.EndAcceptTcpClient(rezultat);
            string a = "Podaj nazwe uzytkownika: ";
            byte[] buffer1 = Encoding.ASCII.GetBytes(a);
            klient.GetStream().Write(buffer1, 0, buffer1.Length);
            while (true)
            {
                byte[] buffer = new byte[Buffer_size];
                byte[] buffer_read_stream = new byte[Buffer_size];
                
                string b = "Podaj haslo uzytkownika kot";
                string dobre_haslo = "Podales dobre haslo, zalogowales sie do systemu";
                string uzytkownik = "kot";
                string haslo = "pies";
                
                byte[] buffer2 = Encoding.ASCII.GetBytes(b);
                byte[] buffer_haslo = Encoding.ASCII.GetBytes(dobre_haslo);
                int message_size = klient.GetStream().Read(buffer, 0, Buffer_size);

                string wiadomosc = Encoding.UTF8.GetString(buffer, 0, message_size);
                

                if (wiadomosc == uzytkownik)
                {
                    klient.GetStream().Write(buffer2, 0, buffer2.Length);
                    buffer = new byte[Buffer_size];
                    message_size = klient.GetStream().Read(buffer, 0, Buffer_size);
                    klient.GetStream().Write(buffer, 0, message_size);
                    wiadomosc = Encoding.UTF8.GetString(buffer, 0, message_size);
                    Console.WriteLine(wiadomosc);
                    if (wiadomosc == haslo)
                    {
                        //wiadomosc przyjmuje postac \r\npies\r\n przez co if nie wykonuje sie
                        klient.GetStream().Write(buffer_haslo, 0, buffer_haslo.Length);
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
