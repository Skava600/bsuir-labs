﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ISOB_2
{
    class TicketGrantingServer
    {
        public void Listen()
        {
            UdpClient reciever = new UdpClient(Config.TGS_port);
            Console.WriteLine($"TGS started on 127.0.0.1:{Config.TGS_port}");
            IPEndPoint remoteIP = null;
            try
            {
                while (true)
                {
                    byte[] data = reciever.Receive(ref remoteIP);
                    remoteIP.Port = Config.C_port;

                    var message = Serialiser<Message>.Deserialise(data);

                    if (message.Type == MessageType.СToTgs)
                    {
                        var tgt_json = Helper.RecoverData(
                            new List<byte>(DES.Decrypt(message.Data[0].ToArray(), Config.K_AS_TGS)));
                        var tgt = Serialiser<TicketGranting>.Deserialise(tgt_json);

                        var aut1_json = Helper.RecoverData(
                            new List<byte>(DES.Decrypt(message.Data[1].ToArray(), Config.K_C_TGS)));
                        var a = Encoding.UTF8.GetString(aut1_json);

                        var aut1 = Serialiser<TimeMark>.Deserialise(aut1_json);

                        var ID = Encoding.UTF8.GetString(message.Data[2].ToArray());

                        Message ReMessage = new Message();

                        if (tgt.ClientIdentity == aut1.C)
                        {
                            if (Helper.CheckTime(tgt.IssuingTime, aut1.T, tgt.Duration))
                            {
                                ReMessage.Type = MessageType.TgsToC;
                                var TGS = new TicketGranting()
                                {
                                    ClientIdentity = aut1.C,
                                    ServiceIdentity = ID,
                                    Duration = Config.TGSTicketDuration.Ticks,
                                    IssuingTime = DateTime.Now,
                                    Key = Encoding.UTF8.GetString(Config.K_C_SS)
                                };
                                var ticket_bytes = Helper.ExtendData(Serialiser<TicketGranting>.Serialise(TGS));
                                var k_c_ss_bytes = Helper.ExtendData(Config.K_C_SS);

                                var tb_enc = DES.Encrypt(ticket_bytes, Config.K_TGS_SS);
                                tb_enc = DES.Encrypt(tb_enc, Config.K_C_TGS);

                                var k_c_ss_enc = DES.Encrypt(k_c_ss_bytes, Config.K_C_TGS);

                                ReMessage.Data.Add(new List<byte>(tb_enc));
                                ReMessage.Data.Add(new List<byte>(k_c_ss_enc));
                            }
                            else
                            {
                                ReMessage.Type = MessageType.TicketNotValid;
                                Console.WriteLine("TicketNotValid in TGS;");
                            }
                        }
                        else
                        {
                            ReMessage.Type = MessageType.AccessDenied;
                            Console.WriteLine("AccessDenied in TGS;");
                        }
                        ReMessage.Send(remoteIP);
                        Console.WriteLine($"Message sended from TGS to {remoteIP.Address}:{remoteIP.Port}!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
