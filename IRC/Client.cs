using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace IRC
{
    class Client
    {
        public string Server { get; set; }

        public string Channel { get; set; }
        public int Port { get; set; }
        public string Nickname { get; set; } = "RTM";

        public Client(string server, string channel, int port)
        {
            Server = server;
            Channel = channel;
            Port = port;
        }

        public void Connect()
        {
            if (Nickname == null)
            {
                Console.WriteLine("Choose a nickname");
                Nickname = Console.ReadLine();
            }

            cancellation = new CancellationTokenSource();
            var t = new Thread(connect) { IsBackground = true };
            t.Start(cancellation.Token);
        }

        private CancellationTokenSource cancellation;

        private TcpClient irc;

        private StreamReader streamReader;

        private StreamWriter streamWriter;

        private void send(string message)
        {
            Console.WriteLine(message);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        private void send(string channel, string message)
        {
            Console.WriteLine("PRIVMSG " + channel + " :" + message);
            streamWriter.WriteLine("PRIVMSG " + channel + " :" + message);
            streamWriter.Flush();
        }
        private void connect(object token)
        {
            Console.WriteLine("Trying to connect to " + Server + ":" + Port);
            irc = new TcpClient(Server, Port);
            var stream = irc.GetStream();
            streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream);
            send("USER " + Nickname + " " + Nickname + " " + Nickname + " :" + "RTM's here");
            send("NICK " + Nickname);
            send("JOIN " + Channel);
            listen((CancellationToken) token);
        }

        private void disconnect()
        {
            cancellation.Cancel();
            irc.Close();
        }

        private void listen(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                var line = streamReader.ReadLine();
                Console.WriteLine(line);
                parse(line);
            }
        }

        private void parse(string input)
        {
            var ircInput = input.Split(' ');

            if (ircInput[0] == "PING")
            {
                send("PONG " + ircInput[1].Replace(":", string.Empty));
                return;
            }

            switch(ircInput[1])
            {
                case "PRIVMSG":
                    var adress = ircInput[ircInput.Length - 2];
                    var command = ircInput[ircInput.Length - 1];
                    if (adress == ":RTM:" && command == "execute")
                    {
                        send(Channel, "Top-3 sneakers by StockX brand | colorway | retail price");
                        send(Channel, Bot.Execute(command).Result);
                    }

                    if (command == "quit" && adress == ":RTM:")
                    {
                        send(Channel, "Bye Bye Bye");
                        disconnect();
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
