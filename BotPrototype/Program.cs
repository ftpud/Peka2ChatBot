using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                if (msg.Text.ToLower().Contains("тест")) {
                    Thread.Sleep(10);
                    Bot.PublishMessage("ответ (c) bot");
                }
                Console.WriteLine($"{msg.From.Name} сказал: {msg.Text}");
            });
            
            Bot.SetPublishDetails(
                "...", // login
                0, //id
                "??" // token
                );

            Bot.Start("126885"); // 126885 - ViewersBW
            Console.ReadLine();
        }

       


    }
}
