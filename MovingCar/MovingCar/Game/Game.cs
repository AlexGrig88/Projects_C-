using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace MovingCar.Game
{
    class Game
    {
        private Car car;
        private Road road;

        public Game()
        {
            car = new Car(40, 20); // задаём положение автомобиля
            road = new Road();
        }

        public void Run()
        {
            car.Show();
            // Thread.Sleep(100);
            road.Movie();

            // Игровая петля.
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    road.Speed = car.Acceleration(10);
                }
                catch (Exception e)
                {
                    road.Speed = 0;
                    Console.SetCursorPosition(38, 20);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();

                    Console.SetCursorPosition(0, 46);

                    foreach (DictionaryEntry de in e.Data)
                        Console.WriteLine($"{de.Key}: {de.Value}");

                    break;
                }
            }
        }
    }
}
