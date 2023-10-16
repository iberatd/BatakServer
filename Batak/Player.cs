using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batak
{
    class Player
    {

        private string name;
        private int point;
        private int id;

        private List<Card> hand;


        public Player(){}

        public Player(String n, int point, int id)
        {
            Name = n;
            Point = point;
            Id = id;
            hand = new List<Card>();
        }


        public Card SelectCard(int n) => Hand[n];
        public void RemoveCard(int n) => Hand.RemoveAt(n);

        public bool HasColor(string color)
        {
            foreach ( Card card in Hand) if (card.Color == color) return true;
            return false;
        }

        public bool HasGreater(string color, int value)
        {
            foreach (Card card in Hand) if (card.Color == color && card.Value > value) return true;
            return false;
        }

        public string HandToString()
        {
            string result = "";
            foreach(Card card in hand)
            {
                result += card.Name + "" + card.Color + " ";
            }
            return result;
        }


        public string Name { get => name; set => name = value; }
        public int Point { get => point; set => point = value; }
        public int Id { get => id; set => id = value; }
        public List<Card> Hand { get => hand; set => hand = value; }
    }
}
