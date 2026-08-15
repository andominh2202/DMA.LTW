using System;

namespace DmaLession01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var menuManager = new MenuManager();
            menuManager.Run();
        }
    }
}