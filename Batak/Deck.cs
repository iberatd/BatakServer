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
        private List<List<Card>> hands;


        public Deck()
        {
            FillDesk();
        }

        public void FillDesk()
        {
            FullDeck = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                FullDeck.Add(new Card(i));
            }

            Hands = new List<List<Card>> { new List<Card>(), new List<Card>(), new List<Card>(), new List<Card>() };
        }


        public void shuffleHands()
        {

            Random rnd = new Random();

            int toHand = 0;

            foreach ( Card card in FullDeck)
            {
                do
                {
                    toHand = rnd.Next(4);

                    if (Hands[toHand].Count < 13) 
                    { 
                        Hands[toHand].Add(card);
                        break;
                    }
                } while (true);

            }
        }


        public List<Card> FullDeck { get => fullDeck; set => fullDeck = value; }
        public List<List<Card>> Hands { get => hands; set => hands = value; }

    }
}
