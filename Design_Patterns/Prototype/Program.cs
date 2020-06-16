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
        public string Class { get; set; }
        public string State { get; set; }

        public Prototype Clone()
        {
            return this.MemberwiseClone() as Prototype;
        }
    }
    /// <summary>
    /// /////////
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Формируем Объект-Класс prototype, который будет являться
            //прототипом для всех производных от него видов

            Prototype prototype = new Prototype();
            prototype.Class = "Биологическая система";
            prototype.State = "Живая";

            var Human = prototype.Clone();
            Human.Class = "Человек";
            Human.State += " Общие признаки человека";

            var Man = Human.Clone();
            Man.Class = "Мужчина";
            Man.State += "Мужские признаки";
        }
    }
}
