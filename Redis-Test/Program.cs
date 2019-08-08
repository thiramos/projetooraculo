using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Redis_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool automatico = true;

            Console.WriteLine("Hello World!");

            var connection = ConnectionMultiplexer.Connect("40.77.24.62");

            var sub = connection.GetSubscriber();

            sub.Subscribe("Perguntas", (ch, msg) =>
            {
                try
                {
                    var str = msg.ToString();

                    var nro = str.Split(":")[0];

                    Console.WriteLine(msg);

                    var resp = !automatico ? Console.ReadLine() : "Brasilia";

                    var db = connection.GetDatabase();

                    var lst = new List<HashEntry>()
                    {
                        new HashEntry("Grupo10", resp)
                    };

                    db.HashSet(nro, lst.ToArray());

                    if (automatico)
                    {
                        Console.WriteLine(resp);
                    }

                    Console.WriteLine("Respondido.");
                }
                catch
                {
                    Console.WriteLine("msg não compreendida: ", msg.ToString());
                }
            });

            Console.ReadKey();
        }
    }
}
