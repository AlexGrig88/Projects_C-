using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Назначение - Клонирование объектов
/// </summary>
namespace Prototype
{
    class Prototype
    {
        static void Main()
        {
            int x = -2;
            int y = 10;
            while (x < y)
            {
                x = x + 1;
                if (x > 0)
                {
                    y -= 2;
                }
                else
                {
                    y -= 1;
                }

            }
            Console.WriteLine(y + x*10);
            Console.ReadLine();
        }
    
    }
}
