using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingCar.Game
{
    class Car
    {
        Engine engine;
        CarBody carBody;

        public Car(int left = 44, int top = 15)
        {
            engine = new Engine();
            carBody = new CarBody(left, top);
        }

        public void Show()
        {
            carBody.Draw();
        }

        // Ускорение.
        public int Acceleration(int delta = 1)
        {
            return engine.Accelerate(delta);
        }

        // Торможение.
        public void Braking()
        {

        }
    }
}
