using System;
using System.Collections.Generic;
using System.Threading;


namespace MovingCar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Car";

            // Задается размер окна.
            Console.SetWindowSize(100, 50);

            // Задается видимость курсора.
            Console.CursorVisible = true;

            Game.Game game = new Game.Game();
            game.Run();

            
            Console.ReadKey();
        }
    }
}
