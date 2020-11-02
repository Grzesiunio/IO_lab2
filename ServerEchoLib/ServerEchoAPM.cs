using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Net;

using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;



namespace ServerEchoLib

{

    public class ServerEchoAPM : ServerEcho

    {

        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public ServerEchoAPM(IPAddress IP, int port) : base(IP, port)

        {

        }

        protected override void AcceptClient()

        {
            byte[] buffer = new byte[Buffer_size];
            while (true)

            {

                TcpClient tcpClient = TcpListener.AcceptTcpClient();

                Stream = tcpClient.GetStream();

                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);

                //callback style

                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);

                // async result style

                //IAsyncResult result = transmissionDelegate.BeginInvoke(Stream, null, null);

                ////operacje......
                

                //while (!result.IsCompleted) ;

                ////sprzątanie

            }

        }



        private void TransmissionCallback(IAsyncResult ar)

        {

            // sprzątanie

        }

        protected override void BeginDataTransmission(NetworkStream stream)

        {

            byte[] buffer = new byte[Buffer_size];
            byte[] buffer_read_stream = new byte[Buffer_size];
            string a = "Podaj nazwe uzytkownika";
            string b = "Podaj haslo uzytkownika kot";
            string dobre_haslo = "Podales dobre haslo, zalogowales sie do systemu";
            string uzytkownik = "kot";
            string haslo = "pies";
            byte[] buffer1 = Encoding.ASCII.GetBytes(a);
            byte[] buffer2 = Encoding.ASCII.GetBytes(b);
            byte[] buffer_haslo = Encoding.ASCII.GetBytes(dobre_haslo);

            stream.Write(buffer1, 0, buffer1.Length);
            while (true)

            {
                

                try

                {

                    
                    int message_size = stream.Read(buffer, 0, Buffer_size);
                    
                    string wiadomosc = Encoding.UTF8.GetString(buffer, 0, message_size);
                    
                    if(wiadomosc==uzytkownik)
                    {
                        stream.Write(buffer2, 0, buffer2.Length);
                        buffer = new byte[Buffer_size];
                        message_size = stream.Read(buffer, 0, Buffer_size);
                        stream.Write(buffer, 0, message_size);
                        wiadomosc = Encoding.UTF8.GetString(buffer, 0, message_size);
                        Console.WriteLine(wiadomosc);
                        if(wiadomosc == haslo)
                        {
                            stream.Write(buffer_haslo, 0, buffer_haslo.Length);
                        }
                        else
                        {
                            
                        }
                        
                    }


                    stream.Write(buffer, 0, message_size);
                }

                catch (IOException e)

                {

                    break;

                }

            }

        }

        public override void Start()

        {

            StartListening();

            //transmission starts within the accept function

            AcceptClient();

        }



    }

}