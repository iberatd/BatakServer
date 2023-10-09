using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batak
{
    class Card
    {
        private string color;
        private string name;
        private int value;
        private int id;

        private static List<String> colors = new List<string> { "♣", "♦", "♠", "♥" };
        private static List<String> royalty = new List<string> { "J", "Q", "K", "A" };

        public int Value { get => value; set => this.value = value; }
        public string Name { get => name; set => name = value; }
        public string Color { get => color; set => color = value; }
        public static List<string> Colors { get => colors; set => colors = value; }
        public static List<string> Royalty { get => royalty; set => royalty = value; }
        public int Id { get => id; set => id = value; }

        public Card() { }

        public Card(int number)
        {
            Id = number+1;
            Color = Colors[number / 13];

            Value = number % 13;
            if (Value > 8)
                Name = Royalty[Value - 9];
            else
                Name = (Value + 2).ToString();
        }
    }
}
