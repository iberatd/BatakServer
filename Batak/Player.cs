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

        public string Name { get => name; set => name = value; }
        public int Point { get => point; set => point = value; }
        public int Id { get => id; set => id = value; }
        public List<Card> Hand { get => hand; set => hand = value; }
    }
}
