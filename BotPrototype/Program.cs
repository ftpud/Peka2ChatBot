using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Bot.AddCommand("/тест", (argument, msg) => {
                Console.WriteLine($"была вызвана команда /тест с аргументом: {argument}");
            });
            Bot.AddCommand("/123", (argument, msg) => {
                Console.WriteLine($"была вызвана команда /123 с аргументом: {argument}");
            });
            Bot.AddCommand("", (argument, msg) => {
                Console.WriteLine($"{msg.from.name} сказал: {msg.text}");
            });

            Bot.Start("56592"); // 126885 - ViewersBW
            Console.ReadLine();
        }

       


    }
}
