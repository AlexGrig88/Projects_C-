using System;
using System.Collections;

/// <summary>
/// Pattern Builder - пошаговое построение сложных объектов
/// </summary>
namespace Builder
{
    /// <summary>
    /// Составные части более сложного объекта
    /// </summary>
    class Basement  //Подвал
    {
        public Basement()
        {
            Console.WriteLine("Подвал построен");
        }
        
    }

    class Storey 
    {
        public Storey()
        {
            Console.WriteLine("Первый этаж построен");
        }
    }

    class Roof
    {
        public Roof()
        {
            Console.WriteLine("Крыша построена");
        }
    }
    /// <summary>
    /// сложный объект, состоящий из составных объектов
    /// </summary>
    class House
    {
        ArrayList parts = new ArrayList();

        public void Add(object part)
        {
            parts.Add(part);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    abstract class Builder
    {
        public abstract void BuildBasement();
        public abstract void BuildStorey();
        public abstract void BuildRoof();
        public abstract House Getresult();
    }

    class ConcreteBuilder : Builder
    {
        House house = new House();
        public override void BuildBasement()
        {
            house.Add(new Basement());
        }

        public override void BuildRoof()
        {
            house.Add(new Roof());
        }

        public override void BuildStorey()
        {
            house.Add(new Storey());
        }

        public override House Getresult()
        {
            return house;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    class Foreman  //Прораб
    {
        Builder builder;
        public Foreman(Builder builder)
        {
            this.builder = builder;
        }

        public void Construct()
        {
            // важен порядок выполнения действий-методов!!!!
            builder.BuildBasement();
            builder.BuildStorey();
            builder.BuildRoof();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Builder builder = new ConcreteBuilder();
            Foreman foreman = new Foreman(builder);
            foreman.Construct();

            House house = builder.Getresult();
            Console.ReadKey();
        }
    }
}
