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
                Console.WriteLine($"{msg.From.Name} сказал: {msg.Text}");
            });

            Bot.Start("126885"); // ID канала, 126885 - виверс
            Console.ReadLine();
        }

       


    }
}
