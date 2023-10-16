using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batak
{
    class BatakGame
    {

        private Deck myDeck;
        private Player[] players;
        private Card[] desk ;
        private string koz;
        private int startTurn;
        private int turn;
        private int[] scores;
        private int[] totalScores;

        private int maxBid;
        private int bidder;

        private int gameStage; // 0- players coming, 1- claim, 2- game, 3- aftergame


        /// these will be refreshed at every turn
        private bool kozOpened;
        private string turnsColor;
        private Card winnerCard;

        private Form1 myForm;

        public BatakGame(Form1 form) 
        {
            StartTurn = 0;
            TotalScores = new int[]{0,0,0,0};
            Scores = new int[] { 0, 0, 0, 0 };
            Players = new Player[4];
            Desk = new Card[4];
            MaxBid = 0;
            GameStage = 0;
            KozOpened = false;
            MyDeck = new Deck();
            MyForm = form;
            TurnsColor = "";
            WinnerCard = null;
        }

        public int Play()
        {
            /// Players Join
            AddPlayer("Berat");
            AddPlayer("Memin");
            AddPlayer("İsot");
            AddPlayer("Ali");

            //// Cards are distributed
            MyDeck.FillDesk();
            MyDeck.shuffleHands();
            DistributeCards();

            //// Who is going to start
            Turn = StartTurn;
            StartTurn++;

            /// Bidding Start
            MaxBid = 0;
            Bidder = -1;

            MyForm.AddTextToTextBox(MakeBid(0) + " bidded 0");

            MyForm.AddTextToTextBox(MakeBid(0) + " bidded 0");

            MyForm.AddTextToTextBox(MakeBid(0) + " bidded 0");

            MyForm.AddTextToTextBox(MakeBid(0) + " bidded 0");


            /// Bidding has ended // if max Bid = 0 => MaxBid = 4
            if (MaxBid == 0) MaxBid = 4;

            MyForm.AddTextToTextBox(Turn.ToString ()+ "starting with bid" + MaxBid.ToString());

            /// Winner of bidding will decide what the koz is

            ChooseKoz(1);

            MyForm.AddTextToTextBox("Koz is: " + Koz);
            //TODO: Add playing card by four and add scoring

            WinnerCard = null;

            return 0;
        }

        public int AddPlayer(String n)
        {

            for (int i = 0; i<4; i++)
            {
                if (Players[i] == null)
                {
                    Players[i] = new Player(n, 0, i);
                    return i;
                }
            }
            GameStage = 1;
            return -1;
        }

        public bool IsFull()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Players[i] == null) return false;
            }
            GameStage = 1;
            return true;
        }

        public void DistributeCards() {
            for(int i = 0; i< 4; i++)   Players[i].Hand = MyDeck.Hands[i];
        }

        /*
         * Takes a claim: 
         * if it is viable make is claim of player and increments turn 
         * if not does nothing
         * returns whose turn it is
        */
        public int MakeBid(int bid)
        {
            if (IsBiddingDone()) return -2; // bidding is done

            if (bid == 0)
            {          
                Turn = ++Turn % 4;
            }else if (bid < MaxBid)
            {
                return -1; // cannot bid this number
            }
            else if (bid > MaxBid)
            {
                MaxBid = bid;
                Bidder = Turn;
                Turn = ++Turn % 4;
            }
            return Turn;
        }


        /*
         * Checks whether claiming process is done
         */
        public bool IsBiddingDone() => (Turn == Bidder);

        public string ChooseKoz(int i)
        {
            Koz = Card.Colors[i];
            GameStage = 2;
            return Card.Colors[i];
        }


        public bool CardPlayable(Card card) {

            if (WinnerCard == null)
            {
                if (KozOpened) return true;
                if (card.Color != Koz) return true;

                int sum = 0;

                for (int i = 0; i < 4; i++)
                    if (Players[Turn].HasColor(Card.Colors[i])) sum++;
                return sum == 1;
            }

            if (WinnerCard.Color == Koz && TurnsColor != Koz) //// Kozlanan el
            {
                if (card.Color == TurnsColor) return true; /// elde acılan renk var ( koz olmayan)

                if (!Players[Turn].HasColor(TurnsColor) && Players[Turn].HasColor(Koz)) // kozlanma + elde el rengi yok & koz var
                {
                    return (card.Color == Koz )&& ((card.Value > WinnerCard.Value) || (!Players[Turn].HasGreater(Koz, WinnerCard.Value) ));
                }else return (!Players[Turn].HasColor(TurnsColor) && !Players[Turn].HasColor(Koz)); // koz & acılan renk yok         
            }
            else if(TurnsColor == Koz)
            {
                if (!Players[Turn].HasColor(Koz)) return true; // koz yoksa
                return (card.Color == Koz && ((card.Value > WinnerCard.Value) || !Players[Turn].HasGreater(Koz, WinnerCard.Value))); // artılabiliyo
            }
            else // kozlanmamıs ve koz olmayan el
            {
                if (card.Color == TurnsColor && (card.Value > WinnerCard.Value || !Players[Turn].HasGreater(TurnsColor, WinnerCard.Value))) return true; /// aynı renk artırma
                return (!Players[Turn].HasColor(TurnsColor) && (card.Color == Koz || !Players[Turn].HasColor(Koz))); // koz var
            }
        } 

        public bool PutCard(Card card)
        {
            if (CardPlayable(card))
            {
                desk[Turn] = card;
                return true;
            }
            return false;
        }

        public bool PlayCard(int c)
        {
            if (this.PutCard(Players[Turn].SelectCard(c)))
            {
                MyForm.AddTextToTextBox("Played Card");

                TurnsColor = Players[Turn].Hand[c].Color;

                Players[Turn].RemoveCard(c);

                if (BeatsCard(Desk[Turn], WinnerCard))
                {
                    WinnerCard = Desk[Turn];
                }
                Turn = ++Turn % 4;
                return true;
            }
            else // Couldnt Play the card
            {
                MyForm.AddTextToTextBox("Cant Play The Card");
                return false;
            }
        }


        /*
         finds the winner of the turn 
         But doesnt add the points or rearrange turn
         */
        public int WinnerOfTheTurn()
        {
            bool kozPlayed = false;

            int maxCard = -1;
            int maxPlayer = 0;

            for (int i = 0; i<4; i++)
            {
                Card card = Desk[i];

                if ((card.Color == Koz) && !kozPlayed)
                {
                    kozPlayed = true;
                    KozOpened = true;
                    maxCard = card.Value;
                    maxPlayer = i;
                }
                else if(!kozPlayed || (card.Color == Koz ) )
                {
                    if(card.Value > maxCard)
                    {
                        maxCard = card.Value;
                        maxPlayer = i;
                    }
                }
            }
            return maxPlayer;
        }

        public bool BeatsCard(Card card1, Card card2)
        {
            if (card2 == null) return true;
            if (card1.Color ==  card2.Color && card1.Value > card2.Value) return true;
            if (card1.Color == Koz && card2.Color != Koz) return true;
            return false;
        }

        public string DeskToString()
        {
            string result = "";
            for(int i =0; i<4; i++)
            {
                if (Desk[i] != null)
                    result += Desk[i].ToString() + " ";
                else
                    result += " ";
            }
            return result;
        }



        public string Koz { get => koz; set => koz = value; }
        public int Turn { get => turn; set => turn = value; }
        public int[] Scores { get => scores; set => scores = value; }
        public int StartTurn { get => startTurn; set => startTurn = value; }
        public int[] TotalScores { get => totalScores; set => totalScores = value; }
        internal Player[] Players { get => players; set => players = value; }
        internal Card[] Desk { get => desk; set => desk = value; }
        public int MaxBid { get => maxBid; set => maxBid = value; }
        public int GameStage { get => gameStage; set => gameStage = value; }
        public int Bidder { get => bidder; set => bidder = value; }
        public bool KozOpened { get => kozOpened; set => kozOpened = value; }
        internal Deck MyDeck { get => myDeck; set => myDeck = value; }
        public Form1 MyForm { get => myForm; set => myForm = value; }
        public string TurnsColor { get => turnsColor; set => turnsColor = value; }
        internal Card WinnerCard { get => winnerCard; set => winnerCard = value; }
    }
}
