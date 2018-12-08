using System;

namespace IRC
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = "irc.freenode.net";
            int port = 6667;
            string channel = "#081218";
            var a = new Client(server, channel, port);
            a.Connect();
            Console.ReadKey();
        }
    }
}
