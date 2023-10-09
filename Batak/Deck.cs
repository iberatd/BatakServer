using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batak
{
    class Deck
    {
        private List<Card> fullDeck;

        public List<Card> FullDeck { get => fullDeck; set => fullDeck = value; }


        public Deck()
        {

            FullDeck = new List<Card>();
            for(int i = 0; i< 52; i++)
            {
                FullDeck.Add(new Card(i));
            }
        }

        public List<List<Card>> shuffleHands()
        {
            List<List<Card>> hands = new List<List<Card>> { new List<Card>(), new List<Card>(), new List<Card>(), new List<Card>() };

            Random rnd = new Random();

            int toHand = 0;

            foreach ( Card card in FullDeck)
            {
                do
                {
                    toHand = rnd.Next(4);

                    if (hands[toHand].Count < 13) 
                    { 
                        hands[toHand].Add(card);
                        break;
                    }
                } while (true);

            }
            return hands;
        }
    }
}
