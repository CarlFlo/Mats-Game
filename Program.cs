using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var wep = new Weapon{
                name = "hej",
                damage = 12,
                type = WeaponTypes.Axe,
            };

            wep.dices.AddDices(4, 10);

            (int value, List<int> rolls) = wep.dices.RollDices();
            Console.Out.WriteLine("{0} {1}", value, rolls.StringifyItems());
        }
    }

    public static class ListExtensions
    {
        public static string StringifyItems<T>(this List<T> list)
        {
            return $"{{{string.Join(" ", list)}}}";
        }
    }

    public enum WeaponTypes {
        Sword,
        Spear,
        Axe,
        Hammer,
        Knife,
        Dagger,
    }

    public class Object 
    {
        public string name {get; set;}
    }

    public class Weapon : Object
    {
        public WeaponTypes type {get; set;}
        public short damage {get; set;}

        public Dices dices = new Dices();
    }

    public class Dices
    {
        List<Dice> dices;

        public void AddDices(int amount, byte sides)
        {
            for (int i = 0; i < amount; ++i)
            {
                this.AddDice(sides);
            }
        }
        
        public void AddDice(Dice d) 
        {
            if (dices == null)
            {
                dices = new List<Dice>();
            }
            dices.Add(d);
        }

        public void AddDice(byte _sides) 
        {
            var d = new Dice{
                sides = _sides,
            };
            this.AddDice(d);
        }

        public (int, List<int>) RollDices() 
        {
            int val = 0;
            var rolls = new List<int>();
            foreach (var d in dices)
            {
                int roll = d.RollDice();
                rolls.Add(roll);
                val += roll;
            }
            return (val, rolls);
        }
    }

    public class Dice 
    {
        public byte sides {get; set;}
        private static readonly Random random = new Random();

        public int RollDice() {
            return random.Next(sides) + 1;
        }
    }
}
