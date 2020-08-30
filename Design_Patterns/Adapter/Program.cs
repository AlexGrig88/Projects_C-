using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{

    /// <summary>
    /// Назначение - адаптирует несовместимые интерфейсы
    /// </summary>

    class Program
    {
        interface ITarget  //может быть также представлен и абстрактным классом
        {
            void Request();
        }

        class Adapter : ITarget   // через композицию
        {
            private Adaptee adaptee = new Adaptee();
            public void Request()
            {
                adaptee.SpecificRequest();
            }
        }

        class Adaptee 
        { 
            public void SpecificRequest()
            {
                Console.WriteLine("SpecificRequest");
            }
        }

        static void Main(string[] args)
        {
            // 1-способ реализации - через наследование

            // 2-способ реализации - через композицию(агрегирование)
            ITarget target = new Adapter();
            target.Request();
        }
    }
}
