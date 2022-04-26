using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ISOB_2;

namespace ISOB_2Client
{
    class Client
    {
        public string Login { get; set; }
        private byte[] TicketGrantingTicket { get; set; }
        private byte[] TicketGrantingService { get; set; }
        private byte[] K_C_TGS { get; set; }
        private byte[] K_C_SS { get; set; }
        DateTime T4 { get; set; }
        public Client()
        {
        }

        private readonly IPEndPoint ASEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Config.AS_port);
        private readonly IPEndPoint SSEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Config.SS_port);
        private readonly IPEndPoint TGSEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Config.TGS_port);
        public void Register(string login)
        {
            Login = login;
            Message message = new Message(MessageType.CToAs);
            message.Data.Add(new List<byte>(Encoding.UTF8.GetBytes(login)));
            message.Send(ASEndPoint);
        }
        public void Listen()
        {
            UdpClient reciever = new UdpClient(Config.C_port);
            IPEndPoint remoteIP = null;
            try
            {
                while (true)
                {
                    byte[] data = reciever.Receive(ref remoteIP);
                    Message message = Serialiser<Message>.Deserialise(data);
                    switch (message.Type)
                    {
                        case MessageType.AsToC:
                            TicketGrantingTicket = DES.Decrypt(message.Data[0].ToArray(), Config.K_C);
                            K_C_TGS = Helper.RecoverData(new List<byte>(DES.Decrypt(message.Data[1].ToArray(), Config.K_C)));
                            var a = Encoding.UTF8.GetString(K_C_TGS);
                            Print("AS to C complete!");
                            message = new Message(MessageType.СToTgs);
                            message.Data.Add(new List<byte>(TicketGrantingTicket));

                            TimeMark mark = new TimeMark() { C = Login, T = DateTime.Now };
                            var Aut1 = Helper.ExtendData(Serialiser<TimeMark>.Serialise(mark));
                            message.Data.Add(new List<byte>(DES.Encrypt(Aut1, K_C_TGS)));
                            message.Data.Add(new List<byte>(Encoding.UTF8.GetBytes(Config.SS_ID)));

                            message.Send(TGSEndPoint);
                            break;
                        case MessageType.TgsToC:
                            TicketGrantingService = DES.Decrypt(message.Data[0].ToArray(), K_C_TGS);
                            K_C_SS = Helper.RecoverData(new List<byte>(DES.Decrypt(message.Data[1].ToArray(), K_C_TGS)));

                            Message msg = new Message(MessageType.CToSs);
                            msg.Data.Add(new List<byte>(TicketGrantingService));

                            mark = new TimeMark() { C = Login, T = DateTime.Now };
                            var Aut2 = Helper.ExtendData(Serialiser<TimeMark>.Serialise(mark));
                            T4 = mark.T;
                            msg.Data.Add(new List<byte>(DES.Encrypt(Aut2, K_C_SS)));

                            msg.Send(SSEndPoint);

                            Print("TGS to C complete");
                            break;
                        case MessageType.SsToC:
                            var t = DES.Decrypt(message.Data[0].ToArray(), K_C_SS);
                            var checkT_bytes = Helper.RecoverData(new List<byte>(t));
                            var asd = Encoding.UTF8.GetString(checkT_bytes);
                            var checkT = Serialiser<long>.Deserialise(checkT_bytes);
                            if (T4.Ticks + 1 == checkT)
                            {
                                Console.WriteLine($"Success.");
                            }
                            break;
                        case MessageType.TicketNotValid:
                            Console.WriteLine("Ticket is not valid");
                            break;
                        case MessageType.AccessDenied:
                            Console.WriteLine("Access denied!");
                            break;
                        default:
                            Console.WriteLine("Invalid type of message");
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void Print(string message)
        {
           Console.WriteLine(message);
        }
    }
}
