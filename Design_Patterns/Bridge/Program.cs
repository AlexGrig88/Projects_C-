using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
///  Назначение - отделить абстракцию от реализации(другой абстракции), чтобы независимо друг от друга можно было развивать, изменять  разные части продукта
/// </summary>
namespace Bridge
{

    abstract class Abstraction
    {
        protected Implementor implementor;
        public Abstraction(Implementor imp)
        {
            implementor = imp;
        }
        public virtual void Operation()
        {
            implementor.OperationImp();
        }
    }

    class RefinedAbstraction : Abstraction  
    {
        public RefinedAbstraction(Implementor imp) : base(imp)
        { }
        public override void Operation()
        {
            //.....
            base.Operation();
            //....
        }
    }


    abstract class Implementor
    {
        public abstract void OperationImp();
    }

    class ConcreteImplementor1 : Implementor
    {
        public override void OperationImp()
        {
            Console.WriteLine("Implementor1");
        }
    }

    class ConcreteImplementor2 : Implementor
    {
        public override void OperationImp()
        {
            Console.WriteLine("Implementor2");
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
